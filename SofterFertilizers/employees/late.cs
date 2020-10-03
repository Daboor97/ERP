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
    public partial class late : UserControl
    {
        public late()
        {
            InitializeComponent();
            deleteButton.Visible = false;
            clear();
            fill();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string status = "new";
        string oldDate;
        string oldHours;
        string oldMinutes;

        void clear()
        {
            this.hoursTextBox.Text = "0";
            this.minutesTextBox.Text = "0";
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

            string Query = "select employeeName as 'اسم الموظّف' ,hours as 'ساعات التأخير', minutes as 'دقائق التأخير' , date as 'التاريخ' from employeelateTable where employeeName = N'" + this.employeeNameComboBox.Text + "' ; ";

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
                string Query = "INSERT INTO employeeLateTable(employeeName,hours,minutes,date,outside) VALUES (N'" + this.employeeNameComboBox.Text + "',N'" + this.hoursTextBox.Text + "',N'" + this.minutesTextBox.Text + "',N'" + this.toDate.Value.ToString("MM/dd/yyyy") + "','False')  ";
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
                SqlConnection conDataBase = new SqlConnection(constring);
                //supplier Code
                conDataBase.Open();
                string Outside = new SqlCommand("select outside from employeeLateTable where employeeName =N'" + this.employeeNameComboBox.Text + "' and hours =N'" + this.oldHours + "' and minutes =N'" + this.oldMinutes + "' and  date =N'" + this.oldDate + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();


                if (Convert.ToBoolean(Outside) == false)
                {

                    string Query = "BEGIN UPDATE employeeLateTable SET date= N'" + this.toDate.Value.ToString("MM/dd/yyyy") + "', hours= N'" + this.hoursTextBox.Text + "', minutes= N'" + this.minutesTextBox.Text + "' where employeeName =N'" + this.employeeNameComboBox.Text + "' and hours =N'" + this.oldHours + "' and minutes =N'" + this.oldMinutes + "' and  date =N'" + this.oldDate + "' END";
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
                    MessageBox.Show("انتهى التعديل");

                    clear();
                    deleteButton.Visible = false;
                    status = "new";
                }
                else
                {
                    MessageBox.Show("لا يمكن تعديله");

                    clear();
                    deleteButton.Visible = false;
                    status = "new";
                }
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
                this.hoursTextBox.Text = row.Cells[1].Value.ToString();
                oldHours = row.Cells[1].Value.ToString();

                this.minutesTextBox.Text = row.Cells[2].Value.ToString();
                oldMinutes = row.Cells[2].Value.ToString();

                this.toDate.Text = row.Cells[3].Value.ToString();
                oldDate = row.Cells[3].Value.ToString();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {

            SqlConnection conDataBase = new SqlConnection(constring);
            //supplier Code
            conDataBase.Open();
            string Outside = new SqlCommand("select outside from employeeLateTable where employeeName =N'" + this.employeeNameComboBox.Text + "' and hours =N'" + this.oldHours + "' and minutes =N'" + this.oldMinutes + "' and  date =N'" + this.oldDate + "';", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();


            if (Convert.ToBoolean(Outside) == false)
            {

                DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string Query = "DELETE FROM employeeLateTable where employeeName =N'" + this.employeeNameComboBox.Text + "' and hours =N'" + this.oldHours + "' and minutes =N'" + this.oldMinutes + "' and  date =N'" + this.oldDate + "' ;";
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
            else
            {
                MessageBox.Show("لا يمكن حذف هذا البند");

                clear();
                deleteButton.Visible = false;
                status = "new";
            }
        }
        public void refreshLoacl()
        {
            deleteButton.Visible = false;
            clear();
            fill();
        }
    }
}
