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
    public partial class categoryAddPermission : UserControl
    {
        public categoryAddPermission()
        {
            InitializeComponent();
            fillCategoryDGV();
            fill();
            clear();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;


        void clear()
        {
            categoryNameTextBox.Text = "";
            categoryCodeTextBox.Text = "";
            quantityTextBox.Text = "0";
            unitComboBox.Text = "";
            priceTextBox.Text = "0";
            categorySumTextBox.Text = "0";
            quantityTextBox.Text = "0";
        }

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

            //permission Addittion Code
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from permissionAdditionMainTable) BEGIN SELECT MAX(Id) FROM permissionAdditionMainTable END", conDataBase).ExecuteScalar().ToString();

            if (maxBill != "")
            {
                int maxBillint = Convert.ToInt32(maxBill);
                permissionAdditionCode.Text = Convert.ToString(maxBillint + 1);
                conDataBase.Close();
            }

            else
            {
                permissionAdditionCode.Text = "1";
                conDataBase.Close();
            }
        }

        void fillCategoryDGV()
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة' from categoryTable;";



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

        private void categoryCodeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryNameSearchTextBox.Text = "";
            categoryStoreCodeSearchTextBox.Text = "";

            categoryDGV.DataBindings.Clear();

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة' from categoryTable where Id like N'%" + this.categoryCodeSearchTextBox.Text + "%';";

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
            categoryStoreCodeSearchTextBox.Text = "";
            categoryCodeSearchTextBox.Text = "";

            categoryDGV.DataBindings.Clear();

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة' from categoryTable where categoryName like N'%" + this.categoryNameSearchTextBox.Text + "%';";

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
            categoryNameSearchTextBox.Text = "";
            categoryCodeSearchTextBox.Text = "";

            categoryDGV.DataBindings.Clear();

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة' from categoryTable where storeCode like N'%" + this.categoryStoreCodeSearchTextBox.Text + "%';";

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

        private void storeNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //store Code
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            storeCodeTextBox.Text = new SqlCommand("select Id from storeTable where storeName =N'" + this.storeNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
         
        }

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
                    this.categoryCodeTextBox.Text = row.Cells[0].Value.ToString();
                    this.categoryNameTextBox.Text = row.Cells[1].Value.ToString();
                    this.priceTextBox.Text = row.Cells[7].Value.ToString(); 
                }
            }
            catch
            {

            }
        }

        private void categoryCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            //Unit Combo Boxes
            unitComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct mainUnit from categoryTable where Id=N'" + this.categoryCodeTextBox.Text+ "';";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    unitComboBox.Items.Add(dr["mainUnit"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            Query = "select distinct subUnit from subUnitTable where categorycode=N'" + this.categoryCodeTextBox.Text + "';";
            dt = new DataTable();
            da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    unitComboBox.Items.Add(dr["subUnit"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (unitComboBox.Items.Count > 0)
            {
                unitComboBox.Text = unitComboBox.Items[0].ToString();
            }
        }


        private void unitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            priceTextBox.Text = new SqlCommand("If EXISTS (SELECT 1 FROM categoryTable where mainUnit=N'"+this.unitComboBox.Text+"') Begin Select sellingPrice from categoryTable Where mainUnit = N'"+this.unitComboBox.Text+ "' END ELSE Begin Select sellingPrice from sunUnitTable Where mainUnit = N'" + this.unitComboBox.Text + "' END ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
        }

        private void addQuantityToStore_Click(object sender, EventArgs e)
        {
            string categoryCode = categoryCodeTextBox.Text;
            string categoryName = categoryNameTextBox.Text;
            string unit = unitComboBox.Text;
            string unitPrice = priceTextBox.Text;
            string quantity = quantityTextBox.Text;
            string unitSum = categorySumTextBox.Text;

            string[] row = { categoryCode, categoryName, unit, unitPrice, quantity, unitSum };
            permissionAdditionDGV.Rows.Add(row);
            clear();
        }

        private void permissionAdditionDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            totalSumTextBox.Text = "0";
            for (int i = 0; i <= permissionAdditionDGV.Rows.Count - 1; i++)
            {
                totalSumTextBox.Text = Convert.ToString(Convert.ToInt32(totalSumTextBox.Text) + Convert.ToInt32(permissionAdditionDGV.Rows[i].Cells[5].Value));
            }
        }
        private void permissionAdditionDGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            totalSumTextBox.Text = "0";
            for (int i = 0; i <= permissionAdditionDGV.Rows.Count - 1; i++)
            {
                totalSumTextBox.Text = Convert.ToString(Convert.ToInt32(totalSumTextBox.Text) + Convert.ToInt32(permissionAdditionDGV.Rows[i].Cells[5].Value));
            }
        }

        private void saveCategoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "IF NOT EXISTS (select 1 FROM permissionAdditionMainTable where Id= N'" + this.permissionAdditionCode.Text + "') BEGIN INSERT INTO permissionAdditionMainTable(StoreName,sum,status,date) VALUES (N'" + this.storeNameComboBox.Text + "',N'" + this.totalSumTextBox.Text + "','False',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "') END ";
                SqlConnection conDataBase = new SqlConnection(constring);
                SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
                SqlDataReader myReader;
                try
                {

                    conDataBase.Open();
                    myReader = cmdDataBase.ExecuteReader();
                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
 
                }

                for (int i = 0; i < permissionAdditionDGV.Rows.Count - 1; i++)
                {

                    Query = "INSERT INTO permissionAdditionSubTable(permissionAdditionCode,categoryName,categoryCode,unit,price,quantity,categorySum) VALUES (N'" + Int32.Parse(this.permissionAdditionCode.Text) + "',N'" +this.permissionAdditionDGV.Rows[i].Cells[1].Value.ToString() + "',N'" + this.permissionAdditionDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.permissionAdditionDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.permissionAdditionDGV.Rows[i].Cells[3].Value.ToString() + "',N'" + this.permissionAdditionDGV.Rows[i].Cells[4].Value.ToString() + "',N'" + this.permissionAdditionDGV.Rows[i].Cells[5].Value.ToString() + "')";
                    conDataBase = new SqlConnection(constring);
                    cmdDataBase = new SqlCommand(Query, conDataBase);
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();
                        while (myReader.Read())
                        {
                        }
                    }
                    catch (Exception ex)
                    {
     
                    }
                }
                MessageBox.Show("حفظ");
                fill();
                fillCategoryDGV();
            }
            catch(Exception ex)
            {
            }
        }

        private void priceTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                categorySumTextBox.Text = (Int32.Parse(quantityTextBox.Text) * Int32.Parse(priceTextBox.Text)).ToString();
            }
            catch
            {

            }
        }

        private void quantityTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                categorySumTextBox.Text = (Int32.Parse(quantityTextBox.Text) * Int32.Parse(priceTextBox.Text)).ToString();
            }
            catch
            {

            }
        }

        public void refreshLocal()
        {
            clear();
            fillCategoryDGV();
            fill();
        }
      
    }
}
