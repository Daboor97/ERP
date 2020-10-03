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
    public partial class customerTypeReport : UserControl
    {
        public customerTypeReport()
        {
            InitializeComponent();
            fill();
        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;


        void fill()
        {

            //Type Combo Boxes
            TypeComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct typeName from typeTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TypeComboBox.Items.Add(dr["typeName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();
            if (TypeComboBox.Items.Count > 0)
            {
                TypeComboBox.Text = TypeComboBox.Items[0].ToString();
            }

            //supplier ComboBox
            customerNameComboBox.Items.Clear();


            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            Query = "select distinct name from customerTable;";
            dt = new DataTable();
            da = new SqlDataAdapter(Query, conDataBase);
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
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();


            try
            {
                //supplier Code
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                customerCodeTextBox.Text = new SqlCommand("select Id from customerTable where name=N'" + this.customerNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
            }
            catch
            {
            }
        }

        private void customerCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();


            try
            {
                //supplierName
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                customerNameComboBox.Text = new SqlCommand("IF EXISTS(select 1 from customerTable where Id=N'" + this.customerCodeTextBox.Text + "') BEGIN select name from customerTable where Id=N'" + this.customerCodeTextBox.Text + "' and active='TRUE' END ;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
            }
            catch
            {
            }
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {

            string Query = "select salesSubtable.billCode as 'كود الفاتورة',salesSubTable.categoryCode as 'كود الصنف' , categoryTable.categoryName as 'اسم الصنف' , salesSubTable.unit as 'الوحدة', salesSubTable.quantity as 'الكمية', salesSubTable.purchasePrice as 'السعر', salesSubTable.discountRate as 'نسبة الخصم',salesSubTable.discountAmount as 'قيمة الخصم', salesSubTable.sum as 'المجموع', salesMainTable.storeName as 'اسم المخزن' , salesMainTable.date as 'التاريخ' from salesMainTable,salesSubTable,categoryTable where salesSubTable.categoryCode = categoryTable.Id and categoryTable.mainType = N'" + this.TypeComboBox.Text + "' and salesSubTable.billCode =salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and customerName=N'" + this.customerNameComboBox.Text + "';";

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
                selectedDGV.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
