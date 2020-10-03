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

namespace SofterFertilizers.Reports
{
    public partial class salesBill : Form
    {
        int billNumber;
        public salesBill(int bill)
        {
            InitializeComponent();
            billNumber = bill;
            fill();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;


        void fill()
        {
            string Query = "select salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل',salesMainTable.safeName as 'الخزنة', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumBefore as 'الإجمالي قبل' ,salesMainTable.discountPercentage as 'نسبة الخصم' ,salesMainTable.discountAmount as 'قيمة الخصم' ,salesMainTable.salesTax as 'ضريبة المبيعات' ,salesMainTable.transport as 'النقل' ,salesMainTable.sumAfter as 'الإجمالي بعد',salesMainTable.paid as 'المدفوع' ,salesMainTable.rest as 'المتبقي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',salesMainTable.debtsRatio as 'نسبة القسط' ,salesMainTable.debtsType as 'نوع التقسيط' from salesMainTable where Id =N'"+ billNumber + "';";

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

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string stringDebts = new SqlCommand("IF EXISTS (select 1 FROM salesMainTable where Id = N'" + billNumber + "') BEGIN select debts FROM salesMainTable where Id = N'" + billNumber+ "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();

            stringDebts = (string.IsNullOrEmpty(stringDebts)) ? "0" : stringDebts;
            bool debts;
            if (stringDebts == "0" || stringDebts == "False")
            {
                debts = false;

                label2.Visible = false;
                divisionDGV.Visible = false;
            }
            else
            {
                debts = true;
                label2.Visible = true;
                divisionDGV.Visible = true;
            }
            conDataBase.Close();

            
            Query = "select salesSubTable.categoryCode as 'كود الصنف', categoryTable.categoryName as 'اسم الصنف', salesSubTable.unit as 'الوحدة',salesSubTable.quantity as 'الكمية', salesSubTable.purchasePrice as 'سعر البيع' , salesSubTable.discountRate as 'نسبة الخصم', salesSubTable.discountAmount as 'قيمة الخصم',  salesSubTable.sum as 'الإجمالي'  from salesSubTable,categoryTable where salesSubTable.categoryCode=categoryTable.Id and billCode=N'"+ billNumber + "' ;";

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
            if (debts)
            {

                Query = "select sum as 'إجمالي الفاتورة', first as 'مقدّم القسط',rest as 'المبلغ المتبقي' , debtsNumber as 'عدد الأقساط',debtAmount as 'مبلغ القسط' ,time as 'فترة القسط' , timeType as 'نوع الفترة' , startDate as 'بداية القسط'  from debtsMainTable where billNumber=N'" + billNumber + "' ;";

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
                    divisionDGV.DataSource = bSource;
                    sda.Update(dbdataset);
                }
                catch (Exception ex)
                {
 
                }
            }
        }
    }
}
