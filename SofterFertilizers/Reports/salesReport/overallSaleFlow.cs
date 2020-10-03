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
    public partial class overallSaleFlow : UserControl
    {
        public overallSaleFlow()
        {
            InitializeComponent();
            reportComboBox.Items.Add("إجمالي");
            reportComboBox.Items.Add("مفصّل");
            reportComboBox.Text = "إجمالي";
            searhTypeComboBox.Items.Add("الكل");
            searhTypeComboBox.Items.Add("المحافظة");
            searhTypeComboBox.Items.Add("المركز");
            searhTypeComboBox.Text = "الكل";
            billTypeComboBox.Items.Add("الكل");
            billTypeComboBox.Items.Add("آجل");
            billTypeComboBox.Items.Add("كاش");
            billTypeComboBox.Text = "الكل";    
        }

        private void searhTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (searhTypeComboBox.Text == "الكل") {
                label6.Visible = false;
                governorateCenterComboBox.Visible = false;

            }
            else if (searhTypeComboBox.Text == "المحافظة")
            {
                label6.Text = "المحافظة";
                label6.Visible = true;
                governorateCenterComboBox.Visible = true;

                //governorate Combo Boxes
                governorateCenterComboBox.Items.Clear();
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string Query = "select distinct governorate from customerTable;";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
                da.Fill(dt);
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        governorateCenterComboBox.Items.Add(dr["governorate"].ToString());
                    }
                }
                catch (Exception ex)
                {
 
                }
                conDataBase.Close();

                if (governorateCenterComboBox.Items.Count > 0)
                {
                    governorateCenterComboBox.Text = governorateCenterComboBox.Items[0].ToString();
                }

            }

            else if (searhTypeComboBox.Text == "المركز")
            {
                label6.Text = "المركز";
                label6.Visible = true;
                governorateCenterComboBox.Visible = true;

                //center Combo Boxes
                governorateCenterComboBox.Items.Clear();
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string Query = "select distinct center from customerTable;";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
                da.Fill(dt);
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        governorateCenterComboBox.Items.Add(dr["center"].ToString());
                    }
                }
                catch (Exception ex)
                {
 
                }
                conDataBase.Close();

                if (governorateCenterComboBox.Items.Count > 0)
                {
                    governorateCenterComboBox.Text = governorateCenterComboBox.Items[0].ToString();
                }
            }
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;


        private void showFlowButton_Click(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;

            if (reportComboBox.Text == "إجمالي")
            {
                if (searhTypeComboBox.Text == "الكل")
                {
                    if (billTypeComboBox.Text == "الكل")
                    {
                        string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumAfter as 'الإجمالي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط' from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ;";

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
                        string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumAfter as 'الإجمالي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط' from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and paymentType=N'آجل';";

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
                        string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumAfter as 'الإجمالي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط' from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and paymentType=N'كاش';";

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

                else if (searhTypeComboBox.Text == "المحافظة")
                {
                    if (governorateCenterComboBox.Text != "")
                    {
                        if (billTypeComboBox.Text == "الكل")
                        {
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumAfter as 'الإجمالي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',customerTable.governorate as 'محافظة العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and customerTable.governorate =N'" + this.governorateCenterComboBox.Text + "';";

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
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumAfter as 'الإجمالي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',customerTable.governorate as 'محافظة العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and paymentType=N'آجل'and customerTable.governorate =N'" + this.governorateCenterComboBox.Text + "';";

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
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumAfter as 'الإجمالي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',customerTable.governorate as 'محافظة العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and paymentType=N'كاش' and customerTable.governorate =N'" + this.governorateCenterComboBox.Text + "';";

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
                    else
                    {
                        MessageBox.Show("لا يوجد محافظات مسجّلة");
                    }
                }
                else if (searhTypeComboBox.Text == "المركز")
                {
                    if (governorateCenterComboBox.Text != "")
                    {
                        if (billTypeComboBox.Text == "الكل")
                        {
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumAfter as 'الإجمالي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',customerTable.center as 'مركز العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and customerTable.center =N'" + this.governorateCenterComboBox.Text + "' ;";

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
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumAfter as 'الإجمالي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',customerTable.center as 'مركز العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and paymentType=N'آجل' and customerTable.center =N'" + this.governorateCenterComboBox.Text + "';";

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
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumAfter as 'الإجمالي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',customerTable.center as 'مركز العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and paymentType=N'كاش' and customerTable.center =N'" + this.governorateCenterComboBox.Text + "';";

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
                    else
                    {
                        MessageBox.Show("لا يوجد مراكز مسجّلة");
                    }

                }

            }

            else if (reportComboBox.Text == "مفصّل")
            {
                if (searhTypeComboBox.Text == "الكل")
                {
                    if (billTypeComboBox.Text == "الكل")
                    {
                        string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumBefore as 'الإجمالي قبل' ,salesMainTable.discountPercentage as 'نسبة الخصم' ,salesMainTable.discountAmount as 'قيمة الخصم' ,salesMainTable.salesTax as 'ضريبة المبيعات' ,salesMainTable.transport as 'النقل' ,salesMainTable.sumAfter as 'الإجمالي بعد',salesMainTable.paid as 'المدفوع' ,salesMainTable.rest as 'المتبقي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',salesMainTable.debtsRatio as 'نسبة القسط' ,salesMainTable.debtsType as 'نوع التقسيط' from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ;";

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
                        string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumBefore as 'الإجمالي قبل' ,salesMainTable.discountPercentage as 'نسبة الخصم' ,salesMainTable.discountAmount as 'قيمة الخصم' ,salesMainTable.salesTax as 'ضريبة المبيعات' ,salesMainTable.transport as 'النقل' ,salesMainTable.sumAfter as 'الإجمالي بعد',salesMainTable.paid as 'المدفوع' ,salesMainTable.rest as 'المتبقي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',salesMainTable.debtsRatio as 'نسبة القسط' ,salesMainTable.debtsType as 'نوع التقسيط' from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and paymentType=N'آجل';";

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
                        string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumBefore as 'الإجمالي قبل' ,salesMainTable.discountPercentage as 'نسبة الخصم' ,salesMainTable.discountAmount as 'قيمة الخصم' ,salesMainTable.salesTax as 'ضريبة المبيعات' ,salesMainTable.transport as 'النقل' ,salesMainTable.sumAfter as 'الإجمالي بعد',salesMainTable.paid as 'المدفوع' ,salesMainTable.rest as 'المتبقي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',salesMainTable.debtsRatio as 'نسبة القسط' ,salesMainTable.debtsType as 'نوع التقسيط' from salesMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and paymentType=N'كاش';";

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

                else if (searhTypeComboBox.Text == "المحافظة")
                {
                    if (governorateCenterComboBox.Text != "")
                    {
                        if (billTypeComboBox.Text == "الكل")
                        {
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumBefore as 'الإجمالي قبل' ,salesMainTable.discountPercentage as 'نسبة الخصم' ,salesMainTable.discountAmount as 'قيمة الخصم' ,salesMainTable.salesTax as 'ضريبة المبيعات' ,salesMainTable.transport as 'النقل' ,salesMainTable.sumAfter as 'الإجمالي بعد',salesMainTable.paid as 'المدفوع' ,salesMainTable.rest as 'المتبقي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',salesMainTable.debtsRatio as 'نسبة القسط' ,salesMainTable.debtsType as 'نوع التقسيط', customerTable.governorate as 'محافظة العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and customerTable.governorate =N'" + this.governorateCenterComboBox.Text + "';";

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
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumBefore as 'الإجمالي قبل' ,salesMainTable.discountPercentage as 'نسبة الخصم' ,salesMainTable.discountAmount as 'قيمة الخصم' ,salesMainTable.salesTax as 'ضريبة المبيعات' ,salesMainTable.transport as 'النقل' ,salesMainTable.sumAfter as 'الإجمالي بعد',salesMainTable.paid as 'المدفوع' ,salesMainTable.rest as 'المتبقي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',salesMainTable.debtsRatio as 'نسبة القسط' ,salesMainTable.debtsType as 'نوع التقسيط',customerTable.governorate as 'محافظة العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and paymentType=N'آجل'and customerTable.governorate =N'" + this.governorateCenterComboBox.Text + "';";

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
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumBefore as 'الإجمالي قبل' ,salesMainTable.discountPercentage as 'نسبة الخصم' ,salesMainTable.discountAmount as 'قيمة الخصم' ,salesMainTable.salesTax as 'ضريبة المبيعات' ,salesMainTable.transport as 'النقل' ,salesMainTable.sumAfter as 'الإجمالي بعد',salesMainTable.paid as 'المدفوع' ,salesMainTable.rest as 'المتبقي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',salesMainTable.debtsRatio as 'نسبة القسط' ,salesMainTable.debtsType as 'نوع التقسيط',customerTable.governorate as 'محافظة العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and paymentType=N'كاش' and customerTable.governorate =N'" + this.governorateCenterComboBox.Text + "';";

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
                    else
                    {
                        MessageBox.Show("لا يوجد محافظات مسجّلة");
                    }
                }
                else if (searhTypeComboBox.Text == "المركز")
                {
                    if (governorateCenterComboBox.Text != "")
                    {
                        if (billTypeComboBox.Text == "الكل")
                        {
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumBefore as 'الإجمالي قبل' ,salesMainTable.discountPercentage as 'نسبة الخصم' ,salesMainTable.discountAmount as 'قيمة الخصم' ,salesMainTable.salesTax as 'ضريبة المبيعات' ,salesMainTable.transport as 'النقل' ,salesMainTable.sumAfter as 'الإجمالي بعد',salesMainTable.paid as 'المدفوع' ,salesMainTable.rest as 'المتبقي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',salesMainTable.debtsRatio as 'نسبة القسط' ,salesMainTable.debtsType as 'نوع التقسيط',customerTable.center as 'مركز العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and customerTable.center =N'" + this.governorateCenterComboBox.Text + "' ;";

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
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumBefore as 'الإجمالي قبل' ,salesMainTable.discountPercentage as 'نسبة الخصم' ,salesMainTable.discountAmount as 'قيمة الخصم' ,salesMainTable.salesTax as 'ضريبة المبيعات' ,salesMainTable.transport as 'النقل' ,salesMainTable.sumAfter as 'الإجمالي بعد',salesMainTable.paid as 'المدفوع' ,salesMainTable.rest as 'المتبقي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',salesMainTable.debtsRatio as 'نسبة القسط' ,salesMainTable.debtsType as 'نوع التقسيط',customerTable.center as 'مركز العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and paymentType=N'آجل' and customerTable.center =N'" + this.governorateCenterComboBox.Text + "';";

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
                            string Query = "select distinct salesMainTable.Id as 'كود الفاتورة', salesMainTable.paymentType as 'نوع الدفع', salesMainTable.storeName as 'اسم المخزن' ,salesMainTable.customerName as 'اسم العميل', salesMainTable.buyingType as 'نوع الفاتورة',salesMainTable.sumBefore as 'الإجمالي قبل' ,salesMainTable.discountPercentage as 'نسبة الخصم' ,salesMainTable.discountAmount as 'قيمة الخصم' ,salesMainTable.salesTax as 'ضريبة المبيعات' ,salesMainTable.transport as 'النقل' ,salesMainTable.sumAfter as 'الإجمالي بعد',salesMainTable.paid as 'المدفوع' ,salesMainTable.rest as 'المتبقي',salesMainTable.date as 'التاريخ',salesMainTable.debts as 'التقسيط',salesMainTable.debtsRatio as 'نسبة القسط' ,salesMainTable.debtsType as 'نوع التقسيط',customerTable.center as 'مركز العميل' from salesMainTable,customerTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and salesMainTable.customerName=customerTable.name and paymentType=N'كاش' and customerTable.center =N'" + this.governorateCenterComboBox.Text + "';";

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
                    else
                    {
                        MessageBox.Show("لا يوجد مراكز مسجّلة");
                    }

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
