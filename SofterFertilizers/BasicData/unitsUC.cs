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
    public partial class unitsUC : UserControl
    {
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string toBeAdjusted = "";

        public unitsUC()
        {
            InitializeComponent();
            adjustButton.Enabled = false;
            fillUnitListBox();
        }

        void fillUnitListBox()
        {
            unitsListBox.Items.Clear();

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct unitName from unitTable;";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    unitsListBox.Items.Add(dr["unitName"].ToString());
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
            unitNameTextBox.Text = "";
            unitSearchTextBox.Text = "";
            unitsListBox.Items.Clear();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string Query = "IF NOT EXISTS (select 1 FROM unitTable where unitName = N'" + this.unitNameTextBox.Text + "') BEGIN INSERT INTO unitTable(unitName) VALUES (N'" + this.unitNameTextBox.Text + "') END ";
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
            fillUnitListBox();
        }

        private void unitSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            unitsListBox.Items.Clear();

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct unitName from unitTable where unitName like N'%" + this.unitSearchTextBox.Text + "%';";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    unitsListBox.Items.Add(dr["unitName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

        }

        private void unitsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            toBeAdjusted = unitsListBox.SelectedItem.ToString();
            unitNameTextBox.Text = toBeAdjusted;
            addButton.Enabled = false;
            adjustButton.Enabled = true;
        }

        private void adjustButton_Click(object sender, EventArgs e)
        {
            //TODO Required admin previlage to adjust
            if (true)
            {
                string Query = "IF EXISTS(select 1 from unitTable where unitName =N'" + toBeAdjusted + "') BEGIN UPDATE unitTable SET unitName = N'" + this.unitNameTextBox.Text + "' where unitName = N'" + toBeAdjusted + "' END";
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
                fillUnitListBox();
                adjustButton.Enabled = false;
                addButton.Enabled = true;
            }
        }

        public void refreshLoacl()
        {
            Clear();
            adjustButton.Enabled = false;
            fillUnitListBox();
        }

        private void unitsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
