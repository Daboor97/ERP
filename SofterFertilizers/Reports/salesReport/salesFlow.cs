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


namespace SofterFertilizers.Reports
{
    public partial class salesFlow : UserControl
    {
        public salesFlow()
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
                string Query = "select distinct salesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', salesSubTable.unit as 'الوحدة', categoryTable.categoryName as 'الكمية', categoryTable.categoryName as 'الإجمالي'  from salesSubTable,categoryTable, salesMainTable where salesSubTable.categoryCode=categoryTable.Id  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.Id = salesSubTable.billCode  and salesMainTable.storeName =N'" + this.storeNameComboBox.Text + "';";

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
                    this.categoryDGV.Rows[i].Cells[3].Value = new SqlCommand("select Sum(quantity) from salesSubTable,salesMainTable where categoryCode =N'"+ this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and unit=N'" + this.categoryDGV.Rows[i].Cells[2].Value.ToString() + "' and salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.storeName =N'" + this.storeNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                    connection.Close();


                    connection.Open();
                    this.categoryDGV.Rows[i].Cells[4].Value = new SqlCommand("select Sum(salesSubTable.sum) from salesSubTable,salesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and unit=N'" + this.categoryDGV.Rows[i].Cells[2].Value.ToString() + "' and salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.storeName =N'" + this.storeNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                    connection.Close();
                }
            }

            else if (reportComboBox.Text == "مفصّل")
            {
                string Query = "select salesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', salesSubTable.unit as 'الوحدة',salesSubTable.quantity as 'الكمية', salesSubTable.purchasePrice as 'سعر البيع' , salesSubTable.discountRate as 'نسبة الخصم', salesSubTable.discountAmount as 'قيمة الخصم',  salesSubTable.sum as 'الإجمالي',salesMainTable.Id as 'رقم المرتجع', salesMainTable.date as 'التاريخ'  from salesSubTable,categoryTable, salesMainTable where salesSubTable.categoryCode=categoryTable.Id  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.Id = salesSubTable.billCode  and salesMainTable.storeName =N'" + this.storeNameComboBox.Text + "';";

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
