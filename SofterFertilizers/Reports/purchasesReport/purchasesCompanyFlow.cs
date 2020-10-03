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
    public partial class purchasesCompanyFlow : UserControl
    {
        public purchasesCompanyFlow()
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

        private void storeNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Company Combo Boxes
            companyComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct companyTable.companyName from companyTable,purchasesSubTable,categoryTable,purchasesMainTable where categoryTable.Id =purchasesSubTable.categoryCode and purchasesSubTable.quantity > 0 and categoryTable.companyName = companyTable.companyName and purchasesSubTable.billCode = purchasesMainTable.Id and purchasesMainTable.storeName=N'" + this.storeNameComboBox.Text + "';";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    companyComboBox.Items.Add(dr["companyName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (companyComboBox.Items.Count > 0)
            {
                companyComboBox.Text = companyComboBox.Items[0].ToString();
            }
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {

            categoryDGV.DataSource = null;

            if (reportComboBox.Text == "إجمالي")
            {
                string Query = "select distinct purchasesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', purchasesSubTable.unit as 'الوحدة', categoryTable.categoryName as 'الكمية', categoryTable.categoryName as 'الإجمالي'  from purchasesSubTable,categoryTable, purchasesMainTable where purchasesSubTable.categoryCode=categoryTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and purchasesMainTable.Id = purchasesSubTable.billCode  and purchasesMainTable.storeName =N'" + this.storeNameComboBox.Text + "' and categoryTable.companyName =N'" + this.companyComboBox.Text + "' ;";

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
                    this.categoryDGV.Rows[i].Cells[3].Value = new SqlCommand("select Sum(quantity) from purchasesSubTable,purchasesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and unit=N'" + this.categoryDGV.Rows[i].Cells[2].Value.ToString() + "' and purchasesSubTable.billCode = purchasesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and purchasesMainTable.storeName =N'" + this.storeNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                    connection.Close();

                    connection.Open();
                    this.categoryDGV.Rows[i].Cells[4].Value = new SqlCommand("select Sum(purchasesSubTable.sum) from purchasesSubTable,purchasesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and unit=N'" + this.categoryDGV.Rows[i].Cells[2].Value.ToString() + "' and purchasesSubTable.billCode = purchasesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and purchasesMainTable.storeName =N'" + this.storeNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                    connection.Close();
                }
            }

            else if (reportComboBox.Text == "مفصّل")
            {
                string Query = "select purchasesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', purchasesSubTable.unit as 'الوحدة',purchasesSubTable.quantity as 'الكمية', purchasesSubTable.purchasePrice as 'سعر البيع' , purchasesSubTable.discountRate as 'نسبة الخصم', purchasesSubTable.discountAmount as 'قيمة الخصم',  purchasesSubTable.sum as 'الإجمالي',purchasesMainTable.Id as 'رقم الفاتورة', purchasesMainTable.date as 'التاريخ'  from purchasesSubTable,categoryTable, purchasesMainTable where purchasesSubTable.categoryCode=categoryTable.Id  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and purchasesMainTable.Id = purchasesSubTable.billCode  and purchasesMainTable.storeName =N'" + this.storeNameComboBox.Text + "' and categoryTable.companyName =N'" + this.companyComboBox.Text + "' ;";

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
