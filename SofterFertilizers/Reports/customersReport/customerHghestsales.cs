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
    public partial class customerHghestsales : UserControl
    {
        public customerHghestsales()
        {
            InitializeComponent();
            fillCategoryDGV();
        }

        string categoryCode = "";

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fillCategoryDGV()
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select distinct categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة الرئيسية', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة' from CategoryTable;";

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

        private void categoryCodeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryNameSearchTextBox.Text = "";
            categoryStoreCodeSearchTextBox.Text = "";

            categoryDGV.DataBindings.Clear();
            string Query = "select distinct categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة الرئيسية', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة' from CategoryTable where categoryTable.Id like N'%" + this.categoryCodeSearchTextBox.Text + "%';";


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

        private void categoryNameSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryStoreCodeSearchTextBox.Text = "";
            categoryCodeSearchTextBox.Text = "";

            categoryDGV.DataBindings.Clear();

            string Query = "select distinct categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة الرئيسية', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة' from CategoryTable where categoryName like N'%" + this.categoryNameSearchTextBox.Text + "%';";

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

        private void categoryStoreCodeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryNameSearchTextBox.Text = "";
            categoryCodeSearchTextBox.Text = "";

            categoryDGV.DataBindings.Clear();

            string Query = "select distinct categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة الرئيسية', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة' from CategoryTable where storeCode like N'%" + this.categoryStoreCodeSearchTextBox.Text + "%';";

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

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();

            DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
            categoryCode = row.Cells[0].Value.ToString();
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();

            editedDGV.Rows.Clear();
            editedDGV.Refresh();

            string Query = "SELECT TOP(" + Int16.Parse(this.countTextBox.Text) + ") salesMainTable.customerName, SUM(salesSubTable.quantity) FROM salesSubTable,salesMainTable,customerTable where salesMainTable.customerName =customerTable.name and customerTable.active='True' and salesSubTable.billCode = salesMainTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesSubTAble.categoryCode = N'"+ categoryCode + "'   GROUP BY salesMainTable.customerName ORDER BY SUM(quantity) Desc ; ";
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

            for (int i = 0; i <= selectedDGV.Rows.Count - 1; i++)
            {
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string Id = new SqlCommand("IF EXISTS(select 1 from customerTable where name = N'" + selectedDGV.Rows[i].Cells[0].Value.ToString() + "') BEGIN select Id from customerTable where name = N'" + selectedDGV.Rows[i].Cells[0].Value.ToString() + "' END;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();


                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string governorate = new SqlCommand("IF EXISTS(select 1 from customerTable where name = N'" + selectedDGV.Rows[i].Cells[0].Value.ToString() + "') BEGIN select governorate from customerTable where name = N'" + selectedDGV.Rows[i].Cells[0].Value.ToString() + "' END;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string center = new SqlCommand("IF EXISTS(select 1 from customerTable where name = N'" + selectedDGV.Rows[i].Cells[0].Value.ToString() + "') BEGIN select center from customerTable where name = N'" + selectedDGV.Rows[i].Cells[0].Value.ToString() + "' END;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                try
                {
                    editedDGV.Rows.Add(Id,selectedDGV.Rows[i].Cells[0].Value.ToString(), governorate, center, selectedDGV.Rows[i].Cells[1].Value.ToString());
                }
                catch { }
            }

        }
    }
}
