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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private bool CheckInput()
        {
            if (string.IsNullOrWhiteSpace(txtAccountName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Tên hoặc mật khẩu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool CheckExistsUser()
        {
            AccountBL accountBL = new AccountBL();
            List<Account> accountList = accountBL.GetAll();
            Account user = accountList.Find(x => (x.AccountName == txtAccountName.Text && x.Password == txtPassword.Text));
            if (user == null)
                return false;
            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
                return;

            if (!CheckExistsUser())
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
