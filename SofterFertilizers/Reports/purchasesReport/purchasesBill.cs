using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class purchasesBill : Form
    {
        int billNumber;

        public purchasesBill(int bill)
        {
            InitializeComponent();
            billNumber = bill;
            fill();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {
            string Query = "select purchasesMainTable.Id as 'كود الفاتورة', purchasesMainTable.paymentType as 'نوع الدفع', purchasesMainTable.storeName as 'اسم المخزن' ,purchasesMainTable.supplierName as 'اسم المورّد',purchasesMainTable.safeName as 'الخزنة', purchasesMainTable.buyingType as 'نوع الفاتورة',purchasesMainTable.sumBefore as 'الإجمالي قبل' ,purchasesMainTable.discountPercentage as 'نسبة الخصم' ,purchasesMainTable.discountAmount as 'قيمة الخصم' ,purchasesMainTable.salesTax as 'ضريبة المبيعات' ,purchasesMainTable.transport as 'النقل' ,purchasesMainTable.sumAfter as 'الإجمالي بعد',purchasesMainTable.paid as 'المدفوع' ,purchasesMainTable.rest as 'المتبقي',purchasesMainTable.date as 'التاريخ' from purchasesMainTable where Id =N'" + billNumber + "';";

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
                mainDetailsDGV.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            Query = "select purchasesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', purchasesSubTable.unit as 'الوحدة',purchasesSubTable.quantity as 'الكمية', purchasesSubTable.purchasePrice as 'سعر الشراء' , purchasesSubTable.discountRate as 'نسبة الخصم', purchasesSubTable.discountAmount as 'قيمة الخصم',  purchasesSubTable.sum as 'الإجمالي'  from purchasesSubTable,categoryTable where purchasesSubTable.categoryCode=categoryTable.Id and billCode=N'" + billNumber + "' ;";

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
                contentDGV.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }

}

