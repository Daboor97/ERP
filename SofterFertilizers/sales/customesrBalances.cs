﻿using System;
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
    public partial class customesrBalances : UserControl
    {
        public customesrBalances()
        {
            InitializeComponent();
            fill();
            deleteButton.Visible = false;
            payBillRadioButton.Checked = true;
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string state = "new";
        string oldAmount;
        string oldSupplier;
        string oldbill;

        private void payBillRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //category DGV
            restOpsDGV.DataBindings.Clear();



            string Query = "select salesMainTable.Id as 'رقم الفاتورة' , salesMainTable.customername as 'اسم العميل' ,  salesMainTable.storeName as 'المخزن', salesMainTable.rest as 'المتبقي', salesMainTable.date as 'التاريخ', salesMainTable.debts as 'تقسيط' from salesMainTable where customerName = N'" + this.supplierNameComboBox.Text + "' and rest > 0;";

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
                restOpsDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();
        }

        private void payDueRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            restOpsDGV.DataBindings.Clear();

            string Query = "select Id as 'رقم الاستحقاق',billDetails as 'التفاصيل',amount as 'المبلغ',date as 'التاريخ',paid as 'مدفوع' from duesTable where customerNumber=N'" + this.supplierCodeTextBox.Text + "';";

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
                restOpsDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            conDataBase.Close();
        }

        void clear()
        {
            paidToTextBox.Text = "0";
            paidFromTextBox.Text = "0";
            sumTextBox.Text = "0";
            notesTextBox.Text = "";
            deleteButton.Visible = false;
        }

        void fill()
        {
            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from customerBalanceTable) BEGIN SELECT MAX(Id) FROM customerBalanceTable END", conDataBase).ExecuteScalar().ToString();

            if (maxBill != "")
            {
                int maxBillint = Convert.ToInt32(maxBill);
                codeTextBox.Text = Convert.ToString(maxBillint + 1);
                conDataBase.Close();
            }

            else
            {
                codeTextBox.Text = "1";
                conDataBase.Close();
            }

            //supplier ComboBox
            supplierNameComboBox.Items.Clear();
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct name from customerTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
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

        private void supplierNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //supplier Code
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                supplierCodeTextBox.Text = new SqlCommand("select Id from customerTable where name=N'" + this.supplierNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                //supplier Balance
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                balanceTextBox.Text = new SqlCommand("select balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
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
                supplierNameComboBox.Text = new SqlCommand("IF EXISTS(select 1 from customerTable where Id=N'" + this.supplierCodeTextBox.Text + "') BEGIN select name from customerTable where Id=N'" + this.supplierCodeTextBox.Text + "' END ;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                //category DGV
                categoryDGV.DataBindings.Clear();

                string Query = "select customerBalanceTable.Id as 'رقم الإيصال', customerTable.name as 'اسم العميل' , billNumber as 'رقم الفاتورة' ,billDetails as 'التفاصيل',amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', date as 'التاريخ' from customerBalanceTable,customerTable where customerTable.Id=N'" + this.supplierCodeTextBox.Text + "' and customerBalanceTable.customerNumber = customerTable.Id;";

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
                    categoryDGV.DataSource = bSource;
                    sda.Update(dbdataset);

                }
                catch (Exception ex)
                {
 
                }

                if (payBillRadioButton.Checked)
                {
                    //category DGV
                    restOpsDGV.DataBindings.Clear();



                    Query = "select salesMainTable.Id as 'رقم الفاتورة' , salesMainTable.customername as 'اسم العميل' ,  salesMainTable.storeName as 'المخزن', salesMainTable.rest as 'المتبقي', salesMainTable.date as 'التاريخ', salesMainTable.debts as 'تقسيط' from salesMainTable where customerName = N'" + this.supplierNameComboBox.Text + "' and rest > 0;";

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
                        restOpsDGV.DataSource = bSource;
                        sda.Update(dbdataset);

                    }
                    catch (Exception ex)
                    {
     
                    }

                    conDataBase.Close();
                }
                else
                {
                    restOpsDGV.DataBindings.Clear();

                    Query = "select Id as 'رقم الاستحقاق',billDetails as 'التفاصيل',amount as 'المبلغ',date as 'التاريخ',paid as 'مدفوع' from duesTable where customerNumber=N'" + this.supplierCodeTextBox.Text + "';";

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
                        restOpsDGV.DataSource = bSource;
                        sda.Update(dbdataset);

                    }
                    catch (Exception ex)
                    {
     

                    }

                    conDataBase.Close();
                }
            }
            catch
            {
            }
        }


        void sumFunction()
        {
            try
            {
                sumTextBox.ResetText();
                double a, b, f;
                double c;
                double.TryParse(paidFromTextBox.Text, out a);
                double.TryParse(paidToTextBox.Text, out f);


                c = Convert.ToDouble(Math.Abs(a - f));
                sumTextBox.Text = c.ToString("G29");
            }
            catch
            {

            }
        }

        private void paidFromTextBox_TextChanged(object sender, EventArgs e)
        {
            sumFunction();
        }

        private void paidToTextBox_TextChanged(object sender, EventArgs e)
        {
            sumFunction();
        }

        private void addButton_Click(object sender, EventArgs e)
        {

            if (paidFromTextBox.ReadOnly == false)
            {
                if (double.Parse(sumTextBox.Text) != 0)
                {
                    string direction;

                    if (Convert.ToDouble(paidToTextBox.Text) < Convert.ToDouble(paidFromTextBox.Text))
                    {
                        direction = "من العميل";
                    }
                    else
                    {
                        direction = "للعميل";
                    }

                    if (state == "new")
                    {
                        string Query = "IF NOT EXISTS(select 1 from customerBalanceTable where Id=N'" + this.codeTextBox.Text + "') BEGIN INSERT INTO customerBalanceTable(customerNumber,billNumber,billDetails,direction,safeName,amount,date,inside) VALUES (N'" + this.supplierCodeTextBox.Text + "','-',N'" + this.notesTextBox.Text + "',N'" + direction + "',N'" + this.safeComboBox.Text + "',N'" + this.sumTextBox.Text + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','True') END";
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

                        if (direction == "من العميل")
                        {
                            Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.sumTextBox.Text + "','customerBalance',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.codeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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

                        }
                        else
                        {
                            Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.sumTextBox.Text + "','customerBalance',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.codeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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
                        }
                        //category DGV
                        categoryDGV.DataBindings.Clear();

                        Query = "select customerBalanceTable.Id as 'رقم الإيصال', customerTable.name as 'اسم العميل' , billNumber as 'رقم الفاتورة' ,billDetails as 'التفاصيل',amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', date as 'التاريخ' from customerBalanceTable,customerTable where customerTable.Id=N'" + this.supplierCodeTextBox.Text + "' and customerBalanceTable.customerNumber = customerTable.Id;";

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
         

                        }

                        conDataBase.Close();


                        double supplierBalance = 0;
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                        if (supplierBalanceString != "")
                        {
                            supplierBalance = Convert.ToDouble(supplierBalanceString);
                            conDataBase.Close();
                        }

                        supplierBalance -= Convert.ToDouble(this.sumTextBox.Text);

                        Query = "IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + this.supplierNameComboBox.Text + "' END";
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
                        deleteButton.Visible = false;
                        fill();
                        state = "new";
                        clear();

                        MessageBox.Show("حفظ");
                    }


                    else if (state == "adjust")
                    {
                        double supplierBalance = 0;
                        SqlConnection conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + oldSupplier + "') BEGIN SELECT balance from customerTable where name=N'" + oldSupplier + "' END", conDataBase).ExecuteScalar().ToString();

                        if (supplierBalanceString != "")
                        {
                            supplierBalance = Convert.ToDouble(supplierBalanceString);
                            conDataBase.Close();
                        }

                        supplierBalance += Convert.ToDouble(oldAmount);

                        string Query = "IF EXISTS (select 1 from customerTable where name=N'" + oldSupplier + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + oldSupplier + "' END";
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

                        Query = "IF EXISTS (select 1 from customerBalanceTable where Id = N'" + this.codeTextBox.Text + "') BEGIN UPDATE customerBalanceTable SET customerNumber =N'" + this.supplierCodeTextBox.Text + "', billNumber='0', billDetails=N'" + this.notesTextBox.Text + "', direction=N'" + direction + "',safeName=N'" + this.safeComboBox.Text + "', amount=N'" + this.sumTextBox.Text + "',date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' where Id=N'" + this.codeTextBox.Text + "' END";
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


                        supplierBalance = 0;
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                        if (supplierBalanceString != "")
                        {
                            supplierBalance = Convert.ToDouble(supplierBalanceString);
                            conDataBase.Close();
                        }

                        supplierBalance -= Convert.ToDouble(this.sumTextBox.Text);

                        Query = "IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + this.supplierNameComboBox.Text + "' END";
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

                        //category DGV
                        categoryDGV.DataBindings.Clear();

                        Query = "select customerBalanceTable.Id as 'رقم الإيصال', customerTable.name as 'اسم العميل' , billNumber as 'رقم الفاتورة' ,billDetails as 'التفاصيل',amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', date as 'التاريخ' from customerBalanceTable,customerTable where customerTable.Id=N'" + this.supplierCodeTextBox.Text + "' and customerBalanceTable.customerNumber = customerTable.Id;";

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
         

                        }

                        conDataBase.Close();




                        if (direction == "من العميل")
                        {
                            Query = "DELETE FROM safeTable where type='customerBalance' and details = 'in' and billNo = N'" + this.codeTextBox.Text + "';";
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

                            Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.sumTextBox.Text + "','customerBalance',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.codeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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

                        }
                        else
                        {
                            Query = "DELETE FROM safeTable where type='customerBalance' and details = 'out' and billNo = N'" + this.codeTextBox.Text + "';";
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

                            Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.sumTextBox.Text + "','customerBalance',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.codeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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
                        }


                        deleteButton.Visible = false;
                        fill();
                        state = "new";
                        clear();
                        MessageBox.Show("حفظ");


                    }
                }
                else
                {
                    MessageBox.Show("ليس هناك مبلغ ليتم إضافته");
                }
            }

            else if (state == "delete")
            {
                MessageBox.Show("لا يمكن تعديل مدفوع فاتورة أو مستحق\n\n  يمكن حذف البند لاسترجاع حالة الفاتورة او المستحق السابقة");
            }

            else
            {

                if (payBillRadioButton.Checked) {
                    string Query = "";
                    SqlConnection conDataBase;
                    SqlCommand cmdDataBase;

                    for (int i = 0; i <= restOpsDGV.Rows.Count - 1; i++)
                    {
                        if (Convert.ToBoolean(restOpsDGV.Rows[i].Cells[0].Value) == true)
                        {
                            Query = "IF NOT EXISTS(select 1 from customerBalanceTable where Id=N'" + this.codeTextBox.Text + "') BEGIN INSERT INTO customerBalanceTable(customerNumber,billNumber,billDetails,direction,safeName,amount,date,inside) VALUES (N'" + this.supplierCodeTextBox.Text + "',N'" + this.restOpsDGV.Rows[i].Cells[1].Value.ToString() + "',N'فاتورة مبيعات',N'من العميل',N'" + this.safeComboBox.Text + "',N'" + this.restOpsDGV.Rows[i].Cells[4].Value.ToString() + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','True') END";
                            conDataBase = new SqlConnection(constring);
                            cmdDataBase = new SqlCommand(Query, conDataBase);
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

                            double supplierBalance = 0;
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                            if (supplierBalanceString != "")
                            {
                                supplierBalance = Convert.ToDouble(supplierBalanceString);
                                conDataBase.Close();
                            }

                            supplierBalance -= Convert.ToDouble(this.restOpsDGV.Rows[i].Cells[4].Value.ToString());

                            Query = "IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + this.supplierNameComboBox.Text + "' END";
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

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string oldPaid = new SqlCommand("SELECT paid from salesMainTable where Id=N'" + this.restOpsDGV.Rows[i].Cells[1].Value.ToString() + "'", conDataBase).ExecuteScalar().ToString();

                            if (supplierBalanceString != "")
                            {
                                supplierBalance = Convert.ToDouble(supplierBalanceString);
                                conDataBase.Close();
                            }

                            double sum = Convert.ToDouble(oldPaid) + Convert.ToDouble(this.restOpsDGV.Rows[i].Cells[4].Value.ToString());

                            Query = "UPDATE salesMainTable SET paid=N'" + sum + "' , rest = '0' where Id=N'" + this.restOpsDGV.Rows[i].Cells[1].Value.ToString() + "'";
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

                            Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.restOpsDGV.Rows[i].Cells[4].Value.ToString() + "','customerBalance',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.codeTextBox.Text + "','sales',N'" + this.supplierCodeTextBox.Text + "')";
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



                            // code textBox
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from customerBalanceTable) BEGIN SELECT MAX(Id) FROM customerBalanceTable END", conDataBase).ExecuteScalar().ToString();

                            if (maxBill != "")
                            {
                                int maxBillint = Convert.ToInt32(maxBill);
                                codeTextBox.Text = Convert.ToString(maxBillint + 1);
                                conDataBase.Close();
                            }

                            else
                            {
                                codeTextBox.Text = "1";
                                conDataBase.Close();
                            }
                        }
                    }
                    deleteButton.Visible = false;
                    fill();
                    state = "new";
                    clear();

                    MessageBox.Show("حفظ");

                    //category DGV
                    categoryDGV.DataBindings.Clear();

                    Query = "select customerBalanceTable.Id as 'رقم الإيصال', customerTable.name as 'اسم العميل' , billNumber as 'رقم الفاتورة' ,billDetails as 'التفاصيل',amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', date as 'التاريخ' from customerBalanceTable,customerTable where customerTable.Id=N'" + this.supplierCodeTextBox.Text + "' and customerBalanceTable.customerNumber = customerTable.Id;";

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
     
                    }

                    //category DGV
                    restOpsDGV.DataBindings.Clear();



                    Query = "select salesMainTable.Id as 'رقم الفاتورة' , salesMainTable.customername as 'اسم العميل' ,  salesMainTable.storeName as 'المخزن', salesMainTable.rest as 'المتبقي', salesMainTable.date as 'التاريخ', salesMainTable.debts as 'تقسيط' from salesMainTable where customerName = N'" + this.supplierNameComboBox.Text + "' and rest > 0;";

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
                        restOpsDGV.DataSource = bSource;
                        sda.Update(dbdataset);

                    }
                    catch (Exception ex)
                    {
     
                    }

                    conDataBase.Close();
                }
                else
                {
                    string Query = "";
                    SqlConnection conDataBase;
                    SqlCommand cmdDataBase;

                    for (int i = 0; i <= restOpsDGV.Rows.Count - 1; i++)
                    {
                        if (Convert.ToBoolean(restOpsDGV.Rows[i].Cells[0].Value) == true)
                        {
                            Query = "IF NOT EXISTS(select 1 from customerBalanceTable where Id=N'" + this.codeTextBox.Text + "') BEGIN INSERT INTO customerBalanceTable(customerNumber,billNumber,billDetails,direction,safeName,amount,date,inside) VALUES (N'" + this.supplierCodeTextBox.Text + "',N'" + this.restOpsDGV.Rows[i].Cells[1].Value.ToString() + "',N'مستحق مدخل يدويًا',N'من العميل',N'" + this.safeComboBox.Text + "',N'" + this.restOpsDGV.Rows[i].Cells[3].Value.ToString() + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','True') END";
                            conDataBase = new SqlConnection(constring);
                            cmdDataBase = new SqlCommand(Query, conDataBase);
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

                            double supplierBalance = 0;
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                            if (supplierBalanceString != "")
                            {
                                supplierBalance = Convert.ToDouble(supplierBalanceString);
                                conDataBase.Close();
                            }

                            supplierBalance -= Convert.ToDouble(this.restOpsDGV.Rows[i].Cells[3].Value.ToString());

                            Query = "IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + this.supplierNameComboBox.Text + "' END";
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

                            Query = "UPDATE duesTable SET paid='True' where Id=N'" + this.restOpsDGV.Rows[i].Cells[1].Value.ToString() + "'";
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

                            Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.restOpsDGV.Rows[i].Cells[3].Value.ToString() + "','customerBalance',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.codeTextBox.Text + "','dues',N'" + this.supplierCodeTextBox.Text + "')";
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



                            // code textBox
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from customerBalanceTable) BEGIN SELECT MAX(Id) FROM customerBalanceTable END", conDataBase).ExecuteScalar().ToString();

                            if (maxBill != "")
                            {
                                int maxBillint = Convert.ToInt32(maxBill);
                                codeTextBox.Text = Convert.ToString(maxBillint + 1);
                                conDataBase.Close();
                            }

                            else
                            {
                                codeTextBox.Text = "1";
                                conDataBase.Close();
                            }
                        }
                    }
                    deleteButton.Visible = false;
                    fill();
                    state = "new";
                    clear();

                    MessageBox.Show("حفظ");

                    //category DGV
                    categoryDGV.DataBindings.Clear();

                    Query = "select customerBalanceTable.Id as 'رقم الإيصال', customerTable.name as 'اسم العميل' , billNumber as 'رقم الفاتورة' ,billDetails as 'التفاصيل',amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', date as 'التاريخ' from customerBalanceTable,customerTable where customerTable.Id=N'" + this.supplierCodeTextBox.Text + "' and customerBalanceTable.customerNumber = customerTable.Id;";

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
     
                    }

                    //category DGV
                    restOpsDGV.DataBindings.Clear();

                    Query = "select Id as 'رقم الاستحقاق',billDetails as 'التفاصيل',amount as 'المبلغ',date as 'التاريخ',paid as 'مدفوع' from duesTable where customerNumber=N'" + this.supplierCodeTextBox.Text + "';";

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
                        restOpsDGV.DataSource = bSource;
                        sda.Update(dbdataset);

                    }
                    catch (Exception ex)
                    {
     

                    }

                    conDataBase.Close();

                }
            }

            paidFromTextBox.ReadOnly = false;
            paidFromTextBox.BackColor = Color.FromArgb(41, 44, 51);
            paidFromTextBox.Text = "0";

            paidToTextBox.ReadOnly = false;
            paidToTextBox.BackColor = Color.FromArgb(41, 44, 51);
            paidToTextBox.Text = "0";

            notesTextBox.ReadOnly = false;
            notesTextBox.BackColor = Color.FromArgb(41, 44, 51);
            notesTextBox.Text = "";

        }

        private void codeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select customerBalanceTable.Id as 'رقم الإيصال', customerTable.name as 'اسم العميل' , billNumber as 'رقم الفاتورة' ,billDetails as 'التفاصيل',amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', date as 'التاريخ' from customerBalanceTable,customerTable where customerTable.Id=N'" + this.supplierCodeTextBox.Text + "' and customerBalanceTable.customerNumber = customerTable.Id and customerBalanceTable.Id like N'%" + this.codeSearchTextBox.Text + "%';";


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
            try
            {

                if (e.RowIndex >= 0)
                {


                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];

                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string stringOutside = new SqlCommand("IF EXISTS (select 1 FROM customerBalanceTable where Id = N'" + row.Cells[0].Value.ToString() + "') BEGIN select inside FROM customerBalanceTable where Id = N'" + Int32.Parse(row.Cells[0].Value.ToString()) + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    stringOutside = (string.IsNullOrEmpty(stringOutside)) ? "0" : stringOutside;
                    bool inside;
                    if (stringOutside == "0" || stringOutside == "False")
                    {
                        inside = false;
                    }
                    else
                    {
                        inside = true;
                    }


                    if (inside)
                    {
                        if (row.Cells[2].Value.ToString() == "0")
                        {
                            state = "adjust";

                            paidFromTextBox.ReadOnly = false;
                            paidFromTextBox.BackColor = Color.FromArgb(41, 44, 51);
                            paidFromTextBox.Text = "0";

                            paidToTextBox.ReadOnly = false;
                            paidToTextBox.BackColor = Color.FromArgb(41, 44, 51);
                            paidToTextBox.Text = "0";

                            notesTextBox.ReadOnly = false;
                            notesTextBox.BackColor = Color.FromArgb(41, 44, 51);
                            notesTextBox.Text = "";

                            addButton.Visible = true;
                        }
                        else
                        {
                            state = "delete";

                            addButton.Visible = false;


                            paidFromTextBox.ReadOnly = true;
                            paidFromTextBox.BackColor = Color.FromArgb(64, 64, 64);

                            paidToTextBox.ReadOnly = true;
                            paidToTextBox.BackColor = Color.FromArgb(64, 64, 64);

                            notesTextBox.ReadOnly = true;
                            notesTextBox.BackColor = Color.FromArgb(64, 64, 64);

                            oldbill = row.Cells[2].Value.ToString();

                        }

                        clear();
                        deleteButton.Visible = true;

                        if (row.Cells[5].Value.ToString() == "للعميل")
                        {
                            this.paidToTextBox.Text = row.Cells[4].Value.ToString();
                        }
                        else
                        {
                            this.paidFromTextBox.Text = row.Cells[4].Value.ToString();
                        }

                        this.codeTextBox.Text = row.Cells[0].Value.ToString();
                        this.supplierNameComboBox.Text = row.Cells[1].Value.ToString();
                        oldSupplier = row.Cells[1].Value.ToString();
                        this.notesTextBox.Text = row.Cells[3].Value.ToString();
                        this.paidToTextBox.Text = row.Cells[4].Value.ToString();
                        this.sumTextBox.Text = row.Cells[4].Value.ToString();
                        oldAmount = row.Cells[4].Value.ToString();
                        this.safeComboBox.Text = row.Cells[6].Value.ToString();
                        this.dateDTP.Text = row.Cells[7].Value.ToString();
                    }
                    else
                    {
                        MessageBox.Show("لا يمكن تعديل هذا الإيصال");
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

            if (state == "adjust")
            {
                DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟\n\nستسترجع جميع القيم قبل البند", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string Query = "IF EXISTS(select 1 from customerBalanceTable where Id=N'" + this.codeTextBox.Text + "') BEGIN DELETE FROM customerBalanceTable where Id= N'" + this.codeTextBox.Text + "' END";
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

                    double supplierBalance = 0;
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + oldSupplier + "') BEGIN SELECT balance from customerTable where name=N'" + oldSupplier + "' END", conDataBase).ExecuteScalar().ToString();

                    if (supplierBalanceString != "")
                    {
                        supplierBalance = Convert.ToDouble(supplierBalanceString);
                        conDataBase.Close();
                    }

                    supplierBalance += Convert.ToDouble(oldAmount);

                    Query = "IF EXISTS (select 1 from customerTable where name=N'" + oldSupplier + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + oldSupplier + "' END";
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
                    deleteButton.Visible = false;

                    Query = "DELETE FROM safeTable where type = 'customerBalance' and billNo = N'" + this.codeTextBox.Text + "';";
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


                    state = "new";
                    clear();
                    fill();
                    //category DGV
                    categoryDGV.DataBindings.Clear();

                    Query = "select customerBalanceTable.Id as 'رقم الإيصال', customerTable.name as 'اسم العميل' , billNumber as 'رقم الفاتورة' ,billDetails as 'التفاصيل',amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', date as 'التاريخ' from customerBalanceTable,customerTable where customerTable.Id=N'" + this.supplierCodeTextBox.Text + "' and customerBalanceTable.customerNumber = customerTable.Id;";

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
     

                    }

                    conDataBase.Close();

                    MessageBox.Show("حفظ");

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }

            else if (state == "delete")
            {
                if (payBillRadioButton.Checked) {
                    DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟\n\nستسترجع جميع القيم قبل البند", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        string Query = "IF EXISTS(select 1 from customerBalanceTable where Id=N'" + this.codeTextBox.Text + "') BEGIN DELETE FROM customerBalanceTable where Id= N'" + this.codeTextBox.Text + "' END";
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

                        double supplierBalance = 0;
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + oldSupplier + "') BEGIN SELECT balance from customerTable where name=N'" + oldSupplier + "' END", conDataBase).ExecuteScalar().ToString();

                        if (supplierBalanceString != "")
                        {
                            supplierBalance = Convert.ToDouble(supplierBalanceString);
                            conDataBase.Close();
                        }

                        supplierBalance += Convert.ToDouble(oldAmount);

                        Query = "IF EXISTS (select 1 from customerTable where name=N'" + oldSupplier + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + oldSupplier + "' END";
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

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string oldPaid = new SqlCommand("SELECT paid from salesMainTable where Id=N'" + oldbill + "'", conDataBase).ExecuteScalar().ToString();

                        if (supplierBalanceString != "")
                        {
                            supplierBalance = Convert.ToDouble(supplierBalanceString);
                            conDataBase.Close();
                        }

                        double sum = Convert.ToDouble(oldPaid) - Convert.ToDouble(oldAmount);

                        Query = "UPDATE salesMainTable SET paid=N'" + sum + "' , rest = N'" + oldAmount + "' where Id=N'" + oldbill + "'";
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



                        Query = "DELETE FROM safeTable where type = 'customerBalance' and details= 'in' and billNo = N'" + this.codeTextBox.Text + "';";
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



                        deleteButton.Visible = false;
                        addButton.Visible = true;

                        state = "new";
                        clear();
                        fill();
                        //category DGV
                        categoryDGV.DataBindings.Clear();

                        Query = "select customerBalanceTable.Id as 'رقم الإيصال', customerTable.name as 'اسم العميل' , billNumber as 'رقم الفاتورة' ,billDetails as 'التفاصيل',amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', date as 'التاريخ' from customerBalanceTable,customerTable where customerTable.Id=N'" + this.supplierCodeTextBox.Text + "' and customerBalanceTable.customerNumber = customerTable.Id;";

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
         

                        }

                        //restOpsDGV DGV
                        restOpsDGV.DataBindings.Clear();



                        Query = "select salesMainTable.Id as 'رقم الفاتورة' , salesMainTable.customername as 'اسم العميل' ,  salesMainTable.storeName as 'المخزن', salesMainTable.rest as 'المتبقي', salesMainTable.date as 'التاريخ', salesMainTable.debts as 'تقسيط' from salesMainTable where customerName = N'" + this.supplierNameComboBox.Text + "' and rest > 0;";

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
                            restOpsDGV.DataSource = bSource;
                            sda.Update(dbdataset);

                        }
                        catch (Exception ex)
                        {
         
                        }

                        conDataBase.Close();

                        MessageBox.Show("حفظ");

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }
                else
                {

                    DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟\n\nستسترجع جميع القيم قبل البند", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        string Query = "IF EXISTS(select 1 from customerBalanceTable where Id=N'" + this.codeTextBox.Text + "') BEGIN DELETE FROM customerBalanceTable where Id= N'" + this.codeTextBox.Text + "' END";
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

                        double supplierBalance = 0;
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + oldSupplier + "') BEGIN SELECT balance from customerTable where name=N'" + oldSupplier + "' END", conDataBase).ExecuteScalar().ToString();

                        if (supplierBalanceString != "")
                        {
                            supplierBalance = Convert.ToDouble(supplierBalanceString);
                            conDataBase.Close();
                        }

                        supplierBalance += Convert.ToDouble(oldAmount);

                        Query = "IF EXISTS (select 1 from customerTable where name=N'" + oldSupplier + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + oldSupplier + "' END";
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

                        Query = "UPDATE duesTable SET paid='False' where Id=N'" + oldbill + "'";
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



                        Query = "DELETE FROM safeTable where type = 'customerBalance' and details= 'in' and billNo = N'" + this.codeTextBox.Text + "';";
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



                        deleteButton.Visible = false;
                        addButton.Visible = true;

                        state = "new";
                        clear();
                        fill();
                        //category DGV
                        categoryDGV.DataBindings.Clear();

                        Query = "select customerBalanceTable.Id as 'رقم الإيصال', customerTable.name as 'اسم العميل' , billNumber as 'رقم الفاتورة' ,billDetails as 'التفاصيل',amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', date as 'التاريخ' from customerBalanceTable,customerTable where customerTable.Id=N'" + this.supplierCodeTextBox.Text + "' and customerBalanceTable.customerNumber = customerTable.Id;";

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
         

                        }


                        //category DGV
                        restOpsDGV.DataBindings.Clear();

                        Query = "select Id as 'رقم الاستحقاق',billDetails as 'التفاصيل',amount as 'المبلغ',date as 'التاريخ',paid as 'مدفوع' from duesTable where customerNumber=N'" + this.supplierCodeTextBox.Text + "';";

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
                            restOpsDGV.DataSource = bSource;
                            sda.Update(dbdataset);

                        }
                        catch (Exception ex)
                        {
         

                        }


                        conDataBase.Close();

                        MessageBox.Show("حفظ");

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }

                }
            }

            paidFromTextBox.ReadOnly = false;
            paidFromTextBox.BackColor = Color.FromArgb(41, 44, 51);
            paidFromTextBox.Text = "0";

            paidToTextBox.ReadOnly = false;
            paidToTextBox.BackColor = Color.FromArgb(41, 44, 51);
            paidToTextBox.Text = "0";

            notesTextBox.ReadOnly = false;
            notesTextBox.BackColor = Color.FromArgb(41, 44, 51);
            notesTextBox.Text = "";
        }

        private void restOpsDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                if (payBillRadioButton.Checked)
                {
                    if (e.RowIndex >= 0)
                    {
                        DataGridViewRow row = this.restOpsDGV.Rows[e.RowIndex];
                        Reports.salesBill salesBill = new Reports.salesBill(Convert.ToInt32(row.Cells[1].Value.ToString()));
                        salesBill.Show();
                        salesBill.BringToFront();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void restOpsDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (payBillRadioButton.Checked)
            {
                DataGridViewRow row = this.restOpsDGV.Rows[e.RowIndex];

                if (e.ColumnIndex == Column1.Index && e.RowIndex != -1 && Convert.ToBoolean(row.Cells[0].Value) == true && Convert.ToBoolean(row.Cells[6].Value) != true)
                {

                    paidFromTextBox.ReadOnly = true;
                    paidFromTextBox.BackColor = Color.FromArgb(64, 64, 64);

                    paidToTextBox.ReadOnly = true;
                    paidToTextBox.BackColor = Color.FromArgb(64, 64, 64);

                    notesTextBox.ReadOnly = true;
                    notesTextBox.BackColor = Color.FromArgb(64, 64, 64);

                    double totalProfitSum = 0;
                    for (int i = 0; i <= restOpsDGV.Rows.Count - 1; i++)
                    {
                        if (Convert.ToBoolean(restOpsDGV.Rows[i].Cells[0].Value) == true)
                        {
                            totalProfitSum += Convert.ToDouble(restOpsDGV.Rows[i].Cells[4].Value);
                        }
                    }
                    paidFromTextBox.Text = totalProfitSum.ToString();
                }

                else if (e.ColumnIndex == Column1.Index && e.RowIndex != -1 && Convert.ToBoolean(row.Cells[0].Value) == true && Convert.ToBoolean(row.Cells[6].Value) == true)
                {
                    row.Cells[0].Value = false.ToString();
                }

                if (e.ColumnIndex == Column1.Index && e.RowIndex != -1 && Convert.ToBoolean(row.Cells[0].Value) == false)
                {
                    int x = 0;
                    for (int i = 0; i <= restOpsDGV.Rows.Count - 1; i++)
                    {

                        if (Convert.ToBoolean(restOpsDGV.Rows[i].Cells[0].Value) == true)
                        {
                            x++;
                            break;
                        }


                    }
                    if (x == 0)
                    {
                        paidFromTextBox.ReadOnly = false;
                        paidFromTextBox.BackColor = Color.FromArgb(41, 44, 51);
                        paidFromTextBox.Text = "0";

                        paidToTextBox.ReadOnly = false;
                        paidToTextBox.BackColor = Color.FromArgb(41, 44, 51);
                        paidToTextBox.Text = "0";

                        notesTextBox.ReadOnly = false;
                        notesTextBox.BackColor = Color.FromArgb(41, 44, 51);
                        notesTextBox.Text = "";

                    }

                    else
                    {

                        paidFromTextBox.ReadOnly = true;
                        paidFromTextBox.BackColor = Color.FromArgb(64, 64, 64);

                        paidToTextBox.ReadOnly = true;
                        paidToTextBox.BackColor = Color.FromArgb(64, 64, 64);

                        notesTextBox.ReadOnly = true;
                        notesTextBox.BackColor = Color.FromArgb(64, 64, 64);

                        double totalProfitSum = 0;
                        for (int i = 0; i <= restOpsDGV.Rows.Count - 1; i++)
                        {
                            if (Convert.ToBoolean(restOpsDGV.Rows[i].Cells[0].Value) == true)
                            {
                                totalProfitSum += Convert.ToDouble(restOpsDGV.Rows[i].Cells[4].Value);
                            }
                        }
                        paidFromTextBox.Text = totalProfitSum.ToString();


                    }
                }
            }

            else if (payDueRadioButton.Checked)
            {
                DataGridViewRow row = this.restOpsDGV.Rows[e.RowIndex];

                if (e.ColumnIndex == Column1.Index && e.RowIndex != -1 && Convert.ToBoolean(row.Cells[0].Value) == true && Convert.ToBoolean(row.Cells[5].Value) != true)
                {

                    paidFromTextBox.ReadOnly = true;
                    paidFromTextBox.BackColor = Color.FromArgb(64, 64, 64);

                    paidToTextBox.ReadOnly = true;
                    paidToTextBox.BackColor = Color.FromArgb(64, 64, 64);

                    notesTextBox.ReadOnly = true;
                    notesTextBox.BackColor = Color.FromArgb(64, 64, 64);

                    double totalProfitSum = 0;
                    for (int i = 0; i <= restOpsDGV.Rows.Count - 1; i++)
                    {
                        if (Convert.ToBoolean(restOpsDGV.Rows[i].Cells[0].Value) == true)
                        {
                            totalProfitSum += Convert.ToDouble(restOpsDGV.Rows[i].Cells[3].Value);
                        }
                    }
                    paidFromTextBox.Text = totalProfitSum.ToString();
                }

                else if (e.ColumnIndex == Column1.Index && e.RowIndex != -1 && Convert.ToBoolean(row.Cells[0].Value) == true && Convert.ToBoolean(row.Cells[5].Value) == true)
                {
                    row.Cells[0].Value = false.ToString();
                }

                if (e.ColumnIndex == Column1.Index && e.RowIndex != -1 && Convert.ToBoolean(row.Cells[0].Value) == false)
                {
                    int x = 0;
                    for (int i = 0; i <= restOpsDGV.Rows.Count - 1; i++)
                    {

                        if (Convert.ToBoolean(restOpsDGV.Rows[i].Cells[0].Value) == true)
                        {
                            x++;
                            break;
                        }


                    }
                    if (x == 0)
                    {
                        paidFromTextBox.ReadOnly = false;
                        paidFromTextBox.BackColor = Color.FromArgb(41, 44, 51);
                        paidFromTextBox.Text = "0";

                        paidToTextBox.ReadOnly = false;
                        paidToTextBox.BackColor = Color.FromArgb(41, 44, 51);
                        paidToTextBox.Text = "0";

                        notesTextBox.ReadOnly = false;
                        notesTextBox.BackColor = Color.FromArgb(41, 44, 51);
                        notesTextBox.Text = "";

                    }

                    else
                    {

                        paidFromTextBox.ReadOnly = true;
                        paidFromTextBox.BackColor = Color.FromArgb(64, 64, 64);

                        paidToTextBox.ReadOnly = true;
                        paidToTextBox.BackColor = Color.FromArgb(64, 64, 64);

                        notesTextBox.ReadOnly = true;
                        notesTextBox.BackColor = Color.FromArgb(64, 64, 64);

                        double totalProfitSum = 0;
                        for (int i = 0; i <= restOpsDGV.Rows.Count - 1; i++)
                        {
                            if (Convert.ToBoolean(restOpsDGV.Rows[i].Cells[0].Value) == true)
                            {
                                totalProfitSum += Convert.ToDouble(restOpsDGV.Rows[i].Cells[3].Value);
                            }
                        }
                        paidFromTextBox.Text = totalProfitSum.ToString();


                    }
                }

            }
        }

        private void restOpsDGV_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == Column1.Index && e.RowIndex != -1)
            {
                restOpsDGV.EndEdit();
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            fill();
            deleteButton.Visible = false;
            payBillRadioButton.Checked = true;
        }

        public void refreshLocal()
        {
            fill();
            deleteButton.Visible = false;
            payBillRadioButton.Checked = true;
        }
    }
}
