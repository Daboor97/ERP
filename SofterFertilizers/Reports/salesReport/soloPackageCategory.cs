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
    public partial class soloPackageCategory : UserControl
    {
        public soloPackageCategory()
        {
            InitializeComponent();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;


        private void showFlowButton_Click(object sender, EventArgs e)
        {
            string Query = "select distinct salesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف' , categoryTable.categoryName as 'مبيعات القطاعي',categoryTable.categoryName as 'مبيعات الجملة' from salesMainTable,salesSubTable,categoryTable where salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and categoryTable.Id = salesSubTable.categoryCode;";

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

            for (int i = 0; i <= categoryDGV.Rows.Count - 1; i++)
            {
                SqlConnection connection = new SqlConnection(constring);

                connection.Open();
                this.categoryDGV.Rows[i].Cells[2].Value = new SqlCommand("select Sum(quantity) from salesSubTable,salesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and buyingType=N'قطاعي' ", connection).ExecuteScalar().ToString();
                this.categoryDGV.Rows[i].Cells[2].Value = (string.IsNullOrEmpty(this.categoryDGV.Rows[i].Cells[2].Value.ToString())) ? "0" : this.categoryDGV.Rows[i].Cells[2].Value;

                connection.Close();

                connection.Open();
                this.categoryDGV.Rows[i].Cells[3].Value = new SqlCommand("select Sum(quantity) from salesSubTable,salesMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and buyingType=N'جملة' ", connection).ExecuteScalar().ToString();
                this.categoryDGV.Rows[i].Cells[3].Value = (string.IsNullOrEmpty(this.categoryDGV.Rows[i].Cells[3].Value.ToString())) ? "0" : this.categoryDGV.Rows[i].Cells[3].Value;

                connection.Close();
            }
        }
    }
}
