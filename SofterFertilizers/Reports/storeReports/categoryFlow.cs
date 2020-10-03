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

namespace SofterFertilizers.Reports.storeReports
{
    public partial class categoryFlow : UserControl
    {
        public categoryFlow()
        {
            InitializeComponent();
            fill();
            fillCategoryDGV();
        }

        string categoryCode = "";

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;


        void fill()
        {

            //store Combo Boxes
            storeNameComboBox.Items.Clear();
            storeNameComboBox.Items.Add("كل المخازن");
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

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            selectedDGV.Rows.Clear();
            selectedDGV.Refresh();

            DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
            categoryCode = row.Cells[0].Value.ToString();
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

        private void categoryCodeSearchTextBox_TextChanged_1(object sender, EventArgs e)
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

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            if (storeNameComboBox.Text == "كل المخازن")
            {


                selectedDGV.Rows.Clear();
                selectedDGV.Refresh();

                salesDGV.DataSource = null;
                salesDGV.Refresh();

                string Query = "select billCode ,categoryCode,unit , quantity, purchasePrice, salesSubTable.discountRate , salesSubTable.discountAmount, salesSubTable.sum from salesSubTable,salesMainTable where salesSubTable.billCode = salesMainTable.Id and categoryCode = '" + this.categoryCode + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'; ";

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
                    salesDGV.DataSource = bSource;
                    sda.Update(dbdataset);

                }
                catch (Exception ex)
                {
 
                }

                conDataBase.Close();

                purchasesDGV.DataSource = null;
                purchasesDGV.Refresh();

                Query = "select billCode ,categoryCode,unit , quantity, purchasePrice, purchasesSubTable.discountRate , purchasesSubTable.discountAmount, purchasesSubTable.sum from purchasesSubTable,purchasesMainTable where purchasesSubTable.billCode = purchasesMainTable.Id and categoryCode = '" + this.categoryCode + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'; ";

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
                    purchasesDGV.DataSource = bSource;
                    sda.Update(dbdataset);

                }
                catch (Exception ex)
                {
 
                }

                conDataBase.Close();

                returnedPurchasesDGV.DataSource = null;
                returnedPurchasesDGV.Refresh();

                Query = "select returnedCode ,categoryCode,unit , quantity, purchasePrice, returnedPurchasesSubTable.discountRate , returnedPurchasesSubTable.discountAmount, returnedPurchasesSubTable.sum from returnedPurchasesSubTable,returnedPurchasesMainTable where returnedPurchasesSubTable.returnedCode = returnedPurchasesMainTable.Id and categoryCode = '" + this.categoryCode + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'; ";

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
                    returnedPurchasesDGV.DataSource = bSource;
                    sda.Update(dbdataset);

                }
                catch (Exception ex)
                {
 
                }

                conDataBase.Close();

            }
            else
            {
                selectedDGV.Rows.Clear();
                selectedDGV.Refresh();

                salesDGV.DataSource = null;
                salesDGV.Refresh();

                string Query = "select billCode ,categoryCode,unit , quantity, purchasePrice, salesSubTable.discountRate , salesSubTable.discountAmount, salesSubTable.sum from salesSubTable,salesMainTable where salesSubTable.billCode = salesMainTable.Id and categoryCode = '" + this.categoryCode + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and storeName='"+this.storeNameComboBox.Text+"'; ";

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
                    salesDGV.DataSource = bSource;
                    sda.Update(dbdataset);

                }
                catch (Exception ex)
                {
 
                }

                conDataBase.Close();

                purchasesDGV.DataSource = null;
                purchasesDGV.Refresh();

                Query = "select billCode ,categoryCode,unit , quantity, purchasePrice, purchasesSubTable.discountRate , purchasesSubTable.discountAmount, purchasesSubTable.sum from purchasesSubTable,purchasesMainTable where purchasesSubTable.billCode = purchasesMainTable.Id and categoryCode = '" + this.categoryCode + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and storeName='" + this.storeNameComboBox.Text + "'; ";

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
                    purchasesDGV.DataSource = bSource;
                    sda.Update(dbdataset);

                }
                catch (Exception ex)
                {
 
                }

                conDataBase.Close();

                returnedPurchasesDGV.DataSource = null;
                returnedPurchasesDGV.Refresh();

                Query = "select returnedCode ,categoryCode,unit , quantity, purchasePrice, returnedPurchasesSubTable.discountRate , returnedPurchasesSubTable.discountAmount, returnedPurchasesSubTable.sum from returnedPurchasesSubTable,returnedPurchasesMainTable where returnedPurchasesSubTable.returnedCode = returnedPurchasesMainTable.Id and categoryCode = '" + this.categoryCode + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and storeName='" + this.storeNameComboBox.Text + "'; ";

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
                    returnedPurchasesDGV.DataSource = bSource;
                    sda.Update(dbdataset);

                }
                catch (Exception ex)
                {
 
                }

                conDataBase.Close();
            }
            if (salesDGV.Rows.Count > 0)
            {
                selectedDGV.Rows.Add();
                int row = selectedDGV.Rows.Count;
                selectedDGV.Rows.Add("", "", "","فواتير المبيعات");
                selectedDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;

                for (int i = 0; i < salesDGV.Rows.Count; i++)
                {
                    try
                    {
                        selectedDGV.Rows.Add(salesDGV.Rows[i].Cells[0].Value.ToString(), salesDGV.Rows[i].Cells[1].Value.ToString(), salesDGV.Rows[i].Cells[2].Value.ToString(), salesDGV.Rows[i].Cells[3].Value.ToString(), salesDGV.Rows[i].Cells[4].Value.ToString(),salesDGV.Rows[i].Cells[5].Value.ToString(), salesDGV.Rows[i].Cells[6].Value.ToString(), salesDGV.Rows[i].Cells[7].Value.ToString());
                        sum += Convert.ToDouble(salesDGV.Rows[i].Cells[3].Value.ToString());
                    }

                    catch { }
                }
                row = selectedDGV.Rows.Count;
                selectedDGV.Rows.Add("مجموع الكمية", "", "", sum.ToString());
                selectedDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                selectedDGV.Rows.Add();
            }

            if (purchasesDGV.Rows.Count > 0)
            {
                selectedDGV.Rows.Add();
                int row = selectedDGV.Rows.Count;
                selectedDGV.Rows.Add("", "","", "فواتير المشتريات");
                selectedDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;

                for (int i = 0; i < purchasesDGV.Rows.Count; i++)
                {
                    try
                    {
                        selectedDGV.Rows.Add(purchasesDGV.Rows[i].Cells[0].Value.ToString(), purchasesDGV.Rows[i].Cells[1].Value.ToString(), purchasesDGV.Rows[i].Cells[2].Value.ToString(), purchasesDGV.Rows[i].Cells[3].Value.ToString(), purchasesDGV.Rows[i].Cells[4].Value.ToString(), purchasesDGV.Rows[i].Cells[5].Value.ToString(), purchasesDGV.Rows[i].Cells[6].Value.ToString(), purchasesDGV.Rows[i].Cells[7].Value.ToString());
                        sum += Convert.ToDouble(purchasesDGV.Rows[i].Cells[3].Value.ToString());
                    }

                    catch { }
                }
                row = selectedDGV.Rows.Count;
                selectedDGV.Rows.Add("مجموع الكمية", "", "", sum.ToString());
                selectedDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                selectedDGV.Rows.Add();
            }

            if (returnedPurchasesDGV.Rows.Count > 0)
            {
                selectedDGV.Rows.Add();
                int row = selectedDGV.Rows.Count;
                selectedDGV.Rows.Add("", "", "","فواتير مرتجع المشتريات");
                selectedDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;

                for (int i = 0; i < returnedPurchasesDGV.Rows.Count; i++)
                {
                    try
                    {
                        selectedDGV.Rows.Add(returnedPurchasesDGV.Rows[i].Cells[0].Value.ToString(), returnedPurchasesDGV.Rows[i].Cells[1].Value.ToString(), returnedPurchasesDGV.Rows[i].Cells[2].Value.ToString(), returnedPurchasesDGV.Rows[i].Cells[3].Value.ToString(), returnedPurchasesDGV.Rows[i].Cells[4].Value.ToString(), returnedPurchasesDGV.Rows[i].Cells[5].Value.ToString(), returnedPurchasesDGV.Rows[i].Cells[6].Value.ToString(), returnedPurchasesDGV.Rows[i].Cells[7].Value.ToString());
                        sum += Convert.ToDouble(returnedPurchasesDGV.Rows[i].Cells[3].Value.ToString());
                    }

                    catch { }
                }
                row = selectedDGV.Rows.Count;
                selectedDGV.Rows.Add("مجموع الكمية", "","",sum.ToString());
                selectedDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                selectedDGV.Rows.Add();
            }
            if (selectedDGV.Rows.Count > 1)
            {
                selectedDGV.Rows.RemoveAt(selectedDGV.Rows.Count - 1);
            }
        }
    }
    
}
