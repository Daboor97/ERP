using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Configuration;

namespace SofterFertilizers
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            passwordTextBox.PasswordChar = '*';

        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string maxBill = new SqlCommand("IF EXISTS(select 1 from usersMainTable where userName = N'" + this.userNameTextBox.Text + "' AND password = '" + this.passwordTextBox.Text + "') BEGIN select 1 from usersMainTable where userName = N'" + this.userNameTextBox.Text + "' AND password = '" + this.passwordTextBox.Text + "' END ELSE SELECT 0 ;", conDataBase).ExecuteScalar().ToString();

                
                if (maxBill == "1")
                {
                    mainForm nextForm = new mainForm(this.userNameTextBox.Text);
                    this.Hide();
                    nextForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    userNameTextBox.Clear();
                    passwordTextBox.Clear();
                    MessageBox.Show("اسم المستخدم أو كلمة المرور خطأ");
                }

                conDataBase.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
