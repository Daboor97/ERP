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

namespace SofterFertilizers.Reports.storeReports
{
    public partial class storeBalance : UserControl
    {
        public storeBalance()
        {
            InitializeComponent();
            fill();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {
            //store Combo Boxes
            storeNameComboBox.Items.Clear();

            storeNameComboBox.Items.Add("الكل");

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
            if(storeNameComboBox.Text == "الكل")
            {
                SqlConnection connection = new SqlConnection(constring);
                connection.Open();
                string first = new SqlCommand("select SUM(quantity * sellingPrice) from categoryUpdateMainTable where date = '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' ", connection).ExecuteScalar().ToString();
                connection.Close();
                firstCategoryBalanceLabel.Text = (string.IsNullOrEmpty(first)) ? "0" : first;

                connection = new SqlConnection(constring);
                connection.Open();
                string last= new SqlCommand("select SUM(quantity * sellingPrice) from categoryUpdateMainTable where date = '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ", connection).ExecuteScalar().ToString();
                connection.Close();
                lastCategoryBalanceLabel.Text = (string.IsNullOrEmpty(last)) ? "0" : last;

                connection = new SqlConnection(constring);
                connection.Open();
                string purchases= new SqlCommand("select SUM(sumAfter) from purchasesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", connection).ExecuteScalar().ToString();
                connection.Close();
                purchasesBillLabel.Text = (string.IsNullOrEmpty(purchases)) ? "0" : purchases;

                connection = new SqlConnection(constring);
                connection.Open();
                string returnedPurchases = new SqlCommand("select SUM(sum) from returnedpurchasesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", connection).ExecuteScalar().ToString();
                connection.Close();
                returnedPurchasesBillLabel.Text = (string.IsNullOrEmpty(returnedPurchases)) ? "0" : returnedPurchases;

                connection = new SqlConnection(constring);
                connection.Open();
                string sales= new SqlCommand("select SUM(sumAfter) from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", connection).ExecuteScalar().ToString();
                connection.Close();
                salesBillLabel.Text = (string.IsNullOrEmpty(sales)) ? "0" : sales;

                totalIncomeLabel.Text = (Convert.ToDouble(returnedPurchasesBillLabel.Text) + Convert.ToDouble(salesBillLabel.Text)).ToString();
                totalOutcomeLabel.Text = (Convert.ToDouble(purchasesBillLabel.Text) + Convert.ToDouble(resturnedSalesBillLabel.Text)).ToString();
            }

            else
            {
                SqlConnection connection = new SqlConnection(constring);
                connection.Open();
                string first = new SqlCommand("select SUM(quantity * sellingPrice) from categoryUpdateMainTable where date = '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "'  and storeName=N'"+this.storeNameComboBox.Text+"' ", connection).ExecuteScalar().ToString();
                connection.Close();
                firstCategoryBalanceLabel.Text = (string.IsNullOrEmpty(first)) ? "0" : first;

                connection = new SqlConnection(constring);
                connection.Open();
                string last = new SqlCommand("select SUM(quantity * sellingPrice) from categoryUpdateMainTable where date = '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and storeName=N'" + this.storeNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                connection.Close();
                lastCategoryBalanceLabel.Text = (string.IsNullOrEmpty(last)) ? "0" : last;

                connection = new SqlConnection(constring);
                connection.Open();
                string purchases = new SqlCommand("select SUM(sumAfter) from purchasesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  and storeName=N'" + this.storeNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                connection.Close();
                purchasesBillLabel.Text = (string.IsNullOrEmpty(purchases)) ? "0" : purchases;

                connection = new SqlConnection(constring);
                connection.Open();
                string returnedPurchases = new SqlCommand("select SUM(sum) from returnedpurchasesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and storeName=N'" + this.storeNameComboBox.Text + "' ", connection).ExecuteScalar().ToString();
                connection.Close();
                returnedPurchasesBillLabel.Text = (string.IsNullOrEmpty(returnedPurchases)) ? "0" : returnedPurchases;

                connection = new SqlConnection(constring);
                connection.Open();
                string sales = new SqlCommand("select SUM(sumAfter) from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  and storeName=N'" + this.storeNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                connection.Close();
                salesBillLabel.Text = (string.IsNullOrEmpty(sales)) ? "0" : sales;

                totalIncomeLabel.Text = (Convert.ToDouble(returnedPurchasesBillLabel.Text) + Convert.ToDouble(salesBillLabel.Text)).ToString();
                totalOutcomeLabel.Text = (Convert.ToDouble(purchasesBillLabel.Text) + Convert.ToDouble(resturnedSalesBillLabel.Text)).ToString();
            }
        }
    }
}