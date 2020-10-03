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
    public partial class customerBalanceDetails : UserControl
    {
        public customerBalanceDetails()
        {
            InitializeComponent();
            fill();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {
            //supplier ComboBox
            customerNameComboBox.Items.Clear();
            

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct name from customerTable where active='TRUE' ;";
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
            categoryDGV.DataSource = null;
            categoryDGV.Refresh();
            dateSumTextBox.Text = "";
            sumTextBox.Text = "";

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
            categoryDGV.DataSource = null;
            categoryDGV.Refresh();
            dateSumTextBox.Text = "";
            sumTextBox.Text = "";

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
            string Query = "SELECT billNumber as 'رقم الفاتورة', billDetails as 'تفاصيل الفاتورة', direction as 'مدفوع', safeName as 'الخزنة', amount as 'المبلغ' , date as 'التاريخ'  from customerBalanceTable where customerNumber =N'"+this.customerCodeTextBox.Text+ "'  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ; ";
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

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            sumTextBox.Text = new SqlCommand("select balance from customerTable where name=N'" + this.customerNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();


           

            try
            {
                dateSumTextBox.Text = "0";
                


                double totalProfitSum = 0;
                for (int i = 0; i <= categoryDGV.Rows.Count - 1; i++)
                {

                    if (categoryDGV.Rows[i].Cells[2].Value.ToString() == "من العميل")
                    {
                        totalProfitSum += Convert.ToDouble(categoryDGV.Rows[i].Cells[4].Value);
                    }

       
                    else if (categoryDGV.Rows[i].Cells[2].Value.ToString() == "للعميل")
                    {
                        totalProfitSum -= Convert.ToDouble(categoryDGV.Rows[i].Cells[4].Value);
                    }
                }

                dateSumTextBox.Text = totalProfitSum.ToString();


            }
            catch
            {
            }
        }
    }
}
