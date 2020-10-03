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

namespace SofterFertilizers.employees
{
    public partial class abscence : UserControl
    {
        public abscence()
        {
            InitializeComponent();
            deleteButton.Visible = false;
            fill();

        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string status = "new";
        string oldfromDate;
        string oldToDae;
        string olddays;
        bool oldPermission;

        void clear()
        {
            this.abscentNo.Text = "";
            this.permissionCheckBox.Checked = false;
            this.fromDate.Value = DateTime.Today;
            this.toDate.Value = DateTime.Today;
        }

        void fill()
        {
            //supplier ComboBox
            employeeNameComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct name from employeesTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    employeeNameComboBox.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (employeeNameComboBox.Items.Count > 0)
            {
                employeeNameComboBox.Text = employeeNameComboBox.Items[0].ToString();
            }


        }

        private void employeeNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            deleteButton.Visible = false;

            SqlConnection conDataBase = new SqlConnection(constring);

            try
            {
                //supplier Code
                 conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                employeeCodeTextBox.Text = new SqlCommand("select Id from employeesTable where name=N'" + this.employeeNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

            }
            catch
            {

            }

            supplierDGV.DataSource = null;

            string Query = "select employeeName as 'اسم الموظّف' ,fromDate as 'من تاريخ', toDate as 'إلى تاريخ' , days as 'عدد الأيام', permission as 'بإذن' ,today as 'تاريخ تسجيل الغياب' from employeeAbscenceTable where employeeName = N'" + this.employeeNameComboBox.Text + "' ; ";

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
                supplierDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

        }

        private void employeeCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conDataBase = new SqlConnection(constring);
                //supplier Code
                conDataBase.Open();
                employeeNameComboBox.Text = new SqlCommand("select name from employeesTable where id=N'" + this.employeeCodeTextBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
            }
            catch
            {
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (status == "new")
            {
                string Query = "INSERT INTO employeeAbscenceTable(employeeName,fromDate,toDate,days,permission,toDay) VALUES (N'" + this.employeeNameComboBox.Text + "',N'" + this.fromDate.Value.ToString("MM/dd/yyyy") + "',N'" + this.fromDate.Value.ToString("MM/dd/yyyy") + "',N'" + this.abscentNo.Text + "',N'" + this.permissionCheckBox.Checked + "',GETDATE())  ";
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
                conDataBase.Close();
                clear();
                deleteButton.Visible = false;
                status = "new";
                MessageBox.Show("حفظ");
            }
            else
            {
                string Query = "BEGIN UPDATE employeeAbscenceTable SET fromDate= N'" + this.fromDate.Value.ToString("MM/dd/yyyy") + "', toDate= N'" + this.toDate.Value.ToString("MM/dd/yyyy") + "', days= N'" + this.abscentNo.Text + "', permission= N'" + this.permissionCheckBox.Checked+ "' where employeeName =N'" + this.employeeNameComboBox.Text + "' and fromDate =N'" + this.oldfromDate + "' and toDate =N'" + this.oldToDae + "' and  permission =N'" + this.oldPermission + "' END";
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

                clear();
                deleteButton.Visible = false;
                status = "new";
            }
            fill();
            
        }

        private void supplierDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            deleteButton.Visible = true;
            status = "adjust";
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.supplierDGV.Rows[e.RowIndex];
                this.fromDate.Text = row.Cells[1].Value.ToString();
                oldfromDate = row.Cells[1].Value.ToString();

                this.toDate.Text = row.Cells[2].Value.ToString();
                oldToDae = row.Cells[2].Value.ToString();

                this.abscentNo.Text = row.Cells[3].Value.ToString();
                olddays= row.Cells[3].Value.ToString();

                this.permissionCheckBox.Checked= Convert.ToBoolean(row.Cells[4].Value.ToString()); 
                oldPermission = Convert.ToBoolean(row.Cells[4].Value.ToString()); 
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string Query = "DELETE FROM employeeAbscenceTable where employeeName =N'" + this.employeeNameComboBox.Text + "' and fromDate =N'" + this.oldfromDate + "' and toDate =N'" + this.oldToDae + "' and  permission =N'" + this.oldPermission + "'  ;";
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
                status = "new";
                MessageBox.Show("حُفظ");
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        public void refreshLocal()
        {
            fill();
            clear();
            deleteButton.Visible = false;

        }
    }
}
