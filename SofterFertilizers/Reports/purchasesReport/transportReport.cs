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
    public partial class transportReport : UserControl
    {
        public transportReport()
        {
            InitializeComponent();
            fill();
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

            string Query = "select distinct purchasesMainTable.Id as 'كود الفاتورة', purchasesMainTable.supplierName as 'اسم المورّد',purchasesMainTable.transport as 'النقل' ,purchasesMainTable.sumAfter as 'الإجمالي بعد',purchasesMainTable.date as 'التاريخ'  from purchasesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and purchasesMainTable.storeName = N'" + this.storeNameComboBox.Text + "' and transport > 0;";

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
            catch { }

            sumTextBox.Visible = true;
            sumLabel.Visible = true;

            SqlConnection connection = new SqlConnection(constring);

            connection.Open();
            sumTextBox.Text = new SqlCommand("select Sum(transport) from purchasesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and purchasesMainTable.storeName =N'" + this.storeNameComboBox.Text + "' and transport > 0", connection).ExecuteScalar().ToString();
            connection.Close();

        }

        private void storeNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sumTextBox.Visible = false;
            sumLabel.Visible = false;
            categoryDGV.DataSource = null;

        }

        private void categoryDGV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];

                    purchasesBill purchasesBill = new purchasesBill(Convert.ToInt32(row.Cells[0].Value.ToString()));
                    purchasesBill.Show();
                    purchasesBill.BringToFront();

                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
