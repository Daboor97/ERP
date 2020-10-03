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
    public partial class historyCategory : UserControl
    {
        public historyCategory()
        {
            InitializeComponent();
        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            string Query = "select distinct categoryUpdateMainTable.categoryNumber as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', categoryTable.mainUnit as 'الوحدة', categoryTable.mainType as 'النوع', categoryTable.storeCode as 'الكود المخزني',categoryUpdateMainTable.sellingPrice as 'سعر الشراء' ,categoryUpdateMainTable.packagePrice as 'سعر الجملة' , categoryUpdateMainTable.buyingPrice as 'سعر البيع' ,storeName as 'اسم المخزن',date as 'التاريخ' ,  categoryUpdateMainTable.quantity as 'الكمية'  from categoryUpdateMainTable,categoryTable where categoryUpdateMainTable.categoryNumber =categoryTable.Id and date = '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "';";

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
