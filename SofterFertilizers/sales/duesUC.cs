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



namespace SofterFertilizers.sales
{
    public partial class duesUC : UserControl
    {
        public duesUC()
        {
            InitializeComponent();
            fill();
            deleteButton.Visible = false;
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string state = "new";
        string oldAmount;
        bool paid;

        void clear()
        {
            amountTextBox.Text = "0";
            notesTextBox.Text = "";
            deleteButton.Visible = false;
        }

        void fill()
        {
            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from duesTable) BEGIN SELECT MAX(Id) FROM duesTable END", conDataBase).ExecuteScalar().ToString();

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

                string Query = "select Id as 'رقم الاستحقاق',billDetails as 'التفاصيل',amount as 'المبلغ',date as 'التاريخ',paid as 'مدفوع' from duesTable where customerNumber=N'" + this.supplierCodeTextBox.Text + "';";

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
            catch
            {
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(amountTextBox.Text) != 0)
            {
                if (state == "new")
                {
                    string Query = "IF NOT EXISTS(select 1 from duesTable where Id=N'" + this.codeTextBox.Text + "') BEGIN INSERT INTO duesTable(customerNumber,billDetails,amount,date,paid) VALUES (N'" + this.supplierCodeTextBox.Text + "',N'" + this.notesTextBox.Text + "' ,N'" + this.amountTextBox.Text + "' ,N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','False') END";
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
                    string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                    if (supplierBalanceString != "")
                    {
                        supplierBalance = Convert.ToDouble(supplierBalanceString);
                        conDataBase.Close();
                    }

                    supplierBalance += Convert.ToDouble(amountTextBox.Text);

                    Query = "IF EXISTS (select 1 from customerTable where name=N'" + supplierNameComboBox.Text + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + supplierNameComboBox.Text + "' END";
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
                        categoryDGV.DataSource = bSource;
                        sda.Update(dbdataset);

                    }
                    catch (Exception ex)
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
                    if (!paid)
                    {
                        string Query = "IF EXISTS (select 1 from duesTable where Id = N'" + this.codeTextBox.Text + "') BEGIN UPDATE duesTable SET customerNumber =N'" + this.supplierCodeTextBox.Text + "', billDetails=N'" + this.notesTextBox.Text + "', amount=N'" + this.amountTextBox.Text + "',date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' where Id=N'" + this.codeTextBox.Text + "' END";
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
                        string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                        if (supplierBalanceString != "")
                        {
                            supplierBalance = Convert.ToDouble(supplierBalanceString);
                            conDataBase.Close();
                        }

                        supplierBalance -= Convert.ToDouble(oldAmount);

                        Query = "IF EXISTS (select 1 from customerTable where name=N'" + supplierNameComboBox.Text + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + supplierNameComboBox.Text + "' END";
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

                        supplierBalance += Convert.ToDouble(this.amountTextBox.Text);

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
                            categoryDGV.DataSource = bSource;
                            sda.Update(dbdataset);

                        }
                        catch (Exception ex)
                        {
         

                        }

                        conDataBase.Close();


                        deleteButton.Visible = false;
                        fill();
                        state = "new";
                        clear();
                        MessageBox.Show("حفظ");
                    }
                    else
                    {
                        MessageBox.Show("لا يمكن تعديل بند مدفوع");


                        deleteButton.Visible = false;
                        fill();
                        state = "new";
                        clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("ليس هناك مبلغ ليتم إضافته");
            }
        }

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];

                    state = "adjust";
                    clear();
                    deleteButton.Visible = true;
                    this.codeTextBox.Text = row.Cells[0].Value.ToString();
                    
                    this.notesTextBox.Text = row.Cells[1].Value.ToString();
                    
                    this.amountTextBox.Text = row.Cells[2].Value.ToString();
                    oldAmount = row.Cells[2].Value.ToString();
                   
                    this.dateDTP.Text = row.Cells[3].Value.ToString();

                    paid = Convert.ToBoolean(row.Cells[4].Value.ToString());

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

            if (!paid)
            {
                DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                 
                    double supplierBalance = 0;
                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string supplierBalanceString = new SqlCommand("IF EXISTS (select 1 from customerTable where name=N'" + supplierNameComboBox.Text + "') BEGIN SELECT balance from customerTable where name=N'" + supplierNameComboBox.Text + "' END", conDataBase).ExecuteScalar().ToString();

                    if (supplierBalanceString != "")
                    {
                        supplierBalance = Convert.ToDouble(supplierBalanceString);
                        conDataBase.Close();
                    }

                    supplierBalance -= Convert.ToDouble(oldAmount);

                    string Query = "IF EXISTS (select 1 from customerTable where name=N'" + supplierNameComboBox.Text + "')BEGIN UPDATE customerTable SET balance=N'" + supplierBalance + "' where name=N'" + supplierNameComboBox.Text + "' END";
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


                     Query = "IF EXISTS(select 1 from duesTable where Id=N'" + this.codeTextBox.Text + "') BEGIN DELETE FROM duesTable where Id= N'" + this.codeTextBox.Text + "' END";
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


                    state = "new";
                    clear();
                    fill();
                    //category DGV
                    categoryDGV.DataBindings.Clear();

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
            else
            {
                MessageBox.Show("لا يمكن حذف استحقاق مدفوع");

                deleteButton.Visible = false;
                fill();
                state = "new";
                clear();
            }
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
