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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void LoadRole()
        {
            RoleBL roleBL = new RoleBL();

            List<Role> roleList = roleBL.GetAll();

            foreach (var item in roleList)
            {
                clbRole.Items.Add(item.RoleName);
            }
        }

        private void InsertAccount()
        {
            AccountBL accountBL = new AccountBL();
            RoleBL roleBL = new RoleBL();

            Account acc = new Account()
            {
                AccountName = txtAccountName.Text,
                FullName = txtFullName.Text,
                Email = txtEmail.Text,
                Tell = txtTell.Text,
                Password = "1",
                DateCreate = DateTime.Now
            };
            if (accountBL.Account_Insert(acc) == 0)
            {
                MessageBox.Show("Lỗi tạo tài khoản", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }                

            foreach (var item in clbRole.CheckedItems)
            {
                Role role = roleBL.GetRoleBy_Name(item.ToString());
                if (role != null)
                {
                    InsertRoleForAccount(acc.AccountName, role.ID);
                }
            }
            string mes = string.Format("Đã tạo tài khoản thành công!\nTên tài khoản: {0}\nMật khẩu mặc định: {1}", acc.AccountName, acc.Password);
            MessageBox.Show(mes, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);    
        }

        private void InsertRoleForAccount(string accountName, int roleID)
        {
            RoleAccountBL roleAccountBL = new RoleAccountBL();

            RoleAccount roleAccount = new RoleAccount()
            {
                RoleID = roleID,
                AccountName = accountName,
                Actived = true,
                Notes = rtxtNotes.Text
            };

            if (roleAccountBL.RoleAccount_Insert(roleAccount) == 0)
                MessageBox.Show("Lỗi thêm vai trò!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FoodAndCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            LoadRole();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAccountName.Text) || string.IsNullOrWhiteSpace(txtFullName.Text) || clbRole.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin của tài khoản (Tên tài khoản, họ tên và vai trò)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }                
            InsertAccount();
        }
    }
}
