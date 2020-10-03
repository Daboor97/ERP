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
    public partial class storeCompanyReport : UserControl
    {
        public storeCompanyReport()
        {
            InitializeComponent();
            fillCompanyListBox();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        string companyName = "";


        void fillCompanyListBox()
        {
            companysListBox.Items.Clear();

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct companyName from companyTable;";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    companysListBox.Items.Add(dr["companyName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();
        }

        private void companysListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();

            companyName = companysListBox.SelectedItem.ToString();
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            string Query = "select distinct categoryQuantityTable.categoryNumber as 'كود الصنف',categoryTable.categoryName as 'اسم الصنف', categoryTable.companyName as 'الشركة', categoryTable.mainUnit as 'الوحدة', categoryTable.mainType as 'النوع', categoryTable.storeCode as 'الكود المخزني',  categoryQuantityTable.Quantity as 'الكمية' ,categoryQuantityTable.storeName as 'اسم المخزن'  from categoryQuantityTable,categoryTable where categoryQuantityTable.categoryNumber = categoryTable.Id  and categoryQuantityTable.categoryNumber IN (select Id from categoryTable where companyName = N'" + this.companyName+"' ) ;";

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
