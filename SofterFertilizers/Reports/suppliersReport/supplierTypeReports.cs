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

namespace SofterFertilizers.Reports.suppliersReport
{
    public partial class supplierTypeReports : UserControl
    {
        public supplierTypeReports()
        {
            InitializeComponent();
            fill();
        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;


        void fill()
        {
            //Type Combo Boxes
            TypeComboBox.Items.Clear();
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
                    TypeComboBox.Items.Add(dr["typeName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();
            if (TypeComboBox.Items.Count > 0)
            {
                TypeComboBox.Text = TypeComboBox.Items[0].ToString();
            }

            //supplier ComboBox
            customerNameComboBox.Items.Clear();


            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            Query = "select distinct name from supplierTable where active='True';";
            dt = new DataTable();
            da = new SqlDataAdapter(Query, conDataBase);
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
            try
            {
                //supplier Code
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                customerCodeTextBox.Text = new SqlCommand("select Id from supplierTable where name=N'" + this.customerNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
            }
            catch
            {
            }
        }

        private void customerCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //supplierName
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                customerNameComboBox.Text = new SqlCommand("IF EXISTS(select 1 from supplierTable where Id=N'" + this.customerCodeTextBox.Text + "') BEGIN select name from supplierTable where Id=N'" + this.customerCodeTextBox.Text + "' END ;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
            }
            catch
            {
            }
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            string Query = "select purchasesSubTable.billCode as 'كود الفاتورة',purchasesSubTable.categoryCode as 'كود الصنف' , categoryTable.categoryName as 'اسم الصنف' , purchasesSubTable.unit as 'الوحدة', purchasesSubTable.quantity as 'الكمية', purchasesSubTable.purchasePrice as 'السعر', purchasesSubTable.discountRate as 'نسبة الخصم',purchasesSubTable.discountAmount as 'قيمة الخصم', purchasesSubTable.sum as 'المجموع', purchasesMainTable.storeName as 'اسم المخزن' , purchasesMainTable.date as 'التاريخ' from purchasesMainTable,purchasesSubTable,categoryTable where purchasesSubTable.categoryCode = categoryTable.Id and categoryTable.mainType = N'" + this.TypeComboBox.Text + "' and purchasesSubTable.billCode =purchasesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and customerName=N'" + this.customerNameComboBox.Text + "';";

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
