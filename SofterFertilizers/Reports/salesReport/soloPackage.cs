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

namespace SofterFertilizers.Reports
{
    public partial class soloPackage : UserControl
    {
        public soloPackage()
        {
            InitializeComponent();
            billTypeComboBox.Items.Add("قطاعي");
            billTypeComboBox.Items.Add("جملة");
            billTypeComboBox.Text = "جملة";
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumBefore as 'الإجمالي قبل' ,salesMainTable.discountPercentage as 'نسبة الخصم' ,salesMainTable.discountAmount as 'قيمة الخصم' ,salesMainTable.salesTax as 'ضريبة المبيعات' ,salesMainTable.transport as 'النقل' ,salesMainTable.sumAfter as 'الإجمالي بعد',salesMainTable.paid as 'المدفوع' ,salesMainTable.rest as 'المتبقي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',salesMainTable.debtsRatio as 'نسبة القسط' ,salesMainTable.debtsType as 'نوع التقسيط' from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and buyingType = N'"+this.billTypeComboBox.Text+ "';";

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
        }

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0)
                {

                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];

                    salesBill salesBill = new salesBill(Convert.ToInt32(row.Cells[0].Value.ToString()));
                    salesBill.Show();
                    salesBill.BringToFront();

                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}