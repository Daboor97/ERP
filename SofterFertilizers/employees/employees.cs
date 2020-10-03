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


namespace SofterFertilizers.employees
{
    public partial class employees : UserControl
    {
        public employees()
        {
            InitializeComponent();
            activeCheckBox.Checked = true;
            deleteButton.Visible = false;
            fill();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        string status = "new";

        void fill()
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from employeesTable) BEGIN SELECT MAX(Id) FROM employeesTable END", conDataBase).ExecuteScalar().ToString();

            if (maxBill != "")
            {
                int maxBillint = Convert.ToInt32(maxBill);
                customerCodeTextBox.Text = Convert.ToString(maxBillint + 1);
                conDataBase.Close();
            }

            else
            {
                customerCodeTextBox.Text = "1";
                conDataBase.Close();
            }

            customerDGV.DataSource = null;

            string Query = "select id as 'كود الموظف'  ,name as 'اسم الموظف', telephone as 'رقم التليفون',mobile as 'الموبايل', fax as 'فاكس',nationalNumber as 'الرقم القومي' , salary as 'المرتب' , email as 'البريد الإلكتروني', address as 'العنوان',notes as 'الملاحظات',active as 'نشط' from employeesTable;";

            conDataBase = new SqlConnection(constring);
            SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                customerDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();
        }

        void clear()
        {
            nameTextBox.Text = "";
            telephoneTextBox.Text = "";
            mobileTextBox.Text = "";
            faxTextBox.Text = "";
            nationalNumberTextBox.Text = "";
            salaryTextBox.Text = "";
            addressTextBox.Text = "";
            notesTextBox.Text = "";
            emailTextBox.Text = "";
            activeCheckBox.Checked = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (status == "new")
            {
                string Query = "IF NOT EXISTS (select 1 FROM employeesTable where name= N'" + this.nameTextBox.Text + "'AND telephone= N'" + this.telephoneTextBox.Text + "'AND mobile= N'" + this.mobileTextBox.Text + "'AND fax= N'" + this.faxTextBox.Text + "'AND nationalNumber=N'" + this.nationalNumberTextBox.Text + "' AND salary=N'" + this.salaryTextBox.Text + "' AND address=N'" + this.addressTextBox.Text + "' ) BEGIN INSERT INTO employeesTable(name,telephone,mobile,fax,nationalNumber,salary,email,address,notes,active) VALUES (N'" + this.nameTextBox.Text + "',N'" + this.telephoneTextBox.Text + "',N'" + this.mobileTextBox.Text + "',N'" + this.faxTextBox.Text + "',N'" + this.nationalNumberTextBox.Text + "',N'" + this.salaryTextBox.Text + "',N'" + this.emailTextBox.Text + "',N'" + this.addressTextBox.Text + "',N'" + this.notesTextBox.Text + "','" + activeCheckBox.Checked + "') END ";
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
                deleteButton.Visible = false;
                status = "new";
            }
            else
            {
                string Query = "IF EXISTS(select 1 from employeesTable where Id =N'" + this.customerCodeTextBox.Text + "') BEGIN UPDATE employeesTable SET name= N'" + this.nameTextBox.Text + "', telephone= N'" + this.telephoneTextBox.Text + "', mobile= N'" + this.mobileTextBox.Text + "', fax= N'" + this.faxTextBox.Text + "', nationalNumber=N'" + this.nationalNumberTextBox.Text + "' , salary=N'" + this.salaryTextBox.Text + "' , address=N'" + this.addressTextBox.Text + "' ,email=N'" + this.emailTextBox.Text + "',notes=N'" + this.notesTextBox.Text + "',active=N'" + this.activeCheckBox.Checked + "' where Id =N'" + this.customerCodeTextBox.Text + "' END";
                SqlConnection conDataBase = new SqlConnection(constring);
                SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
                SqlDataReader myReader;

                try
                {
                    conDataBase.Open();
                    myReader = cmdDataBase.ExecuteReader();
                    while (myReader.Read())
                    {

                    }
                }
                catch (Exception ex)
                {
 
                }
                MessageBox.Show("انتهى التعديل");

                clear();
                fill();
                deleteButton.Visible = false;
                status = "new";

            }
        }

        private void customerDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            deleteButton.Visible = true;
            status = "adjust";
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.customerDGV.Rows[e.RowIndex];
                this.customerCodeTextBox.Text = row.Cells[0].Value.ToString();
                this.nameTextBox.Text = row.Cells[1].Value.ToString();
                this.telephoneTextBox.Text = row.Cells[2].Value.ToString();
                this.mobileTextBox.Text = row.Cells[3].Value.ToString();
                this.faxTextBox.Text = row.Cells[4].Value.ToString();
                this.nationalNumberTextBox.Text = row.Cells[5].Value.ToString();
                this.salaryTextBox.Text = row.Cells[6].Value.ToString();
                this.emailTextBox.Text = row.Cells[7].Value.ToString();
                this.addressTextBox.Text = row.Cells[8].Value.ToString();
                this.notesTextBox.Text = row.Cells[9].Value.ToString();
                this.activeCheckBox.Checked = bool.Parse(row.Cells[10].Value.ToString());
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("SELECT COUNT(clientcode) FROM safeTable WHERE clientCode=N'"+this.customerCodeTextBox.Text+"' and type ='employees'; ", conDataBase).ExecuteScalar().ToString();

            if (maxBill != "0")
            {
                MessageBox.Show("لا يمكن حذف الموظّف لوجود معاملات معه, يمكنك جعله غير نشط إن كان نشطًا");
            }

            else
            {

                DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string Query = "DELETE FROM employeesTable where Id = N'" + this.customerCodeTextBox.Text + "' ;";
                    conDataBase = new SqlConnection(constring);
                    SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
                    SqlDataReader myReader;

                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();
                        while (myReader.Read())
                        {
                        }
                    }
                    catch (Exception ex)
                    {
     
                    }

                    fill();
                    clear();
                    deleteButton.Visible = false;
                    status = "new";
                    MessageBox.Show("حُفظ");
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }

        }
        public void refreshLocal()
        {
            activeCheckBox.Checked = true;
            deleteButton.Visible = false;
            fill();
            clear();
        }
        
        
    }
}
