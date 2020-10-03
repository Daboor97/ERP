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


namespace SofterFertilizers.calculations
{
    public partial class transferBetweenSafe : UserControl
    {
        public transferBetweenSafe()
        {
            InitializeComponent();
            fill();
            deleteButton.Visible = false;

        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        string oldFromSafe;
        string oldToSafe;
        string oldAmount;
        string state = "new";
        void clear()
        {
            amountTransferredTextbox.Text = "0";
        }

        void fill()
        {
            //fromSafeComboBox
            fromSafeComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
           string Query = "select distinct name from safeMainTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    fromSafeComboBox.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (fromSafeComboBox.Items.Count > 0)
            {
                fromSafeComboBox.Text = fromSafeComboBox.Items[0].ToString();
            }

            //toSafeComboBox
            toSafeComboBox.Items.Clear();
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
                    toSafeComboBox.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (toSafeComboBox.Items.Count > 0)
            {
                toSafeComboBox.Text = toSafeComboBox.Items[1].ToString();
            }

            Query = "select Id as 'رقم التحويل', safeTransfer.fromSafe as 'من خزنة' , toSafe as 'إلى خزنة' ,amount as 'المبلغ',date as 'التاريخ' from safeTransfer;";

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
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

            // code textBox
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from safeTransfer) BEGIN SELECT MAX(Id) FROM safeTransfer END", conDataBase).ExecuteScalar().ToString();

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


             conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string sumInFrom = new SqlCommand("Select Sum(money) from safeTable where details = 'in' and name=N'" + this.fromSafeComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            sumInFrom = (string.IsNullOrEmpty(sumInFrom)) ? "0" : sumInFrom;

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string sumOutFrom = new SqlCommand("Select Sum(money) from safeTable where details = 'out' and name=N'" + this.fromSafeComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            sumOutFrom = (string.IsNullOrEmpty(sumOutFrom)) ? "0" : sumOutFrom;

            fromSafeBalanceTextBox.Text = (Convert.ToDouble(sumInFrom) - Convert.ToDouble(sumOutFrom)).ToString();


             conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string sumInTo = new SqlCommand("Select Sum(money) from safeTable where details = 'in' and name=N'" + this.toSafeComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            sumInTo = (string.IsNullOrEmpty(sumInTo)) ? "0" : sumInTo;

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string sumOutTo = new SqlCommand("Select Sum(money) from safeTable where details = 'out' and name=N'" + this.toSafeComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            sumOutTo = (string.IsNullOrEmpty(sumOutTo)) ? "0" : sumOutTo;

            toSafeBalanceTextBox.Text = (Convert.ToDouble(sumInTo) - Convert.ToDouble(sumOutTo)).ToString();
        }



        private void fromSafeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string sumInFrom = new SqlCommand("Select Sum(money) from safeTable where details = 'in' and name=N'" + this.fromSafeComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            sumInFrom = (string.IsNullOrEmpty(sumInFrom)) ? "0" : sumInFrom;

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string sumOutFrom = new SqlCommand("Select Sum(money) from safeTable where details = 'out' and name=N'" + this.fromSafeComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            sumOutFrom = (string.IsNullOrEmpty(sumOutFrom)) ? "0" : sumOutFrom;

            fromSafeBalanceTextBox.Text = (Convert.ToDouble(sumInFrom) - Convert.ToDouble(sumOutFrom)).ToString();

        }

        private void toSafeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string sumInTo = new SqlCommand("Select Sum(money) from safeTable where details = 'in' and name=N'" + this.toSafeComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            sumInTo = (string.IsNullOrEmpty(sumInTo)) ? "0" : sumInTo;

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string sumOutTo = new SqlCommand("Select Sum(money) from safeTable where details = 'out' and name=N'" + this.toSafeComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            sumOutTo = (string.IsNullOrEmpty(sumOutTo)) ? "0" : sumOutTo;

            toSafeBalanceTextBox.Text = (Convert.ToDouble(sumInTo) - Convert.ToDouble(sumOutTo)).ToString();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            if (state == "new")
            {
                string Query = "INSERT INTO safeTransfer(safeTransfer.fromSafe,toSafe,amount,date) VALUES (N'" + this.fromSafeComboBox.Text + "',N'" + this.toSafeComboBox.Text + "',N'" + this.amountTransferredTextbox.Text + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "') ";
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

                Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.toSafeComboBox.Text + "' ,'',N'" + this.amountTransferredTextbox.Text + "' ,'safeTransfer',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.codeTextBox.Text + "','','')";
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

                Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.fromSafeComboBox.Text + "' ,'',N'" + this.amountTransferredTextbox.Text + "' ,'safeTransfer',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.codeTextBox.Text + "','','')";
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

                Query = "select Id as 'رقم التحويل', safeTransfer.fromSafe as 'من خزنة' , toSafe as 'إلى خزنة' ,amount as 'المبلغ',date as 'التاريخ' from safeTransfer;";

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
                fill();
            }
            else if (state == "adjust")
            {
              
                    string Query = "IF EXISTS (select 1 from safeTransfer where Id=N'" + this.codeTextBox.Text + "') BEGIN UPDATE safeTransfer SET fromSafe=N'" + this.fromSafeComboBox.Text + "',toSafe=N'"+this.toSafeComboBox.Text+"' ,amount =N'"+this.amountTransferredTextbox.Text+ "' ,date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "'  where Id=N'" + this.codeTextBox.Text + "' END";
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

                    Query = "DELETE FROM safeTable where type = 'safeTransfer' and details= 'out' and billNo = N'" + this.codeTextBox.Text + "' and name=N'"+oldFromSafe+"';";
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

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.toSafeComboBox.Text + "' ,'',N'" + this.amountTransferredTextbox.Text + "' ,'safeTransfer',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in',N'" + this.codeTextBox.Text + "','','')";
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

                  

                    Query = "DELETE FROM safeTable where type = 'safeTransfer' and details= 'in' and billNo = N'" + this.codeTextBox.Text + "' and name=N'" + oldToSafe + "';";
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

                    Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.fromSafeComboBox.Text + "' ,'',N'" + this.amountTransferredTextbox.Text + "' ,'safeTransfer',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.codeTextBox.Text + "','','')";
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

                    Query = "select Id as 'رقم التحويل', safeTransfer.fromSafe as 'من خزنة' , toSafe as 'إلى خزنة' ,amount as 'المبلغ',date as 'التاريخ' from safeTransfer;";

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
                    fill();

                    state = "new";
                                deleteButton.Visible = false;

            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟\n\nستسترجع جميع القيم قبل البند", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                string Query = "DELETE FROM safeTransfer where Id =N'" + this.codeTextBox.Text + "';";
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

                Query = "DELETE FROM safeTable where type = 'safeTransfer' and details= 'out' and billNo = N'" + this.codeTextBox.Text + "' and name=N'" + oldFromSafe + "';";
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

                Query = "DELETE FROM safeTable where type = 'safeTransfer' and details= 'in' and billNo = N'" + this.codeTextBox.Text + "' and name=N'" + oldToSafe + "';";
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

                Query = "select Id as 'رقم التحويل', safeTransfer.fromSafe as 'من خزنة' , toSafe as 'إلى خزنة' ,amount as 'المبلغ',date as 'التاريخ' from safeTransfer;";

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
                fill();

                state = "new";
                deleteButton.Visible = false;
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
                    this.fromSafeComboBox.Text = row.Cells[1].Value.ToString();
                    oldFromSafe = row.Cells[1].Value.ToString(); ;
                    this.toSafeComboBox.Text = row.Cells[2].Value.ToString();
                    oldToSafe = row.Cells[2].Value.ToString(); ;
                    this.amountTransferredTextbox.Text = row.Cells[3].Value.ToString();
                    oldAmount = row.Cells[3].Value.ToString();
                    this.dateDTP.Text = row.Cells[4].Value.ToString();
                    state = "adjust";
                }
            }
            catch (Exception ex)
            {
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
