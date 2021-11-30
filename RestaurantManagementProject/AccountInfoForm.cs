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
    public partial class AccountInfoForm : Form
    {
        AccountBL accountBL = new AccountBL();
        private string _AccountName;
        public AccountInfoForm(string AccountName)
        {
            InitializeComponent();
            _AccountName = AccountName;
        }   
        
        private Account CreateAccount(string password)
        {
            Account acc = new Account()
            {
                AccountName = _AccountName,
                Password = password,
                FullName = txtFullName.Text,
                Email = txtEmail.Text,
                Tell = txtTell.Text
            };
            return acc; 
        }

        private void rdoChangeInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoChangeInfo.Checked)
            {
                txtNewPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
            }
            else
            {
                txtNewPassword.Enabled = true;
                txtConfirmPassword.Enabled = true;
            }
        }

        private void AccountInfoForm_Load(object sender, EventArgs e)
        {
            Account acc = accountBL.GetAccountByAccountName(_AccountName);
            txtAccountName.Text = acc.AccountName;
            txtFullName.Text = acc.FullName;
            txtEmail.Text = acc.Email;
            txtTell.Text = acc.Tell;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Họ tên hoặc mật khẩu không được để trống!", "cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rdoChangeInfo.Checked)
            {
                if (accountBL.CheckPassword(_AccountName, txtPassword.Text))
                {
                    Account acc = CreateAccount(txtPassword.Text);
                    if (accountBL.Account_Update(acc) == 0)
                        MessageBox.Show("Vui lòng cập nhật lại tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Đã cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    MessageBox.Show("Mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (rdoChangePassword.Checked)
            {
                if (accountBL.CheckPassword(_AccountName, txtPassword.Text))
                {
                    if (txtNewPassword.Text.Equals(txtConfirmPassword.Text) && (!string.IsNullOrWhiteSpace(txtNewPassword.Text) || !string.IsNullOrWhiteSpace(txtConfirmPassword.Text)))
                    {
                        Account acc = CreateAccount(txtNewPassword.Text);
                        if (accountBL.Account_Update(acc) == 0)
                            MessageBox.Show("Vui lòng cập nhật lại tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Đã cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                        MessageBox.Show("Mật khẩu mới và xác nhận không trùng nhau hoặc không được bỏ trống, yêu cầu nhập lại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
