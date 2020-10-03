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


namespace SofterFertilizers.Reports.customersReport
{
    public partial class customerCategorySumReport : UserControl
    {
        public customerCategorySumReport()
        {
            InitializeComponent();
            fill();
        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {

            //Company Combo Boxes
            customerNameComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct name from customerTable where active = 'True';";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    customerNameComboBox.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (customerNameComboBox.Items.Count > 0)
            {
                customerNameComboBox.Text = customerNameComboBox.Items[0].ToString();
            }
        }

        private void customerNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDGV.Rows.Clear();
            selectedDGV.Refresh();


            try
            {
                //supplier Code
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                customerCodeTextBox.Text = new SqlCommand("select Id from customerTable where name=N'" + this.customerNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
            }
            catch
            {
            }
        }

        private void customerCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            selectedDGV.Rows.Clear();
            selectedDGV.Refresh();

            try
            {
                //supplierName
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                customerNameComboBox.Text = new SqlCommand("IF EXISTS(select 1 from customerTable where Id=N'" + this.customerCodeTextBox.Text + "') BEGIN select name from customerTable where Id=N'" + this.customerCodeTextBox.Text + "' and active='TRUE' END ;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
            }
            catch
            {
            }
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            selectedDGV.Rows.Clear();
            selectedDGV.Refresh();

            editedDGV.DataSource = null;
            editedDGV.Refresh();


            string Query = "SELECT distinct categoryCode,unit  FROM salesSubTable,salesMainTable where salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  and customerName=N'"+this.customerNameComboBox.Text+"'; ";
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
                editedDGV.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            for (int i = 0; i <= editedDGV.Rows.Count - 1; i++)
            {
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + Convert.ToInt32(editedDGV.Rows[i].Cells[0].Value) + "') BEGIN select categoryName from categoryTable where Id=N'" + Convert.ToInt32(editedDGV.Rows[i].Cells[0].Value) + "' END;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string sum = new SqlCommand("select SUM(quantity) from salesSubTable,salesMainTable where salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  and customerName=N'" + this.customerNameComboBox.Text + "'and categoryCode=N'" + Convert.ToInt32(editedDGV.Rows[i].Cells[0].Value) + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();


                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string company = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + Convert.ToInt32(editedDGV.Rows[i].Cells[0].Value) + "') BEGIN select companyName from categoryTable where Id=N'" + Convert.ToInt32(editedDGV.Rows[i].Cells[0].Value) + "' END;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string type = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + Convert.ToInt32(editedDGV.Rows[i].Cells[0].Value) + "') BEGIN select mainType from categoryTable where Id=N'" + Convert.ToInt32(editedDGV.Rows[i].Cells[0].Value) + "' END;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                try
                {
                    selectedDGV.Rows.Add(editedDGV.Rows[i].Cells[0].Value.ToString(), names, company, type, editedDGV.Rows[i].Cells[1].Value.ToString(),sum);
                }
                catch { }
            }
        }
    }
}
