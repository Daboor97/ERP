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

namespace SofterFertilizers.Reports.purchasesReport
{
    public partial class returnedPurchasesReport : UserControl
    {
        public returnedPurchasesReport()
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

            //store Combo Boxes
            storeNameComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct storeName from storeTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    storeNameComboBox.Items.Add(dr["storeName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (storeNameComboBox.Items.Count > 0)
            {
                storeNameComboBox.Text = storeNameComboBox.Items[0].ToString();
            }

        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;

            if (reportComboBox.Text == "إجمالي")
            {
                string Query = "select distinct returnedPurchasesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', returnedPurchasesSubTable.unit as 'الوحدة', categoryTable.categoryName as 'الكمية', categoryTable.categoryName as 'الإجمالي'  from returnedPurchasesSubTable,categoryTable, returnedPurchasesMainTable where returnedPurchasesSubTable.categoryCode=categoryTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and returnedPurchasesMainTable.Id = returnedPurchasesSubTable.returnedCode  and returnedPurchasesMainTable.storeName =N'" + this.storeNameComboBox.Text + "';";

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
                    this.categoryDGV.Rows[i].Cells[3].Value = new SqlCommand("select Sum(quantity) from returnedPurchasesSubTable,returnedPurchasesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and unit=N'" + this.categoryDGV.Rows[i].Cells[2].Value.ToString() + "' and returnedPurchasesSubTable.returnedCode = returnedPurchasesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and returnedPurchasesMainTable.storeName =N'" + this.storeNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                    connection.Close();


                    connection.Open();
                    this.categoryDGV.Rows[i].Cells[4].Value = new SqlCommand("select Sum(returnedPurchasesSubTable.sum) from returnedPurchasesSubTable,returnedPurchasesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and unit=N'" + this.categoryDGV.Rows[i].Cells[2].Value.ToString() + "' and returnedPurchasesSubTable.returnedCode = returnedPurchasesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and returnedPurchasesMainTable.storeName =N'" + this.storeNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                    connection.Close();
                }
            }

            else if (reportComboBox.Text == "مفصّل")
            {
                string Query = "select returnedPurchasesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', returnedPurchasesSubTable.unit as 'الوحدة',returnedPurchasesSubTable.quantity as 'الكمية', returnedPurchasesSubTable.purchasePrice as 'سعر البيع' , returnedPurchasesSubTable.discountRate as 'نسبة الخصم', returnedPurchasesSubTable.discountAmount as 'قيمة الخصم',  returnedPurchasesSubTable.sum as 'الإجمالي',returnedPurchasesMainTable.Id as 'رقم الفاتورة', returnedPurchasesMainTable.date as 'التاريخ'  from returnedPurchasesSubTable,categoryTable, returnedPurchasesMainTable where returnedPurchasesSubTable.categoryCode=categoryTable.Id  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and returnedPurchasesMainTable.Id = returnedPurchasesSubTable.returnedCode  and returnedPurchasesMainTable.storeName =N'" + this.storeNameComboBox.Text + "';";

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
