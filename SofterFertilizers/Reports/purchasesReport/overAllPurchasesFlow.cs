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

namespace SofterFertilizers.Reports.purchasesReport
{
    public partial class overAllPurchasesFlow : UserControl
    {
        public overAllPurchasesFlow()
        {
            InitializeComponent();
            billTypeComboBox.Items.Add("الكل");
            billTypeComboBox.Items.Add("آجل");
            billTypeComboBox.Items.Add("كاش");
            billTypeComboBox.Text = "الكل";
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;

            if (billTypeComboBox.Text == "الكل")
            {
                string Query = "select distinct purchasesMainTable.Id as 'كود الفاتورة', purchasesMainTable.paymentType as 'نوع الدفع', purchasesMainTable.storeName as 'اسم المخزن' ,purchasesMainTable.supplierName as 'اسم المورّد', purchasesMainTable.buyingType as 'نوع الفاتورة',purchasesMainTable.sumBefore as 'الإجمالي قبل' ,purchasesMainTable.discountPercentage as 'نسبة الخصم' ,purchasesMainTable.discountAmount as 'قيمة الخصم' ,purchasesMainTable.salesTax as 'ضريبة المبيعات' ,purchasesMainTable.transport as 'النقل' ,purchasesMainTable.sumAfter as 'الإجمالي بعد',purchasesMainTable.paid as 'المدفوع' ,purchasesMainTable.rest as 'المتبقي',purchasesMainTable.date as 'التاريخ'  from purchasesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ;";

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
            }
            else if (billTypeComboBox.Text == "آجل")
            {
                string Query = "select distinct purchasesMainTable.Id as 'كود الفاتورة', purchasesMainTable.paymentType as 'نوع الدفع', purchasesMainTable.storeName as 'اسم المخزن' ,purchasesMainTable.supplierName as 'اسم المورّد', purchasesMainTable.buyingType as 'نوع الفاتورة',purchasesMainTable.sumBefore as 'الإجمالي قبل' ,purchasesMainTable.discountPercentage as 'نسبة الخصم' ,purchasesMainTable.discountAmount as 'قيمة الخصم' ,purchasesMainTable.salesTax as 'ضريبة المبيعات' ,purchasesMainTable.transport as 'النقل' ,purchasesMainTable.sumAfter as 'الإجمالي بعد',purchasesMainTable.paid as 'المدفوع' ,purchasesMainTable.rest as 'المتبقي',purchasesMainTable.date as 'التاريخ'  from purchasesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and paymentType=N'آجل';";

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
            }
            else if (billTypeComboBox.Text == "كاش")
            {
                string Query = "select distinct purchasesMainTable.Id as 'كود الفاتورة', purchasesMainTable.paymentType as 'نوع الدفع', purchasesMainTable.storeName as 'اسم المخزن' ,purchasesMainTable.supplierName as 'اسم المورّد', purchasesMainTable.buyingType as 'نوع الفاتورة',purchasesMainTable.sumBefore as 'الإجمالي قبل' ,purchasesMainTable.discountPercentage as 'نسبة الخصم' ,purchasesMainTable.discountAmount as 'قيمة الخصم' ,purchasesMainTable.salesTax as 'ضريبة المبيعات' ,purchasesMainTable.transport as 'النقل' ,purchasesMainTable.sumAfter as 'الإجمالي بعد',purchasesMainTable.paid as 'المدفوع' ,purchasesMainTable.rest as 'المتبقي',purchasesMainTable.date as 'التاريخ'  from purchasesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and paymentType=N'كاش';";

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
            }
        }

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {

                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];

                    purchasesBill purchasesBill = new purchasesBill(Convert.ToInt32(row.Cells[0].Value.ToString()));
                    purchasesBill.Show();
                    purchasesBill.BringToFront();

                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
