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
    public partial class typeUC : UserControl
    {

    
        public typeUC()
        {
            InitializeComponent();
           
        }
        private void typeUC_Load(object sender, EventArgs e)
        {
            adjustButton.Enabled = false;
            fillTypeListBox();
            fillCompanyComboBox();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string toBeAdjusted = "";
        int index = 0;
        string DGVselectedTypeName = "";
        string DGVselectedCompanyName = "";
        DataTable dbdataset = new DataTable();

        void fillTypeListBox()
        {
         
                typeListBox.Items.Clear();

                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string Query = "select Distinct typeName from typeTable where companyName=N'9 9 99 9 9' ;";


                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
                da.Fill(dt);
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        typeListBox.Items.Add(dr["typeName"].ToString());
                    }
                }
                catch (Exception ex)
                {
 
                }
                conDataBase.Close();
        }

        void fillCompanyComboBox()
        {
            companyComboBox.Items.Clear();

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select companyName from companyTable;";

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    companyComboBox.Items.Add(dr["companyName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();
            if (companyComboBox.Items.Count > 0)
            {
                companyComboBox.Text = companyComboBox.Items[0].ToString();
            }
        }


        void fillTypeDGCforCompany()
        {
            dbdataset.Clear();
            string Query = "select distinct typeName as 'النوع' from typeTable where companyName= N'" + this.companyComboBox.Text + "';";

            SqlConnection conDataBase = new SqlConnection(constring);
            SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmdDataBase;

                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                companyListDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
            }

            conDataBase.Close();
            index = 0;
        }

        void Clear()
        {
            typeTextBox.Text = "";
            typeSearchTextBox.Text = "";
            typeListBox.Items.Clear();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (typeTextBox.Text != "")
            {

                string Query = "IF NOT EXISTS (select 1 FROM typetable where typeName = N'" + this.typeTextBox.Text + "' and companyName=N'9 9 99 9 9') BEGIN INSERT INTO typeTable(typeName,companyName) VALUES (N'" + this.typeTextBox.Text + "',N'9 9 99 9 9') END ";
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
 
                }
                Clear();
                fillTypeListBox();
            }
            else
            {
                MessageBox.Show("الرجاء ادخال قيمة");
            }
        }

        private void companyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillTypeDGCforCompany();
         
        }

        private void typeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            typeListBox.Items.Clear();

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct typeName from typeTable where typeName like N'%" + this.typeSearchTextBox.Text + "%';";


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    typeListBox.Items.Add(dr["typeName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();
        }

        private void typeListBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                toBeAdjusted = typeListBox.SelectedItem.ToString();
            }
            catch { }
        }

        private void connectButon_Click(object sender, EventArgs e)
        {
            if (companyListDGV.Rows.Count > 0)
            {
                for (int i = 0; i < companyListDGV.Rows.Count; i++)
                {
                    if (this.companyListDGV.Rows[i].Cells[0].Value != null)
                    {
                        if (toBeAdjusted == this.companyListDGV.Rows[i].Cells[0].Value.ToString())
                        {
                            MessageBox.Show("النوع موجود مسبقًا");
                            return;
                        }
                    }
                }
            }

                DataRow r = dbdataset.NewRow();
                r.BeginEdit();

                r[0] = toBeAdjusted;

                r.EndEdit();
                dbdataset.Rows.Add(r);
                BindingSource SBind = new BindingSource();

                SBind.DataSource = dbdataset;
                companyListDGV.DataSource = SBind;

                int index;
                if (companyListDGV.Rows.Count <= 0)
                    index = 0;
                else
                    index = companyListDGV.Rows.Count - 2;
                string Query = "IF NOT EXISTS (select 1 FROM typetable where typeName = N'" + this.companyListDGV.Rows[index].Cells[0].Value + "' and companyName=N'" + this.companyComboBox.Text + "') BEGIN INSERT INTO typeTable(typeName,companyName) VALUES (N'" + this.companyListDGV.Rows[index].Cells[0].Value + "',N'" + this.companyComboBox.Text + "') END ";
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

            

        }


        private void companyListDGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            string Query = "DELETE FROM typeTable where typeName=N'"+ DGVselectedTypeName +"' and companyName=N'" + DGVselectedCompanyName + "' ";
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
                MessageBox.Show(ex.Message);
            }

            /*  if (companyListDGV.Rows.Count > 0)
              {
                  for (int i = 0; i < companyListDGV.Rows.Count; i++)
                  {
                      if (this.companyListDGV.Rows[i].Cells[0].Value != null)
                      {

                      }
                  }
              }
              */
        }

        private void companyListDGV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DGVselectedTypeName = this.companyListDGV.CurrentRow.Cells[0].Value.ToString();
            DGVselectedCompanyName = this.companyComboBox.Text;
        }

        private void typeListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            toBeAdjusted = typeListBox.SelectedItem.ToString();
            typeTextBox.Text = toBeAdjusted;
            addButton.Enabled = false;
            adjustButton.Enabled = true;

        }

        private void adjustButton_Click(object sender, EventArgs e)
        {
            //TODO Required admin previlage to adjust
            if (true)
            {
                string Query = "IF EXISTS(select 1 from typeTable where typeName=N'" + toBeAdjusted + "') BEGIN UPDATE typeTable SET typeName = N'" + this.typeTextBox.Text + "' where typeName = N'" + toBeAdjusted + "' END";
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
                fillTypeListBox();
                fillTypeDGCforCompany();

                adjustButton.Enabled = false;
                addButton.Enabled = true;

            }
        }
        public void refreshLocal()
        {
            fillCompanyComboBox();
            fillTypeDGCforCompany();
            fillTypeListBox();
            Clear();
        }
    }
}
