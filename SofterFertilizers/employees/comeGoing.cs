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
    public partial class comeGoing : UserControl
    {
        public comeGoing()
        {
            InitializeComponent();
            //currentTime.Value = DateTime.Now;
            fill();
            fillOthers();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fillOthers()
        {
            supplierDGV.DataSource = null;

            string Query = "select employeeName as 'اسم الموظّف' ,date as 'التاريخ', come as 'الحضور' ,go as 'الانصراف', permission as 'بإذن' from employeeComeGoingTable where employeeName = N'" + this.employeeNameComboBox.Text + "' ; ";

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
                supplierDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();
        }

        void fill7odoor()
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string state = new SqlCommand("IF EXISTS (select 1 from employeeComeGoingTable where date =N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' and employeeName = N'" + this.employeeNameComboBox.Text + "' ) BEGIN select 1  from employeeComeGoingTable where date =N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' and employeeName = N'" + this.employeeNameComboBox.Text + "' END Else SELECT 0 ", conDataBase).ExecuteScalar().ToString();
            if (state == "1")
            {
                state = new SqlCommand("IF EXISTS (select 1 from employeeComeGoingTable where date =N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' and employeeName = N'" + this.employeeNameComboBox.Text + "'  and come IS NOT NULL and go IS NULL) BEGIN select 1  from employeeComeGoingTable where date =N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' and employeeName = N'" + this.employeeNameComboBox.Text + "' and come IS NOT NULL and go IS NULL END Else SELECT 0 ", conDataBase).ExecuteScalar().ToString();
                if (state == "1")
                {
                    label8.Text = "وقت الانصراف الأصلي";
                    label1.Text = "وقت الانصراف";

                    conditionTextBox.Text = "انصراف";

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string specifiedSettings = new SqlCommand("IF EXISTS (select 1 from employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "') BEGIN select 1 from employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END Else SELECT 0 ", conDataBase).ExecuteScalar().ToString();
                    if (specifiedSettings == "1")
                    {

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string startTime = new SqlCommand("BEGIN select endTime FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        time.Text = startTime;
                        currentTime.Value = Convert.ToDateTime(startTime);
                    }
                    else
                    {
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string startTime = new SqlCommand("BEGIN select endTime FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        time.Text = startTime;
                        currentTime.Value = Convert.ToDateTime(startTime);

                    }
                }
                else
                {
                    conditionTextBox.Text = "انتهى تسجيل الموظف اليوم";
                }
            }
            else
            {
                label8.Text = "وقت الحضور الأصلي";
                label1.Text = "وقت الحضور";

                conditionTextBox.Text = "حضور";

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string specifiedSettings = new SqlCommand("IF EXISTS (select 1 from employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "') BEGIN select 1 from employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END Else SELECT 0 ", conDataBase).ExecuteScalar().ToString();
                if (specifiedSettings == "1")
                {

                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string startTime = new SqlCommand("BEGIN select startTime FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    time.Text = startTime;
                    currentTime.Value = Convert.ToDateTime(startTime);
                }
                else
                {
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string startTime = new SqlCommand("BEGIN select startTime FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();
                    time.Text = startTime;
                    currentTime.Value = Convert.ToDateTime(startTime);
                }
            }
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
            fill7odoor();

            //   currentTime.Value = DateTime.Now;
            SqlConnection conDataBase = new SqlConnection(constring);

            try
            {
            //supplier Code
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            employeeCodeTextBox.Text = new SqlCommand("select Id from employeesTable where name=N'" + this.employeeNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();

               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            fillOthers();
        }

        private void employeeCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            //currentTime.Value = DateTime.Now;
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
            if (conditionTextBox.Text == "حضور")
            {
                string Query = "INSERT INTO employeeComeGoingTable(employeeName,date,come) VALUES (N'" + this.employeeNameComboBox.Text + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "',N'" + this.currentTime.Value.ToString("hh:mm:ss tt") + "')  ";
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

                if(currentTime.Value > time.Value)
                {
                    TimeSpan sub = currentTime.Value - time.Value;
                    string hours = sub.Hours.ToString();
                    string minutes = sub.Minutes.ToString();
                    Query = "INSERT INTO employeeLateTable(employeeName,hours,minutes,date,outside) VALUES (N'" + this.employeeNameComboBox.Text + "',N'" + hours + "',N'"+ minutes + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','True')  ";
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
                    conDataBase.Close();

                }

                else if (currentTime.Value < time.Value)
                {
                    TimeSpan sub =  time.Value - currentTime.Value ;
                    string hours = sub.Hours.ToString();
                    string minutes = sub.Minutes.ToString();
                    Query = "INSERT INTO employeeAdditionalTable(employeeName,hours,minutes,date,outside) VALUES (N'" + this.employeeNameComboBox.Text + "',N'" + hours + "',N'" + minutes + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','True')  ";
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
                    conDataBase.Close();
                }

                MessageBox.Show("حفظ");

            }
            else if (conditionTextBox.Text == "انصراف")
            {
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();

                string state = new SqlCommand("IF EXISTS (select 1 from employeeComeGoingTable where date =N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' and employeeName = N'" + this.employeeNameComboBox.Text + "'  and come IS NOT NULL and go IS NOT NULL) BEGIN select 1  from employeeComeGoingTable where date =N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "' and employeeName = N'" + this.employeeNameComboBox.Text + "' and come IS NOT NULL and go IS NOT NULL END Else SELECT 0 ", conDataBase).ExecuteScalar().ToString();
                if (state == "1")
                {
                    MessageBox.Show("الانصراف محفوظ سابقًا");
                }
                else
                {
                    string Query = "BEGIN UPDATE employeeComeGoingTable SET go = N'" + this.currentTime.Value.ToString("hh:mm:ss tt") + "',permission=N'" + this.permissionCheckBox.Checked + "' where employeeName =N'" + this.employeeNameComboBox.Text + "' and date = N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "'END";
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
                    conDataBase.Close();

                    if (currentTime.Value < time.Value && permissionCheckBox.Checked==false)
                    {
                        TimeSpan sub = time.Value-currentTime.Value  ;
                        string hours = sub.Hours.ToString();
                        string minutes = sub.Minutes.ToString();
                        Query = "INSERT INTO employeeLateTable(employeeName,hours,minutes,date,outside) VALUES (N'" + this.employeeNameComboBox.Text + "',N'" + hours + "',N'" + minutes + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','True')  ";
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
                        conDataBase.Close();
                    }
                    else if(currentTime.Value > time.Value)
                    {
                        TimeSpan sub = currentTime.Value - time.Value;
                        string hours = sub.Hours.ToString();
                        string minutes = sub.Minutes.ToString();
                        Query = "INSERT INTO employeeAdditionalTable(employeeName,hours,minutes,date,outside) VALUES (N'" + this.employeeNameComboBox.Text + "',N'" + hours + "',N'" + minutes + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','True')  ";
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
                        conDataBase.Close();
                    }

                    MessageBox.Show("حفظ");

                  
                }
            }

            fillOthers();
            fill7odoor();

            permissionCheckBox.Checked = false;

         
        }

        public void localRefresh()
        {
            fillOthers();
        }

    }
}