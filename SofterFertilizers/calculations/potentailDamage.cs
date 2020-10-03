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
    public partial class potentailDamage : UserControl
    {
        public potentailDamage()
        {
            InitializeComponent();
            fill();
            deleteButton.Visible = false;
            addButton.Enabled = false;
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;


      


        void fill()
        {
            categoryDGV.DataSource = null;

            string Query = "select id as 'رقم الأصل', name as 'اسم الأصل' ,value as 'قمية الأصل', date as 'تاريخ التسجيل' ,damaged as 'مُهلك',reason as 'سبب الإهلاك' ,damageDate as 'تاريخ الإهلاك'  from fixedPotentialTable;";
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

            nameTextBox.Text = "";
            valueTextbox.Text = "0";
            safeCodeTextBox.Text = "";
            reasonTextBox.Text = "";
        }

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
                    this.safeCodeTextBox.Text = row.Cells[0].Value.ToString();
                    this.nameTextBox.Text = row.Cells[1].Value.ToString();
                    this.valueTextbox.Text = row.Cells[2].Value.ToString();
                    addButton.Enabled = true;
                    if (Convert.ToBoolean(row.Cells[4].Value.ToString()))
                    {
                        deleteButton.Visible = true;
                        this.reasonTextBox.Text = row.Cells[5].Value.ToString();
                        this.dateDTP.Text = row.Cells[6].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            
            
        }

        private void addButton_Click(object sender, EventArgs e)
        {
           
                string Query = "UPDATE fixedPotentialTable SET damaged = 'True',reason=N'" + this.reasonTextBox.Text + "',damageDate=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' where Id = N'" + this.safeCodeTextBox.Text + "' ";
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            fill();
            addButton.Enabled = false;
            deleteButton.Visible = false;
            MessageBox.Show("حُفظ");
           
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string Query = "UPDATE fixedPotentialTable SET damaged = 'False',reason='',damageDate=N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' where Id = N'" + this.safeCodeTextBox.Text + "' ";
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
            addButton.Enabled = false;
            deleteButton.Visible = false;
            MessageBox.Show("حُفظ");
        }
        public void refreshLoacl()
        {
            fill();
            deleteButton.Visible = false;
            addButton.Enabled = false;
        }
    }
}
