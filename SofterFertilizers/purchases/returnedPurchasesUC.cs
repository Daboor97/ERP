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
    public partial class returnedPurchasesUC : UserControl
    {
        public returnedPurchasesUC()
        {
            InitializeComponent();
            clear();
            clearAll();

            fill();
            fillCategoryDGV();
            withBillRadioButton.Checked = true;
            newReturnedRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;
            searchReturnedTextBox.Visible = false;
            categoryDGV.Visible = false;
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string returnedBillOption;
        string returnedType;
        string priceUpdate;

        void newBillInitialization()
        {
            supplierNameComboBox.Enabled = false;
            supplierCodeTextBox.Enabled = false;
            balanceTextBox.Enabled = false;
            storeNameComboBox.Enabled = false;
            label12.Visible = false;
            label5.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            categoryCodeSearchTextBox.Visible = false;
            categoryNameSearchTextBox.Visible = false;
            categoryStoreCodeSearchTextBox.Visible = false;
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


        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            DrawGroupBox(box, e.Graphics, Color.Red, Parent.BackColor);
        }

        private void DrawGroupBox(GroupBox box, Graphics g, Color textColor, Color borderColor)
        {
            if (box != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Clear text and border
                g.Clear(this.BackColor);

                // Draw text
                g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }

        private void updatePriceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            priceUpdate = "تحديث";
        }

        private void constantPriceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            priceUpdate = "تثبيت";

        }

        private void newReturnedRadioButton_CheckedChanged(object sender, EventArgs e)
        {

            returnedType = "جديد";
            topLeftLabel.Text = "الفاتورة المسجلة";
            billGroupBox.Visible = true;
            purchasesCodeTextBox.Visible = true;
            searchPurchasesBill.Visible = true;
            returnAllBillButton.Visible = true;
            oldBillDGV.Visible = true;
            oldBillGroupBox.Visible = true;
            returnedCodeTextBox.BackColor = Color.FromArgb(64, 64, 64);
            returnedCodeTextBox.ReadOnly = true;
            categoryDGV.Visible = false;
            label12.Visible = false;
            label5.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            categoryCodeSearchTextBox.Visible = false;
            categoryNameSearchTextBox.Visible = false;
            categoryStoreCodeSearchTextBox.Visible = false;
            searchReturnedTextBox.Visible = false;
            label19.Visible = true;
            fill();
            withBillRadioButton.Checked = true;
        }

        private void adjustReturnedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            returnedType = "تعديل";
            topLeftLabel.Text = "الأصناف المسجلة";
            billGroupBox.Visible = false;
            label19.Visible = false;
            purchasesCodeTextBox.Visible = false;
            searchPurchasesBill.Visible = false;
            returnAllBillButton.Visible = false;
            oldBillDGV.Visible = false;
            oldBillGroupBox.Visible = false;
            categoryDGV.Visible = false;
            returnedCodeTextBox.BackColor = Color.FromArgb(41, 44, 51);
            returnedCodeTextBox.ReadOnly = false;
            label12.Visible = true;
            label5.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            categoryCodeSearchTextBox.Visible = true;
            categoryNameSearchTextBox.Visible = true;
            categoryStoreCodeSearchTextBox.Visible = true;
            searchReturnedTextBox.Visible = true;
            returnedCodeTextBox.Text = "";
            /*
            label4.Visible = false;
            categoryNameTextBox.Visible = false;
            label3.Visible = false;
            categoryCodeTextBox.Visible = false;
            label2.Visible = false;
            unitTextBox.Visible = false;
            unitComboBox.Visible = false;
            label26.Visible = false;
            quantityTextBox.Visible = false;
            quantityLabel.Visible = false;
            categoryNameTextBox.Visible = false;
            categoryNameTextBox.Visible = false;
            */
            addCategoryButton.Visible = false;
        }

        private void withBillRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            clear();

            returnedBillOption = "بفاتورة";
            topLeftLabel.Text = "الفاتورة المسجلة";
            label19.Visible = true;
            purchasesCodeTextBox.Visible = true;
            searchPurchasesBill.Visible = true;
            returnAllBillButton.Visible = true;
            oldBillDGV.Visible = true;
            oldBillGroupBox.Visible = true;
            oldBillDGV.Rows.Clear();
            oldBillDGV.Refresh();
            categoryDGV.Visible = false;
            unitTextBox.Visible = true;
            unitComboBox.Visible = false;
            supplierNameTextBox.Visible = true;
            supplierCodeTextBox.BackColor = Color.FromArgb(64, 64, 64);
            supplierCodeTextBox.ReadOnly = true;
            categoryCodeTextBox.BackColor = Color.FromArgb(64, 64, 64);
            categoryCodeTextBox.ReadOnly = true;
            categoryCodeTextBox.Text = "";
            supplierCodeTextBox.Text = "";
            balanceTextBox.Text = "";
            storeNameTextBox.Visible = true;
            supplierNameComboBox.Visible = false;
            storeNameComboBox.Visible = false;
            purchasesCodeTextBox.Text = "";
            addCategoryButton.Visible = false;
            fill();
        }

        private void withoutBillRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            addCategoryButton.Visible = true;

            clear();
            returnedBillOption = "بدون فاتورة";
            topLeftLabel.Text = "الأصناف المسجلة";
            label19.Visible = false;
            purchasesCodeTextBox.Visible = false;
            searchPurchasesBill.Visible = false;
            returnAllBillButton.Visible = false;
            oldBillDGV.Visible = false;
            oldBillGroupBox.Visible = false;
            categoryDGV.Visible = true;
            label12.Visible = true;
            label5.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            categoryCodeSearchTextBox.Visible = true;
            categoryNameSearchTextBox.Visible = true;
            categoryStoreCodeSearchTextBox.Visible = true;
            unitTextBox.Visible = false;
            unitComboBox.Visible = true;
            supplierNameTextBox.Visible = false;
            supplierCodeTextBox.BackColor = Color.FromArgb(41, 44, 51);
            supplierCodeTextBox.ReadOnly = false;
            categoryCodeTextBox.BackColor = Color.FromArgb(41, 44, 51);
            categoryCodeTextBox.ReadOnly = false;
            categoryCodeTextBox.Text = "";
            storeNameTextBox.Visible = false;
            supplierNameComboBox.Visible = true;
            storeNameComboBox.Visible = true;
            purchasesCodeTextBox.Text = "-";
            fill();
            this.ActiveControl = categoryCodeTextBox;


        }

        void fill()
        {
            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from returnedPurchasesMainTable) BEGIN SELECT MAX(Id) FROM returnedPurchasesMainTable END", conDataBase).ExecuteScalar().ToString();

            if (maxBill != "")
            {
                int maxBillint = Convert.ToInt32(maxBill);
                returnedCodeTextBox.Text = Convert.ToString(maxBillint + 1);
                conDataBase.Close();
            }

            else
            {
                returnedCodeTextBox.Text = "1";
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

        private void oldBillDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            clear();
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.oldBillDGV.Rows[e.RowIndex];
                    this.categoryCodeTextBox.Text = row.Cells[0].Value.ToString();
                    this.categoryNameTextBox.Text = row.Cells[1].Value.ToString();
                    this.buyingPriceTextBox.Text = row.Cells[4].Value.ToString();
                    this.unitTextBox.Text = row.Cells[2].Value.ToString();
                    this.quantityLabel.Text = row.Cells[3].Value.ToString();
                    this.categoryDiscountRateTextBox.Text = row.Cells[5].Value.ToString();
                    this.categoryDiscountAmountTextBox.Text = row.Cells[6].Value.ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void categoryCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (withoutBillRadioButton.Checked)
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
        }

        private void unitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (withoutBillRadioButton.Checked)
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
                        float priceMainUnit = float.Parse(new SqlCommand("IF EXISTS(Select 1 from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "') BEGIN Select buyingPrice from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());
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
        private void supplierNameTextBox_TextChanged(object sender, EventArgs e)
        {

            try
            {
                //supplier Code
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                supplierCodeTextBox.Text = new SqlCommand("select Id from supplierTable where name=N'" + this.supplierNameTextBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                //supplier Balance
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                balanceTextBox.Text = new SqlCommand("select balance from supplierTable where name=N'" + this.supplierNameTextBox.Text + "';", conDataBase).ExecuteScalar().ToString();
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
            if (Int32.Parse(quantityTextBox.Text) <= Int32.Parse(quantityLabel.Text))
            {
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



                    string Query = "UPDATE categoryTable SET buyingPrice=N'" + this.buyingPriceTextBox.Text + "' where Id=N'" + this.categoryCodeTextBox.Text + "'";
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
                    fillCategoryDGV();
                }

                if (!constantizeCheckBox.Checked)
                {
                    constantPriceRadioButton.Checked = true;
                }
                clear();
            }
            else
            {
                if (returnedBillOption == "بفاتورة")
                {
                    MessageBox.Show("الكمية المرتجعة أكبر من الكمية بالفاتورة");
                    quantityTextBox.Text = quantityLabel.Text;
                }
                else
                {
                    MessageBox.Show("الكمية المرتجعة أكبر من الكمية بالمخزن");
                    quantityTextBox.Text = quantityLabel.Text;
                }
            }
        }

        private void searchPurchasesBill_Click(object sender, EventArgs e)
        {
            oldBillDGV.Rows.Clear();
            oldBillDGV.Refresh();
            addCategoryButton.Visible = true;

            clearAll();

            string Query = "IF EXISTS (select 1 from purchasesSubTable where billCode=N'" + this.purchasesCodeTextBox.Text + "') BEGIN select purchasesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الكود',purchasesSubTable.unit as 'الوحدة', purchasesSubTable.quantity as 'الكمية',purchasesSubTable.purchasePrice as 'سعر الشراء', purchasesSubTable.discountRate as'نسبة الخصم', purchasesSubTable.discountAmount as 'قيمة الخصم', purchasesSubTable.sum as 'الإجمالي' from purchasesSubTable,categoryTable where categoryTable.Id = purchasesSubTable.categoryCode and billCode=N'" + this.purchasesCodeTextBox.Text + "' END;";

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
                MessageBox.Show(ex.Message);
            }

            for (int i = 0; i <= editDGV.Rows.Count - 1; i++)
            {
                try
                {
                    oldBillDGV.Rows.Add(editDGV.Rows[i].Cells[0].Value.ToString(), editDGV.Rows[i].Cells[1].Value.ToString(), editDGV.Rows[i].Cells[2].Value.ToString(),
                        editDGV.Rows[i].Cells[3].Value.ToString(), editDGV.Rows[i].Cells[4].Value.ToString(), editDGV.Rows[i].Cells[5].Value.ToString(),
                        editDGV.Rows[i].Cells[6].Value.ToString(), editDGV.Rows[i].Cells[7].Value.ToString());
                }
                catch { }
            }


            try
            {


                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string supplierName = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "') BEGIN select supplierName FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                supplierName = (string.IsNullOrEmpty(supplierName)) ? "0" : supplierName;
                supplierNameTextBox.Text = supplierName;

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string billSum = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "') BEGIN select sumBefore FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                billSum = (string.IsNullOrEmpty(billSum)) ? "0" : billSum;
                oldBillSumBeforeTextBox.Text = billSum;

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string discountRate = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select discountPercentage FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                discountRate = (string.IsNullOrEmpty(discountRate)) ? "0" : discountRate;
                oldBillDiscountRateTextBox.Text = discountRate;
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string discountAmount = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select discountAmount FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                discountAmount = (string.IsNullOrEmpty(discountAmount)) ? "0" : discountAmount;
                oldBillDiscountAmountTextBox.Text = discountAmount;
                conDataBase.Close();


                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string salesTax = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select salesTax FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                salesTax = (string.IsNullOrEmpty(salesTax)) ? "0" : salesTax;
                oldBillSalesTaxTextBox.Text = salesTax;
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string transport = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select transport FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                transport = (string.IsNullOrEmpty(transport)) ? "0" : transport;
                oldBillTransportTextBox.Text = transport;
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string sumAfter = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select sumAfter FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                sumAfter = (string.IsNullOrEmpty(sumAfter)) ? "0" : sumAfter;
                oldBillSumAfterTextBox.Text = sumAfter;
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string paid = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select paid FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                paid = (string.IsNullOrEmpty(paid)) ? "0" : paid;
                oldBillPaidTextBox.Text = paid;
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string rest = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select rest FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                rest = (string.IsNullOrEmpty(rest)) ? "0" : rest;
                oldBillRestTextBox.Text = rest;
                conDataBase.Close();


                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string safe = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select safeName FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                safe = (string.IsNullOrEmpty(safe)) ? "0" : safe;
                safeComboBox.Text = safe;
                conDataBase.Close();


                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string store = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select storeName FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                store = (string.IsNullOrEmpty(store)) ? "0" : store;
                storeNameTextBox.Text = store;
                conDataBase.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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



        private void billDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
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
                double a, b, f;
                double c;
                double.TryParse(billSumBeforeTextBox.Text, out a);
                double.TryParse(billTransportTextBox.Text, out f);
                double.TryParse(billDiscountAmountTextBox.Text, out b);

                c = Convert.ToDouble(a + f - b);
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

        private void billTransportTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
        }
        private void billDiscountAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            billSumFunction();
        }

        private void billSumAfterTextBox_TextChanged(object sender, EventArgs e)
        {
            paidTextBox.Text = billSumAfterTextBox.Text;
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            billSumBeforeTextBox.Text = (string.IsNullOrEmpty(billSumBeforeTextBox.Text)) ? "0" : billSumBeforeTextBox.Text;
            billTransportTextBox.Text = (string.IsNullOrEmpty(billTransportTextBox.Text)) ? "0" : billTransportTextBox.Text;
            billSumAfterTextBox.Text = (string.IsNullOrEmpty(billSumAfterTextBox.Text)) ? "0" : billSumAfterTextBox.Text;
            paidTextBox.Text = (string.IsNullOrEmpty(paidTextBox.Text)) ? "0" : paidTextBox.Text;
            restTextBox.Text = (string.IsNullOrEmpty(restTextBox.Text)) ? "0" : restTextBox.Text;

            string storeNameForEdit = "";
            string oldSupplierName = "";
            string oldRest = "";
            string oldSafeTransactionID = "";

            string storeNameForBillOption;
            string supplierNameForBillOption;

            if (returnedBillOption == "بدون فاتورة")
            {
                storeNameForBillOption = storeNameComboBox.Text;
                supplierNameForBillOption = supplierNameComboBox.Text;
            }
            else
            {
                storeNameForBillOption = storeNameTextBox.Text;
                supplierNameForBillOption = supplierNameTextBox.Text;
            }

            if (returnedType == "تعديل")
            {
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                storeNameForEdit = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + returnedCodeTextBox.Text + "') BEGIN select storeName FROM returnedPurchasesMainTable where Id = N'" + Int32.Parse(returnedCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                oldSupplierName = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + returnedCodeTextBox.Text + "') BEGIN select SupplierName FROM returnedPurchasesMainTable where Id = N'" + Int32.Parse(returnedCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                //rest to subtract it if adjust
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                oldRest = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + returnedCodeTextBox.Text + "') BEGIN select rest FROM returnedPurchasesMainTable where Id = N'" + Int32.Parse(returnedCodeTextBox.Text) + "' END", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();


                //old safe ID 
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                oldSafeTransactionID = new SqlCommand("IF EXISTS (select 1 FROM safeTable where billNo = N'" + returnedCodeTextBox.Text + "' and type='returnedPurchases' and details ='in') BEGIN select Id FROM safeTable where billNo = N'" + returnedCodeTextBox.Text + "' and type='returnedPurchases' and details ='in' END", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
            }

            try
            {

                string Query = "IF NOT EXISTS (select 1 FROM returnedPurchasesMainTable where Id= N'" + this.returnedCodeTextBox.Text + "') BEGIN INSERT INTO returnedPurchasesMainTable(returnedType,billNumber,storeName,supplierName,safeName,sumBefore,discountAmount,transport,sum,paid,rest,date) VALUES (N'" + this.returnedBillOption + "',N'" + this.purchasesCodeTextBox.Text + "',N'" + storeNameForBillOption + "',N'" + supplierNameForBillOption + "',N'" + this.safeComboBox.Text + "',N'" + this.billSumBeforeTextBox.Text + "',N'" + this.billDiscountAmountTextBox.Text + "',N'" + this.billTransportTextBox.Text + "',N'" + this.billSumAfterTextBox.Text + "',N'" + this.paidTextBox.Text + "',N'" + this.restTextBox.Text + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "') END ELSE BEGIN UPDATE returnedPurchasesMainTable SET returnedType=N'" + returnedBillOption + "',billNumber=N'" + this.purchasesCodeTextBox.Text + "' ,storeName =N'" + storeNameForBillOption + "',supplierName=N'" + supplierNameForBillOption + "',safeName=N'" + this.safeComboBox.Text + "',sumBefore=N'" + this.billSumBeforeTextBox.Text + "',discountAmount=N'" + this.billDiscountAmountTextBox.Text + "',transport=N'" + this.billTransportTextBox.Text + "',sum=N'" + this.billSumAfterTextBox.Text + "',paid=N'" + this.paidTextBox.Text + "',rest=N'" + this.restTextBox.Text + "',date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' where Id=N'" + this.returnedCodeTextBox.Text + "' END ";

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

                    double supplierBalance = 0;
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from supplierTable where name=N'" + supplierNameForBillOption + "') BEGIN SELECT balance from supplierTable where name=N'" + supplierNameForBillOption + "' END", conDataBase).ExecuteScalar().ToString();

                    if (supplierBalanceString != "")
                    {
                        supplierBalance = Convert.ToDouble(supplierBalanceString);
                        conDataBase.Close();
                    }

                    supplierBalance -= Convert.ToDouble(this.restTextBox.Text);

                    Query = "IF EXISTS (select 1 from supplierTable where name=N'" + supplierNameForBillOption + "')BEGIN UPDATE supplierTable SET balance=N'" + supplierBalance + "' where name=N'" + supplierNameForBillOption + "' END";
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


                if (returnedType == "جديد")
                {

                    //الخزنة

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.paidTextBox.Text + "','returnedPurchases',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.returnedCodeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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

                        Query = "INSERT INTO returnedPurchasesSubTable(returnedCode,categoryCode,unit,quantity,purchasePrice,discountRate,discountAmount,sum) VALUES (N'" + Int32.Parse(this.returnedCodeTextBox.Text) + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[3].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[4].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[5].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[6].Value.ToString() + "',N'" + this.billDGV.Rows[i].Cells[7].Value.ToString() + "')";
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

                        int quantityInt = 0;
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string quantityString = new SqlCommand("IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + storeNameForBillOption + "') BEGIN SELECT quantity from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + storeNameForBillOption + "' END", conDataBase).ExecuteScalar().ToString();

                        if (quantityString != "")
                        {
                            quantityInt = Convert.ToInt32(quantityString);
                            conDataBase.Close();
                        }

                        quantityInt -= Convert.ToInt32(this.billDGV.Rows[i].Cells[3].Value.ToString());

                        if (quantityInt < 0)
                        {
                            MessageBox.Show("كمية ال'" + this.billDGV.Rows[i].Cells[1].Value.ToString() + "' أقل من الصفر, ستسجل القيمة بالسالب");
                        }

                        Query = "IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "')BEGIN UPDATE CategoryQuantityTable SET quantity=N'" + quantityInt + "' where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + storeNameForBillOption + "' END ELSE BEGIN INSERT INTO CategoryQuantityTable(storeName,quantity,categoryNumber) VALUES(N'" + storeNameForBillOption + "',N'" + quantityInt + "',N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' ) END";
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



                else if (returnedType == "تعديل")
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

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.paidTextBox.Text + "','returnedPurchases',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.returnedCodeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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


                        int quantityInt = 0;
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string quantityString = new SqlCommand("IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + storeNameForEdit + "') BEGIN SELECT quantity from CategoryQuantityTable where categoryNumber=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + storeNameForEdit + "' END", conDataBase).ExecuteScalar().ToString();

                        if (quantityString != "")
                        {
                            quantityInt = Convert.ToInt32(quantityString);
                            conDataBase.Close();
                        }

                        quantityInt -= Convert.ToInt32(this.editDGV.Rows[i].Cells[3].Value.ToString());

                        Query = "IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + storeNameForEdit + "') BEGIN UPDATE CategoryQuantityTable SET quantity=N'" + quantityInt + "' where categoryNumber=N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + storeNameForEdit + "' END ELSE BEGIN INSERT INTO CategoryQuantityTable(storeName,quantity,categoryNumber) VALUES(N'" + storeNameForEdit + "',N'" + quantityInt + "',N'" + this.editDGV.Rows[i].Cells[0].Value.ToString() + "' ) END";
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

                        quantityInt = 0;
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        quantityString = new SqlCommand("IF EXISTS (select 1 from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "') BEGIN SELECT quantity from CategoryQuantityTable where categoryNumber=N'" + this.billDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.storeNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                        if (quantityString != "")
                        {
                            quantityInt = Convert.ToInt32(quantityString);
                            conDataBase.Close();
                        }

                        quantityInt += Convert.ToInt32(this.billDGV.Rows[i].Cells[3].Value.ToString());

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
                MessageBox.Show("حفظ");
                fill();
                fillCategoryDGV();
                clearAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void searchReturnedTextBox_Click(object sender, EventArgs e)
        {

            //TODO still the adjustment buton isn't complete, this button will show the data efficintly but after finishing the eadjusting and pressing the save button it won't save efficiently also this page isn't tested enough
            addCategoryButton.Visible = true;

            clearAll();

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string billType = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + this.returnedCodeTextBox.Text + "') BEGIN select returnedType FROM returnedPurchasesMainTable where Id = N'" + this.returnedCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
            billType = (string.IsNullOrEmpty(billType)) ? "0" : billType;
            conDataBase.Close();
            string Query;
            SqlCommand cmdDataBase;

            if (billType == "بدون فاتورة")
            {
                topLeftLabel.Text = "الأصناف المسجلة";
                returnedBillOption = "بدون فاتورة";
                label19.Visible = false;
                purchasesCodeTextBox.Visible = false;
                searchPurchasesBill.Visible = false;
                returnAllBillButton.Visible = false;
                oldBillDGV.Visible = false;
                oldBillGroupBox.Visible = false;
                categoryDGV.Visible = true;
                label12.Visible = true;
                label5.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                categoryCodeSearchTextBox.Visible = true;
                categoryNameSearchTextBox.Visible = true;
                categoryStoreCodeSearchTextBox.Visible = true;
                unitTextBox.Visible = false;
                unitComboBox.Visible = true;
                supplierNameTextBox.Visible = false;
                supplierCodeTextBox.BackColor = Color.FromArgb(41, 44, 51);
                supplierCodeTextBox.ReadOnly = false;
                storeNameTextBox.Visible = false;
                supplierNameComboBox.Visible = true;
                storeNameComboBox.Visible = true;
                purchasesCodeTextBox.Text = "-";

                Query = "IF EXISTS (select 1 from returnedPurchasesSubTable where returnedCode=N'" + this.returnedCodeTextBox.Text + "') BEGIN select returnedPurchasesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الكود',returnedPurchasesSubTable.unit as 'الوحدة', returnedPurchasesSubTable.quantity as 'الكمية',returnedPurchasesSubTable.purchasePrice as 'سعر الشراء', returnedPurchasesSubTable.discountRate as'نسبة الخصم', returnedPurchasesSubTable.discountAmount as 'قيمة الخصم', returnedPurchasesSubTable.sum as 'الإجمالي' from returnedPurchasesSubTable,categoryTable where categoryTable.Id = returnedPurchasesSubTable.categoryCode and returnedCode=N'" + this.returnedCodeTextBox.Text + "' END;";

                conDataBase = new SqlConnection(constring);
                cmdDataBase = new SqlCommand(Query, conDataBase);

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

                for (int i = 0; i <= editDGV.Rows.Count - 1; i++)
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
                    string supplierName = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + Int32.Parse(this.returnedCodeTextBox.Text) + "') BEGIN select supplierName FROM returnedPurchasesMainTable where Id = N'" + Int32.Parse(this.returnedCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    supplierName = (string.IsNullOrEmpty(supplierName)) ? "0" : supplierName;
                    supplierNameComboBox.Text = supplierName;

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string storeName = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + Int32.Parse(returnedCodeTextBox.Text) + "') BEGIN select storeName FROM returnedPurchasesMainTable where Id = N'" + Int32.Parse(returnedCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    storeName = (string.IsNullOrEmpty(storeName)) ? "0" : storeName;
                    storeNameComboBox.Text = storeName;

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string safeName = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + Int32.Parse(returnedCodeTextBox.Text) + "') BEGIN select safeName FROM returnedPurchasesMainTable where Id = N'" + Int32.Parse(returnedCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    safeName = (string.IsNullOrEmpty(safeName)) ? "0" : safeName;
                    safeComboBox.Text = safeName;

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string billSum = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + Int32.Parse(returnedCodeTextBox.Text) + "') BEGIN select sumBefore FROM returnedPurchasesMainTable where Id = N'" + Int32.Parse(returnedCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    billSum = (string.IsNullOrEmpty(billSum)) ? "0" : billSum;
                    billSumBeforeTextBox.Text = billSum;

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string discountAmount = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + returnedCodeTextBox.Text + "') BEGIN select discountAmount FROM returnedPurchasesMainTable where Id = N'" + returnedCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    discountAmount = (string.IsNullOrEmpty(discountAmount)) ? "0" : discountAmount;
                    billDiscountAmountTextBox.Text = discountAmount;
                    conDataBase.Close();

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string transport = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + returnedCodeTextBox.Text + "') BEGIN select transport FROM returnedPurchasesMainTable where Id = N'" + returnedCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    transport = (string.IsNullOrEmpty(transport)) ? "0" : transport;
                    billTransportTextBox.Text = transport;
                    conDataBase.Close();

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string sumAfter = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + returnedCodeTextBox.Text + "') BEGIN select sum FROM returnedPurchasesMainTable where Id = N'" + returnedCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    sumAfter = (string.IsNullOrEmpty(sumAfter)) ? "0" : sumAfter;
                    billSumAfterTextBox.Text = sumAfter;
                    conDataBase.Close();

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string paid = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + returnedCodeTextBox.Text + "') BEGIN select paid FROM returnedPurchasesMainTable where Id = N'" + returnedCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    paid = (string.IsNullOrEmpty(paid)) ? "0" : paid;
                    paidTextBox.Text = paid;
                    conDataBase.Close();

                }

                catch (Exception ex)
                {
 
                }

            }


            else
            {

                purchasesCodeTextBox.Visible = true;
                purchasesCodeTextBox.BackColor = Color.FromArgb(64, 64, 64);
                purchasesCodeTextBox.ReadOnly = true;
                topLeftLabel.Text = "الفاتورة المسجلة";
                returnedBillOption = "بفاتورة";
                label19.Visible = true;
                returnAllBillButton.Visible = true;
                oldBillDGV.Visible = true;
                oldBillGroupBox.Visible = true;
                categoryDGV.Visible = false;
                unitTextBox.Visible = true;
                unitComboBox.Visible = false;
                supplierNameTextBox.Visible = true;
                supplierCodeTextBox.BackColor = Color.FromArgb(64, 64, 64);
                supplierCodeTextBox.ReadOnly = true;
                supplierCodeTextBox.Text = "";
                balanceTextBox.Text = "";
                storeNameTextBox.Visible = true;
                supplierNameComboBox.Visible = false;
                storeNameComboBox.Visible = false;
                purchasesCodeTextBox.Text = "";


                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string billNumber = new SqlCommand("IF EXISTS (select 1 FROM returnedPurchasesMainTable where Id = N'" + this.returnedCodeTextBox.Text + "') BEGIN select billNumber FROM returnedPurchasesMainTable where Id = N'" + this.returnedCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                billNumber = (string.IsNullOrEmpty(billNumber)) ? "0" : billNumber;
                purchasesCodeTextBox.Text = billNumber;
                conDataBase.Close();


                Query = "IF EXISTS (select 1 from purchasesSubTable where billCode=N'" + this.purchasesCodeTextBox.Text + "') BEGIN select purchasesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الكود',purchasesSubTable.unit as 'الوحدة', purchasesSubTable.quantity as 'الكمية',purchasesSubTable.purchasePrice as 'سعر الشراء', purchasesSubTable.discountRate as'نسبة الخصم', purchasesSubTable.discountAmount as 'قيمة الخصم', purchasesSubTable.sum as 'الإجمالي' from purchasesSubTable,categoryTable where categoryTable.Id = purchasesSubTable.categoryCode and billCode=N'" + this.purchasesCodeTextBox.Text + "' END;";

                conDataBase = new SqlConnection(constring);
                cmdDataBase = new SqlCommand(Query, conDataBase);

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

                for (int i = 0; i <= editDGV.Rows.Count - 1; i++)
                {
                    try
                    {
                        oldBillDGV.Rows.Add(editDGV.Rows[i].Cells[0].Value.ToString(), editDGV.Rows[i].Cells[1].Value.ToString(), editDGV.Rows[i].Cells[2].Value.ToString(),
                            editDGV.Rows[i].Cells[3].Value.ToString(), editDGV.Rows[i].Cells[4].Value.ToString(), editDGV.Rows[i].Cells[5].Value.ToString(),
                            editDGV.Rows[i].Cells[6].Value.ToString(), editDGV.Rows[i].Cells[7].Value.ToString());
                    }
                    catch { }
                }


                try
                {


                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string supplierName = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "') BEGIN select supplierName FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    supplierName = (string.IsNullOrEmpty(supplierName)) ? "0" : supplierName;
                    supplierNameTextBox.Text = supplierName;

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string billSum = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "') BEGIN select sumBefore FROM purchasesMainTable where Id = N'" + Int32.Parse(purchasesCodeTextBox.Text) + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    billSum = (string.IsNullOrEmpty(billSum)) ? "0" : billSum;
                    oldBillSumBeforeTextBox.Text = billSum;

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string discountRate = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select discountPercentage FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    discountRate = (string.IsNullOrEmpty(discountRate)) ? "0" : discountRate;
                    oldBillDiscountRateTextBox.Text = discountRate;
                    conDataBase.Close();

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string discountAmount = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select discountAmount FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    discountAmount = (string.IsNullOrEmpty(discountAmount)) ? "0" : discountAmount;
                    oldBillDiscountAmountTextBox.Text = discountAmount;
                    conDataBase.Close();


                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string salesTax = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select salesTax FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    salesTax = (string.IsNullOrEmpty(salesTax)) ? "0" : salesTax;
                    oldBillSalesTaxTextBox.Text = salesTax;
                    conDataBase.Close();

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string transport = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select transport FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    transport = (string.IsNullOrEmpty(transport)) ? "0" : transport;
                    oldBillTransportTextBox.Text = transport;
                    conDataBase.Close();

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string sumAfter = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select sumAfter FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    sumAfter = (string.IsNullOrEmpty(sumAfter)) ? "0" : sumAfter;
                    oldBillSumAfterTextBox.Text = sumAfter;
                    conDataBase.Close();

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string paid = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select paid FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    paid = (string.IsNullOrEmpty(paid)) ? "0" : paid;
                    oldBillPaidTextBox.Text = paid;
                    conDataBase.Close();

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string rest = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select rest FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    rest = (string.IsNullOrEmpty(rest)) ? "0" : rest;
                    oldBillRestTextBox.Text = rest;
                    conDataBase.Close();


                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string safe = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select safeName FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    safe = (string.IsNullOrEmpty(safe)) ? "0" : safe;
                    safeComboBox.Text = safe;
                    conDataBase.Close();


                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string store = new SqlCommand("IF EXISTS (select 1 FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "') BEGIN select storeName FROM purchasesMainTable where Id = N'" + purchasesCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    store = (string.IsNullOrEmpty(store)) ? "0" : store;
                    storeNameTextBox.Text = store;
                    conDataBase.Close();
                }

                catch (Exception ex)
                {
 
                }

                Query = "IF EXISTS (select 1 from returnedPurchasesSubTable where returnedCode=N'" + this.returnedCodeTextBox.Text + "') BEGIN select returnedPurchasesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الكود',returnedPurchasesSubTable.unit as 'الوحدة', returnedPurchasesSubTable.quantity as 'الكمية',returnedPurchasesSubTable.purchasePrice as 'سعر الشراء', returnedPurchasesSubTable.discountRate as'نسبة الخصم', returnedPurchasesSubTable.discountAmount as 'قيمة الخصم', returnedPurchasesSubTable.sum as 'الإجمالي' from returnedPurchasesSubTable,categoryTable where categoryTable.Id = returnedPurchasesSubTable.categoryCode and returnedCode=N'" + this.returnedCodeTextBox.Text + "' END;";

                conDataBase = new SqlConnection(constring);
                cmdDataBase = new SqlCommand(Query, conDataBase);

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

                for (int i = 0; i <= editDGV.Rows.Count - 1; i++)
                {
                    try
                    {
                        billDGV.Rows.Add(editDGV.Rows[i].Cells[0].Value.ToString(), editDGV.Rows[i].Cells[1].Value.ToString(), editDGV.Rows[i].Cells[2].Value.ToString(),
                            editDGV.Rows[i].Cells[3].Value.ToString(), editDGV.Rows[i].Cells[4].Value.ToString(), editDGV.Rows[i].Cells[5].Value.ToString(),
                            editDGV.Rows[i].Cells[6].Value.ToString(), editDGV.Rows[i].Cells[7].Value.ToString());
                    }
                    catch { }

                }
            }
        }

        private void returnAllBillButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (billDGV.Rows.Count == 1)
                {
                    for (int i = 0; i <= billDGV.Rows.Count - 1; i++)
                    {
                        DataGridViewRow row = oldBillDGV.Rows[i];

                        billDGV.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), row.Cells[7].Value.ToString());
                        clear();
                    }
                }

                else MessageBox.Show("لا يمكن إضافة فاتورة كاملة على اي عناصر اخرى, أزل أي عناصر في الفاتورة");

            }
            catch { }
    }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            clear();
            clearAll();

            fill();
            fillCategoryDGV();
            withBillRadioButton.Checked = true;
            newReturnedRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;
            searchReturnedTextBox.Visible = false;
            categoryDGV.Visible = false;
        }

        public void refreshLocal() {
            clear();
            clearAll();
            fill();
            fillCategoryDGV();
            withBillRadioButton.Checked = true;
            newReturnedRadioButton.Checked = true;
            constantPriceRadioButton.Checked = true;
            searchReturnedTextBox.Visible = false;
            categoryDGV.Visible = false;
            if (withoutBillRadioButton.Checked)
            {
                this.ActiveControl = categoryCodeTextBox;
            }
        }

        private void returnedPurchasesUC_Load(object sender, EventArgs e)
        {
            if (withoutBillRadioButton.Checked)
            {
                this.ActiveControl = categoryCodeTextBox;
            }
        }

        private void categoryCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (withoutBillRadioButton.Checked)
            {
                if (e.KeyChar == (char)13)
                {
                    addCategoryButton_Click(sender, e);
                }
            }
        }
    }
}