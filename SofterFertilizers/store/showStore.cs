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
    public partial class showStore : UserControl
    {
        public showStore()
        {
            InitializeComponent();
            fill();
            clear();
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

        void clear()
        {
            totalSumLabel.Text = "0";
            sumBuyingPriceLabel.Text = "0";
            sumProfitLabel.Text = "0";
        }

        private void storeNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //store Code
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            storeCodeTextBox.Text = new SqlCommand("select Id from storeTable where storeName =N'" + this.storeNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            categoryDGV.DataSource = null;
            clear();
        }

        private void addQuantityToStore_Click(object sender, EventArgs e)
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', quantity as 'الكمية' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'"+this.storeNameComboBox.Text+"';";

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

        private void categoryDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                double sum = 0;
                double buyingPrice = 0;
                double profit = 0;


                for (int i = 0; i <= categoryDGV.Rows.Count - 1; i++)
                {
                    sum = sum + (Convert.ToInt32(this.categoryDGV.Rows[i].Cells[11].Value) * Convert.ToDouble(this.categoryDGV.Rows[i].Cells[7].Value));
                    buyingPrice = buyingPrice + (Convert.ToInt32(this.categoryDGV.Rows[i].Cells[11].Value) * Convert.ToDouble(this.categoryDGV.Rows[i].Cells[10].Value));
                }

                profit = sum - buyingPrice;

                totalSumLabel.Text = sum.ToString();
                sumBuyingPriceLabel.Text = buyingPrice.ToString();
                sumProfitLabel.Text = profit.ToString();
            }
            catch
            {

            }
        }

        private void categoryCodeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', quantity as 'الكمية' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.storeNameComboBox.Text + "' and categoryTable.Id like N'%" + this.categoryCodeSearchTextBox.Text + "%';";

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

            conDataBase.Close();
        }

        private void categoryNameSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', quantity as 'الكمية' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.storeNameComboBox.Text + "' and categoryTable.categoryName like N'%" + this.categoryNameSearchTextBox.Text + "%';";

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

            conDataBase.Close();
        }

        private void categoryStoreCodeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', quantity as 'الكمية' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.storeNameComboBox.Text + "' and categoryTable.storeCode like N'%" + this.categoryStoreCodeSearchTextBox.Text + "%';";

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

            conDataBase.Close();
        }

        public void refreshLoacl()
        {
            fill();
            clear();
        }
    }
}
