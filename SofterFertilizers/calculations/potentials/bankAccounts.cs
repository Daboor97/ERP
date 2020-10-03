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

namespace SofterFertilizers.calculations.potentials
{
    public partial class bankAccounts : UserControl
    {
        public bankAccounts()
        {
            InitializeComponent();
            fill();
            deleteButton.Visible = false;
        }

        string state = "new";
        string oldBankName = "";
        string oldAmount = "";

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {
            categoryDGV.DataSource = null;

            string Query = "select safeMainTable.id as 'كود الخزنة', safeMainTable.name as 'اسم الخزنة' ,safeTable.money as 'رصيد اول المدة',safeTable.date as 'تاريخ التسجيل'  from safeMainTable,safeTable where safeTable.notes=N'رصيد أول المدة' and safeTable.type ='initial' and safeTable.name = safeMainTable.name;";

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

        private void addButton_Click(object sender, EventArgs e)
        {
            if (bankNameTextBox.Text != "")
            {
                if (state == "new")
                {
                    string Query = "IF NOT EXISTS (SELECT name from safeMainTable where name=N'" + this.bankNameTextBox.Text + "') BEGIN INSERT INTO safeMainTable(name,bank) VALUES (N'" + this.bankNameTextBox.Text + "','True') END ";
                    SqlConnection conDataBase = new SqlConnection(constring);
                    SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
                    SqlDataReader myReader;

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

                    Query = "IF NOT EXISTS (SELECT name from safeTable where name=N'" + this.bankNameTextBox.Text + "') BEGIN INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.bankNameTextBox.Text + "',N'رصيد أول المدة',N'" + this.amountTransferredTextbox.Text + "' ,'initial',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in','','','') END ";
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
                    amountTransferredTextbox.Text = "0";
                    bankNameTextBox.Text = "";
                    fill();
                    MessageBox.Show("حُفظ");
                }
                else
                {
                    string Query = "UPDATE safeMainTable SET name = N'" + this.bankNameTextBox.Text + "' where name =N'" + oldBankName + "' ";
                    SqlConnection conDataBase = new SqlConnection(constring);
                    SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
                    SqlDataReader myReader;

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

                    Query = "UPDATE safeTable SET name = N'" + this.bankNameTextBox.Text + "'  where name =N'" + oldBankName + "' ";
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

                    Query = "UPDATE safeTable SET money = N'" + this.amountTransferredTextbox.Text + "'  where name =N'" + this.bankNameTextBox.Text + "' and notes=N'رصيد أول المدة' and type ='initial' and details ='in' ;";
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

                    amountTransferredTextbox.Text = "0";
                    bankNameTextBox.Text = "";
                    fill();
                    deleteButton.Visible = false;
                    state = "new";
                    MessageBox.Show("حُفظ");
                }
            }
            else
            {
                  MessageBox.Show("أكمل العناصر");
            }
        }

        private void categoryDGV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
                    deleteButton.Visible = true;
                    this.bankNameTextBox.Text = row.Cells[1].Value.ToString();
                    oldBankName = row.Cells[1].Value.ToString();
                    this.amountTransferredTextbox.Text = row.Cells[2].Value.ToString();
                    oldAmount = row.Cells[2].Value.ToString();
                    this.dateDTP.Text = row.Cells[3].Value.ToString();
                    state = "adjust";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string count = new SqlCommand("Select Count(name) from safeTable where name=N'" + this.bankNameTextBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            count = (string.IsNullOrEmpty(count)) ? "0" : count;

            if (Convert.ToInt32(count) <= 1)
            {
                DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string Query = "DELETE FROM safeMainTable where name=N'" + oldBankName + "';";
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

                     Query = "DELETE FROM safeTable where name=N'" + oldBankName + "';";
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

                    amountTransferredTextbox.Text = "0";
                    bankNameTextBox.Text = "";
                    fill();
                    deleteButton.Visible = false;
                    state = "new";
                    MessageBox.Show("حُفظ");
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            else
            {
                MessageBox.Show("لا يمكن حذف البنك لأن هناك عمليات أخرى غير رصيد البداية");
            }
        }
        public void refreshLocal()
        {
            amountTransferredTextbox.Text = "0";
            bankNameTextBox.Text = "";
            fill();
            deleteButton.Visible = false;
        }

    }
}
