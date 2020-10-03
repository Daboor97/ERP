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
//TODO CLEAR ALL AFTER SAVING NOT FOUND HERE AT ALL 


namespace SofterFertilizers.BasicData
{
    public partial class sortUC : UserControl
    {
        DataTable quantitiyDT = new DataTable();
        DataTable categoryDt = new DataTable();

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        public sortUC()
        {
            InitializeComponent();
            fill();
            fillCategoryDGV();
            adjustButton.Visible = false;
            
        }

        void clear()
        {
            categoryCodeTextBox.Text = "";
            categoryNameTextBox.Text = "";
            categoryStoreCodeTextBox.Text = "";
            notesTextBox.Text = "";
            sellingPriceTextBox.Text = "0";
            packagePriceTextBox.Text = "0";
            halfPackagePriceTextBox.Text = "0";
            buyingPriceTextBox.Text = "0";
            topDsicountRateTextBox.Text = "0";
            highestBuyingQuantityTextBox.Text = "0";
            lowestQuantityTextBox.Text = "0";
            highestQuantityTextBox.Text = "0";
            quantityDGV.Rows.Clear();
            quantityDGV.Refresh();


        }

    
        

        void fill()
        {
            //Company Combo Boxes
            companyComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct companyName from companyTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    companyComboBox.Items.Add(dr["companyName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();
            if (companyComboBox.Items.Count > 0)
            {
                companyComboBox.Text = companyComboBox.Items[0].ToString();
            }

            //Unit Combo Boxes
            unitComboBox.Items.Clear();
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            Query = "select distinct unitName from unitTable;";
            dt = new DataTable();
            da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    unitComboBox.Items.Add(dr["unitName"].ToString());
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

            //store Combo Boxes
            storeNameComboBox.Items.Clear();
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            Query = "select distinct storeName from storeTable;";
            dt = new DataTable();
             da = new SqlDataAdapter(Query, conDataBase);
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

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', topDiscountRate 'أعلى نسبة خصم', highestBuyingQuantity as 'كمية البيع القصوى', lowestQuantity as 'أقل كمية', highestQuantity as  'أكثر كمية' from categoryTable;";

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

        private void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //type Combo Boxes
            typeCombeBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "IF EXISTS (select typeName from typeTable where companyName=N'" + this.companyComboBox.Text + "') BEGIN select distinct typeName from typeTable where companyName=N'" + this.companyComboBox.Text + "' END;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    typeCombeBox.Items.Add(dr["typeName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();
            if(typeCombeBox.Items.Count > 0)
            {
                typeCombeBox.Text = typeCombeBox.Items[0].ToString();
            }
        }

        private void addQuantityToStore_Click(object sender, EventArgs e)
        {
            if (quantityDGV.Rows.Count > 0)
            {
                for (int i = 0; i < quantityDGV.Rows.Count; i++)
                {
                    if (this.quantityDGV.Rows[i].Cells[0].Value != null)
                    {
                        if (storeNameComboBox.Text == this.quantityDGV.Rows[i].Cells[0].Value.ToString() && Int32.Parse(quantityStoreTextBox.Text) == Int32.Parse(this.quantityDGV.Rows[i].Cells[1].Value.ToString()))
                        {
                            MessageBox.Show("النوع موجود مسبقًا");
                            return;
                        }
                    }
                }
            }
            this.quantityDGV.Rows.Add(storeNameComboBox.Text, Int32.Parse(quantityStoreTextBox.Text));
        }

        private void saveCategoryButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(categoryCodeTextBox.Text))
            {
                if (sellingPriceTextBox.Text != "0" || packagePriceTextBox.Text != "0" || buyingPriceTextBox.Text != "0")
                {
                    try
                    {
                        string Query = "IF NOT EXISTS (select 1 FROM categoryTable where Id=N'" + this.categoryCodeTextBox.Text + "' and categoryName= N'" + this.categoryNameTextBox.Text + "'AND mainUnit= N'" + this.unitComboBox.Text + "'AND mainType = N'" + this.typeCombeBox.Text + "'AND storeCode = N'" + this.categoryStoreCodeTextBox.Text + "') BEGIN INSERT INTO categoryTable(Id,categoryName,companyName,mainUnit,mainType,storeCode,notes,sellingPrice,packagePrice,halfPackagePrice,buyingPrice,topDiscountRate,highestBuyingQuantity,lowestQuantity,highestQuantity) VALUES (N'" + this.categoryCodeTextBox.Text + "',N'" + this.categoryNameTextBox.Text + "',N'" + this.companyComboBox.Text + "',N'" + this.unitComboBox.Text + "',N'" + this.typeCombeBox.Text + "',N'" + this.categoryStoreCodeTextBox.Text + "',N'" + this.notesTextBox.Text + "',N'" + float.Parse(this.sellingPriceTextBox.Text) + "',N'" + float.Parse(this.packagePriceTextBox.Text) + "',N'" + float.Parse(this.halfPackagePriceTextBox.Text) + "',N'" + float.Parse(this.buyingPriceTextBox.Text) + "',N'" + Int32.Parse(this.topDsicountRateTextBox.Text) + "',N'" + Int32.Parse(this.highestBuyingQuantityTextBox.Text) + "',N'" + Int32.Parse(this.lowestQuantityTextBox.Text) + "',N'" + Int32.Parse(this.highestQuantityTextBox.Text) + "') END ";
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
                        for (int i = 0; i < quantityDGV.Rows.Count - 1; i++)
                        {
                            Query = "IF NOT EXISTS (select 1 FROM categoryQuantityTable where categoryNumber=N'" + this.categoryCodeTextBox.Text + "' and storeName=N'" + this.quantityDGV.Rows[i].Cells[0].Value + "' and quantity=N'" + Int32.Parse(this.quantityDGV.Rows[i].Cells[1].Value.ToString()) + "') BEGIN INSERT INTO categoryQuantityTable(storeName,quantity,categoryNumber) VALUES (N'" + this.quantityDGV.Rows[i].Cells[0].Value + "',N'" + Int32.Parse(this.quantityDGV.Rows[i].Cells[1].Value.ToString()) + "',N'" + this.categoryCodeTextBox.Text + "') END ";
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
                        clear();
                    }
                    catch (Exception ex)
                    {
     
                    }
                }
                else
                {
                    MessageBox.Show("اكمل أسعار الصنف");
                }
            }
            else
            {
                MessageBox.Show("ادخل كود الصنف");
            }
        }

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            saveCategoryButton.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            adjustButton.Visible = true;
            storeNameComboBox.Visible = false;
            quantityStoreTextBox.Visible = false;
            quantityDGV.Visible = false;
            addQuantityToStore.Visible = false;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
                this.categoryCodeTextBox.Text = row.Cells[0].Value.ToString();
                this.categoryNameTextBox.Text = row.Cells[1].Value.ToString();
                this.companyComboBox.Text = row.Cells[2].Value.ToString();
                this.unitComboBox.Text = row.Cells[3].Value.ToString();
                this.typeCombeBox.Text = row.Cells[4].Value.ToString();
                this.categoryStoreCodeTextBox.Text = row.Cells[5].Value.ToString();
                this.notesTextBox.Text = row.Cells[6].Value.ToString();
                this.sellingPriceTextBox.Text = row.Cells[7].Value.ToString();
                this.packagePriceTextBox.Text = row.Cells[8].Value.ToString();
                this.halfPackagePriceTextBox.Text = row.Cells[9].Value.ToString();
                this.buyingPriceTextBox.Text = row.Cells[10].Value.ToString();
                this.topDsicountRateTextBox.Text = row.Cells[11].Value.ToString();
                this.highestBuyingQuantityTextBox.Text = row.Cells[12].Value.ToString();
                this.lowestQuantityTextBox.Text = row.Cells[13].Value.ToString();
                this.highestQuantityTextBox.Text = row.Cells[14].Value.ToString();  
            }
        }

        private void adjustButton_Click(object sender, EventArgs e)
        {
            //TODO Required admin previlage to adjust
            //TODO

            if (true)
            {
                string Query = "IF EXISTS(select 1 from categoryTable where Id =N'" + this.categoryCodeTextBox.Text + "') BEGIN UPDATE categoryTable SET Id=N'"+this.categoryCodeTextBox.Text+ "', categoryName = N'" + this.categoryNameTextBox.Text + "',companyName=N'" + this.companyComboBox.Text + "',mainUnit=N'" + this.unitComboBox.Text + "',mainType=N'" + this.unitComboBox.Text + "',storeCode=N'" + this.categoryStoreCodeTextBox.Text + "',notes=N'" + this.notesTextBox.Text + "',sellingPrice=N'" + float.Parse(this.sellingPriceTextBox.Text) + "',packagePrice=N'" + float.Parse(this.packagePriceTextBox.Text) + "',halfPackagePrice=N'" + float.Parse(this.halfPackagePriceTextBox.Text) + "',buyingPrice=N'" + float.Parse(this.buyingPriceTextBox.Text) + "',topDiscountRate=N'" + float.Parse(this.topDsicountRateTextBox.Text) + "',highestbuyingQuantity=N'" + Int32.Parse(this.highestBuyingQuantityTextBox.Text) + "',lowestQuantity=N'" + Int32.Parse(this.lowestQuantityTextBox.Text) + "',highestQuantity=N'" + Int32.Parse(this.highestQuantityTextBox.Text) + "' where Id =N'" + this.categoryCodeTextBox.Text + "' END";
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
                MessageBox.Show("انتهى التعديل");

                saveCategoryButton.Visible = true;
                label24.Visible = true;
                label25.Visible = true;
                label26.Visible = true;
                adjustButton.Visible = false;
                storeNameComboBox.Visible = true;
                quantityStoreTextBox.Visible = true;
                quantityDGV.Visible = true;
                addQuantityToStore.Visible = true;
                fill();
                fillCategoryDGV();
                clear();
            }
            //TODO check if delete button is required
        }

        private void categoryCodeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryNameSearchTextBox.Text = "";
            categoryStoreCodeSearchTextBox.Text = "";

            categoryDGV.DataBindings.Clear();

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', topDiscountRate 'أعلى نسبة خصم', highestBuyingQuantity as 'كمية البيع القصوى', lowestQuantity as 'أقل كمية', highestQuantity as  'أكثر كمية' from categoryTable where Id like N'%" + this.categoryCodeSearchTextBox.Text + "%';";

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

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', topDiscountRate 'أعلى نسبة خصم', highestBuyingQuantity as 'كمية البيع القصوى', lowestQuantity as 'أقل كمية', highestQuantity as  'أكثر كمية' from categoryTable where categoryName like N'%" + this.categoryNameSearchTextBox.Text + "%';";

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

            string Query = "select id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', topDiscountRate 'أعلى نسبة خصم', highestBuyingQuantity as 'كمية البيع القصوى', lowestQuantity as 'أقل كمية', highestQuantity as  'أكثر كمية' from categoryTable where storeCode like N'%" + this.categoryStoreCodeSearchTextBox.Text + "%';";

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

        public void refreshButton_Click(object sender, EventArgs e)
        {
            fill();
            fillCategoryDGV();
            adjustButton.Visible = false;
            saveCategoryButton.Visible = true;
            label24.Visible = true;
            label25.Visible = true;
            label26.Visible = true;
            storeNameComboBox.Visible = true;
            quantityStoreTextBox.Visible = true;
            quantityDGV.Visible = true;
            addQuantityToStore.Visible = true;
            clear();
        }

        public void refresh() {
            fill();
            fillCategoryDGV();
            adjustButton.Visible = false;
            saveCategoryButton.Visible = true;
            label24.Visible = true;
            label25.Visible = true;
            label26.Visible = true;
            storeNameComboBox.Visible = true;
            quantityStoreTextBox.Visible = true;
            quantityDGV.Visible = true;
            addQuantityToStore.Visible = true;
            clear();
        }

        private void sortUC_Load(object sender, EventArgs e)
        {

        }
    }
}
