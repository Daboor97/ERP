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

namespace SofterFertilizers.Reports.suppliersReport
{
    public partial class supplierDetails : UserControl
    {
        public supplierDetails()
        {
            InitializeComponent();
            fill();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {
            //supplier ComboBox
            customerNameComboBox.Items.Clear();
            customerNameComboBox.Items.Add("الكل");
            customerNameComboBox.Text = "الكل";

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct name from supplierTable where active='True';";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    customerNameComboBox.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (customerNameComboBox.Items.Count > 0)
            {
                customerNameComboBox.Text = customerNameComboBox.Items[0].ToString();
            }


        }

        private void customerNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (customerNameComboBox.Text == "الكل")
            {
                customerCodeTextBox.Text = "0"; 
            }
            else
            {
               
                try
                {
                    //supplier Code
                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    customerCodeTextBox.Text = new SqlCommand("select Id from supplierTable where name=N'" + this.customerNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                }
                catch
                {
                }
            }
          
        }

        private void customerCodeTextBox_TextChanged(object sender, EventArgs e)
        {

            if (customerCodeTextBox.Text == "0")
            {
                customerNameComboBox.Text = "الكل";
            }
            else
            {
                try
                {
                    //supplierName
                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    customerNameComboBox.Text = new SqlCommand("IF EXISTS(select 1 from supplierTable where Id=N'" + this.customerCodeTextBox.Text + "') BEGIN select name from supplierTable where Id=N'" + this.customerCodeTextBox.Text + "' END ;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                }
                catch
                {
                }
            } 
        }



        private void showFlowButton_Click(object sender, EventArgs e)
        {
            string Query = "select distinct Id as 'كود المورّد' , name as 'الاسم', telephone as 'التليفون', mobile as 'الموبايل', fax as 'الفاكس' , address as 'العنوان', balance as 'الرصيد' ,active as 'نشط',  notes as 'ملاحظات' from supplierTable where active ='True'; ";

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
                categoryDGV.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch
            {
            }
        }
    }
}
