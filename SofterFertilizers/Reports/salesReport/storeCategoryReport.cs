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
    public partial class storeCategoryReport : UserControl
    {
        public storeCategoryReport()
        {
            InitializeComponent();
            fill();
        }

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

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            string Query = "select distinct salesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف' , categoryTable.categoryName as 'مبيعات القطاعي',categoryTable.categoryName as 'مبيعات الجملة',categoryTable.categoryName as 'إجمالي دخل بيع الصنف' from salesMainTable,salesSubTable,categoryTable where salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and categoryTable.Id = salesSubTable.categoryCode and salesMainTable.storeName=N'"+this.storeNameComboBox.Text+"';";

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
                MessageBox.Show(ex.Message);
            }
            SqlConnection connection = new SqlConnection(constring);

            for (int i = 0; i <= categoryDGV.Rows.Count - 1; i++)
            {

                connection.Open();
                this.categoryDGV.Rows[i].Cells[2].Value = new SqlCommand("select Sum(quantity) from salesSubTable,salesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and buyingType=N'قطاعي' and salesMainTable.storeName=N'" + this.storeNameComboBox.Text + "' ", connection).ExecuteScalar().ToString();
                this.categoryDGV.Rows[i].Cells[2].Value = (string.IsNullOrEmpty(this.categoryDGV.Rows[i].Cells[2].Value.ToString())) ? "0" : this.categoryDGV.Rows[i].Cells[2].Value;
                connection.Close();

                connection.Open();
                this.categoryDGV.Rows[i].Cells[3].Value = new SqlCommand("select Sum(quantity) from salesSubTable,salesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and buyingType=N'جملة' and salesMainTable.storeName=N'" + this.storeNameComboBox.Text + "' ", connection).ExecuteScalar().ToString();
                this.categoryDGV.Rows[i].Cells[3].Value = (string.IsNullOrEmpty(this.categoryDGV.Rows[i].Cells[3].Value.ToString())) ? "0" : this.categoryDGV.Rows[i].Cells[3].Value;
                connection.Close();

                connection.Open();
                this.categoryDGV.Rows[i].Cells[4].Value = new SqlCommand("select Sum(sum) from salesSubTable,salesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.storeName=N'" + this.storeNameComboBox.Text + "' ", connection).ExecuteScalar().ToString();
                this.categoryDGV.Rows[i].Cells[4].Value = (string.IsNullOrEmpty(this.categoryDGV.Rows[i].Cells[4].Value.ToString())) ? "0" : this.categoryDGV.Rows[i].Cells[4].Value;
                connection.Close();

            }

            connection.Open();
            this.sumBeforeTextbox.Text = new SqlCommand("select Sum(sumBefore) from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.storeName=N'" + this.storeNameComboBox.Text + "' ", connection).ExecuteScalar().ToString();
            this.sumBeforeTextbox.Text = (string.IsNullOrEmpty(this.sumBeforeTextbox.Text) ? "0" : this.sumBeforeTextbox.Text);
            connection.Close();

            connection.Open();
            this.sumAfterTextBox.Text = new SqlCommand("select Sum(sumAfter) from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.storeName=N'" + this.storeNameComboBox.Text + "' ", connection).ExecuteScalar().ToString();
            this.sumAfterTextBox.Text = (string.IsNullOrEmpty(this.sumAfterTextBox.Text) ? "0" : this.sumAfterTextBox.Text);
            connection.Close();

            connection.Open();
            this.profitTextBox.Text = new SqlCommand("select Sum(profit) from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.storeName=N'" + this.storeNameComboBox.Text + "' ", connection).ExecuteScalar().ToString();
            this.profitTextBox.Text = (string.IsNullOrEmpty(this.profitTextBox.Text) ? "0" : this.profitTextBox.Text);
            connection.Close();
        }
    }
}
