using BusinessLogic;
using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagementProject
{
    public partial class MainForm : Form
    {
        private string _AccountName;
        private string _TableID = null;
        private Button btnTemp;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ResetButtonColor(Button btn)
        {
            btnTemp.BackColor = SystemColors.ButtonFace;
            btnTemp.UseVisualStyleBackColor = true;
        }

        private void CheckRoleAdmin()
        {
            AccountBL accountBL = new AccountBL();
            if (accountBL.CheckRoleID(_AccountName))
                AdminToolStripMenuItem.Enabled = true;
            else
                AdminToolStripMenuItem.Enabled = false;
        }

        private void CreateBill()
        {
            BillBL billDL = new BillBL();
            Bill bill = new Bill()
            {
                ID = 0,
                Name = "Hóa đơn thanh toán",
                TableID = int.Parse(_TableID),
                Amount = 0,
                Discount = Double.Parse(txtDiscount.Text),
                Tax = Double.Parse(txtTax.Text),
                Status = false,
                CheckoutDate = DateTime.Now,
                AccountName = _AccountName
            };
            int BillID = billDL.Bill_Insert(bill);
            if (BillID != 0)
                Add_BillDetail_To_Bill(BillID);
            else
                MessageBox.Show("Lỗi tạo hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Add_BillDetail_To_Bill(int Bill_ID)
        {
            BillDetailsBL billDetailBL = new BillDetailsBL();
            BillDetails billDetail = new BillDetails()
            {
                ID = 0,
                BillID = Bill_ID,
                FoodID = Convert.ToInt32(cbbFood.SelectedValue.ToString()),
                Quantity = Convert.ToInt32(nmrQuantity.Value)
            };
            int kq = billDetailBL.BillDetails_Insert(billDetail);
            if (kq == 0)
                MessageBox.Show("Lỗi khi thêm chi tiết hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                LoadListView(Bill_ID);
        }

        private void LoadListView(int BillID)
        {
            BillDetailsBL billDetailsBL = new BillDetailsBL();
            DataTable billInfo = billDetailsBL.BillDetailsInfo(BillID);
            lvFoodTable.Items.Clear();
            foreach (DataRow row in billInfo.Rows)
            {
                ListViewItem item = new ListViewItem(row["Name"].ToString());
                item.SubItems.Add(row["Quantity"].ToString());
                item.SubItems.Add(row["Price"].ToString());
                item.SubItems.Add(row["Summary"].ToString());
                item.Tag = row["FoodID"].ToString();
                lvFoodTable.Items.Add(item);
            }
            CalSummary();
        }

        private void CalSummary()
        {
            long sum = 0;
            foreach (ListViewItem item in lvFoodTable.Items)
            {
                sum += Convert.ToInt64(item.SubItems[3].Text);
            }
            txtAmount.Text = sum.ToString();
        }

        private void DeleteBillDetails(int BillID, int FoodID)
        {
            BillDetailsBL billDetailsBL = new BillDetailsBL();
            BillDetails bd = billDetailsBL.GetBillDetailBy_BillID_FoodID(BillID, FoodID);
            if (billDetailsBL.BillDetails_Delete(bd) == 0)
                MessageBox.Show("Lỗi xóa chi tiết hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DeleteBill(int BillID)
        {
            BillBL billBL = new BillBL();
            Bill bill = billBL.GetBillBy_ID(BillID);
            if (billBL.Bills_Delete(bill) == 0)
                MessageBox.Show("Lỗi xóa hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoadTable()
        {
            TableBL tableBL = new TableBL();
            List<Table> TableList = tableBL.GetAll();

            fpnlTable.Controls.Clear();
            string status, capacity;
            foreach (var table in TableList)
            {
                Button btn = new Button();
                btn.Width = 90;
                btn.Height = 60;

                btn.Tag = table.ID.ToString();
                status = table.Status == false ? "Còn trống" : "Có khách";
                btn.Text = "Bàn " + table.Name + "\n" + status;
                capacity = table.Capacity.ToString() + "Chỗ";

                if (status == "Có khách") { btn.BackColor = Color.Aquamarine; }                
                TableCapacityToolTip.SetToolTip(btn, capacity);
                btn.Click += Btn_Click; ;

                fpnlTable.Controls.Add(btn);
            }
            _TableID = null;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackColor != Color.Aquamarine)
                btn.BackColor = Color.Orange;
            if (btnTemp != null)
            {
                if (btnTemp.BackColor != Color.Aquamarine)
                    ResetButtonColor(btnTemp);
            }
            _TableID = btn.Tag.ToString();
            btnTemp = btn;

            BillBL billBL = new BillBL();
            int BillID = billBL.CheckExistBillTable(int.Parse(_TableID));
            if (BillID == 0)
            {
                lvFoodTable.Items.Clear();
                txtAmount.Text = "0";
            }
            else
            {
                LoadListView(BillID);
            }
        }

        private void LoadCategory()
        {
            CategoryBL categoryBL = new CategoryBL();
            List<Category> catList = categoryBL.GetAll();

            cbbCategory.DataSource = catList;
            cbbCategory.DisplayMember = "Name";
            cbbCategory.ValueMember = "ID";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginForm frm = new LoginForm();
            frm.ShowDialog();
            if (frm.DialogResult != DialogResult.OK)
                Application.Exit();
            _AccountName = frm.txtAccountName.Text;
            CheckRoleAdmin();
            LoadTable();
            LoadCategory();
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbCategory.ValueMember = "ID";
            var catID = int.Parse(cbbCategory.SelectedValue.ToString());        

            FoodBL foodBL = new FoodBL();
            List<Food> FoodList = foodBL.GetByCatID(catID);

            cbbFood.DataSource = FoodList;
            cbbFood.DisplayMember = "Name";
            cbbFood.ValueMember = "ID";
        }

        private void AccountInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountInfoForm frm = new AccountInfoForm(_AccountName);
            frm.ShowDialog();
        }

        private void btnAddFoodBill_Click(object sender, EventArgs e)
        {
            if (_TableID == null)
            {
                MessageBox.Show("Vui lòng chọn bàn trước khi thêm món ăn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BillBL billDL = new BillBL();
            int BillID = billDL.CheckExistBillTable(Convert.ToInt32(_TableID));
            if (BillID == 0)
            {
                CreateBill();
                LoadTable();
                nmrQuantity.Value = 1;
            }
            else
            {
                Add_BillDetail_To_Bill(BillID);
            }                
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            BillBL billBL = new BillBL();
            int BillID = billBL.CheckExistBillTable(Convert.ToInt32(_TableID));

            if (BillID == 0)
            {
                MessageBox.Show("Không thể thanh toán khi không có hóa đơn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thanh toán!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    billBL.BillUpdateAmount(BillID, Convert.ToInt32(_TableID), txtAmount.Text);
                    LoadTable();
                    lvFoodTable.Items.Clear();
                    txtAmount.Text = "0";
                }
                else
                    return;
            }
        }

        private void FoodCancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = lvFoodTable.SelectedItems.Count;
            BillBL billBL = new BillBL();
            if (count > 0)
            {
                int BillID = billBL.CheckExistBillTable(Convert.ToInt32(_TableID));
                int FoodID;

                if (count == lvFoodTable.Items.Count)
                {
                    foreach (ListViewItem item in lvFoodTable.SelectedItems)
                    {
                        FoodID = Convert.ToInt32(item.Tag.ToString());
                        DeleteBillDetails(BillID, FoodID);
                    }
                    DeleteBill(BillID);
                    LoadTable();
                    lvFoodTable.Items.Clear();
                    txtAmount.Text = "0";
                }
                else
                {
                    foreach (ListViewItem item in lvFoodTable.SelectedItems)
                    {
                        FoodID = Convert.ToInt32(item.Tag.ToString());
                        DeleteBillDetails(BillID, FoodID);
                        LoadListView(BillID);

                        if (lvFoodTable.Items.Count == 0)
                        {
                            DeleteBill(BillID);
                            LoadTable();
                        }
                    }
                }
            }
        }

        private void AdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminForm frm = new AdminForm();
            frm.Show();
        }
    }
}
