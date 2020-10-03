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
    public partial class categoryQuantities : UserControl
    {
        public categoryQuantities()
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
            unitComboBox.Text = "";
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

        private void storeNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            //store Code
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            storeCodeTextBox.Text = new SqlCommand("select Id from storeTable where storeName =N'" + this.storeNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            quantityDGV.Rows.Clear();
            quantityDGV.Refresh();
            clear();

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

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
                    this.categoryCodeTextBox.Text = row.Cells[0].Value.ToString();
                    this.categoryNameTextBox.Text = row.Cells[1].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void categoryCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            //Unit Combo Boxes
            unitComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct mainUnit from categoryTable where Id=N'" + this.categoryCodeTextBox.Text + "';";
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
            try
            {
                //Quantity in mainUnit
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                float quantity = float.Parse(new SqlCommand("Select quantity from CategoryQuantityTable where categoryNumber=N'" + this.categoryCodeTextBox.Text + "' and storeName=N'" + this.storeNameComboBox.Text + "'", conDataBase).ExecuteScalar().ToString());
                conDataBase.Close();

                //If the mainUnit is used => use the quantity Straight Away, if the subUnit is used then multiply the quantity by the ratio in the subUnit Table 

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string mainUnit = new SqlCommand("IF EXISTS (select mainUnit from categoryTable where Id = N'" + this.categoryCodeTextBox.Text + "') BEGIN select mainUnit from categoryTable where Id = N'" + this.categoryCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                if (mainUnit == unitComboBox.Text)
                {
                    quantityTextBox.Text = quantity.ToString();
                }

                else
                {
                    //ratio
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "'", conDataBase).ExecuteScalar().ToString());
                    conDataBase.Close();

                    quantityTextBox.Text = (ratio * quantity).ToString();
                }
            }
            catch(Exception ex)
            {
            }
        }

        private void addQuantityToStore_Click(object sender, EventArgs e)
        {

            string categoryCode = categoryCodeTextBox.Text;
            string categoryName = categoryNameTextBox.Text;
            string unit = unitComboBox.Text;
            string quantity = quantityTextBox.Text;
           

            string[] row = { categoryCode, categoryName, unit, quantity};
            quantityDGV.Rows.Add(row);
            clear();
        }

        private void saveCategoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < quantityDGV.Rows.Count - 1; i++)
                {
                    //First See the unit if it was the mainUnit, insert it directly to the quantity Table if it wasn't divide by the ratio and then inserrt it to the quantity Table


                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string mainUnit = new SqlCommand("IF EXISTS (select mainUnit from categoryTable where Id=N'" +this.quantityDGV.Rows[i].Cells[0].Value.ToString() + "') BEGIN select mainUnit from categoryTable where Id =N'" + this.quantityDGV.Rows[i].Cells[0].Value.ToString() + "' END", conDataBase).ExecuteScalar().ToString();
                    float quantity;

                    if (mainUnit == this.quantityDGV.Rows[i].Cells[2].Value.ToString())
                    {
                         quantity = float.Parse(this.quantityDGV.Rows[i].Cells[3].Value.ToString());
                    }

                    else
                    {
                        //ratio
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where categoryCode=N'" + this.quantityDGV.Rows[i].Cells[0].Value.ToString() + "'", conDataBase).ExecuteScalar().ToString());
                        conDataBase.Close();

                        quantity = (float.Parse(this.quantityDGV.Rows[i].Cells[3].Value.ToString()) / ratio);
                    }

                    string Query = "IF EXISTS (select 1 from CategoryQuantityTable where storeName=N'" + this.storeNameComboBox.Text + "' and categoryNumber=N'"+ this.quantityDGV.Rows[i].Cells[0].Value.ToString()+ "') BEGIN UPDATE categoryQuantityTable SET quantity = N'"+quantity +"' WHERE storeName =N'"+ this.storeNameComboBox.Text+ "' AND categoryNumber=N'"+ this.quantityDGV.Rows[i].Cells[0].Value.ToString() + "' END ELSE INSERT INTO categoryQuantityTable(storeName,quantity,categoryNumber) Values (N'"+this.storeNameComboBox.Text+"', N'"+ quantity + "', N'" + this.quantityDGV.Rows[i].Cells[0].Value.ToString() + "')  ";

                    conDataBase = new SqlConnection(constring);
                    SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
                    SqlDataReader myReader;
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
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void refreshLocal()
        {
            fillCategoryDGV();
            fill();
            clear();
        }
    }
}
