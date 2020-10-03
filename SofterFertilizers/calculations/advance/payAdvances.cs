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

namespace SofterFertilizers.calculations.advance
{
    public partial class payAdvances : UserControl
    {
        public payAdvances()
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
            loanNumberComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct Id from loansMainTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    loanNumberComboBox.Items.Add(dr["Id"].ToString());
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (loanNumberComboBox.Items.Count > 0)
            {
                loanNumberComboBox.Text = loanNumberComboBox.Items[0].ToString();
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

        private void loanNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;

            string Query = "select debtorder as 'رقم القسط'  ,debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط',status as 'الحالة',paidAmount as 'قيمة المدفوع', paidDate as 'تاريخ الدفع' from advanceTable where advanceMainTableNumber =N'" + this.loanNumberComboBox.Text + "' ;";

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

                c = Convert.ToInt32(a + f);
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
                    string Query = "UPDATE advanceTable SET status ='TRUE', paidDate=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "', paidAmount=N'" + this.totalPaidTextBox.Text + "'  where debtorder=N'" + this.divisionNumber.Text + "' and advanceMainTableNumber=N'" + this.loanNumberComboBox.Text + "' ";
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

                    Query = "DELETE FROM safeTable where billNo = N'" + loanNumberComboBox.Text + "' and type='advancePay' and details ='in' and notes = N'قسط رقم " + divisionNumber.Text + "' ;";
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
                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,N'قسط رقم " + divisionNumber.Text + "',N'" + this.paidAmount.Text + "','advancePay',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.loanNumberComboBox.Text + "','cash','')";
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




                    //DGV update

                    categoryDGV.DataSource = null;

                    Query = "select debtorder as 'رقم القسط'  ,debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط',status as 'الحالة',paidAmount as 'قيمة المدفوع', paidDate as 'تاريخ الدفع' from advanceTable where advanceMainTableNumber =N'" + this.loanNumberComboBox.Text + "' ;";

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
                    string Query = "UPDATE advanceTable Set status ='False', paidDate=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "', paidAmount=N'" + this.totalPaidTextBox.Text + "'  where debtorder=N'" + this.divisionNumber.Text + "' and advanceMainTableNumber= N'" + this.loanNumberComboBox.Text + "'";
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

                    Query = "DELETE FROM safeTable where billNo = N'" + loanNumberComboBox.Text + "' and type='advancePay' and details ='in' and notes = N'قسط رقم " + divisionNumber.Text + "' ;";
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
                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,N'قسط رقم " + divisionNumber.Text + "',N'" + this.paidAmount.Text + "','advancePay',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.loanNumberComboBox.Text + "','cash','')";
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


                    //DGV update

                    categoryDGV.DataSource = null;

                    Query = "select debtorder as 'رقم القسط'  ,debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط',status as 'الحالة',paidAmount as 'قيمة المدفوع', paidDate as 'تاريخ الدفع' from advanceTable where advanceMainTableNumber =N'" + this.loanNumberComboBox.Text + "' ;";

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
                    string Query = "UPDATE advanceTable SET status ='TRUE', paidDate=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "', paidAmount=N'" + this.totalPaidTextBox.Text + "'  where debtorder=N'" + this.divisionNumber.Text + "' and advanceMainTableNumber=N'" + this.loanNumberComboBox.Text + "' ";
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

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,N'قسط رقم " + divisionNumber.Text + "',N'" + this.paidAmount.Text + "','advancePay',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.loanNumberComboBox.Text + "','cash','')";
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



                    categoryDGV.DataSource = null;

                    Query = "select debtorder as 'رقم القسط'  ,debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط',status as 'الحالة',paidAmount as 'قيمة المدفوع', paidDate as 'تاريخ الدفع' from advanceTable where advanceMainTableNumber =N'" + this.loanNumberComboBox.Text + "' ;";

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
                    string Query = "UPDATE advanceTable SET status ='False' ,paidDate=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "', paidAmount=N'" + this.totalPaidTextBox.Text + "'  where debtorder=N'" + this.divisionNumber.Text + "' and advanceMainTableNumber=N'" + this.loanNumberComboBox.Text + "'";
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

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,N'قسط رقم " + divisionNumber.Text + "',N'" + this.paidAmount.Text + "','advanceMainTableNumber',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.loanNumberComboBox.Text + "','cash','')";
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

                    categoryDGV.DataSource = null;

                    Query = "select debtorder as 'رقم القسط'  ,debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط',status as 'الحالة',paidAmount as 'قيمة المدفوع', paidDate as 'تاريخ الدفع' from advanceTable where advanceMainTableNumber =N'" + this.loanNumberComboBox.Text + "' ;";

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
                paidAmount.ReadOnly = true;
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
        public void refreshLocal()
        {
            fill();
            clear();

        }
    }
}
