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


//TODO Testing

namespace SofterFertilizers.sales
{
    public partial class payDivision : UserControl
    {
        public payDivision()
        {
            InitializeComponent();
            fill();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string oldAmount;
        string adjustString;

        void clear()
        {
            divisionNumber.Text = "";
            divisionAmount.Text = "";
            oldPaidAmount.Text = "";
            paidAmount.Text = "";
            totalPaidTextBox.Text = "";  
        }

        void fill()
        {

            //supplier ComboBox
            supplierNameComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
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
            Query = "select distinct name from safeTable;";
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

                //billNumber comboBox
                billNumberComboBox.Items.Clear();
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string Query = "select distinct billNumber from debtsMainTable where billNumber in (select Id from salesMainTable where customerName=N'"+this.supplierNameComboBox.Text+"')  ;";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
                da.Fill(dt);
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        billNumberComboBox.Items.Add(dr["billNumber"].ToString());
                    }
                }
                catch (Exception ex)
                {
 
                }
                conDataBase.Close();

                if (billNumberComboBox.Items.Count > 0)
                {
                    billNumberComboBox.Text = billNumberComboBox.Items[0].ToString();
                }

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

              

                //billNumber comboBox
                billNumberComboBox.Items.Clear();
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
               string Query = "select distinct billNumber from debtsMainTable where billNumber in (select Id from salesMainTable where customerName=N'" + this.supplierNameComboBox.Text + "')  ;";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
                da.Fill(dt);
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        billNumberComboBox.Items.Add(dr["billNumber"].ToString());
                    }
                }
                catch (Exception ex)
                {
 
                }
                conDataBase.Close();

                if (billNumberComboBox.Items.Count > 0)
                {
                    billNumberComboBox.Text = billNumberComboBox.Items[0].ToString();
                }

            }
            catch
            {

            }   
        }

        private void billNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;

            string Query = "select debtorder as 'رقم القسط'  ,debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط',status as 'الحالة',paidAmount as 'قيمة المدفوع', paidDate as 'تاريخ الدفع' from debtsTable where debtMainTableNumber =(select Id from debtsMainTable where billNumber = N'"+this.billNumberComboBox.Text+"') ;";

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
            if (e.RowIndex == 0)
            {
                DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
               

                this.divisionNumber.Text = row.Cells[0].Value.ToString();
                this.divisionAmount.Text = row.Cells[1].Value.ToString();
                this.oldPaidAmount.Text = row.Cells[4].Value.ToString();
                oldAmount = row.Cells[4].Value.ToString();
            }
            else if (e.RowIndex > 0)
            {
                bool previousDebtState = Convert.ToBoolean(this.categoryDGV.Rows[e.RowIndex - 1].Cells[3].Value.ToString());
                    if (previousDebtState)
                {
                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];

                    this.divisionNumber.Text = row.Cells[0].Value.ToString();
                    this.divisionAmount.Text = row.Cells[1].Value.ToString();
                    this.oldPaidAmount.Text = row.Cells[4].Value.ToString();
                    oldAmount = row.Cells[4].Value.ToString();
                }
                else
                {
                    MessageBox.Show("لا يمكن دفع قسط جديد ما لم يتم الانتهاء من الأقساط قبله");
                }
            }
        }

        void sumFunction()
        {
            try
            {
                totalPaidTextBox.ResetText();
                double a, b, f;
                int c;
                double.TryParse(oldPaidAmount.Text, out a);
                double.TryParse(paidAmount.Text, out f);

                c = Convert.ToInt32(a +  f);
                totalPaidTextBox.Text = c.ToString("G29");
            }
            catch
            {

            }
        }

        private void paidAmount_TextChanged(object sender, EventArgs e)
        {
            if (adjustCheckBox.Checked == false)
            {
                sumFunction();
            }
        }

        private void oldPaidAmount_TextChanged(object sender, EventArgs e)
        {
                totalPaidTextBox.Text = oldPaidAmount.Text; 
        }

        private void totalPaidTextBox_TextChanged(object sender, EventArgs e)
        {
              if (adjustCheckBox.Checked == true)
            {
                paidAmount.Text = totalPaidTextBox.Text;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (adjustCheckBox.Checked)
            {
                if (Convert.ToInt32(divisionAmount.Text) == Convert.ToInt32(totalPaidTextBox.Text))
                {
                    string Query = "UPDATE debtsTable SET status ='TRUE', paidDate=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "', paidAmount=N'" + this.totalPaidTextBox.Text + "'  where debtorder=N'" + this.divisionNumber.Text + "' and debtMainTableNumber=(select Id from debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "') ";
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

                    Query = "DELETE FROM safeTable where billNo = N'" + billNumberComboBox.Text + "' and type='divisionPay' and details ='in' and notes = N'قسط رقم " + divisionNumber.Text + "' ;";
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

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,N'قسط رقم " + divisionNumber.Text + "',N'" + this.paidAmount.Text + "','divisionPay',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.billNumberComboBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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

                    Query = "INSERT INTO customerBalanceTable(customerNumber,billNumber,billDetails,direction,safeName,amount,date,inside) VALUES (N'" + this.supplierCodeTextBox.Text + "',N'" + this.billNumberComboBox.Text + "',N'تعديل قسط رقم " + divisionNumber.Text + "',N'للعميل',N'" + this.safeComboBox.Text + "',N'" + oldAmount + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','False')";
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

                    //Adjust Balance Update
                    int supplierBalance = 0;
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                    if (supplierBalanceString != "")
                    {
                        supplierBalance = Convert.ToInt32(supplierBalanceString);
                        conDataBase.Close();
                    }

                    supplierBalance += Convert.ToInt32(oldAmount);

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

                    //new entry
                    Query = "INSERT INTO customerBalanceTable(customerNumber,billNumber,billDetails,direction,safeName,amount,date,inside) VALUES (N'" + this.supplierCodeTextBox.Text + "',N'" + this.billNumberComboBox.Text + "',N'قسط رقم " + divisionNumber.Text + "',N'من العميل',N'" + this.safeComboBox.Text + "',N'" + this.totalPaidTextBox.Text + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','False')";
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

                    //supplier Balance Update
                    supplierBalance = 0;
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                    if (supplierBalanceString != "")
                    {
                        supplierBalance = Convert.ToInt32(supplierBalanceString);
                        conDataBase.Close();
                    }

                    supplierBalance -= Convert.ToInt32(this.totalPaidTextBox.Text);

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


                    //DGV update
                    categoryDGV.DataSource = null;

                    Query = "select debtorder as 'رقم القسط'  ,debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط',status as 'الحالة',paidAmount as 'قيمة المدفوع', paidDate as 'تاريخ الدفع' from debtsTable where debtMainTableNumber =(select Id from debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "') ;";

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

                    clear();

                    MessageBox.Show("حفظ");

                }

                else if (Convert.ToInt32(divisionAmount.Text) > Convert.ToInt32(totalPaidTextBox.Text))
                {
                    string Query = "UPDATE debtsTable Set status ='False', paidDate=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "', paidAmount=N'" + this.totalPaidTextBox.Text + "'  where debtorder=N'" + this.divisionNumber.Text + "' and debtMainTableNumber=(select Id from debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "') ";
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

                    Query = "DELETE FROM safeTable where billNo = N'" + billNumberComboBox.Text + "' and type='divisionPay' and details ='in' and notes = N'قسط رقم " + divisionNumber.Text + "' ;";
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

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,N'قسط رقم " + divisionNumber.Text + "',N'" + this.paidAmount.Text + "','divisionPay',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.billNumberComboBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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

                    //Adjust Balance Update

                    Query = "INSERT INTO customerBalanceTable(customerNumber,billNumber,billDetails,direction,safeName,amount,date,inside) VALUES (N'" + this.supplierCodeTextBox.Text + "',N'" + this.billNumberComboBox.Text + "',N'تعديل قسط رقم " + divisionNumber.Text + "',N'للعميل',N'" + this.safeComboBox.Text + "',N'" + oldAmount + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','False')";
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

                    int supplierBalance = 0;
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                    if (supplierBalanceString != "")
                    {
                        supplierBalance = Convert.ToInt32(supplierBalanceString);
                        conDataBase.Close();
                    }

                    supplierBalance += Convert.ToInt32(oldAmount);

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

                    Query = "INSERT INTO customerBalanceTable(customerNumber,billNumber,billDetails,direction,safeName,amount,date,inside) VALUES (N'" + this.supplierCodeTextBox.Text + "',N'" + this.billNumberComboBox.Text + "',N'قسط رقم " + divisionNumber.Text + "',N'من العميل',N'" + this.safeComboBox.Text + "',N'" + this.totalPaidTextBox.Text + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','False')";
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

                    //supplier Balance Update
                    supplierBalance = 0;
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                    if (supplierBalanceString != "")
                    {
                        supplierBalance = Convert.ToInt32(supplierBalanceString);
                        conDataBase.Close();
                    }

                    supplierBalance -= Convert.ToInt32(this.totalPaidTextBox.Text);

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


                    //DGV update
                    categoryDGV.DataSource = null;

                    Query = "select debtorder as 'رقم القسط'  ,debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط',status as 'الحالة',paidAmount as 'قيمة المدفوع', paidDate as 'تاريخ الدفع' from debtsTable where debtMainTableNumber =(select Id from debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "') ;";

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

                    clear();

                    MessageBox.Show("حفظ");

                }

                else if (Convert.ToInt32(divisionAmount.Text) < Convert.ToInt32(totalPaidTextBox.Text))
                {
                    MessageBox.Show("المدفوع أكبر من قيمة القسط");
                }

                adjustCheckBox.Checked = false;
            }
            else
            {
                if (Convert.ToInt32(divisionAmount.Text) == Convert.ToInt32(totalPaidTextBox.Text))
                {
                    string Query = "UPDATE debtsTable SET status ='TRUE', paidDate=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "', paidAmount=N'" + this.totalPaidTextBox.Text + "'  where debtorder=N'" + this.divisionNumber.Text + "' and debtMainTableNumber=(select Id from debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "') ";
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

                    //الخزنة

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,N'قسط رقم " + divisionNumber.Text + "',N'" + this.paidAmount.Text + "','divisionPay',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.billNumberComboBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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

                    Query = "INSERT INTO customerBalanceTable(customerNumber,billNumber,billDetails,direction,safeName,amount,date,inside) VALUES (N'" + this.supplierCodeTextBox.Text + "',N'" + this.billNumberComboBox.Text + "',N'قسط رقم " + divisionNumber.Text + "',N'من العميل',N'" + this.safeComboBox.Text + "',N'" + this.paidAmount.Text + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','False')";
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

                    //supplier Balance Update
                    int supplierBalance = 0;
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                    if (supplierBalanceString != "")
                    {
                        supplierBalance = Convert.ToInt32(supplierBalanceString);
                        conDataBase.Close();
                    }
                    //TODO ERROR
                    supplierBalance -= Convert.ToInt32(this.paidAmount.Text);

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


                    //DGV update
                    categoryDGV.DataSource = null;

                    Query = "select debtorder as 'رقم القسط',debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط',status as 'الحالة',paidAmount as 'قيمة المدفوع', paidDate as 'تاريخ الدفع' from debtsTable where debtMainTableNumber =(select Id from debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "') ;";

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

                    clear();

                    MessageBox.Show("حفظ");

                }

                else if (Convert.ToInt32(divisionAmount.Text) > Convert.ToInt32(totalPaidTextBox.Text))
                {
                    string Query = "UPDATE debtsTable SET status ='False' ,paidDate=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "', paidAmount=N'" + this.totalPaidTextBox.Text + "'  where debtorder=N'" + this.divisionNumber.Text + "' and debtMainTableNumber=(select Id from debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "') ";
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


                    //الخزنة

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,N'قسط رقم " + divisionNumber.Text + "',N'" + this.paidAmount.Text + "','divisionPay',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.billNumberComboBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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

                    Query = "INSERT INTO customerBalanceTable(customerNumber,billNumber,billDetails,direction,safeName,amount,date,inside) VALUES (N'" + this.supplierCodeTextBox.Text + "',N'" + this.billNumberComboBox.Text + "',N'قسط رقم " + divisionNumber.Text + "',N'من العميل',N'" + this.safeComboBox.Text + "',N'" + this.paidAmount.Text + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','False')";
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

                    //supplier Balance Update
                    int supplierBalance = 0;
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + this.supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                    if (supplierBalanceString != "")
                    {
                        supplierBalance = Convert.ToInt32(supplierBalanceString);
                        conDataBase.Close();
                    }

                    supplierBalance -= Convert.ToInt32(this.paidAmount.Text);

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


                    //DGV update
                    categoryDGV.DataSource = null;

                    Query = "select debtorder as 'رقم القسط'  ,debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط',status as 'الحالة',paidAmount as 'قيمة المدفوع', paidDate as 'تاريخ الدفع' from debtsTable where debtMainTableNumber =(select Id from debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "') ;";

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

                    clear();

                    MessageBox.Show("حفظ");

                }

                else if (Convert.ToInt32(divisionAmount.Text) < Convert.ToInt32(totalPaidTextBox.Text))
                {
                    MessageBox.Show("المدفوع أكبر من قيمة القسط");
                }

                adjustCheckBox.Checked = false;

            }
        }

        private void adjustCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (adjustCheckBox.Checked)
            {
                oldPaidAmount.BackColor = Color.FromArgb(41, 44, 51);
                oldPaidAmount.ReadOnly = false;
                paidAmount.BackColor = Color.FromArgb(64, 64, 64);
                paidAmount.ReadOnly = true ;
                clear();
            }

            else
            {
                oldPaidAmount.BackColor = Color.FromArgb(64, 64, 64);
                oldPaidAmount.ReadOnly = true;
                paidAmount.BackColor = Color.FromArgb(41, 44, 51);
                paidAmount.ReadOnly = false;

                clear();
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            fill();
            clear();

        }
        public void refreshLocal()
        {
            fill();
            clear();
        }
    }
}
