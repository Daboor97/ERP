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

namespace SofterFertilizers.store
{
    public partial class storeDestroyedReport : UserControl
    {
        public storeDestroyedReport()
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

                string Query = "select distinct destroyedSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', destroyedSubTable.unit as 'الوحدة', categoryTable.categoryName as 'الكمية'  from destroyedSubTable,categoryTable, destroyedMainTable where destroyedSubTable.categoryCode=categoryTable.Id  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and destroyedMainTable.Id = destroyedSubTable.destroyedCode  and destroyedMainTable.storeName =N'" + this.storeNameComboBox.Text + "';";

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
                    this.categoryDGV.Rows[i].Cells[3].Value = new SqlCommand("select Sum(quantity) from destroyedSubTable,destroyedMainTable where categoryCode =N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and unit=N'" + this.categoryDGV.Rows[i].Cells[2].Value.ToString() + "' and destroyedSubTable.destroyedCode = destroyedMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and destroyedMainTable.storeName =N'" + this.storeNameComboBox.Text + "'", connection).ExecuteScalar().ToString();
                    connection.Close();

                
                }
        }
    }
}
