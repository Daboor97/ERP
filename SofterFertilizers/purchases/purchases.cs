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

namespace SofterFertilizers.purchases
{
    public partial class purchases : UserControl
    {
        public purchases()
        {
            InitializeComponent();
            clear();
            clearAll();
            fillCategoryDGV();
            fill();
            cashRadioButton.Checked = true;
            soloRadioButton.Checked = true;
            newPurchasesRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;

        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string paymentType;
        string buyingType;
        string billType;
        string priceUpdate;

        void clearAll()
        {
            categoryNameTextBox.Text = "";
            categoryCodeTextBox.Text = "";
            unitComboBox.Text = "";
            quantityTextBox.Text = "1";
            buyingPriceTextBox.Text = "0";
            categoryDiscountRateTextBox.Text = "0";
            categoryDiscountAmountTextBox.Text = "0";
            categorySumTextBox.Text = "0";
            quantityLabel.Text = "0";

            billDGV.Rows.Clear();
            billDGV.Refresh();

            billSumBeforeTextBox.Text = "0";
            billDiscountRateTextBox.Text = "0";
            billDiscountAmountTextBox.Text = "0";
            billTransportTextBox.Text = "0";
            billSalesTaxTextBox.Text = "0";
            billSumAfterTextBox.Text = "0";
            paidTextBox.Text = "0";
            restTextBox.Text = "0";
        }

        void clearSpecified()
        {

            categoryNameTextBox.Text = "";
            categoryCodeTextBox.Text = "";
            unitComboBox.Text = "";
            quantityTextBox.Text = "1";
            buyingPriceTextBox.Text = "0";
            categorySumTextBox.Text = "0";
            quantityLabel.Text = "0";
        }

        void clear()
        {
            categoryNameTextBox.Text = "";
            categoryCodeTextBox.Text = "";
            unitComboBox.Text = "";
            quantityTextBox.Text = "1";
            buyingPriceTextBox.Text = "0";
            categoryDiscountRateTextBox.Text = "0";
            categoryDiscountAmountTextBox.Text = "0";
            categorySumTextBox.Text = "0";
            quantityLabel.Text = "0";
        }

        void fill()
        {
            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from purchasesMainTable) BEGIN SELECT MAX(Id) FROM purchasesMainTable END", conDataBase).ExecuteScalar().ToString();

            if (maxBill != "")
            {
                int maxBillint = Convert.ToInt32(maxBill);
                purchasesCodeTextBox.Text = Convert.ToString(maxBillint + 1);
                conDataBase.Close();
            }

            else
            {
                purchasesCodeTextBox.Text = "1";
                conDataBase.Close();
            }

            //store Combo Boxes
            storeNameComboBox.Items.Clear();
            conDataBase = new SqlConnection(constring);
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

            //supplier ComboBox
            supplierNameComboBox.Items.Clear();
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            Query = "select distinct name from supplierTable where active='TRUE';";
            dt = new DataTable();
            da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    supplierNameComboBox.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (supplierNameComboBox.Items.Count > 0)
            {
                supplierNameComboBox.Text = supplierNameComboBox.Items[0].ToString();
            }

            //safeComboBox
            safeComboBox.Items.Clear();
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            Query = "select distinct name from safeMainTable;";
            dt = new DataTable();
            da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    safeComboBox.Items.Add(dr["name"].ToString());
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (safeComboBox.Items.Count > 0)
            {
                safeComboBox.Text = safeComboBox.Items[0].ToString();
            }

        }

        void fillCategoryDGV()
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', buyingPrice as 'سعر الشراء'  from categoryTable;";

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

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', buyingPrice as 'سعر الشراء' from categoryTable where Id like N'%" + this.categoryCodeSearchTextBox.Text + "%';";

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

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', buyingPrice as 'سعر الشراء' from categoryTable where categoryName like N'%" + this.categoryNameSearchTextBox.Text + "%';";

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

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر',buyingPrice as 'سعر الشراء' from categoryTable where storeCode like N'%" + this.categoryStoreCodeSearchTextBox.Text + "%';";

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
            clear();
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
                    this.categoryCodeTextBox.Text = row.Cells[0].Value.ToString();
                    this.categoryNameTextBox.Text = row.Cells[1].Value.ToString();
                    this.buyingPriceTextBox.Text = row.Cells[8].Value.ToString();

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void categoryCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();

            bool unitBool;
            if (unitBoolString == "0")
            {
                unitBool = false;
            }
            else
            {
                unitBool = true;
            }

            if (!unitBool)
            {
                try
                {
                    //Quantity in mainUnit
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string categoryName = new SqlCommand("IF EXISTS(Select 1 from categoryTable where Id=N'" + this.categoryCodeTextBox.Text + "' ) BEGIN Select categoryName from categoryTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    categoryNameTextBox.Text = categoryName.ToString();

                    //Unit Combo Boxes
                    unitComboBox.Items.Clear();
                    conDataBase = new SqlConnection(constring);
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
                    }
                    conDataBase.Close();
                }
                catch
                {

                }
                if (unitComboBox.Items.Count > 0)
                {
                    unitComboBox.Text = unitComboBox.Items[0].ToString();
                }
            }
            else
            {
                //categoryName in mainUnit
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string categoryName = new SqlCommand("IF EXISTS(Select 1 from subUnitTAble where Id=N'" + this.categoryCodeTextBox.Text + "' ) BEGIN Select categoryCode from subUnitTAble where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                //categoryName in mainUnit
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                categoryName = new SqlCommand("IF EXISTS(Select 1 from categoryTable where Id=N'" + categoryName + "' ) BEGIN Select categoryName from categoryTable where Id=N'" + categoryName + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                categoryNameTextBox.Text = categoryName.ToString();

                //Unit Combo Boxes
                unitComboBox.Items.Clear();
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string Query = "select distinct mainUnit from categoryTable where Id=N'" + categoryName + "';";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
                da.Fill(dt);
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["mainUnit"].ToString() != "")
                            unitComboBox.Items.Add(dr["mainUnit"].ToString());
                    }
                }
                catch
                {
                }
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                Query = "select distinct subUnit from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "';";
                dt = new DataTable();
                da = new SqlDataAdapter(Query, conDataBase);
                da.Fill(dt);
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["subUnit"].ToString() != "")
                            unitComboBox.Items.Add(dr["subUnit"].ToString());
                    }
                }
                catch
                {

                }
                conDataBase.Close();

                //categoryName in mainUnit
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                categoryName = new SqlCommand("IF EXISTS(Select 1 from subUnitTAble where Id=N'" + this.categoryCodeTextBox.Text + "' ) BEGIN Select subUnit from subUnitTAble where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                this.unitComboBox.Text = categoryName.ToString();

            }


        }

        private void unitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();

            bool unitBool;
            if (unitBoolString == "0")
            {
                unitBool = false;
            }
            else
            {
                unitBool = true;

            }
            if (!unitBool)
            {
                try
                {
                    //Quantity in mainUnit
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    float quantity = float.Parse(new SqlCommand("IF EXISTS(Select 1 from CategoryQuantityTable where categoryNumber=N'" + this.categoryCodeTextBox.Text + "' and storeName=N'" + this.storeNameComboBox.Text + "') BEGIN Select quantity from CategoryQuantityTable where categoryNumber=N'" + this.categoryCodeTextBox.Text + "' and storeName=N'" + this.storeNameComboBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                    conDataBase.Close();

                    //If the mainUnit is used => use the quantity Straight Away, if the subUnit is used then multiply the quantity by the ratio in the subUnit Table 

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string mainUnit = new SqlCommand("IF EXISTS (select mainUnit from categoryTable where Id = N'" + this.categoryCodeTextBox.Text + "') BEGIN select mainUnit from categoryTable where Id = N'" + this.categoryCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                    if (mainUnit == unitComboBox.Text)
                    {
                        quantityLabel.Text = quantity.ToString();
                    }

                    else
                    {
                        //ratio
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "'", conDataBase).ExecuteScalar().ToString());
                        conDataBase.Close();

                        quantityLabel.Text = (ratio * quantity).ToString();
                    }

                    if (mainUnit == unitComboBox.Text)
                    {
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from CategoryTable where Id=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select buyingPrice from CategoryTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                        conDataBase.Close();
                        buyingPriceTextBox.Text = priceMainUnit.ToString();
                    }
                    else
                    {
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select buyingPrice from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                        conDataBase.Close();
                        buyingPriceTextBox.Text = priceMainUnit.ToString();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            else
            {
                try
                {
                    //categoryName in mainUnit
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string categoryNumber = new SqlCommand("IF EXISTS(Select 1 from subUnitTAble where Id=N'" + this.categoryCodeTextBox.Text + "' ) BEGIN Select categoryCode from subUnitTAble where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //Quantity in mainUnit
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    float quantity = float.Parse(new SqlCommand("IF EXISTS(Select 1 from CategoryQuantityTable where categoryNumber=N'" + categoryNumber + "' and storeName=N'" + this.storeNameComboBox.Text + "') BEGIN Select quantity from CategoryQuantityTable where categoryNumber=N'" + categoryNumber + "' and storeName=N'" + this.storeNameComboBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                    conDataBase.Close();

                    //If the mainUnit is used => use the quantity Straight Away, if the subUnit is used then multiply the quantity by the ratio in the subUnit Table 

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string mainUnit = new SqlCommand("IF EXISTS (select mainUnit from categoryTable where Id = N'" + categoryNumber + "') BEGIN select mainUnit from categoryTable where Id = N'" + categoryNumber + "' END", conDataBase).ExecuteScalar().ToString();

                    if (mainUnit == unitComboBox.Text)
                    {
                        quantityLabel.Text = quantity.ToString();
                    }

                    else
                    {
                        //ratio
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' and  subUnit= N'" + this.unitComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString());
                        conDataBase.Close();

                        quantityLabel.Text = (ratio * quantity).ToString();
                    }

                    if (mainUnit == unitComboBox.Text)
                    {
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from CategoryTable where Id=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select buyingPrice from CategoryTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                        conDataBase.Close();
                        buyingPriceTextBox.Text = priceMainUnit.ToString();
                    }
                    else
                    {
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select buyingPrice from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                        conDataBase.Close();
                        buyingPriceTextBox.Text = priceMainUnit.ToString();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void supplierNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //supplier Code
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                supplierCodeTextBox.Text = new SqlCommand("select Id from supplierTable where name=N'" + this.supplierNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                //supplier Balance
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                balanceTextBox.Text = new SqlCommand("select balance from supplierTable where name=N'" + this.supplierNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
            }
            catch
            {

            }
        }

        private void supplierCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //supplierName
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                supplierNameComboBox.Text = new SqlCommand("IF EXISTS(select 1 from supplierTable where Id=N'" + this.supplierCodeTextBox.Text + "' and active='TRUE') BEGIN select name from supplierTable where Id=N'" + this.supplierCodeTextBox.Text + "' and active='TRUE' END ;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();


            }
            catch
            {

            }
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();

            bool unitBool;
            if (unitBoolString == "0")
            {
                unitBool = false;
            }
            else
            {
                unitBool = true;
            }



            categoryCodeTextBox.Text = (string.IsNullOrEmpty(categoryCodeTextBox.Text)) ? "0" : categoryCodeTextBox.Text;
            quantityTextBox.Text = (string.IsNullOrEmpty(quantityTextBox.Text)) ? "0" : quantityTextBox.Text;
            buyingPriceTextBox.Text = (string.IsNullOrEmpty(buyingPriceTextBox.Text)) ? "0" : buyingPriceTextBox.Text;
            categoryDiscountRateTextBox.Text = (string.IsNullOrEmpty(categoryDiscountRateTextBox.Text)) ? "0" : categoryDiscountRateTextBox.Text;
            categoryDiscountAmountTextBox.Text = (string.IsNullOrEmpty(categoryDiscountAmountTextBox.Text)) ? "0" : categoryDiscountAmountTextBox.Text;
            categorySumTextBox.Text = (string.IsNullOrEmpty(categorySumTextBox.Text)) ? "0" : categorySumTextBox.Text;


            string categoryCode = categoryCodeTextBox.Text;
            string categoryName = categoryNameTextBox.Text;
            string unit = unitComboBox.Text;
            string quantity = quantityTextBox.Text;
            string buyingPrice = buyingPriceTextBox.Text;
            string discountRate = categoryDiscountRateTextBox.Text;
            string discountAmount = categoryDiscountAmountTextBox.Text;
            string categorySum = categorySumTextBox.Text;

            bool rowExist = false;

            for (int j = 0; j <= billDGV.Rows.Count - 1; j++)
            {
                if (categoryCodeTextBox.Text == Convert.ToString(billDGV.Rows[j].Cells[0].Value) &&
                    categoryNameTextBox.Text == Convert.ToString(billDGV.Rows[j].Cells[1].Value) &&
                    unitComboBox.Text == Convert.ToString(billDGV.Rows[j].Cells[2].Value) &&
                  buyingPriceTextBox.Text == Convert.ToString(billDGV.Rows[j].Cells[4].Value) &&
                  categoryDiscountRateTextBox.Text == Convert.ToString(billDGV.Rows[j].Cells[5].Value) &&
               categoryDiscountAmountTextBox.Text == Convert.ToString(billDGV.Rows[j].Cells[6].Value))
                {

                    int a, b, c;


                    int.TryParse(Convert.ToString(quantity), out a);
                    int.TryParse(Convert.ToString(billDGV.Rows[j].Cells[3].Value), out b);


                    c = a + b;

                    billDGV.Rows[j].Cells[3].Value = c.ToString("G29");

                    double d, joo, f;
                    double.TryParse(Convert.ToString(categorySum), out d);
                    double.TryParse(Convert.ToString(billDGV.Rows[j].Cells[7].Value), out joo);

                    f = d + joo;

                    billDGV.Rows[j].Cells[7].Value = f.ToString("G29");


                    billSumBeforeTextBox.Text = "0";
                    for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
                    {
                        billSumBeforeTextBox.Text = Convert.ToString(Convert.ToDouble(billSumBeforeTextBox.Text) + Convert.ToDouble(billDGV.Rows[i].Cells[7].Value));
                    }

                    rowExist = true;
                    break;

                }

            }



            if (!rowExist)
            {
                string[] row = { categoryCode, categoryName, unit, quantity, buyingPrice, discountRate, discountAmount, categorySum };
                billDGV.Rows.Add(row);
            }



            if (priceUpdate == "تحديث")
            {
                if (!unitBool)
                {
                    string Query = "UPDATE categoryTable SET buyingPrice=N'" + this.buyingPriceTextBox.Text + "' where Id=N'" + this.categoryCodeTextBox.Text + "'";
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
                    fillCategoryDGV();
                }

                else
                {
                    string Query = "UPDATE subUnitTable SET buyingPrice=N'" + this.buyingPriceTextBox.Text + "' where Id=N'" + this.categoryCodeTextBox.Text + "'";
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
                    fillCategoryDGV();
                } 
            }

            if (!constantizeCheckBox.Checked)
            {
                constantPriceRadioButton.Checked = true;
            }

            if (constantRatiosCheckBox.Checked)
                clearSpecified();
            else
                clear();
        }
    

        void sumFunction()
        {
            categorySumTextBox.ResetText();
            double a, b, d, e;
            double c;
            double.TryParse(quantityTextBox.Text, out a);
            double.TryParse(buyingPriceTextBox.Text, out b);
            double.TryParse(categoryDiscountRateTextBox.Text, out d);
            double.TryParse(categoryDiscountAmountTextBox.Text, out e);


            c = Convert.ToDouble((a * b) - (((d / 100.0) * a * b) + (e)));
            if (c > 0)
            {
                categorySumTextBox.Text = c.ToString("G29");
            }
        }

        private void quantityTextBox_TextChanged(object sender, EventArgs e)
        {
            sumFunction();
        }

        private void buyingPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            sumFunction();
        }

        private void categoryDiscountRateTextBox_TextChanged(object sender, EventArgs e)
        {
            sumFunction();
        }

        private void categoryDiscountAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            sumFunction();
        }

        private void destroyedDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            billSumBeforeTextBox.Text = "0";
            for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
            {
                billSumBeforeTextBox.Text = Convert.ToString(Convert.ToDouble(billSumBeforeTextBox.Text) + Convert.ToDouble(billDGV.Rows[i].Cells[7].Value));
            }
        }
        private void billDGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            billSumBeforeTextBox.Text = "0";
            for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
            {
                billSumBeforeTextBox.Text = Convert.ToString(Convert.ToDouble(billSumBeforeTextBox.Text) + Convert.ToDouble(billDGV.Rows[i].Cells[7].Value));
            }
        }

        void billSumFunction()
        {
            try
            {
                billSumAfterTextBox.ResetText();
                double a, b, d, g, f;
                double c;
                double.TryParse(billSumBeforeTextBox.Text, out a);
                double.TryParse(billDiscountRateTextBox.Text, out b);
                double.TryParse(billDiscountAmountTextBox.Text, out d);
                double.TryParse(billSalesTaxTextBox.Text, out g);
                double.TryParse(billTransportTextBox.Text, out f);

                c = Convert.ToDouble(a - ((b / 100) * a) - d + ((g / 100) * (a - ((b / 100) * a) - d)) + f);
                if (c >= 0)
                {
                    billSumAfterTextBox.Text = c.ToString("G29");
                }
            }
            catch
            {

            }
        }

        private void billSumBeforeTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
        }

        private void billDiscountRateTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
        }

        private void billDiscountAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
        }

        private void billSalesTaxTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
        }

        private void billTransportTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
        }

        private void paidTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                restTextBox.ResetText();
                double a, b;
                double c;
                double.TryParse(paidTextBox.Text, out a);
                double.TryParse(billSumAfterTextBox.Text, out b);
                c = Convert.ToDouble(b - a);

                if (c >= 0)
                {
                    restTextBox.Text = c.ToString("G29");
                }

            }

            catch
            {

            }
        }

        private void billSumAfterTextBox_TextChanged(object sender, EventArgs e)
        {
            if (cashRadioButton.Checked)
            {
             
                paidTextBox.Text = billSumAfterTextBox.Text;
            }

            else if (laterRadioButton.Checked)
            {
                paidTextBox.Text = "1";
                paidTextBox.Text = "0";
            }
        }

        private void cashRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            paymentType = "كاش";
            paidTextBox.Text = billSumAfterTextBox.Text;
        }

        private void laterRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            paymentType = "آجل";
            paidTextBox.Text = "1";
            paidTextBox.Text = "0";
        }

        private void soloRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            buyingType = "قطاعي";
        }

        private void packageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            buyingType = "جملة";
        }

        private void newPurchasesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            billType = "جديدة";
            purchasesCodeTextBox.BackColor = Color.FromArgb(64, 64, 64);
            purchasesCodeTextBox.ReadOnly = true;
            fill();
        }

        private void adjustPurhasesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            billType = "تعديل";
            purchasesCodeTextBox.BackColor = Color.FromArgb(41, 44, 51);
            purchasesCodeTextBox.ReadOnly = false;
            purchasesCodeTextBox.Text = "";
        }

        private void constantPriceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            priceUpdate = "تثبيت";
        }

        private void updatePriceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            priceUpdate = "تحديث";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            billSumBeforeTextBox.Text = (string.IsNullOrEmpty(billSumBeforeTextBox.Text)) ? "0" : billSumBeforeTextBox.Text;
            billDiscountRateTextBox.Text = (string.IsNullOrEmpty(billDiscountRateTextBox.Text)) ? "0" : billDiscountRateTextBox.Text;
            billDiscountAmountTextBox.Text = (string.IsNullOrEmpty(billDiscountAmountTextBox.Text)) ? "0" : billDiscountAmountTextBox.Text;
            billTransportTextBox.Text = (string.IsNullOrEmpty(billTransportTextBox.Text)) ? "0" : billTransportTextBox.Text;
            billSumAfterTextBox.Text = (string.IsNullOrEmpty(billSumAfterTextBox.Text)) ? "0" : billSumAfterTextBox.Text;
            paidTextBox.Text = (string.IsNullOrEmpty(paidTextBox.Text)) ? "0" : paidTextBox.Text;
            restTextBox.Text = (string.IsNullOrEmpty(restTextBox.Text)) ? "0" : restTextBox.Text;

            string storeNameForEdit = "";
            string oldSupplierName = "";
            string oldRest = "";
            string oldSafeTransactionID = "";

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string stringReturned = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select returned FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();

            stringReturned = (string.IsNullOrEmpty(stringReturned)) ? "0" : stringReturned;
            bool returned;
            if (stringReturned == "0" || stringReturned == "False")
            {
                returned = false;
            }
            else
            {
                returned = true;
            }

            if (billType == "تعديل")
            {
                if (returned == false)
                {
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    storeNameForEdit = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select storeName FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    oldSupplierName = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select SupplierName FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //rest to subtract it if adjust
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    oldRest = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select rest FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //old safe ID 
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    oldSafeTransactionID = new SqlCommand("IF EXISTS (select 1 FROM safeTable where billNo = N'" + purchasesCodeTextBox.Text + "' and type='purchases' and details ='out') BEGIN select Id FROM safeTable where  billNo = N'" + purchasesCodeTextBox.Text + "' and type='purchases' and details ='out' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                }
                else
                {
                    MessageBox.Show("لا يمكن تعديل فاتورة بها مرتجع");
                }
            }


            try
            {
                string Query = "IF NOT EXISTS (select 1 FROM purchasesMainTable where Id= N'" + this.purchasesCodeTextBox.Text + "') BEGIN INSERT INTO purchasesMainTable(paymentType,storeName,supplierName,safeName,buyingType,sumBefore,discountPercentage,discountAmount,salesTax,transport,sumAfter,paid,rest,date) VALUES (N'" + paymentType + "',N'" + this.storeNameComboBox.Text + "',N'" + this.supplierNameComboBox.Text + "',N'" + this.safeComboBox.Text + "',N'" + buyingType + "',N'" + this.billSumBeforeTextBox.Text + "',N'" + this.billDiscountRateTextBox.Text + "',N'" + this.billDiscountAmountTextBox.Text + "',N'" + this.billSalesTaxTextBox.Text + "',N'" + this.billTransportTextBox.Text + "',N'" + this.billSumAfterTextBox.Text + "',N'" + this.paidTextBox.Text + "',N'" + this.restTextBox.Text + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "') END ELSE BEGIN UPDATE purchasesMainTable SET paymentType=N'" + paymentType + "',storeName =N'" + this.storeNameComboBox.Text + "',supplierName=N'" + this.supplierNameComboBox.Text + "',safeName=N'" + this.safeComboBox.Text + "',buyingType=N'" + buyingType + "',sumBefore=N'" + this.billSumBeforeTextBox.Text + "',discountPercentage=N'" + this.billDiscountRateTextBox.Text + "',discountAmount=N'" + this.billDiscountAmountTextBox.Text + "',salesTax=N'" + this.billSalesTaxTextBox.Text + "',transport=N'" + this.billTransportTextBox.Text + "',sumAfter=N'" + this.billSumAfterTextBox.Text + "',paid=N'" + this.paidTextBox.Text + "',rest=N'" + this.restTextBox.Text + "',date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' where Id=N'" + this.purchasesCodeTextBox.Text + "' END ";
                conDataBase = new SqlConnection(constring);
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

                    double supplierBalance = 0;
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from supplierTable where name=N'" + this.supplierNameComboBox.Text + "') BEGIN SELECT balance from supplierTable where name=N'" + this.supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                    if (supplierBalanceString != "")
                    {
                        supplierBalance = Convert.ToDouble(supplierBalanceString);
                        conDataBase.Close();
                    }

                    supplierBalance += Convert.ToDouble(this.restTextBox.Text);

                    Query = "IF EXISTS (select 1 from supplierTable where name=N'" + this.supplierNameComboBox.Text + "')BEGIN UPDATE supplierTable SET balance=N'" + supplierBalance + "' where name=N'" + this.supplierNameComboBox.Text + "' END";
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
                catch (Exception ex)
                {
                }

                if (billType == "جديدة")
                {

                    //الخزنة

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.paidTextBox.Text + "','purchases',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.purchasesCodeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
                    conDataBase = new SqlConnection(constring);
                    cmdDataBase = new SqlCommand(Query, conDataBase);
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

                    catch { }

                    for (int i = 0; i < billDGV.Rows.Count - 1; i++)
                    {

                        Query = "INSERT INTO purchasesSubTable(billCode,categoryCode,unit,quantity,purchasePrice,discountRate,discountAmount,sum) VALUES (N'" + Int32.Parse(this.purchasesCodeTextBox.Text) + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[3].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[4].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[5].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[6].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[7].Value.ToString() + "')";
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

               conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();

                            bool unitBool;

                            if (unitBoolString == "0")
                            {
                                unitBool = false;
                            }
                            else
                            {
                                unitBool = true;
                            }

                            if (!unitBool)
                            {
                                double quantityInt = 0;
                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                string quantityString = new SqlCommand("IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "') BEGIN SELECT quantity from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                                if (quantityString != "")
                                {
                                    quantityInt = Convert.ToDouble(quantityString);
                                    conDataBase.Close();
                                }

                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                string mainUnit = new SqlCommand("IF EXISTS (select mainUnit from categoryTable where Id = N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "') BEGIN select mainUnit from categoryTable where Id = N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' END", conDataBase).ExecuteScalar().ToString();

                                if (mainUnit == this.billDGV.Rows[i].Cells[2].Value.ToString())
                                {
                                    quantityInt += Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString());

                                    Query = "IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "')BEGIN UPDATE CategoryQuantityTable SET quantity=N'" + quantityInt + "' where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "' END ELSE BEGIN INSERT INTO CategoryQuantityTable(storeName,quantity,categoryNumber) VALUES(N'" + this.storeNameComboBox.Text + "',N'" + quantityInt + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' ) END";
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
                                else
                                {
                                    //ratio
                                    conDataBase = new SqlConnection(constring);
                                    conDataBase.Open();
                                    float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and subUnit= N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "' ", conDataBase).ExecuteScalar().ToString());
                                    conDataBase.Close();

                                    quantityInt += (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) * ratio);

                                    Query = "IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "')BEGIN UPDATE CategoryQuantityTable SET quantity=N'" + quantityInt + "' where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "' END ELSE BEGIN INSERT INTO CategoryQuantityTable(storeName,quantity,categoryNumber) VALUES(N'" + this.storeNameComboBox.Text + "',N'" + quantityInt + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' ) END";
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
                            }
                            else
                            {
                                //categoryName in mainUnit
                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                string categoryName = new SqlCommand("IF EXISTS(Select 1 from subUnitTAble where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' ) BEGIN Select categoryCode from subUnitTAble where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                                conDataBase.Close();

                                double quantityInt = 0;
                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                string quantityString = new SqlCommand("IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "') BEGIN SELECT quantity from CategoryQuantityTable where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                                if (quantityString != "")
                                {
                                    quantityInt = Convert.ToDouble(quantityString);
                                    conDataBase.Close();
                                }


                                //ratio
                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and  subUnit= N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "' ", conDataBase).ExecuteScalar().ToString());
                                conDataBase.Close();

                                quantityInt += (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) / ratio);

                                Query = "IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "')BEGIN UPDATE CategoryQuantityTable SET quantity=N'" + quantityInt + "' where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "' END ELSE BEGIN INSERT INTO CategoryQuantityTable(storeName,quantity,categoryNumber) VALUES(N'" + this.storeNameComboBox.Text + "',N'" + quantityInt + "',N'" + categoryName + "' ) END";
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

                    }
                }

                else if (billType == "تعديل")
                {
                    Query = "DELETE FROM safeTable where Id=N'" + oldSafeTransactionID + "';";
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

                    //الخزنة

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.paidTextBox.Text + "','purchases',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.purchasesCodeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
                    conDataBase = new SqlConnection(constring);
                    cmdDataBase = new SqlCommand(Query, conDataBase);
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

                    catch { }

                        Query = "DELETE FROM purchasesSubTable where billCode=N'" + this.purchasesCodeTextBox.Text + "';";
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

                    for (int i = 0; i < editDGV.Rows.Count-1; i++)
                    {
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();

                        bool unitBool;

                        if (unitBoolString == "0")
                        {
                            unitBool = false;
                        }
                        else
                        {
                            unitBool = true;
                        }

                        if (!unitBool)
                        {
                            double quantityInt = 0;
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string quantityString = new SqlCommand("IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "') BEGIN SELECT quantity from CategoryQuantityTable where categoryNumber=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                            if (quantityString != "")
                            {
                                quantityInt = Convert.ToDouble(quantityString);
                                conDataBase.Close();
                            }

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string mainUnit = new SqlCommand("IF EXISTS (select mainUnit from categoryTable where Id = N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "') BEGIN select mainUnit from categoryTable where Id = N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' END", conDataBase).ExecuteScalar().ToString();

                            if (mainUnit == this.editDGV.Rows[i].Cells[1].Value.ToString())
                            {
                                quantityInt += Convert.ToDouble(this.editDGV.Rows[i].Cells[2].Value.ToString());

                                Query = "IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "')BEGIN UPDATE CategoryQuantityTable SET quantity=N'" + quantityInt + "' where categoryNumber=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "' END ELSE BEGIN INSERT INTO CategoryQuantityTable(storeName,quantity,categoryNumber) VALUES(N'" + this.storeNameComboBox.Text + "',N'" + quantityInt + "',N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' ) END";
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
                            else
                            {
                                //ratio
                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where Id=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "'", conDataBase).ExecuteScalar().ToString());
                                conDataBase.Close();

                                quantityInt += (Convert.ToDouble(this.editDGV.Rows[i].Cells[2].Value.ToString()) * ratio);

                                Query = "IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "')BEGIN UPDATE CategoryQuantityTable SET quantity=N'" + quantityInt + "' where categoryNumber=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "' END ELSE BEGIN INSERT INTO CategoryQuantityTable(storeName,quantity,categoryNumber) VALUES(N'" + this.storeNameComboBox.Text + "',N'" + quantityInt + "',N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' ) END";
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
                        }

                        else
                        {
                            //categoryName in mainUnit
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string categoryName = new SqlCommand("IF EXISTS(Select 1 from subUnitTAble where Id=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' ) BEGIN Select categoryCode from subUnitTAble where Id=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();

                            double quantityInt = 0;
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string quantityString = new SqlCommand("IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "') BEGIN SELECT quantity from CategoryQuantityTable where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                            if (quantityString != "")
                            {
                                quantityInt = Convert.ToDouble(quantityString);
                                conDataBase.Close();
                            }


                            //ratio
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where Id=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "'", conDataBase).ExecuteScalar().ToString());
                            conDataBase.Close();

                            quantityInt += (Convert.ToDouble(this.editDGV.Rows[i].Cells[2].Value.ToString()) / ratio);

                            Query = "IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "')BEGIN UPDATE CategoryQuantityTable SET quantity=N'" + quantityInt + "' where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "' END ELSE BEGIN INSERT INTO CategoryQuantityTable(storeName,quantity,categoryNumber) VALUES(N'" + this.storeNameComboBox.Text + "',N'" + quantityInt + "',N'" + categoryName + "' ) END";
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
                    }

                        double supplierBalance = 0;
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from supplierTable where name=N'" + oldSupplierName + "') BEGIN SELECT balance from supplierTable where name=N'" + oldSupplierName + "' END", conDataBase).ExecuteScalar().ToString();

                        if (supplierBalanceString != "")
                        {
                            supplierBalance = Convert.ToDouble(supplierBalanceString);
                            conDataBase.Close();
                        }

                        supplierBalance -= Convert.ToDouble(oldRest);

                        Query = "IF EXISTS (select 1 from supplierTable where name=N'" + oldSupplierName + "')BEGIN UPDATE supplierTable SET balance=N'" + supplierBalance + "' where name=N'" + oldSupplierName + "' END";
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


                    for (int i = 0; i < billDGV.Rows.Count - 1; i++)
                    {

                        Query = "INSERT INTO purchasesSubTable(billCode,categoryCode,unit,quantity,purchasePrice,discountRate,discountAmount,sum) VALUES (N'" + Int32.Parse(this.purchasesCodeTextBox.Text) + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[3].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[4].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[5].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[6].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[7].Value.ToString() + "')";
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

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();

                        bool unitBool;

                        if (unitBoolString == "0")
                        {
                            unitBool = false;
                        }
                        else
                        {
                            unitBool = true;
                        }

                        if (!unitBool)
                        {
                            double quantityInt = 0;
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string quantityString = new SqlCommand("IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "') BEGIN SELECT quantity from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                            if (quantityString != "")
                            {
                                quantityInt = Convert.ToDouble(quantityString);
                                conDataBase.Close();
                            }

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string mainUnit = new SqlCommand("IF EXISTS (select mainUnit from categoryTable where Id = N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "') BEGIN select mainUnit from categoryTable where Id = N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' END", conDataBase).ExecuteScalar().ToString();

                            if (mainUnit == this.billDGV.Rows[i].Cells[2].Value.ToString())
                            {
                                quantityInt += Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString());

                                Query = "IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "')BEGIN UPDATE CategoryQuantityTable SET quantity=N'" + quantityInt + "' where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "' END ELSE BEGIN INSERT INTO CategoryQuantityTable(storeName,quantity,categoryNumber) VALUES(N'" + this.storeNameComboBox.Text + "',N'" + quantityInt + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' ) END";
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
                            else
                            {
                                //ratio
                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and subUnit= N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "' ", conDataBase).ExecuteScalar().ToString());
                                conDataBase.Close();

                                quantityInt += (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) * ratio);

                                Query = "IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "')BEGIN UPDATE CategoryQuantityTable SET quantity=N'" + quantityInt + "' where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "' END ELSE BEGIN INSERT INTO CategoryQuantityTable(storeName,quantity,categoryNumber) VALUES(N'" + this.storeNameComboBox.Text + "',N'" + quantityInt + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' ) END";
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
                        }
                        else
                        {
                            //categoryName in mainUnit
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string categoryName = new SqlCommand("IF EXISTS(Select 1 from subUnitTAble where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' ) BEGIN Select categoryCode from subUnitTAble where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();

                            double quantityInt = 0;
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string quantityString = new SqlCommand("IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "') BEGIN SELECT quantity from CategoryQuantityTable where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                            if (quantityString != "")
                            {
                                quantityInt = Convert.ToDouble(quantityString);
                                conDataBase.Close();
                            }


                            //ratio
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where Id=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and  subUnit= N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "' ", conDataBase).ExecuteScalar().ToString());
                            conDataBase.Close();

                            quantityInt += (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) / ratio);

                            Query = "IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "')BEGIN UPDATE CategoryQuantityTable SET quantity=N'" + quantityInt + "' where categoryNumber=N'" + categoryName + "' and storeName=N'" + this.storeNameComboBox.Text + "' END ELSE BEGIN INSERT INTO CategoryQuantityTable(storeName,quantity,categoryNumber) VALUES(N'" + this.storeNameComboBox.Text + "',N'" + quantityInt + "',N'" + categoryName + "' ) END";
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

                    }


                }
                MessageBox.Show("حفظ");
                fill();
                fillCategoryDGV();
                clearAll();
            }
            catch (Exception ex)
            {
            }

            this.ActiveControl = categoryCodeTextBox;

        }

        private void billDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.billDGV.Rows[e.RowIndex];
                    this.categoryCodeTextBox.Text = row.Cells[0].Value.ToString();
                    this.categoryNameTextBox.Text = row.Cells[1].Value.ToString();
                    this.unitComboBox.Text = row.Cells[2].Value.ToString();
                    this.quantityTextBox.Text = row.Cells[3].Value.ToString();
                    this.buyingPriceTextBox.Text = row.Cells[4].Value.ToString();
                    this.categoryDiscountRateTextBox.Text = row.Cells[5].Value.ToString();
                    this.categoryDiscountAmountTextBox.Text = row.Cells[6].Value.ToString();
                    this.categorySumTextBox.Text = row.Cells[7].Value.ToString();
                }
            }
            catch
            {
            }
        }

        private void returnBillButton_Click(object sender, EventArgs e)
        {

        }



        private void refreshButton_Click_1(object sender, EventArgs e)
        {
            clear();
            clearAll();
            fillCategoryDGV();
            fill();
            cashRadioButton.Checked = true;
            soloRadioButton.Checked = true;
            newPurchasesRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;
            this.ActiveControl = categoryCodeTextBox;
        }

        public void refreshLocal()
        {
            clear();
            clearAll();
            fillCategoryDGV();
            fill();
            cashRadioButton.Checked = true;
            soloRadioButton.Checked = true;
            newPurchasesRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;
            this.ActiveControl = categoryCodeTextBox;
        }

        private void purchases_Load(object sender, EventArgs e)
        {
            this.ActiveControl = categoryCodeTextBox;
        }

        private void categoryCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                addCategoryButton_Click(sender, e);
            }
        }

        private void purchasesCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (adjustPurhasesRadioButton.Checked)
            {
                if (purchasesCodeTextBox.Text == "")
                {
                    clearAll();
                }
                else
                {
                    clearAll();

                    string Query = "IF EXISTS (select 1 from purchasesSubTable where billCode=N'" + this.purchasesCodeTextBox.Text + "') BEGIN select purchasesSubTable.categoryCode as 'كود الصنف',purchasesSubTable.unit as 'الوحدة', purchasesSubTable.quantity as 'الكمية',purchasesSubTable.purchasePrice as 'سعر الشراء', purchasesSubTable.discountRate as'نسبة الخصم', purchasesSubTable.discountAmount as 'قيمة الخصم', purchasesSubTable.sum as 'الإجمالي' from purchasesSubTable where billCode=N'" + this.purchasesCodeTextBox.Text + "' END;";

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
                        editDGV.DataSource = bSource;
                        sda.Update(dbdataset);
                    }

                    catch (Exception ex)
                    {
     
                    }

                    for (int i = 0; i < editDGV.Rows.Count - 1; i++)
                    {
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + editDGV.Rows[i].Cells[0].Value.ToString() + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();

                        bool unitBool;
                        if (unitBoolString == "0")
                        {
                            unitBool = false;
                        }
                        else
                        {
                            unitBool = true;
                        }
                        string categoryName;
                        if (!unitBool)
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            categoryName = new SqlCommand("Select categoryName from categoryTable where Id=N'" + editDGV.Rows[i].Cells[0].Value.ToString() + "'", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();

                        }
                        else
                        {
                            //categoryName in mainUnit
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string categoryCode = new SqlCommand("IF EXISTS(Select 1 from subUnitTAble where Id=N'" + editDGV.Rows[i].Cells[0].Value.ToString() + "' ) BEGIN Select categoryCode from subUnitTAble where Id=N'" + editDGV.Rows[i].Cells[0].Value.ToString() + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();

                            //categoryName in mainUnit
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            categoryName = new SqlCommand("IF EXISTS(Select 1 from categoryTable where Id=N'" + categoryCode + "' ) BEGIN Select categoryName from categoryTable where Id=N'" + categoryCode + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                        }
                        try
                        {

                            billDGV.Rows.Add(editDGV.Rows[i].Cells[0].Value.ToString(), categoryName, editDGV.Rows[i].Cells[1].Value.ToString(), editDGV.Rows[i].Cells[2].Value.ToString(),
                            editDGV.Rows[i].Cells[3].Value.ToString(), editDGV.Rows[i].Cells[4].Value.ToString(), editDGV.Rows[i].Cells[5].Value.ToString(),
                            editDGV.Rows[i].Cells[6].Value.ToString());

                        }
                        catch
                        {
                        }
                    }


                    try
                    {
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string paymentType = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "') BEGIN select paymentType FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();

                        if (paymentType == "كاش")
                        {
                            cashRadioButton.Checked = true;
                        }
                        else
                        {
                            laterRadioButton.Checked = true;
                        }

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string supplierName = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "') BEGIN select supplierName FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        supplierName = (string.IsNullOrEmpty(supplierName)) ? "0" : supplierName;
                        supplierNameComboBox.Text = supplierName;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string storeName = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "') BEGIN select storeName FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        storeName = (string.IsNullOrEmpty(storeName)) ? "0" : storeName;
                        storeNameComboBox.Text = storeName;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string safeName = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "') BEGIN select safeName FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        safeName = (string.IsNullOrEmpty(safeName)) ? "0" : safeName;
                        safeComboBox.Text = safeName;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string buyingTypee = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "') BEGIN select buyingType FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        if (buyingType == "قطاعي")
                        {
                            soloRadioButton.Checked = true;
                        }
                        else
                        {
                            packageRadioButton.Checked = true;
                        }

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string billSum = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "') BEGIN select sumBefore FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        billSum = (string.IsNullOrEmpty(billSum)) ? "0" : billSum;
                        billSumBeforeTextBox.Text = billSum;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string discountRate = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select discountPercentage FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        discountRate = (string.IsNullOrEmpty(discountRate)) ? "0" : discountRate;
                        billDiscountRateTextBox.Text = discountRate;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string discountAmount = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select discountAmount FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        discountAmount = (string.IsNullOrEmpty(discountAmount)) ? "0" : discountAmount;
                        billDiscountAmountTextBox.Text = discountAmount;
                        conDataBase.Close();


                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string salesTax = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select salesTax FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        salesTax = (string.IsNullOrEmpty(salesTax)) ? "0" : salesTax;
                        billSalesTaxTextBox.Text = salesTax;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string transport = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select transport FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        transport = (string.IsNullOrEmpty(transport)) ? "0" : transport;
                        billTransportTextBox.Text = transport;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string sumAfter = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select sumAfter FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        sumAfter = (string.IsNullOrEmpty(sumAfter)) ? "0" : sumAfter;
                        billSumAfterTextBox.Text = sumAfter;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string paid = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select paid FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        paid = (string.IsNullOrEmpty(paid)) ? "0" : paid;
                        paidTextBox.Text = paid;
                        conDataBase.Close();
                    }

                    catch (Exception ex)
                    {
     
                    }
                }
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            


        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string header = "QTY       item                       price     Total ";
            string sahpe1 = "**********************************************************";
            int j = 240;
            string thing = ((char)0x200E).ToString();
            Bitmap b = new Bitmap(SofterFertilizers.Properties.Resources.logo);

            Image im = b;

            e.Graphics.DrawImage(im, 80, 0);
            e.Graphics.DrawString("فاتورة مشتريـــات", new Font("fontA", 10, FontStyle.Bold), Brushes.Black, 85, 120);
            e.Graphics.DrawString("01099235588", new Font("fontA", 10, FontStyle.Bold), Brushes.Black, 0, 150);
            e.Graphics.DrawString(DateTime.Now.ToString(), new Font("fontA", 10, FontStyle.Bold), Brushes.Black, 100, 150);

            e.Graphics.DrawString(sahpe1, new Font("fontA", 10, FontStyle.Regular), Brushes.Black, 0, 180);
            e.Graphics.DrawString(header, new Font("fontA", 10, FontStyle.Bold), Brushes.Black, 0, 210);
            e.Graphics.DrawString(sahpe1, new Font("fontA", 10, FontStyle.Regular), Brushes.Black, 0, 240);
            for (int i =0;i<billDGV.RowCount-1;i++) {
                j += 20;
                thing = billDGV.Rows[i].Cells[1].Value.ToString();
                e.Graphics.DrawString(billDGV.Rows[i].Cells[3].Value.ToString(), new Font("fontA", 10, FontStyle.Regular), Brushes.Black, 0, j);
                e.Graphics.DrawString(thing, new Font("fontA", 10, FontStyle.Regular), Brushes.Black, 60, j);
                e.Graphics.DrawString(billDGV.Rows[i].Cells[4].Value.ToString(), new Font("fontA", 10, FontStyle.Regular), Brushes.Black, 180, j);
                e.Graphics.DrawString(billDGV.Rows[i].Cells[7].Value.ToString(), new Font("fontA", 10, FontStyle.Regular), Brushes.Black, 240, j);
            }
            j += 30;
            e.Graphics.DrawString(sahpe1, new Font("fontA", 10, FontStyle.Regular), Brushes.Black, 0, j);
            j += 30;
            e.Graphics.DrawString("Final  Total :      "+ billSumAfterTextBox.Text, new Font("fontA", 20, FontStyle.Bold), Brushes.Black, 0, j);

        }

        private void roundedButton1_Click_1(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

      
    }
}
