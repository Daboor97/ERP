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
    public partial class highestBought : UserControl
    {
        public highestBought()
        {
            InitializeComponent();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;
            categoryDGV.Refresh();

            editedDGV.Rows.Clear();
            editedDGV.Refresh();


            string Query= "SELECT TOP("+ Int16.Parse(this.countTextBox.Text)+ ") salesSubTable.categoryCode , SUM(quantity)  FROM salesSubTable,salesMainTable where salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  GROUP BY salesSubTable.categoryCode ORDER BY SUM(quantity) ASC ; ";
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
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + Convert.ToInt32(categoryDGV.Rows[i].Cells[0].Value) + "') BEGIN select categoryName from categoryTable where Id=N'" + Convert.ToInt32(categoryDGV.Rows[i].Cells[0].Value) + "' END;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();


                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string company = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + Convert.ToInt32(categoryDGV.Rows[i].Cells[0].Value) + "') BEGIN select companyName from categoryTable where Id=N'" + Convert.ToInt32(categoryDGV.Rows[i].Cells[0].Value) + "' END;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string type = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + Convert.ToInt32(categoryDGV.Rows[i].Cells[0].Value) + "') BEGIN select mainType from categoryTable where Id=N'" + Convert.ToInt32(categoryDGV.Rows[i].Cells[0].Value) + "' END;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                try
                {
                    editedDGV.Rows.Add(categoryDGV.Rows[i].Cells[0].Value.ToString(),names,company,type, categoryDGV.Rows[i].Cells[1].Value.ToString());
                }
                catch { }
            }
         


        }
    }
}