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
    public partial class statement : UserControl
    {
        public statement()
        {
            InitializeComponent();
            clearAll();
            fill();
            fillCategoryDGV();
            soloRadioButton.Checked = true;
            newPurchasesRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;
           
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string buyingType;
        string billType;
        string priceUpdate;

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
        }
        void clearAll()
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

            billDGV.Rows.Clear();
            billDGV.Refresh();

            editDGV.DataSource = null;
            editDGV.Refresh();

            billSumBeforeTextBox.Text = "0";
            billDiscountRateTextBox.Text = "0";
            billDiscountAmountTextBox.Text = "0";
            billTransportTextBox.Text = "0";
            billSalesTaxTextBox.Text = "0";
            billSumAfterTextBox.Text = "0";

        }

        void fill()
        {
            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from statementMainTable) BEGIN SELECT MAX(Id) FROM statementMainTable END", conDataBase).ExecuteScalar().ToString();

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
                MessageBox.Show(ex.Message);

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
                MessageBox.Show(ex.Message);

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
                MessageBox.Show(ex.Message);

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
                MessageBox.Show(ex.Message);

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
                MessageBox.Show(ex.Message);

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
                    if (soloRadioButton.Checked)
                    {
                        this.sellingPriceTextBox.Text = row.Cells[7].Value.ToString();
                    }
                    else
                    {
                        this.sellingPriceTextBox.Text = row.Cells[8].Value.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void categoryCodeTextBox_TextChanged_1(object sender, EventArgs e)
        {

            try
            {
                //Quantity in mainUnit
                SqlConnection conDataBase = new SqlConnection(constring);
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

        private void unitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                //Quantity in mainUnit
                SqlConnection conDataBase = new SqlConnection(constring);
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
                        float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select packagePrice from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
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
                        float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select sellingPrice from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
                        conDataBase.Close();
                        sellingPriceTextBox.Text = priceMainUnit.ToString();
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
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

            SqlConnection conDataBase = new SqlConnection(constring);
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

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string buyingPrice2 = (string.IsNullOrEmpty(this.billDGV.Rows[i].Cells[0].Value.ToString()) ? "0" : this.billDGV.Rows[i].Cells[0].Value.ToString());
                            string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + buyingPrice2 + "') BEGIN select buyingPrice from categoryTable where Id=N'" + buyingPrice2 + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            names = (string.IsNullOrEmpty(names)) ? "0" : names;
                            double intpurchasePrice2 = Convert.ToDouble(names);

                            totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice2);
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
                        fillCategoryDGV();
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
                    string buyingPrice = (string.IsNullOrEmpty(this.billDGV.Rows[i].Cells[0].Value.ToString()) ? "0" : this.billDGV.Rows[i].Cells[0].Value.ToString());
                    string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + buyingPrice + "') BEGIN select buyingPrice from categoryTable where Id=N'" + buyingPrice + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    names = (string.IsNullOrEmpty(names)) ? "0" : names;
                    double intpurchasePrice = Convert.ToDouble(names);
                    totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice);
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
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string buyingPrice = (string.IsNullOrEmpty(this.billDGV.Rows[i].Cells[0].Value.ToString()) ? "0" : this.billDGV.Rows[i].Cells[0].Value.ToString());
                    string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + buyingPrice + "') BEGIN select buyingPrice from categoryTable where Id=N'" + buyingPrice + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    names = (string.IsNullOrEmpty(names)) ? "0" : names;
                    double intpurchasePrice = Convert.ToDouble(names);
                    totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice);
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

        private void billDiscountRateTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
            try
            {
                profitTextBox.Text = "0";
                double totalProfitSum = 0;
                for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
                {

                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string buyingPrice = (string.IsNullOrEmpty(this.billDGV.Rows[i].Cells[0].Value.ToString()) ? "0" : this.billDGV.Rows[i].Cells[0].Value.ToString());
                    string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + buyingPrice + "') BEGIN select buyingPrice from categoryTable where Id=N'" + buyingPrice + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    names = (string.IsNullOrEmpty(names)) ? "0" : names;
                    double intpurchasePrice = Convert.ToDouble(names);
                    totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice);
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
            try
            {
                profitTextBox.Text = "0";
                double totalProfitSum = 0;
                for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
                {
                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string buyingPrice = (string.IsNullOrEmpty(this.billDGV.Rows[i].Cells[0].Value.ToString()) ? "0" : this.billDGV.Rows[i].Cells[0].Value.ToString());
                    string names = new SqlCommand("IF EXISTS(select 1 from categoryTable where Id=N'" + buyingPrice + "') BEGIN select buyingPrice from categoryTable where Id=N'" + buyingPrice + "' END ELSE SELECT 0;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    names = (string.IsNullOrEmpty(names)) ? "0" : names;
                    double intpurchasePrice = Convert.ToDouble(names);
                    totalProfitSum += Convert.ToDouble(billDGV.Rows[i].Cells[7].Value) - (Convert.ToDouble(billDGV.Rows[i].Cells[3].Value) * intpurchasePrice);
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

        private void saveButton_Click(object sender, EventArgs e)
        {
           
                billSumBeforeTextBox.Text = (string.IsNullOrEmpty(billSumBeforeTextBox.Text)) ? "0" : billSumBeforeTextBox.Text;
                billDiscountRateTextBox.Text = (string.IsNullOrEmpty(billDiscountRateTextBox.Text)) ? "0" : billDiscountRateTextBox.Text;
                billDiscountAmountTextBox.Text = (string.IsNullOrEmpty(billDiscountAmountTextBox.Text)) ? "0" : billDiscountAmountTextBox.Text;
                billTransportTextBox.Text = (string.IsNullOrEmpty(billTransportTextBox.Text)) ? "0" : billTransportTextBox.Text;
                billSumAfterTextBox.Text = (string.IsNullOrEmpty(billSumAfterTextBox.Text)) ? "0" : billSumAfterTextBox.Text;
                profitTextBox.Text = (string.IsNullOrEmpty(profitTextBox.Text)) ? "0" : profitTextBox.Text;

             

                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string stringReturned = new SqlCommand("IF EXISTS (select 1 FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select returned FROM statementMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
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
                    string Query = "IF NOT EXISTS (select 1 FROM statementMainTable where Id= N'" + this.salesCodeTextBox.Text + "') BEGIN INSERT INTO statementMainTable(storeName,customerName,buyingType,sumBefore,discountPercentage,discountAmount,salesTax,transport,sumAfter,profit,date,returned) VALUES (N'" + this.storeNameComboBox.Text + "',N'" + this.customerNameTextBox.Text + "',N'" + buyingType + "',N'" + this.billSumBeforeTextBox.Text + "',N'" + this.billDiscountRateTextBox.Text + "',N'" + this.billDiscountAmountTextBox.Text + "',N'" + this.billSalesTaxTextBox.Text + "',N'" + this.billTransportTextBox.Text + "',N'" + this.billSumAfterTextBox.Text + "',N'" + this.profitTextBox.Text + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','FALSE') END ELSE BEGIN UPDATE statementMainTable SET storeName =N'" + this.storeNameComboBox.Text + "',customerName=N'" + this.customerNameTextBox.Text + "',buyingType=N'" + buyingType + "',sumBefore=N'" + this.billSumBeforeTextBox.Text + "',discountPercentage=N'" + this.billDiscountRateTextBox.Text + "',discountAmount=N'" + this.billDiscountAmountTextBox.Text + "',salesTax=N'" + this.billSalesTaxTextBox.Text + "',transport=N'" + this.billTransportTextBox.Text + "',sumAfter=N'" + this.billSumAfterTextBox.Text + "',profit=N'" + this.profitTextBox.Text + "', date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' where Id=N'" + this.salesCodeTextBox.Text + "' and returned='False' END ";
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

                       

                    }
                    catch (Exception ex)
                    {
     
                    }

                    if (billType == "جديدة")
                    {
                        for (int i = 0; i < billDGV.Rows.Count; i++)
                        {

                            Query = "INSERT INTO statementSubTable(billCode,categoryCode,unit,quantity,purchasePrice,discountRate,discountAmount,sum) VALUES (N'" + Int32.Parse(this.salesCodeTextBox.Text) + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[3].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[4].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[5].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[6].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[7].Value.ToString() + "')";
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

                    else if (billType == "تعديل")
                    {
                        if (returned == false)
                        {

                            Query = "DELETE FROM statementSubTable where billCode=N'" + this.salesCodeTextBox.Text + "';";
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


                                Query = "INSERT INTO statementSubTable(billCode,categoryCode,unit,quantity,purchasePrice,discountRate,discountAmount,sum) VALUES (N'" + Int32.Parse(this.salesCodeTextBox.Text) + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[3].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[4].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[5].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[6].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[7].Value.ToString() + "')";
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
                    newPurchasesRadioButton.Checked = true;

                    clearAll();
                }
                catch (Exception ex)
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
                }
                else
                {
                    //if(bool.Parse((string.IsNullOrEmpty(salesCodeTextBox.Text)) ? "0" : billSumBeforeTextBox.Text)){ 
                    clearAll();

                    string Query = "IF EXISTS (select 1 from statementSubTable where billCode=N'" + this.salesCodeTextBox.Text + "') BEGIN select statementSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف',statementSubTable.unit as 'الوحدة', statementSubTable.quantity as 'الكمية',statementSubTable.purchasePrice as 'سعر البيع', statementSubTable.discountRate as'نسبة الخصم', statementSubTable.discountAmount as 'قيمة الخصم', statementSubTable.sum as 'الإجمالي' from statementSubTable,categoryTable where categoryTable.Id = statementSubTable.categoryCode and billCode=N'" + this.salesCodeTextBox.Text + "' END;";

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
                        try
                        {
                            billDGV.Rows.Add(editDGV.Rows[i].Cells[0].Value.ToString(), editDGV.Rows[i].Cells[1].Value.ToString(), editDGV.Rows[i].Cells[2].Value.ToString(),
                                editDGV.Rows[i].Cells[3].Value.ToString(), editDGV.Rows[i].Cells[4].Value.ToString(), editDGV.Rows[i].Cells[5].Value.ToString(),
                                editDGV.Rows[i].Cells[6].Value.ToString(), editDGV.Rows[i].Cells[7].Value.ToString());
                        }
                        catch { }
                    }

                    try
                    {
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string customerName = new SqlCommand("IF EXISTS (select 1 FROM statementMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select customerName FROM statementMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        customerNameTextBox.Text = customerName;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string storeName = new SqlCommand("IF EXISTS (select 1 FROM statementMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select storeName FROM statementMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        storeName = (string.IsNullOrEmpty(storeName)) ? "0" : storeName;
                        storeNameComboBox.Text = storeName;

                     

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string buyingTypee = new SqlCommand("IF EXISTS (select 1 FROM statementMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select buyingType FROM statementMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
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
                        string billSum = new SqlCommand("IF EXISTS (select 1 FROM statementMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "') BEGIN select sumBefore FROM statementMainTable where Id = N'" + Int32.Parse(salesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        billSum = (string.IsNullOrEmpty(billSum)) ? "0" : billSum;
                        billSumBeforeTextBox.Text = billSum;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string discountRate = new SqlCommand("IF EXISTS (select 1 FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select discountPercentage FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        discountRate = (string.IsNullOrEmpty(discountRate)) ? "0" : discountRate;
                        billDiscountRateTextBox.Text = discountRate;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string discountAmount = new SqlCommand("IF EXISTS (select 1 FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select discountAmount FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        discountAmount = (string.IsNullOrEmpty(discountAmount)) ? "0" : discountAmount;
                        billDiscountAmountTextBox.Text = discountAmount;
                        conDataBase.Close();


                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string salesTax = new SqlCommand("IF EXISTS (select 1 FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select salesTax FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        salesTax = (string.IsNullOrEmpty(salesTax)) ? "0" : salesTax;
                        billSalesTaxTextBox.Text = salesTax;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string transport = new SqlCommand("IF EXISTS (select 1 FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select transport FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        transport = (string.IsNullOrEmpty(transport)) ? "0" : transport;
                        billTransportTextBox.Text = transport;
                        conDataBase.Close();

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string sumAfter = new SqlCommand("IF EXISTS (select 1 FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select sumAfter FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        sumAfter = (string.IsNullOrEmpty(sumAfter)) ? "0" : sumAfter;
                        billSumAfterTextBox.Text = sumAfter;
                        conDataBase.Close();

                       

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string date = new SqlCommand("IF EXISTS (select 1 FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "') BEGIN select date FROM statementMainTable where Id = N'" + salesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                        dateDTP.Text = date;
                        conDataBase.Close();

                      
                    }

                    catch (Exception ex)
                    {
                    }
                }
            }
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

        private void returnBillButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("حذف البيان بالكامل؟", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                billSumBeforeTextBox.Text = (string.IsNullOrEmpty(billSumBeforeTextBox.Text)) ? "0" : billSumBeforeTextBox.Text;
                billDiscountRateTextBox.Text = (string.IsNullOrEmpty(billDiscountRateTextBox.Text)) ? "0" : billDiscountRateTextBox.Text;
                billDiscountAmountTextBox.Text = (string.IsNullOrEmpty(billDiscountAmountTextBox.Text)) ? "0" : billDiscountAmountTextBox.Text;
                billTransportTextBox.Text = (string.IsNullOrEmpty(billTransportTextBox.Text)) ? "0" : billTransportTextBox.Text;
                billSumAfterTextBox.Text = (string.IsNullOrEmpty(billSumAfterTextBox.Text)) ? "0" : billSumAfterTextBox.Text;
                profitTextBox.Text = (string.IsNullOrEmpty(profitTextBox.Text)) ? "0" : profitTextBox.Text;


                try
                {

                    if (billType == "تعديل")
                    {


                        string Query = "DELETE FROM statementMainTable where Id=N'" + this.salesCodeTextBox.Text + "';";
                        SqlConnection conDataBase = new SqlConnection(constring);
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

                        Query = "DELETE FROM statementSubTable where billCode=N'" + this.salesCodeTextBox.Text + "';";
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
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            clearAll();
            fill();
            fillCategoryDGV();
            soloRadioButton.Checked = true;
            newPurchasesRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;
        }

        public void refreshLocal()
        {
            clearAll();
            fill();
            fillCategoryDGV();
            soloRadioButton.Checked = true;
            newPurchasesRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;
            this.ActiveControl = categoryCodeTextBox;
        }

        private void statement_Load(object sender, EventArgs e)
        {
            this.ActiveControl = categoryCodeTextBox;
        }

       

        private void statement_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void categoryCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                addCategoryButton_Click(sender, e);

            }

        }

        private void billSumBeforeTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
        }

        private void billSumAfterTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
