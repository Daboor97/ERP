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

namespace SofterFertilizers.Reports.safeReports
{
    public partial class safeTransferReport : UserControl
    {
        public safeTransferReport()
        {
            InitializeComponent();
        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;


        private void showFlowButton_Click(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;
            categoryDGV.Refresh();
            string Query = "select Id as 'رقم التحويل' ,fromSafe as 'من خزنة',toSafe as 'إلى خزنة', amount as 'المبلغ',  date as 'التاريخ' from safeTransfer where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ; ";

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

            conDataBase.Close();
        }
    }
}
