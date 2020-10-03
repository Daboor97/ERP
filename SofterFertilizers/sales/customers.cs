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


namespace SofterFertilizers.sales
{
    public partial class customers : UserControl
    {
        public customers()
        {
            InitializeComponent();
            fillStoreCode_DGV();
            activeCheckBox.Checked = true;
            adjustButton.Visible = false;
        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;


        void fillStoreCode_DGV()
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from customerTable) BEGIN SELECT MAX(Id) FROM customerTable END", conDataBase).ExecuteScalar().ToString();

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

            string Query = "select id as 'كود العميل'  ,name as 'اسم العميل', telephone as 'رقم التليفون',mobile as 'الموبايل', fax as 'فاكس',governorate as 'المحافظة', center as 'المركز', address as 'العنوان', email as 'البريد الإلكتروني',  balance as 'الرصيد',notes as 'الملاحظات',active as 'نشط' from customerTable;";

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
            governorateTextBox.Text = "";
            centerTextBox.Text = "";
            balanceTextBox.Text = "";
            addressTextBox.Text = "";
            notesTextBox.Text = "";
            emailTextBox.Text = "";
            activeCheckBox.Checked = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string Query = "IF NOT EXISTS (select 1 FROM customerTable where name= N'" + this.nameTextBox.Text + "'AND telephone= N'" + this.telephoneTextBox.Text + "'AND mobile= N'" + this.mobileTextBox.Text + "'AND fax= N'" + this.faxTextBox.Text + "'AND address=N'" + this.addressTextBox.Text + "' ) BEGIN INSERT INTO customerTable(name,telephone,mobile,fax,notes,governorate,center,address,balance,active,email) VALUES (N'" + this.nameTextBox.Text + "',N'" + this.telephoneTextBox.Text + "',N'" + this.mobileTextBox.Text + "',N'" + this.faxTextBox.Text + "',N'" + this.notesTextBox.Text + "',N'" + this.governorateTextBox.Text + "',N'" + this.centerTextBox.Text + "',N'" + this.addressTextBox.Text + "',N'" + this.balanceTextBox.Text + "','" + activeCheckBox.Checked + "',N'"+this.emailTextBox.Text + "') END ";
            SqlConnection conDataBase = new SqlConnection(constring);
            SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
            SqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                MessageBox.Show("حفظ");
                while (myReader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            fillStoreCode_DGV();
            clear();
        }

        private void customerDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            addButton.Visible = false;
            adjustButton.Visible = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.customerDGV.Rows[e.RowIndex];
                this.customerCodeTextBox.Text = row.Cells[0].Value.ToString();
                this.nameTextBox.Text = row.Cells[1].Value.ToString();
                this.telephoneTextBox.Text = row.Cells[2].Value.ToString();
                this.mobileTextBox.Text = row.Cells[3].Value.ToString();
                this.faxTextBox.Text = row.Cells[4].Value.ToString();
                this.governorateTextBox.Text = row.Cells[5].Value.ToString();
                this.centerTextBox.Text = row.Cells[6].Value.ToString();
                this.addressTextBox.Text = row.Cells[7].Value.ToString();
                this.emailTextBox.Text = row.Cells[8].Value.ToString();
                this.balanceTextBox.Text = row.Cells[9].Value.ToString();
                this.notesTextBox.Text = row.Cells[10].Value.ToString();
                this.activeCheckBox.Checked = bool.Parse(row.Cells[11].Value.ToString());
            }
        }

        private void adjustButton_Click(object sender, EventArgs e)
        {

            //TODO Required admin previlage to adjust
            if (true)
            {
                string Query = "IF EXISTS(select 1 from customerTable where Id =N'" + this.customerCodeTextBox.Text + "') BEGIN UPDATE customerTable SET name = N'" + this.nameTextBox.Text + "',telephone=N'" + this.telephoneTextBox.Text + "',mobile=N'" + this.mobileTextBox.Text + "',fax=N'" + this.faxTextBox.Text + "',governorate=N'"+this.governorateTextBox.Text+"',center=N'"+this.centerTextBox.Text+ "', address=N'" + this.addressTextBox.Text + "',balance=N'" + this.balanceTextBox.Text + "',notes=N'" + this.notesTextBox.Text + "',active=N'" + this.activeCheckBox.Checked + "', email=N'"+this.emailTextBox.Text+"' where Id =N'" + this.customerCodeTextBox.Text + "' END";
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
                fillStoreCode_DGV();
                addButton.Visible = true;
                adjustButton.Visible = false;
            }
            //TODO check if delete button is required
        }

        private void categoryCodeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryNameSearchTextBox.Text = "";

            customerDGV.DataSource = null;
            string Query = "select id as 'كود العميل'  ,name as 'اسم العميل', telephone as 'رقم التليفون',mobile as 'الموبايل', fax as 'فاكس',governorate as 'المحافظة', center as 'المركز', address as 'العنوان', email as 'البريد الإلكتروني',balance as 'الرصيد',notes as 'الملاحظات',active as 'نشط' from customerTable where Id like N'%" + this.categoryCodeSearchTextBox.Text + "%';";


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
                customerDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {

            }

            conDataBase.Close();
        }

        private void categoryNameSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryCodeSearchTextBox.Text = "";

            customerDGV.DataSource = null;
            string Query = "select id as 'كود العميل'  ,name as 'اسم العميل', telephone as 'رقم التليفون',mobile as 'الموبايل', fax as 'فاكس',governorate as 'المحافظة', center as 'المركز', address as 'العنوان', email as 'البريد الإلكتروني' ,balance as 'الرصيد',notes as 'الملاحظات',active as 'نشط' from customerTable where name like N'%" + this.categoryNameSearchTextBox.Text + "%';";



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
                customerDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {

            }

            conDataBase.Close();
        }
        public void refreshLocal()
        {
            clear();
            fillStoreCode_DGV();
            activeCheckBox.Checked = true;
            adjustButton.Visible = false;
        }
    }
}
