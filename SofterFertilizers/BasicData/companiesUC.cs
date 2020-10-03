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
    public partial class companiesUC : UserControl
    {
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string toBeAdjusted="";

        public companiesUC()
        {
            InitializeComponent();
            adjustButton.Enabled = false;
            fillCompanyListBox();
        }

        void fillCompanyListBox()
        {
            companysListBox.Items.Clear();

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct companyName from companyTable;";

           
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query,conDataBase);
            da.Fill(dt);
            try
            {
              foreach(DataRow dr in dt.Rows)
                {
                    companysListBox.Items.Add(dr["companyName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

              

            conDataBase.Close();

        }

        void Clear()
        {
            companyNameTextBox.Text = "";
            companySearchTextBox.Text = "";
            companysListBox.Items.Clear();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string Query = "IF NOT EXISTS (select 1 FROM companyTable where companyName = N'" + this.companyNameTextBox.Text + "') BEGIN INSERT INTO companyTable(companyName) VALUES (N'" + this.companyNameTextBox.Text + "') END ";
            SqlConnection conDataBase = new SqlConnection(constring);
            SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
            SqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                MessageBox.Show("حفظ");
                while (myReader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Clear();
            fillCompanyListBox();
        }

     
        
        private void companySearchTextBox_TextChanged(object sender, EventArgs e)
        {
            companysListBox.Items.Clear();

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select companyName from companyTable where companyName like N'%"+this.companySearchTextBox.Text+"%';";


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    companysListBox.Items.Add(dr["companyName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            conDataBase.Close();

        }

        private void companysListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            toBeAdjusted = companysListBox.SelectedItem.ToString();
            companyNameTextBox.Text = toBeAdjusted;
            addButton.Enabled = false;
            adjustButton.Enabled = true;
        }

        private void adjustButton_Click(object sender, EventArgs e)
        {
            //TODO Required admin previlage to adjust
            if (true)
            {
                string Query = "IF EXISTS(select 1 from companyTable where companyName =N'" + toBeAdjusted + "') BEGIN UPDATE companyTable SET companyName = N'" + this.companyNameTextBox.Text + "' where companyName = N'"+toBeAdjusted+"' END";
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
                MessageBox.Show("انتهى التعديل");

                Clear();
                fillCompanyListBox();
                adjustButton.Enabled = false;
                addButton.Enabled = true;
            }


        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Clear();
            fillCompanyListBox();
            adjustButton.Enabled = false;
            MessageBox.Show("انتهى التعديل");
        }

        public void refreshButton()
        {
            Clear();
            fillCompanyListBox();
        }
    }
}
