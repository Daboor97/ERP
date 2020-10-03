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



namespace SofterFertilizers.sales
{
    public partial class sales : UserControl
    {
        public sales()
        {
            InitializeComponent();
            clearAll();
            fill();
            fillCategoryDGV();
            soloRadioButton.Checked = true;
            cashRadioButton.Checked = true;
            newPurchasesRadioButton.Checked = true;
            ordinaryBillRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;
            restRadioButton.Checked = true;
            dayRadioButton.Checked = true;
            detailedDebtGroupBox.Visible = false;

        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string paymentType;
        string buyingType;
        string billType;
        string divideBill;
        string priceUpdate;
        string divideDetails;

        void clearAll()
        {
            categoryNameTextBox.Text = "";
            unitComboBox.Text = "";
            quantityTextBox.Text = "1";
            categoryCodeTextBox.Text = "";
            sellingPriceTextBox.Text = "0";
            categoryDiscountRateTextBox.Text = "0";
            categoryDiscountAmountTextBox.Text = "0";
            categorySumTextBox.Text = "0";
            quantityLabel.Text = "0";
            ratioTextBox.Text = "0";

            billDGV.Rows.Clear();
            billDGV.Refresh();

            editDGV.DataSource = null;
            editDGV.Refresh();



            debtGrid.Rows.Clear();
            debtGrid.Refresh();
            DebtsNumberTextBox.Text = "";
            debtAmountTextBox.Text = "";
            debtTimeValueTextBox.Text = "";
            startDebtDateDGV.Text = "";
            endDebtDateDGV.Text = "";



            billSumBeforeTextBox.Text = "0";
            billDiscountRateTextBox.Text = "0";
            billDiscountAmountTextBox.Text = "0";
            billTransportTextBox.Text = "0";
            billSalesTaxTextBox.Text = "0";
            billSumAfterTextBox.Text = "0";
            paidTextBox.Text = "0";
            restTextBox.Text = "0";
        }

        void clear()
        {
            categoryNameTextBox.Text = "";
            categoryCodeTextBox.Text = "";
            unitComboBox.Text = "";
            quantityTextBox.Text = "1";
            sellingPriceTextBox.Text = "0";
            categoryDiscountRateTextBox.Text = "0";
            categoryDiscountAmountTextBox.Text = "0";
            categorySumTextBox.Text = "0";
            quantityLabel.Text = "0";
            ratioTextBox.Text = "0";

        }

        void clearSpecified()
        {
            categoryNameTextBox.Text = "";
            categoryCodeTextBox.Text = "";
            unitComboBox.Text = "";
            quantityTextBox.Text = "1";
            sellingPriceTextBox.Text = "0";
            categorySumTextBox.Text = "0";
            quantityLabel.Text = "0";
            ratioTextBox.Text = "0";
        }

        void fill()
        {
            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from salesMainTable) BEGIN SELECT MAX(Id) FROM salesMainTable END", conDataBase).ExecuteScalar().ToString();

            if (maxBill != "")
            {
                int maxBillint = Convert.ToInt32(maxBill);
                salesCodeTextBox.Text = Convert.ToString(maxBillint + 1);
                conDataBase.Close();
            }

            else
            {
                salesCodeTextBox.Text = "1";
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
                 
            }
            conDataBase.Close();

            if (storeNameComboBox.Items.Count > 0)
            {
                storeNameComboBox.Text = storeNameComboBox.Items[0].ToString();
            }

            //supplier ComboBox
            customerNameComboBox.Items.Clear();
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            Query = "select distinct name from customerTable where active='TRUE';";
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
                 
            }
            conDataBase.Close();

            if (customerNameComboBox.Items.Count > 0)
            {
                customerNameComboBox.Text = customerNameComboBox.Items[0].ToString();
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

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.storeNameComboBox.Text + "';";

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
            clear();
            categoryDGV.DataBindings.Clear();

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.storeNameComboBox.Text + "';";

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
            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.storeNameComboBox.Text + "' and categoryTable.Id like N'%" + this.categoryCodeSearchTextBox.Text + "%';";


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

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.storeNameComboBox.Text + "' and categoryName like N'%" + this.categoryNameSearchTextBox.Text + "%';";

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

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.storeNameComboBox.Text + "' and storeCode like N'%" + this.categoryStoreCodeSearchTextBox.Text + "%';";

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
                    if (soloaymentRadioButton.Checked)
                    {
                        this.sellingPriceTextBox.Text = row.Cells[7].Value.ToString();
                    }
                    else {
                        this.sellingPriceTextBox.Text = row.Cells[8].Value.ToString();

                    }
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
            if(unitBoolString == "0")
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
                    //categoryName in mainUnit
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
                    Query = "select distinct subUnit from subUnitTable where categorycode=N'" + this.categoryCodeTextBox.Text + "';";
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
                categoryName = new SqlCommand("IF EXISTS(Select 1 from categoryTable where Id=N'" + categoryName+ "' ) BEGIN Select categoryName from categoryTable where Id=N'" + categoryName + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
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
                        float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "' and  subUnit= N'" + this.unitComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString());
                        conDataBase.Close();

                        quantityLabel.Text = (ratio * quantity).ToString();
                    }

                    if (packagePaymentRadioButton.Checked)
                    {
                        if (mainUnit == unitComboBox.Text)
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from CategoryTable where Id=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select packagePrice from CategoryTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                            conDataBase.Close();
                            sellingPriceTextBox.Text = priceMainUnit.ToString();
                        }
                        else
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select packagePrice from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "' and  subUnit= N'" + this.unitComboBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                            conDataBase.Close();
                            sellingPriceTextBox.Text = priceMainUnit.ToString();
                        }
                    }
                    else
                    {
                        if (mainUnit == unitComboBox.Text)
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from CategoryTable where Id=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select sellingPrice from CategoryTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                            conDataBase.Close();
                            sellingPriceTextBox.Text = priceMainUnit.ToString();
                        }
                        else
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select sellingPrice from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "' and  subUnit= N'" + this.unitComboBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                            conDataBase.Close();
                            sellingPriceTextBox.Text = priceMainUnit.ToString();
                        }
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
                    string categoryNumber= new SqlCommand("IF EXISTS(Select 1 from subUnitTAble where Id=N'" + this.categoryCodeTextBox.Text + "' ) BEGIN Select categoryCode from subUnitTAble where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
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

                    if (packagePaymentRadioButton.Checked)
                    {
                        if (mainUnit == unitComboBox.Text)
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from CategoryTable where Id=N'" + categoryNumber + "') BEGIN Select packagePrice from CategoryTable where Id=N'" + categoryNumber + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                            conDataBase.Close();
                            sellingPriceTextBox.Text = priceMainUnit.ToString();
                        }
                        else
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select packagePrice from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' and  subUnit= N'" + this.unitComboBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                            conDataBase.Close();
                            sellingPriceTextBox.Text = priceMainUnit.ToString();
                        }
                    }
                    else
                    {
                        if (mainUnit == unitComboBox.Text)
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from CategoryTable where Id=N'" + categoryNumber + "') BEGIN Select sellingPrice from CategoryTable where Id=N'" + categoryNumber + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                            conDataBase.Close();
                            sellingPriceTextBox.Text = priceMainUnit.ToString();
                        }
                        else
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select sellingPrice from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' and  subUnit= N'" + this.unitComboBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                            conDataBase.Close();
                            sellingPriceTextBox.Text = priceMainUnit.ToString();
                        }
                    }


                }
                catch (Exception ex)
                {

                }
            }
        }

        private void customerNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //supplier Code
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                customerCodeTextBox.Text = new SqlCommand("select Id from customerTable where name=N'" + this.customerNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                //supplier Balance
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                balanceTextBox.Text = new SqlCommand("select balance from customerTable where name=N'" + this.customerNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
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
                customerNameComboBox.Text = new SqlCommand("IF EXISTS(select 1 from customerTable where Id=N'" + this.customerCodeTextBox.Text + "' and active='TRUE') BEGIN select name from customerTable where Id=N'" + this.customerCodeTextBox.Text + "' and active='TRUE' END ;", conDataBase).ExecuteScalar().ToString();
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

            if (!unitBool)
            {
                categoryCodeTextBox.Text = (string.IsNullOrEmpty(categoryCodeTextBox.Text)) ? "0" : categoryCodeTextBox.Text;
                quantityTextBox.Text = (string.IsNullOrEmpty(quantityTextBox.Text)) ? "0" : quantityTextBox.Text;
                sellingPriceTextBox.Text = (string.IsNullOrEmpty(sellingPriceTextBox.Text)) ? "0" : sellingPriceTextBox.Text;
                categoryDiscountRateTextBox.Text = (string.IsNullOrEmpty(categoryDiscountRateTextBox.Text)) ? "0" : categoryDiscountRateTextBox.Text;
                categoryDiscountAmountTextBox.Text = (string.IsNullOrEmpty(categoryDiscountAmountTextBox.Text)) ? "0" : categoryDiscountAmountTextBox.Text;
                categorySumTextBox.Text = (string.IsNullOrEmpty(categorySumTextBox.Text)) ? "0" : categorySumTextBox.Text;

                string categoryCode = categoryCodeTextBox.Text;
                string categoryName = categoryNameTextBox.Text;
                string unit = unitComboBox.Text;
                string quantity = quantityTextBox.Text;
                string buyingPrice = sellingPriceTextBox.Text;
                string discountRate = categoryDiscountRateTextBox.Text;
                string discountAmount = categoryDiscountAmountTextBox.Text;
                string categorySum = categorySumTextBox.Text;

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string purchasingPrice = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + categoryCode + "') BEGIN select buyingPrice from categoryTable where Id=N'" + categoryCode + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                purchasingPrice = (string.IsNullOrEmpty(purchasingPrice)) ? "0" : purchasingPrice;
                double intpurchasePrice = Convert.ToDouble(purchasingPrice);

                if (intpurchasePrice < Convert.ToDouble(buyingPrice))
                {
                    bool rowExist = false;

                    for (int j = 0; j <= billDGV.Rows.Count - 1; j++)
                    {
                        if (categoryCodeTextBox.Text == Convert.ToString(billDGV.Rows[j].Cells[0].Value) &&
                            categoryNameTextBox.Text == Convert.ToString(billDGV.Rows[j].Cells[1].Value) &&
                            unitComboBox.Text == Convert.ToString(billDGV.Rows[j].Cells[2].Value) &&
                          sellingPriceTextBox.Text == Convert.ToString(billDGV.Rows[j].Cells[4].Value) &&
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
                            profitTextBox.Text = "0";

                            double totalProfitSum = 0;
                            for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
                            {
                                billSumBeforeTextBox.Text = Convert.ToString(Convert.ToDouble(billSumBeforeTextBox.Text) + Convert.ToDouble(billDGV.Rows[i].Cells[7].Value));

                          
                                string buyingPrice2 = (string.IsNullOrEmpty(this.billDGV.Rows[i].Cells[0].Value.ToString()) ? "0" : this.billDGV.Rows[i].Cells[0].Value.ToString());

                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                                conDataBase.Close();

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
                                conDataBase.Open();
                                string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from categoryTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                                conDataBase.Close();
                                names = (string.IsNullOrEmpty(names)) ? "0" : names;
                                double intpurchasePrice2 = Convert.ToDouble(names);
                                totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                                }
                                else
                                {
                                    conDataBase.Open();
                                    string names = new SqlCommand("IF EXISTS(select 1 from subUnitTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                                    conDataBase.Close();
                                    names = (string.IsNullOrEmpty(names)) ? "0" : names;
                                    double intpurchasePrice2 = Convert.ToDouble(names);
                                    totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                                }

                            }

                            double x, y, z;
                            double w;
                            double.TryParse(billSumBeforeTextBox.Text, out z);
                            double.TryParse(billDiscountRateTextBox.Text, out x);
                            double.TryParse(billDiscountAmountTextBox.Text, out y);

                            w = Convert.ToDouble(totalProfitSum - ((x / 100) * z) - y);
                            if (w >= 0)
                            {
                                profitTextBox.Text = w.ToString("G29");
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


                    if (updatePriceRadioButton.Checked)
                    {
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();

                        if (unitBoolString == "0")
                        {
                            unitBool = false;
                        }
                        else
                        {
                            unitBool = true;
                        }

                        if (!unitBool) {
                            if (soloaymentRadioButton.Checked)
                            {
                                string Query = "UPDATE categoryTable SET sellingPrice=N'" + this.sellingPriceTextBox.Text + "' where Id=N'" + this.categoryCodeTextBox.Text + "'";
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
                            else 
                            {
                                string Query = "UPDATE categoryTable SET packagePrice=N'" + this.sellingPriceTextBox.Text + "' where Id=N'" + this.categoryCodeTextBox.Text + "'";
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

                        }
                        else if(unitBool)
                        {
                            if (soloaymentRadioButton.Checked)
                            {
                                string Query = "UPDATE SubUnitTable SET sellingPrice=N'" + this.sellingPriceTextBox.Text + "' where Id=N'" + this.categoryCodeTextBox.Text + "'";
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
                            else 
                            {
                                string Query = "UPDATE SubUnitTable SET packagePrice=N'" + this.sellingPriceTextBox.Text + "' where Id=N'" + this.categoryCodeTextBox.Text + "'";
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
                        }
                            fillCategoryDGV();
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
                else
                {
                    MessageBox.Show("سعر البيع أقل أو يساوي سعر الشراء");
                }
            }
            else
            {

                categoryCodeTextBox.Text = (string.IsNullOrEmpty(categoryCodeTextBox.Text)) ? "0" : categoryCodeTextBox.Text;
                quantityTextBox.Text = (string.IsNullOrEmpty(quantityTextBox.Text)) ? "0" : quantityTextBox.Text;
                sellingPriceTextBox.Text = (string.IsNullOrEmpty(sellingPriceTextBox.Text)) ? "0" : sellingPriceTextBox.Text;
                categoryDiscountRateTextBox.Text = (string.IsNullOrEmpty(categoryDiscountRateTextBox.Text)) ? "0" : categoryDiscountRateTextBox.Text;
                categoryDiscountAmountTextBox.Text = (string.IsNullOrEmpty(categoryDiscountAmountTextBox.Text)) ? "0" : categoryDiscountAmountTextBox.Text;
                categorySumTextBox.Text = (string.IsNullOrEmpty(categorySumTextBox.Text)) ? "0" : categorySumTextBox.Text;

                string categoryCode = categoryCodeTextBox.Text;
                string categoryName = categoryNameTextBox.Text;
                string unit = unitComboBox.Text;
                string quantity = quantityTextBox.Text;
                string buyingPrice = sellingPriceTextBox.Text;
                string discountRate = categoryDiscountRateTextBox.Text;
                string discountAmount = categoryDiscountAmountTextBox.Text;
                string categorySum = categorySumTextBox.Text;

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string purchasingPrice = new SqlCommand("IF EXISTS(select 1 from subUnitTable where Id=N'" + categoryCode + "') BEGIN select buyingPrice from subUnitTable where Id=N'" + categoryCode + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                purchasingPrice = (string.IsNullOrEmpty(purchasingPrice)) ? "0" : purchasingPrice;
                double intpurchasePrice = Convert.ToDouble(purchasingPrice);

                if (intpurchasePrice < Convert.ToDouble(buyingPrice))
                {
                    bool rowExist = false;

                    for (int j = 0; j <= billDGV.Rows.Count - 1; j++)
                    {
                        if (categoryCodeTextBox.Text == Convert.ToString(billDGV.Rows[j].Cells[0].Value) &&
                            categoryNameTextBox.Text == Convert.ToString(billDGV.Rows[j].Cells[1].Value) &&
                            unitComboBox.Text == Convert.ToString(billDGV.Rows[j].Cells[2].Value) &&
                          sellingPriceTextBox.Text == Convert.ToString(billDGV.Rows[j].Cells[4].Value) &&
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
                            profitTextBox.Text = "0";

                            double totalProfitSum = 0;
                            for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
                            {
                                billSumBeforeTextBox.Text = Convert.ToString(Convert.ToDouble(billSumBeforeTextBox.Text) + Convert.ToDouble(billDGV.Rows[i].Cells[7].Value));

                                string buyingPrice2 = (string.IsNullOrEmpty(this.billDGV.Rows[i].Cells[0].Value.ToString()) ? "0" : this.billDGV.Rows[i].Cells[0].Value.ToString());


                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                                conDataBase.Close();

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
                                    conDataBase.Open();
                                    string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from categoryTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                                    conDataBase.Close();
                                    names = (string.IsNullOrEmpty(names)) ? "0" : names;
                                    double intpurchasePrice2 = Convert.ToDouble(names);
                                    totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                                }
                                else
                                {
                                    conDataBase.Open();
                                    string names = new SqlCommand("IF EXISTS(select 1 from subUnitTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                                    conDataBase.Close();
                                    names = (string.IsNullOrEmpty(names)) ? "0" : names;
                                    double intpurchasePrice2 = Convert.ToDouble(names);
                                    totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                                }
                            }

                            double x, y, z;
                            double w;
                            double.TryParse(billSumBeforeTextBox.Text, out z);
                            double.TryParse(billDiscountRateTextBox.Text, out x);
                            double.TryParse(billDiscountAmountTextBox.Text, out y);

                            w = Convert.ToDouble(totalProfitSum - ((x / 100) * z) - y);
                            if (w >= 0)
                            {
                                profitTextBox.Text = w.ToString("G29");
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
                        if (soloRadioButton.Checked)
                        {
                            string Query = "UPDATE subUnitTable SET sellingPrice=N'" + this.sellingPriceTextBox.Text + "' where Id=N'" + this.categoryCodeTextBox.Text + "'";
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
                            string Query = "UPDATE subUnitTable SET packagePrice=N'" + this.sellingPriceTextBox.Text + "' where Id=N'" + this.categoryCodeTextBox.Text + "'";
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
                else
                {
                    MessageBox.Show("سعر البيع أقل أو يساوي سعر الشراء");
                }
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
            updateLabel.Text = "سيتم تحديث سعر القطاعي";
        }

        private void packageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            buyingType = "جملة";
            updateLabel.Text = "سيتم تحديث سعر الجملة";
        }

        private void newPurchasesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            clear();
            clearAll();
            billType = "جديدة";
            returnBillButton.Visible = false;
            salesCodeTextBox.BackColor = Color.FromArgb(64, 64, 64);
            salesCodeTextBox.ReadOnly = true;
            fill();
        }

        private void adjustPurhasesRadioButton_CheckedChanged(object sender, EventArgs e)
        {

            billType = "تعديل";
            returnBillButton.Visible = true;
            salesCodeTextBox.BackColor = Color.FromArgb(41, 44, 51);
            salesCodeTextBox.ReadOnly = false;
            salesCodeTextBox.Text = "";
        }

        private void constantPriceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            priceUpdate = "تثبيت";
        }

        private void updatePriceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            priceUpdate = "تحديث";
        }

        private void ordinaryBillRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            divideBill = "عادية";
            divideGroupBox.Visible = false;
            saveButton.Text = "حفظ";
            detailedDebtGroupBox.Visible = false;
        }

        private void dividedBillRadioButoon_CheckedChanged(object sender, EventArgs e)
        {
            divideBill = "تقسيط";
            divideGroupBox.Visible = true;
            saveButton.Text = "التالي";
           
        }

        private void restRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            divideDetails = "الباقي";
            divisionFunction();

        }

        private void reductionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            divideDetails = "الإجمالي";
            divisionFunction();
        }

        void sumFunction()
        {
            categorySumTextBox.ResetText();
            double a, b, d, e;
            double c;
            double.TryParse(quantityTextBox.Text, out a);
            double.TryParse(sellingPriceTextBox.Text, out b);
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

        private void sellingPriceTextBox_TextChanged(object sender, EventArgs e)
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

        private void billDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                billSumBeforeTextBox.Text = "0";
                profitTextBox.Text = "0";

                double totalProfitSum = 0;
                for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
                {
                    billSumBeforeTextBox.Text = Convert.ToString(Convert.ToDouble(billSumBeforeTextBox.Text) + Convert.ToDouble(billDGV.Rows[i].Cells[7].Value));

                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();

                    string buyingPrice2 = (string.IsNullOrEmpty(this.billDGV.Rows[i].Cells[0].Value.ToString()) ? "0" : this.billDGV.Rows[i].Cells[0].Value.ToString());


                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                  string  unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
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
                        conDataBase.Open();
                        string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from categoryTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        names = (string.IsNullOrEmpty(names)) ? "0" : names;
                        double intpurchasePrice2 = Convert.ToDouble(names);
                        totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                    }
                    else
                    {
                        conDataBase.Open();
                        string names = new SqlCommand("IF EXISTS(select 1 from subUnitTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        names = (string.IsNullOrEmpty(names)) ? "0" : names;
                        double intpurchasePrice2 = Convert.ToDouble(names);
                        totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                    }
                }

                double a, b, d;
                double c;
                double.TryParse(billSumBeforeTextBox.Text, out d);
                double.TryParse(billDiscountRateTextBox.Text, out a);
                double.TryParse(billDiscountAmountTextBox.Text, out b);

                c = Convert.ToDouble(totalProfitSum - ((a / 100) * d) - b);
                if (c >= 0)
                {
                    profitTextBox.Text = c.ToString("G29");
                }
            }
            catch 
            {
            }

        }
    
        private void billDGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                billSumBeforeTextBox.Text = "0";
                profitTextBox.Text = "0";


                double totalProfitSum = 0;
                for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
                {
                    billSumBeforeTextBox.Text = Convert.ToString(Convert.ToDouble(billSumBeforeTextBox.Text) + Convert.ToDouble(billDGV.Rows[i].Cells[7].Value));

                    SqlConnection conDataBase = new SqlConnection(constring);
                    string buyingPrice2 = (string.IsNullOrEmpty(this.billDGV.Rows[i].Cells[0].Value.ToString()) ? "0" : this.billDGV.Rows[i].Cells[0].Value.ToString());


                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
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
                        conDataBase.Open();
                        string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from categoryTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        names = (string.IsNullOrEmpty(names)) ? "0" : names;
                        double intpurchasePrice2 = Convert.ToDouble(names);
                        totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                    }
                    else
                    {
                        conDataBase.Open();
                        string names = new SqlCommand("IF EXISTS(select 1 from subUnitTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        names = (string.IsNullOrEmpty(names)) ? "0" : names;
                        double intpurchasePrice2 = Convert.ToDouble(names);
                        totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                    }
                }

                double a, b, d;
                double c;
                double.TryParse(billSumBeforeTextBox.Text, out d);
                double.TryParse(billDiscountRateTextBox.Text, out a);
                double.TryParse(billDiscountAmountTextBox.Text, out b);

                c = Convert.ToDouble(totalProfitSum - ((a / 100) * d) - b);
                if (c >= 0)
                {
                    profitTextBox.Text = c.ToString("G29");
                }
            }
            catch
            {

            }
        }

        void billSumFunction()
        {
            try
            {
                billSumAfterTextBox.ResetText();
                double a, b, d, g, f, i, j;
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

        private void ratioTextBox_TextChanged(object sender, EventArgs e)
        {
            divisionFunction();
        }

        private void billSumBeforeTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
        }

        private void billDiscountRateTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
            try {
                profitTextBox.Text = "0";
                double totalProfitSum = 0;
                for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
                {

                    string buyingPrice2 = (string.IsNullOrEmpty(this.billDGV.Rows[i].Cells[0].Value.ToString()) ? "0" : this.billDGV.Rows[i].Cells[0].Value.ToString());


                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
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
                        conDataBase.Open();
                        string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from categoryTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        names = (string.IsNullOrEmpty(names)) ? "0" : names;
                        double intpurchasePrice2 = Convert.ToDouble(names);
                        totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                    }
                    else
                    {
                        conDataBase.Open();
                        string names = new SqlCommand("IF EXISTS(select 1 from subUnitTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        names = (string.IsNullOrEmpty(names)) ? "0" : names;
                        double intpurchasePrice2 = Convert.ToDouble(names);
                        totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                    }
                }

                double a, b, d;
                    double c;
                    double.TryParse(billSumBeforeTextBox.Text, out d);
                    double.TryParse(billDiscountRateTextBox.Text, out a);
                    double.TryParse(billDiscountAmountTextBox.Text, out b);

                    c = Convert.ToDouble(totalProfitSum - ((a / 100) * d) - b);
                    if (c >= 0)
                    {
                        profitTextBox.Text = c.ToString("G29");
                    }
                }
               
            catch
            {

            }
        }

        private void billDiscountAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
            try { 
            profitTextBox.Text = "0";
            double totalProfitSum = 0;
            for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
            {
                    string buyingPrice2 = (string.IsNullOrEmpty(this.billDGV.Rows[i].Cells[0].Value.ToString()) ? "0" : this.billDGV.Rows[i].Cells[0].Value.ToString());


                  SqlConnection  conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string unitBoolString = new SqlCommand("IF EXISTS(Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' ) BEGIN Select 1 from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
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
                        conDataBase.Open();
                        string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from categoryTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        names = (string.IsNullOrEmpty(names)) ? "0" : names;
                        double intpurchasePrice2 = Convert.ToDouble(names);
                        totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                    }
                    else
                    {
                        conDataBase.Open();
                        string names = new SqlCommand("IF EXISTS(select 1 from subUnitTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from subUnitTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        names = (string.IsNullOrEmpty(names)) ? "0" : names;
                        double intpurchasePrice2 = Convert.ToDouble(names);
                        totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
                    }
                }

                double a, b, d;
                double c;
                double.TryParse(billSumBeforeTextBox.Text, out d);
                double.TryParse(billDiscountRateTextBox.Text, out a);
                double.TryParse(billDiscountAmountTextBox.Text, out b);

                c = Convert.ToDouble(totalProfitSum - ((a / 100) * d) - b);
                if (c >= 0)
                {
                    profitTextBox.Text = c.ToString("G29");
                }
            }
            catch
            {

            }
        }

        private void billSalesTaxTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
        }

        private void billTransportTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
        }

        private void billSumAfterTextBox_TextChanged(object sender, EventArgs e)
        {
            try
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
                divisionFunction();

            }
            catch
            {
            }
        }

        void divisionFunction()
        {
            try
            {
                if (restRadioButton.Checked)
                {
                    double a, b, d;
                    double c;
                    double.TryParse(billSumAfterTextBox.Text, out a);
                    double.TryParse(paidTextBox.Text, out b);
                    double.TryParse(ratioTextBox.Text, out d);

                    c = Convert.ToDouble((a - b) + (a - b) * (d / 100));
                    if (c >= 0)
                    {
                        restTextBox.Text = c.ToString("G29");
                    }
                }
                else if (reductionRadioButton.Checked)
                {
                    double a, b, d;
                    double c;
                    double.TryParse(billSumAfterTextBox.Text, out a);
                    double.TryParse(paidTextBox.Text, out b);
                    double.TryParse(ratioTextBox.Text, out d);

                    c = Convert.ToDouble((a) * (d / 100) + (a - b));
                    if (c >= 0)
                    {
                        restTextBox.Text = c.ToString("G29");
                    }
                }
            }
            catch
            {

            }
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
                divisionFunction();

            }

            catch
            {
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

          
            if (ordinaryBillRadioButton.Checked)
            {
                billSumBeforeTextBox.Text = (string.IsNullOrEmpty(billSumBeforeTextBox.Text)) ? "0" : billSumBeforeTextBox.Text;
                billDiscountRateTextBox.Text = (string.IsNullOrEmpty(billDiscountRateTextBox.Text)) ? "0" : billDiscountRateTextBox.Text;
                billDiscountAmountTextBox.Text = (string.IsNullOrEmpty(billDiscountAmountTextBox.Text)) ? "0" : billDiscountAmountTextBox.Text;
                billTransportTextBox.Text = (string.IsNullOrEmpty(billTransportTextBox.Text)) ? "0" : billTransportTextBox.Text;
                billSumAfterTextBox.Text = (string.IsNullOrEmpty(billSumAfterTextBox.Text)) ? "0" : billSumAfterTextBox.Text;
                paidTextBox.Text = (string.IsNullOrEmpty(paidTextBox.Text)) ? "0" : paidTextBox.Text;
                restTextBox.Text = (string.IsNullOrEmpty(restTextBox.Text)) ? "0" : restTextBox.Text;
                profitTextBox.Text = (string.IsNullOrEmpty(profitTextBox.Text)) ? "0" : profitTextBox.Text;

                string balanceTableId ="";
                string storeNameForEdit = "";
                string oldSupplierName = "";
                string balanceTableIdForEdit = "";
                string oldRest = "";
                string oldSafeTransactionID = "";
               
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string stringReturned = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select returned FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
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
                        storeNameForEdit = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select storeName FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        balanceTableIdForEdit = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select balanceTableNumber FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();


                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        oldSupplierName = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select customerName FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();

                        //rest to subtract it if adjust
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        oldRest = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select rest FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();

                        //old safe ID 
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        oldSafeTransactionID = new SqlCommand("IF EXISTS (select 1 FROM safeTable where billNo = N'" + salesCodeTextBox.Text + "' and type='sales' and details ='in') BEGIN select Id FROM safeTable where billNo = N'" + salesCodeTextBox.Text + "' and type='sales' and details ='in' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                    }
                    else
                    {
                        MessageBox.Show("لا يمكن تعديل فاتورة بها مرتجع");
                    }
                }


                try
                {
                   string Query = "IF NOT EXISTS (select 1 FROM salesMainTable where Id= N'" + this.salesCodeTextBox.Text + "') BEGIN INSERT INTO salesMainTable(paymentType,storeName,customerName,safeName,buyingType,sumBefore,discountPercentage,discountAmount,salesTax,transport,sumAfter,paid,rest,profit,date,returned,debts) VALUES (N'" + paymentType + "',N'" + this.storeNameComboBox.Text + "',N'" + this.customerNameComboBox.Text + "',N'" + this.safeComboBox.Text + "',N'" + buyingType + "',N'" + this.billSumBeforeTextBox.Text + "',N'" + this.billDiscountRateTextBox.Text + "',N'" + this.billDiscountAmountTextBox.Text + "',N'" + this.billSalesTaxTextBox.Text + "',N'" + this.billTransportTextBox.Text + "',N'" + this.billSumAfterTextBox.Text + "',N'" + this.paidTextBox.Text + "',N'" + this.restTextBox.Text + "',N'"+this.profitTextBox.Text+"',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','FALSE','FALSE') END ELSE BEGIN UPDATE salesMainTable SET paymentType=N'" + paymentType + "',storeName =N'" + this.storeNameComboBox.Text + "',customerName=N'" + this.customerNameComboBox.Text + "',safeName=N'" + this.safeComboBox.Text + "',buyingType=N'" + buyingType + "',sumBefore=N'" + this.billSumBeforeTextBox.Text + "',discountPercentage=N'" + this.billDiscountRateTextBox.Text + "',discountAmount=N'" + this.billDiscountAmountTextBox.Text + "',salesTax=N'" + this.billSalesTaxTextBox.Text + "',transport=N'" + this.billTransportTextBox.Text + "',sumAfter=N'" + this.billSumAfterTextBox.Text + "',paid=N'" + this.paidTextBox.Text + "',rest=N'" + this.restTextBox.Text + "',profit=N'"+this.profitTextBox.Text+"', date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' where Id=N'" + this.salesCodeTextBox.Text + "' and returned='False' END ";
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

                        double customerBalance = 0;
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string customerBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.customerNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.customerNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                        if (customerBalanceString != "")
                        {
                            customerBalance = Convert.ToDouble(customerBalanceString);
                            conDataBase.Close();
                        }

                        
                        Query = "INSERT INTO customerBalanceTable(customerNumber,billNumber,billDetails,direction,safeName,amount,date,inside) VALUES (N'" + this.customerCodeTextBox.Text + "',N'" + this.salesCodeTextBox.Text + "',N'فاتورة مبيعات',N'من العميل',N'" + this.safeComboBox.Text + "',N'" + this.paidTextBox.Text + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','False')";
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
                         balanceTableId = new SqlCommand("IF EXISTS (select 1 FROM customerBalanceTable where customerNumber= N'" + this.customerCodeTextBox.Text + "' and billNumber =N'" + this.salesCodeTextBox.Text + "' and billDetails =N'فاتورة مبيعات'  and direction=N'من العميل' and safeName=N'" + this.safeComboBox.Text + "' and amount =N'" + this.paidTextBox.Text + "' and date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "'  and inside ='False') BEGIN select Id FROM customerBalanceTable where customerNumber= N'" + this.customerCodeTextBox.Text + "' and billNumber =N'" + this.salesCodeTextBox.Text + "' and billDetails =N'فاتورة مبيعات'  and direction=N'من العميل' and safeName=N'" + this.safeComboBox.Text + "' and amount =N'" + this.paidTextBox.Text + "' and date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "'  and inside ='False' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();


                        customerBalance += Convert.ToDouble(this.restTextBox.Text);

                        Query = "IF EXISTS (select 1 from customerTable where name=N'" + this.customerNameComboBox.Text + "')BEGIN UPDATE customerTable SET balance=N'" + customerBalance + "' where name=N'" + this.customerNameComboBox.Text + "' END";
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
                        Query = "Update salesMainTable Set BalanceTableNumber =N'"+ balanceTableId + "'  where Id =N'" + this.salesCodeTextBox.Text + "' ";
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

                        Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'"+this.safeComboBox.Text+ "' ,'',N'" + this.paidTextBox.Text + "','sales',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.salesCodeTextBox.Text + "','notDivided',N'" + this.customerCodeTextBox.Text + "')";
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


                        for (int i = 0; i < billDGV.Rows.Count; i++)
                        {

                            Query = "INSERT INTO salesSubTable(billCode,categoryCode,unit,quantity,purchasePrice,discountRate,discountAmount,sum) VALUES (N'" + Int32.Parse(this.salesCodeTextBox.Text) + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[3].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[4].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[5].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[6].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[7].Value.ToString() + "')";
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
                                    quantityInt -= Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString());

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

                                    quantityInt -= (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) * ratio);

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

                                quantityInt -= (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) / ratio);

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
                        if (returned == false)
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

                            Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.paidTextBox.Text + "','sales',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.salesCodeTextBox.Text + "','notDivided',N'" + this.customerCodeTextBox.Text + "')";
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


                            Query = "DELETE FROM customerBalanceTable where Id=N'" + balanceTableIdForEdit + "';";
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

                            double supplierBalance = 0;
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + oldSupplierName + "') BEGIN SELECT balance from customerTable where name=N'" + oldSupplierName + "' END", conDataBase).ExecuteScalar().ToString();

                            if (supplierBalanceString != "")
                            {
                                supplierBalance = Convert.ToDouble(supplierBalanceString);
                                conDataBase.Close();
                            }

                            supplierBalance -= Convert.ToDouble(oldRest);

                            Query = "IF EXISTS (select 1 from customerTable where name=N'" + oldSupplierName + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + oldSupplierName + "' END";
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

                            Query = "DELETE FROM salesSubTable where billCode=N'" + this.salesCodeTextBox.Text + "';";
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

                            for (int i = 0; i < billDGV.Rows.Count; i++)
                            {

                                Query = "INSERT INTO salesSubTable(billCode,categoryCode,unit,quantity,purchasePrice,discountRate,discountAmount,sum) VALUES (N'" + Int32.Parse(this.salesCodeTextBox.Text) + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[3].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[4].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[5].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[6].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[7].Value.ToString() + "')";
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
                                        quantityInt -= Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString());

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

                                        quantityInt -= (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) * ratio);

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

                                    quantityInt -= (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) / ratio);

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
                    }
                    if (soloaymentRadioButton.Checked)
                    {
                        MessageBox.Show("حُفظ بأسعار قطاعي");
                    }
                    else
                    {
                        MessageBox.Show("حُفظ بأسعار الجملة");
                    }
                    fill();
                    fillCategoryDGV();
                    newPurchasesRadioButton.Checked = true;

                    clearAll();
                }
                catch (Exception ex)
                {
                     

                }

            }
            else if (dividedBillRadioButoon.Checked)
            {
                detailedDebtGroupBox.Visible = true;
                detailedDebtGroupBox.BringToFront();
                totalBillDebtTextBox.Text = billSumAfterTextBox.Text;
                paidDebtTextBox.Text = paidTextBox.Text;
                restDebtTextBox.Text = restTextBox.Text;
                addCategoryButton.Visible = false;
                if (adjustPurhasesRadioButton.Checked)
                {
                    DebtsNumberTextBox.Text = (Convert.ToInt32(DebtsNumberTextBox.Text)+1).ToString();
                    DebtsNumberTextBox.Text = (Convert.ToInt32(DebtsNumberTextBox.Text)-1).ToString();

                    debtGrid.Rows.Clear();
                    int row = 0;
                    DateTime debtDate = this.startDebtDate.Value.Date;

                    for (int i = 1; i <= (Convert.ToInt32(DebtsNumberTextBox.Text)); i++)
                    {
                        debtGrid.Rows.Add();
                        row = debtGrid.Rows.Count - 1;


                        debtGrid["debtNoColumn", row].Value = i.ToString();
                        debtGrid["debtAmountColumn", row].Value = debtAmountTextBox.Text;
                        debtGrid["debtDateColumn", row].Value = debtDate.ToShortDateString();

                        if (dayRadioButton.Checked)
                        {
                            debtDate = debtDate.AddDays(Convert.ToInt32(debtTimeValueTextBox.Text));
                        }
                        else if (monthRadioButton.Checked)
                        {
                            debtDate = debtDate.AddMonths(Convert.ToInt32(debtTimeValueTextBox.Text));
                        }
                    }
                    startDebtDateDGV.Text = this.startDebtDate.Value.Date.ToShortDateString();
                    endDebtDateDGV.Text = debtGrid["debtDateColumn", row].Value.ToString();
                }
            }
            this.ActiveControl = categoryCodeTextBox;
        }


        private void savedDivisionButton_Click(object sender, EventArgs e)
        {
            billSumBeforeTextBox.Text = (string.IsNullOrEmpty(billSumBeforeTextBox.Text)) ? "0" : billSumBeforeTextBox.Text;
            billDiscountRateTextBox.Text = (string.IsNullOrEmpty(billDiscountRateTextBox.Text)) ? "0" : billDiscountRateTextBox.Text;
            billDiscountAmountTextBox.Text = (string.IsNullOrEmpty(billDiscountAmountTextBox.Text)) ? "0" : billDiscountAmountTextBox.Text;
            billTransportTextBox.Text = (string.IsNullOrEmpty(billTransportTextBox.Text)) ? "0" : billTransportTextBox.Text;
            billSumAfterTextBox.Text = (string.IsNullOrEmpty(billSumAfterTextBox.Text)) ? "0" : billSumAfterTextBox.Text;
            paidTextBox.Text = (string.IsNullOrEmpty(paidTextBox.Text)) ? "0" : paidTextBox.Text;
            restTextBox.Text = (string.IsNullOrEmpty(restTextBox.Text)) ? "0" : restTextBox.Text;
            ratioTextBox.Text = (string.IsNullOrEmpty(ratioTextBox.Text)) ? "0" : ratioTextBox.Text;
            profitTextBox.Text = (string.IsNullOrEmpty(profitTextBox.Text)) ? "0" : profitTextBox.Text;

            string storeNameForEdit = "";
            string balanceTableIdForEdit = "";
            string balanceTableId = "";
            string oldSupplierName = "";
            string oldRest = "";
            string oldDebtId = "";
            string oldSafeTransactionID = "";

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string stringReturned = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select returned FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
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
                    balanceTableIdForEdit = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select balanceTableNumber FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    storeNameForEdit = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select storeName FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    oldSupplierName = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select customerName FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //rest to subtract it if adjust
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    oldRest = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select rest FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //old debts ID 
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    oldDebtId = new SqlCommand("IF EXISTS (select 1 FROM debtsMainTable where billNumber = N'" + salesCodeTextBox.Text + "') BEGIN select Id FROM debtsMainTable where billNumber = N'" + salesCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    
                    //old safe ID 
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    oldSafeTransactionID = new SqlCommand("IF EXISTS (select 1 FROM safeTable where billNo = N'" + salesCodeTextBox.Text + "' and type='sales' and details ='in') BEGIN select Id FROM safeTable where billNo = N'" + salesCodeTextBox.Text + "' and type='sales' and details ='in' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                }
                else
                {
                    MessageBox.Show("لا يمكن تعديل فاتورة بها مرتجع");
                }
            }


            try
            {
                string Query = "IF NOT EXISTS (select 1 FROM salesMainTable where Id= N'" + this.salesCodeTextBox.Text + "') BEGIN INSERT INTO salesMainTable(paymentType,storeName,customerName,safeName,buyingType,sumBefore,discountPercentage,discountAmount,salesTax,transport,sumAfter,paid,rest,profit,date,returned,debts,debtsRatio,debtsType) VALUES (N'" + paymentType + "',N'" + this.storeNameComboBox.Text + "',N'" + this.customerNameComboBox.Text + "',N'" + this.safeComboBox.Text + "',N'" + buyingType + "',N'" + this.billSumBeforeTextBox.Text + "',N'" + this.billDiscountRateTextBox.Text + "',N'" + this.billDiscountAmountTextBox.Text + "',N'" + this.billSalesTaxTextBox.Text + "',N'" + this.billTransportTextBox.Text + "',N'" + this.billSumAfterTextBox.Text + "',N'" + this.paidTextBox.Text + "',N'" + this.restTextBox.Text + "',N'" + this.profitTextBox.Text + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','FALSE','TRUE',N'" + this.ratioTextBox.Text + "',N'" + divideDetails + "') END ELSE BEGIN UPDATE salesMainTable SET paymentType=N'" + paymentType + "',storeName =N'" + this.storeNameComboBox.Text + "',customerName=N'" + this.customerNameComboBox.Text + "',safeName=N'" + this.safeComboBox.Text + "',buyingType=N'" + buyingType + "',sumBefore=N'" + this.billSumBeforeTextBox.Text + "',discountPercentage=N'" + this.billDiscountRateTextBox.Text + "',discountAmount=N'" + this.billDiscountAmountTextBox.Text + "',salesTax=N'" + this.billSalesTaxTextBox.Text + "',transport=N'" + this.billTransportTextBox.Text + "',sumAfter=N'" + this.billSumAfterTextBox.Text + "',paid=N'" + this.paidTextBox.Text + "',rest=N'" + this.restTextBox.Text + "',profit=N'" + this.profitTextBox.Text + "', date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "',debtsRatio=N'" + this.ratioTextBox.Text + "',debtsType=N'" + divideDetails + "' where Id=N'" + this.salesCodeTextBox.Text + "' and returned='False' END ";
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

                    double customerBalance = 0;
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string customerBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.customerNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.customerNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                    if (customerBalanceString != "")
                    {
                        customerBalance = Convert.ToDouble(customerBalanceString);
                        conDataBase.Close();
                    }

                    Query = "INSERT INTO customerBalanceTable(customerNumber,billNumber,billDetails,direction,safeName,amount,date,inside) VALUES (N'" + this.customerCodeTextBox.Text + "',N'" + this.salesCodeTextBox.Text + "',N'فاتورة مبيعات',N'من العميل',N'" + this.safeComboBox.Text + "',N'" + this.paidTextBox.Text + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','False')";
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
                    balanceTableId = new SqlCommand("IF EXISTS (select 1 FROM customerBalanceTable where customerNumber= N'" + this.customerCodeTextBox.Text + "' and billNumber =N'" + this.salesCodeTextBox.Text + "' and billDetails =N'فاتورة مبيعات'  and direction=N'من العميل' and safeName=N'" + this.safeComboBox.Text + "' and amount =N'" + this.paidTextBox.Text + "' and date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "'  and inside ='False') BEGIN select Id FROM customerBalanceTable where customerNumber= N'" + this.customerCodeTextBox.Text + "' and billNumber =N'" + this.salesCodeTextBox.Text + "' and billDetails =N'فاتورة مبيعات'  and direction=N'من العميل' and safeName=N'" + this.safeComboBox.Text + "' and amount =N'" + this.paidTextBox.Text + "' and date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "'  and inside ='False' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    customerBalance += Convert.ToDouble(this.restTextBox.Text);

                    Query = "IF EXISTS (select 1 from customerTable where name=N'" + this.customerNameComboBox.Text + "') BEGIN UPDATE customerTable SET balance=N'" + customerBalance + "' where name=N'" + this.customerNameComboBox.Text + "' END";
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

                    Query = "Update salesMainTable Set BalanceTableNumber =N'" + balanceTableId + "' where Id =N'" + this.salesCodeTextBox.Text + "' ";
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

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.paidTextBox.Text + "','sales',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.salesCodeTextBox.Text + "','Divided',N'" + this.customerCodeTextBox.Text + "')";
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

                    for (int i = 0; i < billDGV.Rows.Count; i++)
                    {

                        Query = "INSERT INTO salesSubTable(billCode,categoryCode,unit,quantity,purchasePrice,discountRate,discountAmount,sum) VALUES (N'" + Int32.Parse(this.salesCodeTextBox.Text) + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[3].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[4].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[5].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[6].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[7].Value.ToString() + "')";
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
                                quantityInt -= Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString());

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

                                quantityInt -= (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) * ratio);

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

                            quantityInt -= (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) / ratio);

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
                    //debts
                    Query = "INSERT INTO debtsMainTable (sum,first,paid,rest,debtsNumber,debtAmount,time,timeType,billNumber,startDate) VALUES (N'" + this.totalBillDebtTextBox.Text + "',N'" + this.paidDebtTextBox.Text + "','0',N'" + this.restDebtTextBox.Text + "',N'" + this.DebtsNumberTextBox.Text + "',N'" + this.debtAmountTextBox.Text + "',N'" + this.debtTimeValueTextBox.Text + "',N'" + debtTime + "',N'" + this.salesCodeTextBox.Text + "',N'" + this.startDebtDate.Value.ToString("MM/dd/yyyy") + "') ;";
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
                    string debtId = new SqlCommand("SELECT Id FROM debtsMainTable WHERE sum = N'" + this.totalBillDebtTextBox.Text + "' AND first = N'" + this.paidDebtTextBox.Text + "' AND paid = '0' AND rest = N'" + this.restDebtTextBox.Text + "' AND debtsNumber = N'" + this.DebtsNumberTextBox.Text + "' AND debtAmount = N'" + this.debtAmountTextBox.Text + "'AND time = N'" + this.debtTimeValueTextBox.Text + "' AND timeType = N'" + debtTime + "' AND billNumber = N'" + this.salesCodeTextBox.Text + "'", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();


                    for (int i = 0; i < debtGrid.Rows.Count; i++)
                    {
                        Query = "INSERT INTO debtsTable (debtOrder,debtAmount,debtDate,status,paidDate,paidAmount,debtMainTableNumber) VALUES (N'" + this.debtGrid.Rows[i].Cells[0].Value + "',N'" + this.debtGrid.Rows[i].Cells[1].Value + "',N'" + this.debtGrid.Rows[i].Cells[2].Value + "','False',GETDATE(),'0',N'" + debtId + "') ;";
                        conDataBase = new SqlConnection(constring);
                        cmdDataBase = new SqlCommand(Query, conDataBase);

                        try
                        {
                            conDataBase.Open();
                            cmdDataBase.ExecuteReader();
                            conDataBase.Close();

                        }
                        catch (Exception ex)
                        {
                             
                        }
                    }

                }

                else if (billType == "تعديل")
                {
                    if (returned == false)
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

                        Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.paidTextBox.Text + "','sales',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.salesCodeTextBox.Text + "','Divided',N'" + this.customerCodeTextBox.Text + "')";
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

                        Query = "DELETE FROM customerBalanceTable where Id=N'" + balanceTableIdForEdit + "';";
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

                        Query = "DELETE FROM salesSubTable where billCode=N'" + this.salesCodeTextBox.Text + "';";
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

                        Query = "DELETE FROM debtsMainTable where billNumber=N'" + this.salesCodeTextBox.Text + "';";
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

                        Query = "DELETE FROM debtsTable where debtMainTableNumber =N'" + oldDebtId + "';";
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

                        double supplierBalance = 0;
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + oldSupplierName + "') BEGIN SELECT balance from customerTable where name=N'" + oldSupplierName + "' END", conDataBase).ExecuteScalar().ToString();

                        if (supplierBalanceString != "")
                        {
                            supplierBalance = Convert.ToDouble(supplierBalanceString);
                            conDataBase.Close();
                        }

                        supplierBalance -= Convert.ToDouble(oldRest);

                        Query = "IF EXISTS (select 1 from customerTable where name=N'" + oldSupplierName + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + oldSupplierName + "' END";
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

                        for (int i = 0; i < editDGV.Rows.Count; i++)
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

                                if (mainUnit == this.editDGV.Rows[i].Cells[2].Value.ToString())
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
                                float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where Id=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' and  subUnit= N'" + this.editDGV.Rows[i].Cells[2].Value.ToString() + "' ", conDataBase).ExecuteScalar().ToString());
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

                        for (int i = 0; i < billDGV.Rows.Count; i++)
                        {

                            Query = "INSERT INTO salesSubTable(billCode,categoryCode,unit,quantity,purchasePrice,discountRate,discountAmount,sum) VALUES (N'" + Int32.Parse(this.salesCodeTextBox.Text) + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[3].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[4].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[5].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[6].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[7].Value.ToString() + "')";
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
                                    quantityInt -= Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString());

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

                                    quantityInt -= (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) * ratio);

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

                                quantityInt -= (Convert.ToDouble(this.billDGV.Rows[i].Cells[3].Value.ToString()) / ratio);

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

                        //debts
                        Query = "INSERT INTO debtsMainTable (sum,first,paid,rest,debtsNumber,debtAmount,time,timeType,billNumber,startDate) VALUES (N'" + this.totalBillDebtTextBox.Text + "',N'" + this.paidDebtTextBox.Text + "','0',N'" + this.restDebtTextBox.Text + "',N'" + this.DebtsNumberTextBox.Text + "',N'" + this.debtAmountTextBox.Text + "',N'" + this.debtTimeValueTextBox.Text + "',N'" + debtTime + "',N'" + this.salesCodeTextBox.Text + "',N'" + this.startDebtDate.Value.ToString("MM/dd/yyyy") + "') ;";
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
                        string debtId = new SqlCommand("SELECT Id FROM debtsMainTable WHERE sum = N'" + this.totalBillDebtTextBox.Text + "' AND first = N'" + this.paidDebtTextBox.Text + "' AND paid = '0' AND rest = N'" + this.restDebtTextBox.Text + "' AND debtsNumber = N'" + this.DebtsNumberTextBox.Text + "' AND debtAmount = N'" + this.debtAmountTextBox.Text + "'AND time = N'" + this.debtTimeValueTextBox.Text + "' AND timeType = N'" + debtTime + "' AND billNumber = N'" + this.salesCodeTextBox.Text + "'", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();

                        for (int i = 0; i < debtGrid.Rows.Count; i++)
                        {
                            Query = "INSERT INTO debtsTable (debtOrder,debtAmount,debtDate,status,paidDate,paidAmount,debtMainTableNumber) VALUES (N'" + this.debtGrid.Rows[i].Cells[0].Value + "',N'" + this.debtGrid.Rows[i].Cells[1].Value + "',N'" + this.debtGrid.Rows[i].Cells[2].Value + "','False',GETDATE(),'0',N'" + debtId + "') ;";
                            conDataBase = new SqlConnection(constring);
                            cmdDataBase = new SqlCommand(Query, conDataBase);

                            try
                            {
                                conDataBase.Open();
                                cmdDataBase.ExecuteReader();
                                conDataBase.Close();

                            }
                            catch (Exception ex)
                            {
                                 
                            }
                        }

                    }
                }
                     if (soloaymentRadioButton.Checked)
                    {
                        MessageBox.Show("حُفظ بأسعار قطاعي");
                    }
                    else
                    {
                        MessageBox.Show("حُفظ بأسعار الجملة");
                    }
                fill();
                fillCategoryDGV();
                clearAll();
                newPurchasesRadioButton.Checked = true;
                detailedDebtGroupBox.Visible = false;
                detailedDebtGroupBox.SendToBack();
                addCategoryButton.Visible = true;

            }
            catch (Exception ex)
            {
            }


        }


        private void DebtsNumberTextBox_TextChanged(object sender, EventArgs e)
        {

            double c, m, g = 0;

            double.TryParse(restDebtTextBox.Text, out c);
            double.TryParse(DebtsNumberTextBox.Text, out m);

            if (m == 0)
            {
                g = c;
            }
            else if (m > 0)
            {
                g = c / m;
            }
            if (g > 0)
            {
                debtAmountTextBox.Text = Convert.ToString(Convert.ToInt32(g));
            }
        }

        private void debtAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            double c, m, g = 0;

            double.TryParse(restDebtTextBox.Text, out c);
            double.TryParse(debtAmountTextBox.Text, out m);

            if (m == 0)
            {

            }
            else if (m > 0)
            {
                g = c / m;
            }
            if (g > 0)
            {
                DebtsNumberTextBox.Text = Convert.ToString(Convert.ToInt32(g));
            }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            debtGrid.Rows.Clear();
            int row = 0;
            DateTime debtDate = this.startDebtDate.Value.Date;

            for (int i = 1; i <= (Convert.ToInt32(DebtsNumberTextBox.Text)); i++)
            {
                debtGrid.Rows.Add();
                row = debtGrid.Rows.Count - 1;


                debtGrid["debtNoColumn", row].Value = i.ToString();
                debtGrid["debtAmountColumn", row].Value = debtAmountTextBox.Text;
                debtGrid["debtDateColumn", row].Value = debtDate.ToShortDateString();

                if (dayRadioButton.Checked)
                {
                    debtDate = debtDate.AddDays(Convert.ToInt32(debtTimeValueTextBox.Text));
                }
                else if (monthRadioButton.Checked)
                {
                    debtDate = debtDate.AddMonths(Convert.ToInt32(debtTimeValueTextBox.Text));
                }
            }
            startDebtDateDGV.Text = this.startDebtDate.Value.Date.ToShortDateString();
            endDebtDateDGV.Text = debtGrid["debtDateColumn", row].Value.ToString();
        }

 
        string debtTime;
        private void dayRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            debtTime = "days";
        }

        private void monthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            debtTime = "month";
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
                    this.sellingPriceTextBox.Text = row.Cells[4].Value.ToString();
                    this.categoryDiscountRateTextBox.Text = row.Cells[5].Value.ToString();
                    this.categoryDiscountAmountTextBox.Text = row.Cells[6].Value.ToString();
                    this.categorySumTextBox.Text = row.Cells[7].Value.ToString();
                }
            }
            catch
            {

            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            detailedDebtGroupBox.Visible = false;
            detailedDebtGroupBox.SendToBack();
            totalBillDebtTextBox.Text = billSumAfterTextBox.Text;
            paidDebtTextBox.Text = paidTextBox.Text;
            restDebtTextBox.Text = restTextBox.Text;
            debtGrid.Rows.Clear();
            debtGrid.Refresh();
            addCategoryButton.Visible = true ;
        }

        private void returnBillButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("إرجاع الفاتورة بالكامل؟", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (ordinaryBillRadioButton.Checked)
                {

                    billSumBeforeTextBox.Text = (string.IsNullOrEmpty(billSumBeforeTextBox.Text)) ? "0" : billSumBeforeTextBox.Text;
                    billDiscountRateTextBox.Text = (string.IsNullOrEmpty(billDiscountRateTextBox.Text)) ? "0" : billDiscountRateTextBox.Text;
                    billDiscountAmountTextBox.Text = (string.IsNullOrEmpty(billDiscountAmountTextBox.Text)) ? "0" : billDiscountAmountTextBox.Text;
                    billTransportTextBox.Text = (string.IsNullOrEmpty(billTransportTextBox.Text)) ? "0" : billTransportTextBox.Text;
                    billSumAfterTextBox.Text = (string.IsNullOrEmpty(billSumAfterTextBox.Text)) ? "0" : billSumAfterTextBox.Text;
                    paidTextBox.Text = (string.IsNullOrEmpty(paidTextBox.Text)) ? "0" : paidTextBox.Text;
                    restTextBox.Text = (string.IsNullOrEmpty(restTextBox.Text)) ? "0" : restTextBox.Text;
                    profitTextBox.Text = (string.IsNullOrEmpty(profitTextBox.Text)) ? "0" : profitTextBox.Text;

                    string storeNameForEdit = "";
                    string oldSupplierName = "";
                    string oldRest = "";

                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string stringReturned = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select returned FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
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

                    try
                    {

                        if (billType == "تعديل")
                        {
                            if (returned == false)
                            {
                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                storeNameForEdit = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select storeName FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                                conDataBase.Close();

                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                oldSupplierName = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select customerName FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                                conDataBase.Close();

                                //rest to subtract it if adjust
                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                oldRest = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select rest FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                                conDataBase.Close();



                                    double supplierBalance = 0;
                                    conDataBase = new SqlConnection(constring);
                                    conDataBase.Open();
                                    string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + oldSupplierName + "') BEGIN SELECT balance from customerTable where name=N'" + oldSupplierName + "' END", conDataBase).ExecuteScalar().ToString();

                                    if (supplierBalanceString != "")
                                    {
                                        supplierBalance = Convert.ToDouble(supplierBalanceString);
                                        conDataBase.Close();
                                    }

                                    supplierBalance -= Convert.ToDouble(oldRest);

                                    string Query = "IF EXISTS (select 1 from customerTable where name=N'" + oldSupplierName + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + oldSupplierName + "' END";
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

                                Query = "DELETE FROM salesMainTable where Id=N'" + this.salesCodeTextBox.Text + "';";
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

                                Query = "DELETE FROM salesSubTable where billCode=N'" + this.salesCodeTextBox.Text + "';";
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

                                for (int i = 0; i < billDGV.Rows.Count; i++)
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
                                        conDataBase = new SqlConnection(constring);
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
                                            //ratio
                                            conDataBase = new SqlConnection(constring);
                                            conDataBase.Open();
                                            float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where categoryCode=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and  subUnit= N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "' ", conDataBase).ExecuteScalar().ToString());
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
                                }
                            }
                            MessageBox.Show("حفظ");
                            fill();
                            fillCategoryDGV();
                            newPurchasesRadioButton.Checked = true;

                            clearAll();

                        }

                        else
                        {
                            MessageBox.Show("لا يمكن تعديل فاتورة بها مرتجع");
                        }
                    }

                    catch (Exception ex)
                    {
                         

                    }
                

                }

                else if (dividedBillRadioButoon.Checked)
                {
                    
                billSumBeforeTextBox.Text = (string.IsNullOrEmpty(billSumBeforeTextBox.Text)) ? "0" : billSumBeforeTextBox.Text;
                billDiscountRateTextBox.Text = (string.IsNullOrEmpty(billDiscountRateTextBox.Text)) ? "0" : billDiscountRateTextBox.Text;
                billDiscountAmountTextBox.Text = (string.IsNullOrEmpty(billDiscountAmountTextBox.Text)) ? "0" : billDiscountAmountTextBox.Text;
                billTransportTextBox.Text = (string.IsNullOrEmpty(billTransportTextBox.Text)) ? "0" : billTransportTextBox.Text;
                billSumAfterTextBox.Text = (string.IsNullOrEmpty(billSumAfterTextBox.Text)) ? "0" : billSumAfterTextBox.Text;
                paidTextBox.Text = (string.IsNullOrEmpty(paidTextBox.Text)) ? "0" : paidTextBox.Text;
                restTextBox.Text = (string.IsNullOrEmpty(restTextBox.Text)) ? "0" : restTextBox.Text;
                profitTextBox.Text = (string.IsNullOrEmpty(profitTextBox.Text)) ? "0" : profitTextBox.Text;

                string storeNameForEdit = "";
                string oldSupplierName = "";
                string oldRest = "";
                string oldDebtId = "";

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string stringReturned = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select returned FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
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

                try
                {
                        if (billType == "تعديل")
                        {
                            if (returned == false)
                            {
                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                storeNameForEdit = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select storeName FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                                conDataBase.Close();

                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                oldSupplierName = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select customerName FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                                conDataBase.Close();

                                //rest to subtract it if adjust
                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                oldRest = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select rest FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                                conDataBase.Close();

                                //old debts ID 
                                conDataBase = new SqlConnection(constring);
                                conDataBase.Open();
                                oldDebtId = new SqlCommand("IF EXISTS (select 1 FROM debtsMainTable where billNumber = N'" + salesCodeTextBox.Text + "') BEGIN select Id FROM debtsMainTable where billNumber = N'" + salesCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                                conDataBase.Close();

                                try
                                {



                                    double supplierBalance = 0;
                                    conDataBase = new SqlConnection(constring);
                                    conDataBase.Open();
                                    string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + oldSupplierName + "') BEGIN SELECT balance from customerTable where name=N'" + oldSupplierName + "' END", conDataBase).ExecuteScalar().ToString();

                                    if (supplierBalanceString != "")
                                    {
                                        supplierBalance = Convert.ToDouble(supplierBalanceString);
                                        conDataBase.Close();
                                    }

                                    supplierBalance -= Convert.ToDouble(oldRest);

                                    string Query = "IF EXISTS (select 1 from customerTable where name=N'" + oldSupplierName + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + oldSupplierName + "' END";
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


                                    Query = "DELETE FROM salesMainTable where Id=N'" + this.salesCodeTextBox.Text + "';";
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

                                    Query = "DELETE FROM salesSubTable where billCode=N'" + this.salesCodeTextBox.Text + "';";
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

                                    for (int i = 0; i < billDGV.Rows.Count; i++)
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
                                        conDataBase = new SqlConnection(constring);
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
                                            //ratio
                                            conDataBase = new SqlConnection(constring);
                                            conDataBase.Open();
                                            float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where categoryCode=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and  subUnit= N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "' ", conDataBase).ExecuteScalar().ToString());
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
                                    }

                                    Query = "DELETE FROM debtsMainTable where billNumber=N'" + this.salesCodeTextBox.Text + "';";
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

                                    Query = "DELETE FROM debtsTable where debtMainTableNumber =N'" + oldDebtId + "';";
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

                                    MessageBox.Show("حفظ");
                                    fill();
                                    fillCategoryDGV();
                                    clearAll();
                                    detailedDebtGroupBox.Visible = false;
                                    detailedDebtGroupBox.SendToBack();
                                    addCategoryButton.Visible = true;
                                }
                                catch (Exception ex)
                                {
                                }
                              
                            }
                            else
                            {
                                MessageBox.Show("لا يمكن تعديل فاتورة بها مرتجع");
                            }
                        }
            }

                catch (Exception ex)
                {
                }

                }

            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void salesCodeTextBox_TextChanged(object sender, EventArgs e)
        {   
            if (adjustPurhasesRadioButton.Checked)
            {
                if (salesCodeTextBox.Text == "")
                {
                    clearAll();
                    ordinaryBillRadioButton.Checked = true;
                }
                else
                {
                    //if(bool.Parse((string.IsNullOrEmpty(salesCodeTextBox.Text)) ? "0" : billSumBeforeTextBox.Text)){ 
                    clearAll();

                    editDGV.DataSource = null;
                    editDGV.Refresh();
                    
                    string Query = "IF EXISTS (select 1 from salesSubTable where billCode=N'" + this.salesCodeTextBox.Text + "') BEGIN select salesSubTable.categoryCode as 'كود الصنف', salesSubTable.unit as 'الوحدة', salesSubTable.quantity as 'الكمية',salesSubTable.purchasePrice as 'سعر البيع', salesSubTable.discountRate as 'نسبة الخصم', salesSubTable.discountAmount as 'قيمة الخصم', salesSubTable.sum as 'الإجمالي' from salesSubTable where billCode=N'" + this.salesCodeTextBox.Text + "' END;";

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
                        conDataBase.Close();
                    }
                    catch 
                    {
                        
                    }

                    for (int i = 0; i < editDGV.Rows.Count -1; i++)
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
                            categoryName = new SqlCommand("Select categoryName from categoryTable where Id=N'" + editDGV.Rows[i].Cells[0].Value.ToString() + "'  ", conDataBase).ExecuteScalar().ToString();
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
                        string paymentType = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select paymentType FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
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
                        string customerName = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select customerName FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        customerName = (string.IsNullOrEmpty(customerName)) ? "0" : customerName;
                        customerNameComboBox.Text = customerName;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string storeName = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select storeName FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        storeName = (string.IsNullOrEmpty(storeName)) ? "0" : storeName;
                        storeNameComboBox.Text = storeName;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string safeName = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select safeName FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        safeName = (string.IsNullOrEmpty(safeName)) ? "0" : safeName;
                        safeComboBox.Text = safeName;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string buyingTypee = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select buyingType FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
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
                        string billSum = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select sumBefore FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        billSum = (string.IsNullOrEmpty(billSum)) ? "0" : billSum;
                        billSumBeforeTextBox.Text = billSum;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string discountRate = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select discountPercentage FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        discountRate = (string.IsNullOrEmpty(discountRate)) ? "0" : discountRate;
                        billDiscountRateTextBox.Text = discountRate;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string discountAmount = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select discountAmount FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        discountAmount = (string.IsNullOrEmpty(discountAmount)) ? "0" : discountAmount;
                        billDiscountAmountTextBox.Text = discountAmount;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string salesTax = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select salesTax FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        salesTax = (string.IsNullOrEmpty(salesTax)) ? "0" : salesTax;
                        billSalesTaxTextBox.Text = salesTax;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string transport = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select transport FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        transport = (string.IsNullOrEmpty(transport)) ? "0" : transport;
                        billTransportTextBox.Text = transport;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string sumAfter = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select sumAfter FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        sumAfter = (string.IsNullOrEmpty(sumAfter)) ? "0" : sumAfter;
                        billSumAfterTextBox.Text = sumAfter;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string paid = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select paid FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        paid = (string.IsNullOrEmpty(paid)) ? "0" : paid;
                        paidTextBox.Text = paid;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string date = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select date FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        dateDTP.Text = date;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string stringDebts = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select debts FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();

                        stringDebts = (string.IsNullOrEmpty(stringDebts)) ? "0" : stringDebts;
                        bool debts;
                        if (stringDebts == "0" || stringDebts == "False")
                        {

                            debts = false;
                            ordinaryBillRadioButton.Checked = true;
                        }
                        else
                        {
                            dividedBillRadioButoon.Checked = true;
                            debts = true;
                        }
                        conDataBase.Close();
                        if (debts)
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string debtsRatio = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select debtsRatio FROM salesMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                            debtsRatio = (string.IsNullOrEmpty(debtsRatio)) ? "0" : debtsRatio;
                            ratioTextBox.Text = debtsRatio;
                            conDataBase.Close();


                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string debtsType = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select debtsType FROM salesMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            if (debtsType == "الباقي")
                            {
                                restRadioButton.Checked = true;
                            }
                            else
                            {
                                reductionRadioButton.Checked = true;
                            }

                            totalBillDebtTextBox.Text = billSumAfterTextBox.Text;
                            paidDebtTextBox.Text = paidTextBox.Text;
                            restDebtTextBox.Text = restTextBox.Text;

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string divsionNumber = new SqlCommand("IF EXISTS (select 1 FROM debtsMainTable where billNumber = N'" + salesCodeTextBox.Text + "') BEGIN select debtsNumber FROM debtsMainTable where billNumber = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                            divsionNumber = (string.IsNullOrEmpty(divsionNumber)) ? "0" : divsionNumber;
                            DebtsNumberTextBox.Text = divsionNumber;
                            conDataBase.Close();



                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string debtTimeValue = new SqlCommand("IF EXISTS (select 1 FROM debtsMainTable where billNumber = N'" + salesCodeTextBox.Text + "') BEGIN select time FROM debtsMainTable where billNumber = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                            debtTimeValue = (string.IsNullOrEmpty(debtTimeValue)) ? "0" : debtTimeValue;
                            debtTimeValueTextBox.Text = debtTimeValue;
                            conDataBase.Close();

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string timeType = new SqlCommand("IF EXISTS (select 1 FROM debtsMainTable where billNumber = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select timeType FROM debtsMainTable where billNumber = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();

                            if (timeType == "days")
                            {
                                dayRadioButton.Checked = true;
                            }

                            else
                            {
                                monthRadioButton.Checked = true;
                            }

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string startDate = new SqlCommand("IF EXISTS (select 1 FROM debtsMainTable where billNumber = N'" + salesCodeTextBox.Text + "') BEGIN select startDate FROM debtsMainTable where billNumber = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                            startDebtDate.Text = startDate;
                            conDataBase.Close();
                        }

                    }

                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            clearAll();
            fill();
            fillCategoryDGV();
            soloRadioButton.Checked = true;
            cashRadioButton.Checked = true;
            newPurchasesRadioButton.Checked = true;
            ordinaryBillRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;
            restRadioButton.Checked = true;
            dayRadioButton.Checked = true;
            detailedDebtGroupBox.Visible = false;
            this.ActiveControl = categoryCodeTextBox;
        }

        public void refreshLocal()
        {
            clearAll();
            fill();
            fillCategoryDGV();
            soloRadioButton.Checked = true;
            cashRadioButton.Checked = true;
            newPurchasesRadioButton.Checked = true;
            ordinaryBillRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;
            restRadioButton.Checked = true;
            dayRadioButton.Checked = true;
            detailedDebtGroupBox.Visible = false;
            this.ActiveControl = categoryCodeTextBox;
        }

        private void sales_Load(object sender, EventArgs e)
        {
            this.ActiveControl=categoryCodeTextBox;
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void sales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                addCategoryButton_Click(sender, e);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {   
            string header = "QTY       item                       price     Total ";
            string sahpe1 = "**********************************************************";
            int j = 240;
            string thing = ((char)0x200E).ToString();
            Bitmap b = new Bitmap(@"C:\Untitled.png");

            Image im = b;

            e.Graphics.DrawImage(im, 80, 0);
            e.Graphics.DrawString("فاتوره مبيعات   ", new Font("fontA", 10, FontStyle.Bold), Brushes.Black, 85, 120);
            e.Graphics.DrawString("01099235588", new Font("fontA", 10, FontStyle.Bold), Brushes.Black, 0, 150);
            e.Graphics.DrawString(DateTime.Now.ToString(), new Font("fontA", 10, FontStyle.Bold), Brushes.Black, 100, 150);

            e.Graphics.DrawString(sahpe1, new Font("fontA", 10, FontStyle.Regular), Brushes.Black, 0, 180);
            e.Graphics.DrawString(header, new Font("fontA", 10, FontStyle.Bold), Brushes.Black, 0, 210);
            e.Graphics.DrawString(sahpe1, new Font("fontA", 10, FontStyle.Regular), Brushes.Black, 0, 240);
            for (int i = 0; i < billDGV.RowCount ; i++)
            {
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
            e.Graphics.DrawString("Final  Total :      " + billSumAfterTextBox.Text, new Font("fontA", 20, FontStyle.Bold), Brushes.Black, 0, j);

        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void editDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }
    }
}
