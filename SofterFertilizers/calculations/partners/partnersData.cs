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
    public partial class partnersData : UserControl
    {
        public partnersData()
        {
            InitializeComponent();
            fill();
            deleteButton.Visible = false;
            activeCheckBox.Checked = true;
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        string state = "new";
        string oldName = "";
        string oldPercentage = "";
        string oldAmount = "";
        bool active;

     public  void clear()
        {
            nameTextBox.Text = "";
            telephoneTextBox.Text = "";
            mobileTextBox.Text = "";
            faxTextBox.Text = "";
            percentageTextBox.Text = "";
            potentialMoneyTextBox.Text = "";
            addressTextBox.Text = "";
            notesTextBox.Text = "";
            activeCheckBox.Checked = true;
        }

        void fill()
        {
            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from partnersTable) BEGIN SELECT MAX(Id) FROM partnersTable END", conDataBase).ExecuteScalar().ToString();

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

            categoryDGV.DataSource = null;


            string Query = "select id as 'كود الشريك', name as 'اسم الشريك' ,telephone as 'التليفون', mobile as 'الموبايل' , fax as 'الفاكس',percentage as 'نسبة الشراكة' , potential as 'رأس مال الشراكة' , address as 'عنوان الشريك', notes as 'الملاحظات' , active as 'نشط' from partnersTable;";

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
                if (activeCheckBox.Checked)
                    active = true;
                else
                    active = false;

                string Query = "IF NOT EXISTS (SELECT 1 from partnersTable where Id=N'" + this.codeTextBox.Text + "') BEGIN INSERT INTO partnersTable(name,telephone,mobile,fax,percentage,potential,address,notes,active) VALUES (N'" + this.nameTextBox.Text + "',N'" + this.telephoneTextBox.Text + "',N'" + this.mobileTextBox.Text + "',N'" + this.faxTextBox.Text + "',N'" + this.percentageTextBox.Text + "',N'" + this.potentialMoneyTextBox.Text + "',N'" + this.addressTextBox.Text + "',N'" + this.notesTextBox.Text + "',N'"+active+"') END ";
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
                clear();
                MessageBox.Show("حُفظ");
            }

            else if(state == "adjust")
            {
                if (activeCheckBox.Checked)
                    active = true;
                else
                    active = false;

                string Query = "UPDATE partnersTable SET name = N'" + this.nameTextBox.Text + "',telephone=N'" + this.telephoneTextBox.Text + "',mobile=N'" + this.mobileTextBox.Text + "',fax=N'" + this.faxTextBox.Text + "',percentage=N'" + this.percentageTextBox.Text + "',potential=N'" + this.potentialMoneyTextBox.Text + "',address=N'" + this.addressTextBox.Text + "',notes=N'" + this.notesTextBox.Text + "',active=N'"+active+"' where Id =N'" + this.codeTextBox.Text+ "'";
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
                clear();
                deleteButton.Visible = false;
                state = "new";
                MessageBox.Show("حُفظ");
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
                    this.nameTextBox.Text = row.Cells[1].Value.ToString();
                    this.telephoneTextBox.Text = row.Cells[2].Value.ToString();
                    this.mobileTextBox.Text = row.Cells[3].Value.ToString();
                    this.faxTextBox.Text = row.Cells[4].Value.ToString();
                    this.percentageTextBox.Text = row.Cells[5].Value.ToString();
                    this.potentialMoneyTextBox.Text = row.Cells[6].Value.ToString();
                    this.addressTextBox.Text = row.Cells[7].Value.ToString();
                    this.notesTextBox.Text = row.Cells[8].Value.ToString();
                   
                    state = "adjust";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
                DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string Query = "DELETE FROM partnersTable where Id = N'" + this.codeTextBox.Text + "' ;";
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

                    fill();
                    clear();
                    deleteButton.Visible = false;
                    state = "new";
                    MessageBox.Show("حُفظ");
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                } 
        }
    }
    
}
