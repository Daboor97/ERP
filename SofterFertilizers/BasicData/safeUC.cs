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

namespace SofterFertilizers.BasicData
{
    public partial class safeUC : UserControl
    {
        public safeUC()
        {
            InitializeComponent();
            fill();
            fill_safeDGV();
            deleteButton.Visible = false;
        }

        string state ="new";
        string oldBankName = "";
        string oldAmount = "";

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {
            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from safeMainTable) BEGIN SELECT MAX(Id) FROM safeMainTable END", conDataBase).ExecuteScalar().ToString();

            if (maxBill != "")
            {
                int maxBillint = Convert.ToInt32(maxBill);
                safeCodeTextBox.Text = Convert.ToString(maxBillint + 1);
                conDataBase.Close();
            }

            else
            {
                safeCodeTextBox.Text = "1";
                conDataBase.Close();
            }
        }

        void clear()
        {
            fill();
            safeNameTextBox.Text = "";
            noteTextBox.Text = "";
            addressTextBox.Text = "";
            amountTransferredTextbox.Text = "0";
        }

        void fill_safeDGV() { 

            safeDGV.DataSource = null;
            string Query = "select safeMainTable.id as 'كود الخزنة',safeMainTable.name as 'اسم الخزنة' ,safeTable.money as 'رصيد البداية', safeMainTable.address as 'العنوان', safeMainTable.notes as 'الملاحظات', safeTable.date as 'تاريخ التسجيل' from safeMainTable,safeTable where safeTable.notes=N'رصيد أول المدة' and safeTable.type ='initial safe' and safeTable.name = safeMainTable.name ;";
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
                safeDGV.DataSource = bSource;
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
            if (safeNameTextBox.Text != "")
            {
                if (state == "new")
                {
                    string Query = "IF NOT EXISTS (select 1 FROM safeMainTable where name= N'" + this.safeNameTextBox.Text + "') BEGIN INSERT INTO safeMainTable(name,address,notes,bank) VALUES (N'" + this.safeNameTextBox.Text + "',N'" + this.addressTextBox.Text + "',N'" + this.noteTextBox.Text + "','False') END ";
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


                    Query = "IF NOT EXISTS (SELECT name from safeTable where name=N'" + this.safeNameTextBox.Text + "') BEGIN INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeNameTextBox.Text + "',N'رصيد أول المدة',N'" + this.amountTransferredTextbox.Text + "' ,'initial safe',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in','','','') END ";
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
                    fill_safeDGV();
                    clear();
                    MessageBox.Show("حُفظ");
                }
                else
                {
                    string Query = "UPDATE safeMainTable SET name = N'" + this.safeNameTextBox.Text + "' ,address=N'" + this.addressTextBox.Text + "',notes=N'" + this.noteTextBox.Text + "'  where name =N'" + oldBankName + "'and Id = N'" + this.safeCodeTextBox.Text + "' ";
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

                    Query = "UPDATE safeTable SET name = N'" + this.safeNameTextBox.Text + "'  where name =N'" + oldBankName + "' ";
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

                    Query = "UPDATE safeTable SET money = N'" + this.amountTransferredTextbox.Text + "',date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "'  where name =N'" + this.safeNameTextBox.Text + "' and notes=N'رصيد أول المدة' and type ='initial safe' and details ='in' ;";
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


                    deleteButton.Visible = false;
                    state = "new";
                    fill_safeDGV();
                    clear();
                    MessageBox.Show("حُفظ");
                }
            }
            else
            {
                MessageBox.Show("أكمل العناصر");
            }
        }

        private void safeDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            deleteButton.Visible=  true;

        
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.safeDGV.Rows[e.RowIndex];
                deleteButton.Visible = true;
                this.safeCodeTextBox.Text = row.Cells[0].Value.ToString();
                this.safeNameTextBox.Text = row.Cells[1].Value.ToString();
                oldBankName = row.Cells[1].Value.ToString();
                this.amountTransferredTextbox.Text = row.Cells[2].Value.ToString();
                oldAmount = row.Cells[2].Value.ToString();
                this.addressTextBox.Text = row.Cells[3].Value.ToString();
                this.noteTextBox.Text = row.Cells[4].Value.ToString();
                this.dateDTP.Text = row.Cells[5].Value.ToString();
                state = "adjust";
            }


        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string count = new SqlCommand("Select Count(name) from safeTable where name=N'" + this.safeNameTextBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
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


                    deleteButton.Visible = false;
                    state = "new";
                    fill_safeDGV();
                    clear();
                    MessageBox.Show("حُفظ");
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            else
            {
                MessageBox.Show("لا يمكن حذف الخزنة لأن هناك عمليات أخرى غير رصيد البداية");
            }
        }
        public void refreshLocal()
        {
            clear();
            fill_safeDGV();
            deleteButton.Visible = false;
        }
    }
    
}
