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
    public partial class fixedPotentials : UserControl
    {
        public fixedPotentials()
        {
            InitializeComponent();
            fill();
            deleteButton.Visible = false;
        }


        string state = "new";
        string oldName = "";
        string oldAmount = "";

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {

            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from fixedPotentialTable) BEGIN SELECT MAX(Id) FROM fixedPotentialTable END", conDataBase).ExecuteScalar().ToString();

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

            categoryDGV.DataSource = null;

           string Query = "select id as 'رقم الأصل', name as 'اسم الأصل' ,value as 'قمية الأصل', date as 'تاريخ التسجيل' ,damaged as 'مُهلك' from fixedPotentialTable;";

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

            nameTextBox.Text = "";
            valueTextbox.Text = "0";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text != "")
            {
                if (state == "new")
                {
                    string Query = "IF NOT EXISTS (SELECT 1 from fixedPotentialTable where name=N'" + this.nameTextBox.Text + "') BEGIN INSERT INTO fixedPotentialTable(name,value,date,damaged) VALUES (N'" + this.nameTextBox.Text + "',N'" + this.valueTextbox.Text + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','False') END ";
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


                    fill();
                    MessageBox.Show("حُفظ");
                }
                else
                {
                    SqlConnection conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string damagedString = new SqlCommand("Select damaged from fixedPotentialTable where Id = N'" + this.safeCodeTextBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    bool damaged = Convert.ToBoolean(damagedString);
                    if (!damaged)
                    {
                        string Query = "UPDATE fixedPotentialTable SET name = N'" + this.nameTextBox.Text + "',value=N'" + this.valueTextbox.Text + "',date=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' where name =N'" + oldName + "' and Id = N'" + this.safeCodeTextBox.Text + "' ";
                        conDataBase = new SqlConnection(constring);
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

                        fill();
                        deleteButton.Visible = false;
                        state = "new";
                        MessageBox.Show("حُفظ");
                    }
                    else
                    {
                        MessageBox.Show("لا يمكن تعديل أصل مُهلك");
                        fill();
                        deleteButton.Visible = false;
                        state = "new";
                    }
                }
            }
            else
            {
                MessageBox.Show("أكمل العناصر");
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
                    this.safeCodeTextBox.Text = row.Cells[0].Value.ToString();
                    this.nameTextBox.Text = row.Cells[1].Value.ToString();
                    oldName = row.Cells[1].Value.ToString();
                    this.valueTextbox.Text = row.Cells[2].Value.ToString();
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
            string damagedString = new SqlCommand("Select damaged from fixedPotentialTable where Id = N'" + this.safeCodeTextBox.Text + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            bool damaged = Convert.ToBoolean(damagedString);
            if (!damaged)
            {

                DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string Query = "DELETE FROM fixedPotentialTable where name=N'" + oldName + "' and Id = N'" + this.safeCodeTextBox.Text + "' ;";
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
                MessageBox.Show("لا يمكن حذف أصل مُهلك");
                fill();
                deleteButton.Visible = false;
                state = "new";

            }
        }

        public void refreshLocal()
        {
            fill();
            deleteButton.Visible = false;
        }
    }
}
