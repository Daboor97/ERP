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


namespace SofterFertilizers.BasicData
{
    public partial class storeAddingUC : UserControl
    {
        public storeAddingUC()
        {
            InitializeComponent();
            fillStoreCode_DGV();
            adjustButton.Enabled = false;
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

     
        void fillStoreCode_DGV()
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from storeTable) BEGIN SELECT MAX(Id) FROM storeTable END", conDataBase).ExecuteScalar().ToString();

            if (maxBill != "")
            {
                int maxBillint = Convert.ToInt32(maxBill);
                storeCodeTextBox.Text = Convert.ToString(maxBillint + 1);
                conDataBase.Close();
            }

            else
            {
                storeCodeTextBox.Text = "1";
                conDataBase.Close();
            }

            string Query = "select id as 'كود المخزن'  ,storeName as 'اسم المخزن', manager as 'المسؤول',phoneNumber as 'الموبايل', address as 'العنوان',notes as 'الملاحظات'from storeTable;";

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
                storeDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
            }

            conDataBase.Close();

        }

        void clear()
        {
            this.storeNameTextBox.Text = "";
            this.managerTextBox.Text = "";
            this.mobileTextBox.Text = "";
            this.addressTextBox.Text = "";
            this.noteTextBox.Text = "";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string Query = "IF NOT EXISTS (select 1 FROM storeTable where id = N'" + this.storeCodeTextBox.Text + "'AND storeName= N'" + this.storeNameTextBox.Text + "'AND manager = N'" + this.managerTextBox.Text + "'AND phoneNumber = N'" + this.mobileTextBox.Text + "'AND address = N'" + this.addressTextBox.Text+ "'AND  notes = N'" + this.noteTextBox.Text + "') BEGIN INSERT INTO storeTable(storeName,manager,phoneNumber,address,notes) VALUES (N'" + this.storeNameTextBox.Text + "',N'" + this.managerTextBox.Text + "',N'" + this.mobileTextBox.Text + "',N'" + this.addressTextBox.Text + "',N'" + this.noteTextBox.Text + "') END ";
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

        private void storeDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            addButton.Enabled = false;
            adjustButton.Enabled=true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.storeDGV.Rows[e.RowIndex];
                this.storeCodeTextBox.Text = row.Cells[0].Value.ToString();
                this.storeNameTextBox.Text = row.Cells[1].Value.ToString();
                this.managerTextBox.Text = row.Cells[2].Value.ToString();
                this.mobileTextBox.Text = row.Cells[3].Value.ToString();
                this.addressTextBox.Text = row.Cells[4].Value.ToString();
                this.noteTextBox.Text = row.Cells[5].Value.ToString();
            }
        }

        private void adjustButton_Click(object sender, EventArgs e)
        {
            //TODO Required admin previlage to adjust
            if (true)
            {
                string Query = "IF EXISTS(select 1 from storeTable where Id =N'" + this.storeCodeTextBox.Text + "') BEGIN UPDATE storeTable SET storeName = N'" + this.storeNameTextBox.Text + "',manager=N'" + this.managerTextBox.Text + "',phoneNumber=N'" + this.mobileTextBox.Text + "',address=N'" + this.addressTextBox.Text + "',notes=N'" + this.noteTextBox.Text + "' where Id =N'" + this.storeCodeTextBox.Text + "' END";
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
                adjustButton.Enabled = false;
                addButton.Enabled = true;
            }
            //TODO check if delete button is required
        }

        public void refreshLocal()
        {
            fillStoreCode_DGV();
            clear();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
