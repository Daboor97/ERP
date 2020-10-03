using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Configuration;


namespace SofterFertilizers.settings
{
    public partial class addUsers : UserControl
    {
        public addUsers()
        {
            InitializeComponent();
            fill();
            clear();

        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string status = "new";
        string oldId;
        public void clear()
        {
            nameTextBox.Text = "";
            userNameTextBox.Text = "";
            passwordTextBox.Text = "";
            retypePasswordTextBox.Text = "";
            userNameTextBox.ReadOnly = false;
            userNameTextBox.BackColor = Color.FromArgb(41, 44, 51);
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            if (status == "new")
            {
                if (userNameTextBox.Text != "admin")
                {
                    if (passwordTextBox.Text == retypePasswordTextBox.Text)
                    {

                        string Query = "IF NOT EXISTS (select 1 FROM usersMainTable where name = N'" + this.nameTextBox.Text + "'AND userName = N'" + this.userNameTextBox.Text + "'AND password = N'" + this.passwordTextBox.Text + "') BEGIN INSERT INTO usersMainTable (name,userName,password,owner) VALUES (N'" + this.nameTextBox.Text + "',N'" + this.userNameTextBox.Text + "',N'" + this.passwordTextBox.Text + "','False') END ";
                        SqlConnection conDataBase = new SqlConnection(constring);
                        SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
                        SqlDataReader myReader;
                        try
                        {
                            conDataBase.Open();
                            myReader = cmdDataBase.ExecuteReader();
                            MessageBox.Show("حُفظ");
                            while (myReader.Read())
                            {

                            }
                        }
                        catch (Exception ex)
                        {
         
                        }
                        fill();
                        clear();
                        status = "new";
                    }

                    else if (passwordTextBox.Text != retypePasswordTextBox.Text)
                    {
                        MessageBox.Show("كلمتي السر غير متطابقتين", "خطأ");
                    }
                }
                else
                {
                    MessageBox.Show("لا يمكن تسمية اسم المستخدم بهذا الاسم");
                    fill();
                    clear();
                    status = "new";
                }
            }
            else
            {
                if (passwordTextBox.Text == retypePasswordTextBox.Text)
                {
                    string Query = "UPDATE usersMainTable set  name = N'" + this.nameTextBox.Text + "', userName = N'" + this.userNameTextBox.Text + "', password = N'" + this.passwordTextBox.Text + "' where Id=N'" + this.oldId + "' ";
                    SqlConnection conDataBase = new SqlConnection(constring);
                    SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
                    SqlDataReader myReader;
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();
                        MessageBox.Show("حُفظ");
                        while (myReader.Read())
                        {

                        }
                    }
                    catch (Exception ex)
                    {
     
                    }
                    fill();
                    clear();
                    status = "new";
                }
                else if (passwordTextBox.Text != retypePasswordTextBox.Text)
                {
                    MessageBox.Show("كلمتي السر غير متطابقتين", "خطأ");
                }

            }


        }
        

        
        //TODO Previlege

        void fill()
        {
            string Query = "select id as 'رقم المستخدم', name as 'الاسم',userName as 'اسم المستخدم', password as 'الرقم السري' from usersMainTable;";
            SqlConnection conDataBase = new SqlConnection(constring);
            SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                typeDataGridView.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void refreshLocal()
        {
            fill();
            clear();  
        }

        private void typeDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            clear();
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.typeDataGridView.Rows[e.RowIndex];
                    oldId = row.Cells[0].Value.ToString();
                    this.nameTextBox.Text = row.Cells[1].Value.ToString();
                    this.userNameTextBox.Text = row.Cells[2].Value.ToString();

                    this.passwordTextBox.Text = row.Cells[3].Value.ToString();
                    this.retypePasswordTextBox.Text = row.Cells[3].Value.ToString();

                    if (userNameTextBox.Text == "admin")
                    {
                        userNameTextBox.ReadOnly = true;
                        userNameTextBox.BackColor = Color.FromArgb(64, 64, 64);
                    }
                    else
                    {
                        userNameTextBox.ReadOnly = false;
                        userNameTextBox.BackColor = Color.FromArgb(41, 44, 51);
                    }
            status = "adjust";
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
