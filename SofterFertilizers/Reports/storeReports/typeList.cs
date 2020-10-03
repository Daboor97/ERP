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
    public partial class typeList : UserControl
    {
        public typeList()
        {
            InitializeComponent();
            fillCompanyListBox();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        string typeName = "";


        void fillCompanyListBox()
        {
            typeListBox.Items.Clear();

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct typeName from typeTable;";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    typeListBox.Items.Add(dr["typeName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();
        }

        private void typeListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();

            typeName = typeListBox.SelectedItem.ToString();
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            string Query = "select categoryQuantityTable.categoryNumber as 'كود الصنف',categoryTable.categoryName as 'اسم الصنف', categoryTable.mainUnit as 'الوحدة', categoryTable.mainType as 'النوع', categoryTable.storeCode as 'الكود المخزني',  categoryQuantityTable.Quantity as 'الكمية' ,categoryQuantityTable.storeName as 'اسم المخزن'  from categoryQuantityTable,categoryTable where categoryQuantityTable.categoryNumber = categoryTable.Id  and categoryQuantityTable.categoryNumber IN (select Id from categoryTable where mainType = N'" + this.typeName + "' ) ;";

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