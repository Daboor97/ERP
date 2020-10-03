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
    public partial class supplierReturnedReport : UserControl
    {
        public supplierReturnedReport()
        {
            InitializeComponent();
            fill();
            reportComboBox.Items.Add("مفصّل");
            reportComboBox.Items.Add("إجمالي");
            reportComboBox.Text = "إجمالي";
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;



        void fill()
        {

            //supplier ComboBox
            customerNameComboBox.Items.Clear();


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
            categoryDGV.DataSource = null;
            categoryDGV.Refresh();

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

        private void customerCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;
            categoryDGV.Refresh();

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

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;

            if (reportComboBox.Text == "إجمالي")
            {
                string Query = "select distinct returnedPurchasesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', returnedPurchasesSubTable.unit as 'الوحدة', categoryTable.categoryName as 'الكمية', categoryTable.categoryName as 'الإجمالي'  from returnedPurchasesSubTable,categoryTable, returnedPurchasesMainTable where returnedPurchasesSubTable.categoryCode=categoryTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and returnedPurchasesMainTable.Id = returnedPurchasesSubTable.returnedCode  and returnedPurchasesMainTable.supplierName =N'" + this.customerNameComboBox.Text + "';";

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

                for (int i = 0; i <= categoryDGV.Rows.Count - 1; i++)
                {
                    SqlConnection connection = new SqlConnection(constring);

                    connection.Open();
                    this.categoryDGV.Rows[i].Cells[3].Value = new SqlCommand("select Sum(quantity) from returnedPurchasesSubTable,returnedPurchasesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and unit=N'" + this.categoryDGV.Rows[i].Cells[2].Value.ToString() + "' and returnedPurchasesSubTable.returnedCode = returnedPurchasesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and returnedPurchasesMainTable.supplierName =N'" + this.customerNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                    connection.Close();


                    connection.Open();
                    this.categoryDGV.Rows[i].Cells[4].Value = new SqlCommand("select Sum(returnedPurchasesSubTable.sum) from returnedPurchasesSubTable,returnedPurchasesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and unit=N'" + this.categoryDGV.Rows[i].Cells[2].Value.ToString() + "' and returnedPurchasesSubTable.returnedCode = returnedPurchasesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and returnedPurchasesMainTable.supplierName =N'" + this.customerNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                    connection.Close();
                }
            }

            else if (reportComboBox.Text == "مفصّل")
            {
                string Query = "select returnedPurchasesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', returnedPurchasesSubTable.unit as 'الوحدة',returnedPurchasesSubTable.quantity as 'الكمية', returnedPurchasesSubTable.purchasePrice as 'سعر البيع' , returnedPurchasesSubTable.discountRate as 'نسبة الخصم', returnedPurchasesSubTable.discountAmount as 'قيمة الخصم',  returnedPurchasesSubTable.sum as 'الإجمالي',returnedPurchasesMainTable.Id as 'رقم الفاتورة', returnedPurchasesMainTable.date as 'التاريخ'  from returnedPurchasesSubTable,categoryTable, returnedPurchasesMainTable where returnedPurchasesSubTable.categoryCode=categoryTable.Id  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and returnedPurchasesMainTable.Id = returnedPurchasesSubTable.returnedCode  and returnedPurchasesMainTable.supplierName =N'" + this.customerNameComboBox.Text + "';";

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
