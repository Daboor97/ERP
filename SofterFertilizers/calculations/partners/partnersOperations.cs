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

namespace SofterFertilizers.calculations.partners
{
    public partial class partnersOperations : UserControl
    {
        public partnersOperations()
        {
            InitializeComponent();
            fill();
            deleteButton.Visible = false;
        }


        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string state = "new";
        string oldAmount;
        string oldSupplier;
        string oldbill;

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
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from partnersBalanceTable) BEGIN SELECT MAX(Id) FROM partnersBalanceTable END", conDataBase).ExecuteScalar().ToString();

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
            string Query = "select distinct name from partnersTable;";
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
            catch {
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
                supplierCodeTextBox.Text = new SqlCommand("select Id from partnersTable where name=N'" + this.supplierNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();


            }
            catch
            {
            }
        }

        private void partnerCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //supplierName
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                supplierNameComboBox.Text = new SqlCommand("IF EXISTS(select 1 from partnersTable where Id=N'" + this.supplierCodeTextBox.Text + "') BEGIN select name from partnersTable where Id=N'" + this.supplierCodeTextBox.Text + "' END ;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();


                //category DGV
                categoryDGV.DataSource = null;
                string Query = "select partnersBalanceTable.Id as 'رقم الإيصال', partnersTable.name as 'اسم الشريك' , amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', partnersBalanceTable.notes as 'التفاصيل',date as 'التاريخ' from partnersBalanceTable,partnersTable where partnersTable.Id=N'" + this.supplierCodeTextBox.Text + "' and partnersBalanceTable.partnerNumber = partnersTable.Id;";

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
                conDataBase.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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


                c = Convert.ToDouble(Math.Abs(f - a));
                sumTextBox.Text = c.ToString("G29");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
              if (Int32.Parse(sumTextBox.Text) != 0)
                {
                    string direction;
                if (Convert.ToDouble(paidToTextBox.Text) < Convert.ToDouble(paidFromTextBox.Text))
                {
                    direction = "من الشريك";
                }
                else
                {
                    direction = "للشريك";
                }

                    if (state == "new")
                    {
                        string Query = "IF NOT EXISTS(select 1 from partnersBalanceTable where Id=N'" + this.codeTextBox.Text + "') BEGIN INSERT INTO partnersBalanceTable(partnerNumber,direction,safeName,amount,notes,date) VALUES (N'" + this.supplierCodeTextBox.Text + "',N'" + direction + "',N'" + this.safeComboBox.Text + "',N'" + this.sumTextBox.Text + "' ,N'"+this.notesTextBox.Text+ "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "')END";
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
                    catch
                    {
                    }


                    if (direction == "للشريك")
                    {

                        Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.sumTextBox.Text + "','partnerBalance',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.codeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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

                        catch
                        {
                        }

                        conDataBase.Close();
                    }
                    else
                    {
                        Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.sumTextBox.Text + "','partnerBalance',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.codeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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

                        catch
                        {
                        }

                        conDataBase.Close();
                    }

                    //category DGV
                    categoryDGV.DataSource = null;
                    Query = "select partnersBalanceTable.Id as 'رقم الإيصال', partnersTable.name as 'اسم الشريك' , amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', partnersBalanceTable.notes as 'التفاصيل',date as 'التاريخ' from partnersBalanceTable,partnersTable where partnersTable.Id=N'" + this.supplierCodeTextBox.Text + "' and partnersBalanceTable.partnerNumber = partnersTable.Id;";

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
                    catch
                    {
                    }

                    conDataBase.Close();

                   
                        deleteButton.Visible = false;
                        fill();
                        state = "new";
                        clear();

                        MessageBox.Show("حفظ");
                    }


                    else if (state == "adjust")
                    {
                     string Query = "IF EXISTS (select 1 from partnersBalanceTable where Id = N'" + this.codeTextBox.Text + "') BEGIN UPDATE partnersBalanceTable SET partnerNumber =N'" + this.supplierCodeTextBox.Text + "', notes=N'" + this.notesTextBox.Text + "', direction=N'" + direction + "',safeName=N'" + this.safeComboBox.Text + "', amount=N'" + this.sumTextBox.Text + "',date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' where Id=N'" + this.codeTextBox.Text + "' END";
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
                    catch
                    {
                    }

                    Query = "DELETE FROM safeTable where type='partnerBalance' and billNo = N'" + this.codeTextBox.Text + "';";
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
                    catch
                    {
                    }

                    //الخزنة

                    if (direction == "للشريك")
                    {

                        Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.sumTextBox.Text + "','partnerBalance',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.codeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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

                        catch
                        {
                        }

                        conDataBase.Close();
                    }
                    else
                    {
                        Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.sumTextBox.Text + "','partnerBalance',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.codeTextBox.Text + "','cash',N'" + this.supplierCodeTextBox.Text + "')";
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

                        catch
                        {
                        }

                        conDataBase.Close();
                    }

                    //category DGV
                    categoryDGV.DataSource = null;

                    Query = "select partnersBalanceTable.Id as 'رقم الإيصال', partnersTable.name as 'اسم الشريك' , amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', partnersBalanceTable.notes as 'التفاصيل',date as 'التاريخ' from partnersBalanceTable,partnersTable where partnersTable.Id=N'" + this.supplierCodeTextBox.Text + "' and partnersBalanceTable.partnerNumber = partnersTable.Id;";

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
                    catch
                    {
                    }

                    conDataBase.Close();


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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟\n\nستسترجع جميع القيم قبل البند", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string Query = "DELETE FROM partnersBalanceTable where Id= N'" + this.codeTextBox.Text + "'";
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
                catch
                {
                }

                Query = "DELETE FROM safeTable where type='partnerBalance' and billNo = N'" + this.codeTextBox.Text + "';";
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
                catch
                {
                }

                deleteButton.Visible = false;
                state = "new";
                clear();
                fill();

                //category DGV
                categoryDGV.DataSource = null;

                Query = "select partnersBalanceTable.Id as 'رقم الإيصال', partnersTable.name as 'اسم الشريك' , amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', partnersBalanceTable.notes as 'التفاصيل',date as 'التاريخ' from partnersBalanceTable,partnersTable where partnersTable.Id=N'" + this.supplierCodeTextBox.Text + "' and partnersBalanceTable.partnerNumber = partnersTable.Id;";

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
                catch
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

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
                    deleteButton.Visible = true;
                    this.codeTextBox.Text = row.Cells[0].Value.ToString();
                    this.supplierNameComboBox.Text = row.Cells[1].Value.ToString();
                    if (row.Cells[3].Value.ToString() == "للشريك")
                    {
                        this.paidToTextBox.Text = row.Cells[2].Value.ToString();
                    }
                    else
                    {
                        this.paidFromTextBox.Text = row.Cells[2].Value.ToString();
                    }
                    this.sumTextBox.Text = row.Cells[2].Value.ToString();
                    this.safeComboBox.Text = row.Cells[4].Value.ToString();
                    this.notesTextBox.Text = row.Cells[5].Value.ToString();
                    this.dateDTP.Text = row.Cells[6].Value.ToString();
                   
                    state = "adjust";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void codeSearchTextBox_TextChanged(object sender, EventArgs e)
        {

            //category DGV
            categoryDGV.DataSource = null;

            string Query = "select partnersBalanceTable.Id as 'رقم الإيصال', partnersTable.name as 'اسم الشريك' , amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', partnersBalanceTable.notes as 'التفاصيل',date as 'التاريخ' from partnersBalanceTable,partnersTable where partnersTable.Id=N'" + this.supplierCodeTextBox.Text + "' and partnersBalanceTable.partnerNumber = partnersTable.Id and partnersBalanceTable.Id like N'%" + this.codeSearchTextBox.Text + "%';";

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
            catch
            {
            }

            conDataBase.Close();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            fill();
            deleteButton.Visible = false;
            clear();
        }
        public void refreshLocal()
        {
            fill();
            deleteButton.Visible = false;
            clear();
        }
    }
}
