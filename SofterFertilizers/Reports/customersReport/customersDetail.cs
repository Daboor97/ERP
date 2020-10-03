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

namespace SofterFertilizers.Reports.customersReport
{
    public partial class customersDetail : UserControl
    {
        public customersDetail()
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
           string Query = "select distinct name from customerTable where active='True';";
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

            //governorte ComboBox
            governorateComboBox.Items.Clear();
            governorateComboBox.Items.Add("الكل");
            governorateComboBox.Text = "الكل";

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            Query = "select distinct governorate from customerTable;";
            dt = new DataTable();
            da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    governorateComboBox.Items.Add(dr["governorate"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

          


            //center ComboBox
            centerComboBox.Items.Clear();
            centerComboBox.Items.Add("الكل");
            centerComboBox.Text = "الكل";

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            Query = "select distinct center from customerTable;";
            dt = new DataTable();
            da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    centerComboBox.Items.Add(dr["center"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

           
        }

        private void customerNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (customerNameComboBox.Text == "الكل")
            {
                customerCodeTextBox.Text = "0";
                label1.Visible = true;
                governorateComboBox.Visible = true;
                label2.Visible = true;
                centerComboBox.Visible = true;
            }
            else
            {
                label1.Visible = false;
                governorateComboBox.Visible = false;
                label2.Visible = false;
                centerComboBox.Visible = false;

                try
                {
                    //supplier Code
                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    customerCodeTextBox.Text = new SqlCommand("select Id from customerTable where name=N'" + this.customerNameComboBox.Text + "' where active='True';", conDataBase).ExecuteScalar().ToString();
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
                    customerNameComboBox.Text = new SqlCommand("IF EXISTS(select 1 from customerTable where Id=N'" + this.customerCodeTextBox.Text + "') BEGIN select name from customerTable where Id=N'" + this.customerCodeTextBox.Text + "' END ;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                }
                catch
                {
                }
            }
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            if (customerNameComboBox.Text == "الكل" && governorateComboBox.Text == "الكل" && centerComboBox.Text == "الكل")
            {
                string Query = "select distinct Id as 'كود العميل' , name as 'الاسم', telephone as 'التليفون', mobile as 'الموبايل', fax as 'الفاكس' , governorate as 'المحافظة' , center as 'المركز', address as 'العنوان', balance as 'الرصيد' ,active as 'نشط', email as 'البريد الإلكتروني', debtAge as 'عمر الدين بالشهور',  notes as 'ملاحظات' from customerTable";

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
                catch (Exception ex)
                {
 
                }
            }
            else if (customerNameComboBox.Text != "الكل")
            {
                string Query = "select distinct Id as 'كود العميل' , name as 'الاسم', telephone as 'التليفون', mobile as 'الموبايل', fax as 'الفاكس' , governorate as 'المحافظة' , center as 'المركز', address as 'العنوان', balance as 'الرصيد' ,active as 'نشط' ,email as 'البريد الإلكتروني', debtAge as 'عمر الدين بالشهور',  notes as 'ملاحظات' from customerTable where name =N'" + this.customerNameComboBox.Text + "' ";

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
                catch (Exception ex)
                {
 
                }
            }
            else if (customerNameComboBox.Text == "الكل" && governorateComboBox.Text != "الكل" && centerComboBox.Text == "الكل")
            {
                string Query = "select distinct Id as 'كود العميل' , name as 'الاسم', telephone as 'التليفون', mobile as 'الموبايل', fax as 'الفاكس' , governorate as 'المحافظة' , center as 'المركز', address as 'العنوان', balance as 'الرصيد' ,active as 'نشط', email as 'البريد الإلكتروني', debtAge as 'عمر الدين بالشهور',  notes as 'ملاحظات' from customerTable where governorate =N'"+this.governorateComboBox.Text+"' ";

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
                catch (Exception ex)
                {
 
                }
            }
            else if (customerNameComboBox.Text == "الكل" && governorateComboBox.Text == "الكل" && centerComboBox.Text != "الكل")
            {
                string Query = "select distinct Id as 'كود العميل' , name as 'الاسم', telephone as 'التليفون', mobile as 'الموبايل', fax as 'الفاكس' , governorate as 'المحافظة' , center as 'المركز', address as 'العنوان', balance as 'الرصيد' ,active as 'نشط', email as 'البريد الإلكتروني', debtAge as 'عمر الدين بالشهور',  notes as 'ملاحظات' from customerTable where center=N'" + this.centerComboBox.Text + "' ";

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
                catch (Exception ex)
                {
 
                }
            }
            else if (customerNameComboBox.Text == "الكل" && governorateComboBox.Text != "الكل" && centerComboBox.Text != "الكل")
            {
                string Query = "select distinct Id as 'كود العميل' , name as 'الاسم', telephone as 'التليفون', mobile as 'الموبايل', fax as 'الفاكس' , governorate as 'المحافظة' , center as 'المركز', address as 'العنوان', balance as 'الرصيد' ,active as 'نشط' ,email as 'البريد الإلكتروني', debtAge as 'عمر الدين بالشهور',  notes as 'ملاحظات' from customerTable where governorate =N'" + this.governorateComboBox.Text + "' and center=N'"+this.centerComboBox.Text+"' ";

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
                catch (Exception ex)
                {
 
                }
            }
        }
    }
}