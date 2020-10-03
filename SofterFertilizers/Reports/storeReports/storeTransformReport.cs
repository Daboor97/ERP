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
    public partial class storeTransformReport : UserControl
    {
        public storeTransformReport()
        {
            InitializeComponent();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;

            string Query = "select transportSubTable.categoryCode as 'كود الصنف' ,categoryTable.categoryName as 'اسم الصنف',transportSubTable.unit as 'الوحدة' , categoryTable.companyName as 'اسم الشركة', transportSubTable.quantity as 'الكمية',transportMainTable.fromStoreName as 'من مخزن' ,transportMainTable.toStoreName as 'إلى مخزن', transportMainTable.notes as 'ملاحظات', transportMainTable.date as 'تاريخ'  from transportSubTable,transportMainTable,categoryTable where transportSubTable.categoryCode = categoryTable.Id and transportSubTable.transportCode = transportMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ";
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
        }
    }
}
