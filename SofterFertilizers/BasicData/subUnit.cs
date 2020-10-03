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

namespace SofterFertilizers.BasicData
{
    public partial class subUnit : UserControl
    {
        public subUnit()
        {
            InitializeComponent();
            fill();
            adjustButton.Visible = false;
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        string oldId;

        void clear()
        {
            sellingPriceTextBox.Text = "0";
            packagePriceTextBox.Text = "0";
            halfPackagePriceTextBox.Text = "0";
            buyingPriceTextBox.Text = "0";
            subUnitTextBox.Text = "0";
            mainUnitTextBox.Text = "1";
            ratioTextBox.Text = "0";
            IdTextBox.Text = "";
        }

        void fill()
        {

            //Category Combo Boxes
            categoryNameComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct categoryName from categoryTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    categoryNameComboBox.Items.Add(dr["categoryName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();
            if (categoryNameComboBox.Items.Count > 0)
            {
                categoryNameComboBox.Text = categoryNameComboBox.Items[0].ToString();
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


            // unitDGV
            unitsDGV.DataSource = null ;

            Query = "select subUnitTable.id as 'كود الصنف بالوحدة الفرعية', categoryName as 'اسم الصنف' , subUnitTable.subUnit as 'الوحدة الفرعية' ,subUnitTable.sellingPrice as 'سعر البيع', subUnitTable.packagePrice as 'سعر الجملة' ,subUnitTable.halfPackagePrice as 'سعر النص جملة', subUnitTable.buyingPrice as 'سعر الشراء', subUnitTable.subUnitRep as 'الوحدة الفرعية', subUnitTable.mainUnitRep as 'الوحدة الرئيسية', subUnitTable.ratio as 'النسبة' from subUnitTable,categoryTable where categoryTable.Id = subUnitTable.categoryCode;";

            conDataBase = new SqlConnection(constring);
            SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                unitsDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            conDataBase.Close();

            categoryDGV.DataSource = null ;

            Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber;";

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
             // unitDGV
            unitsDGV.DataBindings.Clear();

           string Query = "select subUnitTable.id as 'كود الصنف بالوحدة الفرعية', categoryName as 'اسم الصنف' , subUnitTable.subUnit as 'الوحدة الفرعية' ,subUnitTable.sellingPrice as 'سعر البيع', subUnitTable.packagePrice as 'سعر الجملة' ,subUnitTable.halfPackagePrice as 'سعر النص جملة', subUnitTable.buyingPrice as 'سعر الشراء', subUnitTable.subUnitRep as 'الوحدة الفرعية', subUnitTable.mainUnitRep as 'الوحدة الرئيسية', subUnitTable.ratio as 'النسبة' from subUnitTable,categoryTable where categoryTable.Id = subUnitTable.categoryCode and categoryName like N'%" + this.categoryNameSearchTextBox.Text + "%';;";

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
                unitsDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {

            }

            conDataBase.Close();
        }

        private void unitsDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            clear();
            adjustButton.Visible = true;
            categoryNameComboBox.Enabled = false;
            saveButton.Visible = false;
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.unitsDGV.Rows[e.RowIndex];
                    this.IdTextBox.Text = row.Cells[0].Value.ToString();
                    oldId = row.Cells[0].Value.ToString(); 
                    this.categoryNameComboBox.Text = row.Cells[1].Value.ToString();
                    this.unitComboBox.Text = row.Cells[2].Value.ToString();
                    this.sellingPriceTextBox.Text = row.Cells[3].Value.ToString();
                    this.packagePriceTextBox.Text = row.Cells[4].Value.ToString();
                    this.halfPackagePriceTextBox.Text = row.Cells[5].Value.ToString();
                    this.buyingPriceTextBox.Text = row.Cells[6].Value.ToString();
                    this.subUnitTextBox.Text = row.Cells[7].Value.ToString();
                    this.mainUnitTextBox.Text = row.Cells[8].Value.ToString();
                    this.ratioTextBox.Text = row.Cells[9].Value.ToString(); 
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string Query = "INSERT INTO subUnitTable(Id,categoryCode,subUnit,sellingPrice,packagePrice,halfPackagePrice,buyingPrice,subUnitRep,mainUnitRep,ratio) Values (N'"+this.IdTextBox.Text + "', (select Id from CategoryTable where categoryName = N'" + this.categoryNameComboBox.Text + "' ),N'" + this.unitComboBox.Text + "',N'" + this.sellingPriceTextBox.Text + "',N'" + this.packagePriceTextBox.Text + "',N'" + this.halfPackagePriceTextBox.Text + "',N'" + this.buyingPriceTextBox.Text + "',N'" +  float.Parse(this.subUnitTextBox.Text) + "',N'" + float.Parse(this.mainUnitTextBox.Text) + "',N'" + float.Parse(this.ratioTextBox.Text) + "')  ";

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
                MessageBox.Show("حفظ");
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
            fill();
        }

        private void adjustButton_Click(object sender, EventArgs e)
        {
            //TODO Required admin previlage to adjust
            if (true)
            {
                string Query = "IF EXISTS(select 1 FROM subUnitTable where Id = N'"+this.oldId+"' ) Begin UPDATE subUnitTable SET Id = N'" + this.IdTextBox.Text + "', subUnit=N'" + this.unitComboBox.Text + "',sellingPrice=N'" + this.sellingPriceTextBox.Text + "',packagePrice=N'" + this.packagePriceTextBox.Text + "',halfPackagePrice=N'" + this.halfPackagePriceTextBox.Text + "', buyingPrice=N'" + this.buyingPriceTextBox.Text + "',subUnitRep=N'" + float.Parse(this.subUnitTextBox.Text) + "', mainUnitRep=N'" + float.Parse(mainUnitTextBox.Text) + "', ratio=N'" + float.Parse(ratioTextBox.Text) + "' where Id=N'"+this.oldId+"'  END ELSE BEGIN INSERT INTO subUnitTable(Id,categoryCode,subUnit,sellingPrice,packagePrice,halfPackagePrice,buyingPrice,subUnitRep,mainUnitRep,ratio) Values (N'"+this.IdTextBox.Text+"',(select Id from CategoryTable where categoryName = N'" + this.categoryNameComboBox.Text + "' ),N'" + this.unitComboBox.Text + "',N'" + this.sellingPriceTextBox.Text + "',N'" + this.packagePriceTextBox.Text + "',N'" + this.halfPackagePriceTextBox.Text + "',N'" + this.buyingPriceTextBox.Text + "',N'" + float.Parse(this.subUnitTextBox.Text) + "',N'" + float.Parse(this.mainUnitTextBox.Text) + "',N'" + float.Parse(this.ratioTextBox.Text) + "') END  ;";
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

                saveButton.Visible = true;
                adjustButton.Visible = false;
                categoryNameComboBox.Enabled = true;
                clear();
                fill();

            }
            //TODO check if delete button is required
        }

        private void subUnitTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ratioTextBox.Text = (float.Parse(subUnitTextBox.Text) / float.Parse(mainUnitTextBox.Text)).ToString();

            }
            catch
            {

            }
        }

        private void mainUnitTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ratioTextBox.Text = (float.Parse(subUnitTextBox.Text) / float.Parse(mainUnitTextBox.Text)).ToString();


            }
            catch
            {
            }
        }
        public void refreshLocal()
        {
            fill();
            clear();
            adjustButton.Visible = false;
            saveButton.Visible = true;
            categoryNameComboBox.Enabled = true;
        }

        private void categoryNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear();
        }

        private void categoryCodeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            // unitDGV
            unitsDGV.DataBindings.Clear();

            string Query = "select subUnitTable.id as 'كود الصنف بالوحدة الفرعية', categoryName as 'اسم الصنف' , subUnitTable.subUnit as 'الوحدة الفرعية' ,subUnitTable.sellingPrice as 'سعر البيع', subUnitTable.packagePrice as 'سعر الجملة' ,subUnitTable.halfPackagePrice as 'سعر النص جملة', subUnitTable.buyingPrice as 'سعر الشراء', subUnitTable.subUnitRep as 'الوحدة الفرعية', subUnitTable.mainUnitRep as 'الوحدة الرئيسية', subUnitTable.ratio as 'النسبة' from subUnitTable,categoryTable where categoryTable.Id = subUnitTable.categoryCode and subUnitTable.Id like N'%" + this.categoryCodeSearchTextBox.Text + "%';;";

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
                unitsDGV.DataSource = bSource;
                sda.Update(dbdataset);
            conDataBase.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ratioTextBox_TextChanged(object sender, EventArgs e)
        {

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string mainSellingPrice = new SqlCommand("SELECT sellingPrice FROM categoryTable WHERE categoryName = N'" + this.categoryNameComboBox.Text + "'", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            mainSellingPrice = (string.IsNullOrEmpty(mainSellingPrice)) ? "0" : mainSellingPrice;

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string mainPackagePrice = new SqlCommand("SELECT packagePrice FROM categoryTable WHERE categoryName = N'" + this.categoryNameComboBox.Text + "'", conDataBase).ExecuteScalar().ToString();
            mainPackagePrice = (string.IsNullOrEmpty(mainPackagePrice)) ? "0" : mainPackagePrice;
            conDataBase.Close();

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string mainHalfPackagePrice = new SqlCommand("SELECT halfPackagePrice FROM categoryTable WHERE categoryName = N'" + this.categoryNameComboBox.Text + "'", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            mainHalfPackagePrice = (string.IsNullOrEmpty(mainHalfPackagePrice)) ? "0" : mainHalfPackagePrice;

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string mainBuyingPrice = new SqlCommand("SELECT buyingPrice FROM categoryTable WHERE categoryName = N'" + this.categoryNameComboBox.Text + "'", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            mainBuyingPrice = (string.IsNullOrEmpty(mainBuyingPrice)) ? "0" : mainBuyingPrice;

            l0.Text = (Convert.ToDouble(mainSellingPrice) / Convert.ToDouble(this.ratioTextBox.Text)).ToString();
            l1.Text = (Convert.ToDouble(mainPackagePrice) / Convert.ToDouble(this.ratioTextBox.Text)).ToString();
            l2.Text = (Convert.ToDouble(mainHalfPackagePrice) / Convert.ToDouble(this.ratioTextBox.Text)).ToString();
            l3.Text = (Convert.ToDouble(mainBuyingPrice) / Convert.ToDouble(this.ratioTextBox.Text)).ToString();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
         
            textBox1.Text = "";

            categoryDGV.DataBindings.Clear();

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة',buyingPrice as 'سعر البيع'  from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber  and categoryTable.categoryName like N'%" + this.textBox2.Text + "%';";

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = "";

            categoryDGV.DataBindings.Clear();

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'سعر القطاعي', packagePrice as 'سعر الجملة',halfPackagePrice as 'نص جملة',buyingPrice as 'سعر البيع'  from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber  and categoryTable.Id like N'%" + this.textBox1.Text + "%';";

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
    }
}
