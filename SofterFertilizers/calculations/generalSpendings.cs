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
    public partial class generalSpendings : UserControl
    {
        public generalSpendings()
        {
            InitializeComponent();
            fill();
            spendingTypeComboBox.Items.Add("مصروفات عامة");
            spendingTypeComboBox.Items.Add("مصروفات حكومية");
            spendingTypeComboBox.Items.Add("مصروفات رأسمالية");
            spendingTypeComboBox.Items.Add("أجور");
            spendingTypeComboBox.Items.Add("إيجار");
            spendingTypeComboBox.Text = spendingTypeComboBox.Items[0].ToString();
            deleteButton.Visible = false;
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string state = "new";

        void clear()
        {
            amountTextbox.Text = "0";
            detsilsTextBox.Text = "";
            deleteButton.Visible = false;
        }

        void fill()
        {
            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from generalSpendingTable) BEGIN SELECT MAX(Id) FROM generalSpendingTable END", conDataBase).ExecuteScalar().ToString();

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



            //safeComboBox
            safeComboBox.Items.Clear();
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct name from safeMainTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    safeComboBox.Items.Add(dr["name"].ToString());
                }
            }
            catch
            {
            }
            conDataBase.Close();

            if (safeComboBox.Items.Count > 0)
            {
                safeComboBox.Text = safeComboBox.Items[0].ToString();
            }

            //category DGV
            categoryDGV.DataSource = null;

            Query = "select generalSpendingTable.Id as 'رقم المصروف', type as 'نوع المصروف' ,amount as 'المبلغ',  safe as 'الخزنة', details as 'التفاصيل',date as 'التاريخ' from generalSpendingTable;";

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

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (state == "new")
            {
                string Query = "IF NOT EXISTS(select 1 from generalSpendingTable where Id=N'" + this.codeTextBox.Text + "') BEGIN INSERT INTO generalSpendingTable(type,safe,amount,details,date) VALUES (N'" + this.spendingTypeComboBox.Text + "',N'" + this.safeComboBox.Text + "',N'" + this.amountTextbox.Text + "' ,N'" + this.detsilsTextBox.Text + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "')END";
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
                



                        Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,N'"+this.spendingTypeComboBox.Text+"',N'" + this.amountTextbox.Text + "','generalSpendings',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.codeTextBox.Text + "','cash',N'')";
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


                deleteButton.Visible = false;
                fill();
                state = "new";
                clear();

                MessageBox.Show("حفظ");
            }

            else if (state == "adjust")
            {
                string Query = "IF EXISTS (select 1 from generalSpendingTable where Id = N'" + this.codeTextBox.Text + "') BEGIN UPDATE generalSpendingTable SET type =N'" + this.spendingTypeComboBox.Text + "', safe=N'" + this.safeComboBox.Text + "',amount=N'" + this.amountTextbox.Text + "',details=N'"+this.detsilsTextBox.Text+"',date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' where Id=N'" + this.codeTextBox.Text + "' END";
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

                Query = "DELETE FROM safeTable where type='generalSpendings' and billNo = N'" + this.codeTextBox.Text + "';";
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

                Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,N'" + this.spendingTypeComboBox.Text + "',N'" + this.amountTextbox.Text + "','generalSpendings',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.codeTextBox.Text + "','cash',N'')";
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

                deleteButton.Visible = false;
                fill();
                state = "new";
                clear();
                MessageBox.Show("حفظ");

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
                    this.spendingTypeComboBox.Text = row.Cells[1].Value.ToString();
                    this.amountTextbox.Text = row.Cells[2].Value.ToString();
                    this.safeComboBox.Text = row.Cells[3].Value.ToString();
                    this.detsilsTextBox.Text = row.Cells[4].Value.ToString();
                    this.dateDTP.Text = row.Cells[5].Value.ToString();

                    state = "adjust";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟\n\nستسترجع جميع القيم قبل البند", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string Query = "DELETE FROM generalSpendingTable where Id= N'" + this.codeTextBox.Text + "'";
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

                Query = "DELETE FROM safeTable where type='generalSpendings' and billNo = N'" + this.codeTextBox.Text + "';";
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


                state = "new";
                clear();
                fill();

                deleteButton.Visible = false;

                MessageBox.Show("حفظ");

            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }

        public void refreshLocal()
        {
            fill();
            deleteButton.Visible = false;
            clear();
        }
    }
}
