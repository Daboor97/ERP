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
    public partial class employeeSpecifiedSettings : UserControl
    {
        public employeeSpecifiedSettings()
        {
            InitializeComponent();
            workingDaysComboBox.Items.Add("1");
            workingDaysComboBox.Items.Add("2");
            workingDaysComboBox.Items.Add("3");
            workingDaysComboBox.Items.Add("4");
            workingDaysComboBox.Items.Add("5");
            workingDaysComboBox.Items.Add("6");
            workingDaysComboBox.Items.Add("7");
            workingDaysComboBox.Text = workingDaysComboBox.Items[0].ToString();

            vacationDay1ComboBox.Items.Add("الجمعة");
            vacationDay1ComboBox.Items.Add("السبت");
            vacationDay1ComboBox.Items.Add("الأحد");
            vacationDay1ComboBox.Items.Add("الاثنين");
            vacationDay1ComboBox.Items.Add("الثلاثاء");
            vacationDay1ComboBox.Items.Add("الأربعاء");
            vacationDay1ComboBox.Items.Add("الخميس");
            vacationDay1ComboBox.Items.Add("لا يوجد");
            vacationDay1ComboBox.Text = vacationDay1ComboBox.Items[0].ToString();

            vacationDay2ComboBox.Items.Add("الجمعة");
            vacationDay2ComboBox.Items.Add("السبت");
            vacationDay2ComboBox.Items.Add("الأحد");
            vacationDay2ComboBox.Items.Add("الاثنين");
            vacationDay2ComboBox.Items.Add("الثلاثاء");
            vacationDay2ComboBox.Items.Add("الأربعاء");
            vacationDay2ComboBox.Items.Add("الخميس");
            vacationDay2ComboBox.Items.Add("لا يوجد");
            vacationDay2ComboBox.Text = vacationDay2ComboBox.Items[1].ToString();

            accountingTypeTextBox.Items.Add("أيام");
            accountingTypeTextBox.Items.Add("مبلغ ثابت");
            accountingTypeTextBox.Text = accountingTypeTextBox.Items[0].ToString();

            fill();

        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string state = "new";
        string s1;
        string s2;
        string s3;

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
            try
            {
                //supplier Code
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                employeeCodeTextBox.Text = new SqlCommand("select Id from employeesTable where name=N'" + this.employeeNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
            }
            catch
            {
            }

        }

        private void employeeCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);

            try
            {
                 conDataBase = new SqlConnection(constring);
                //supplier Code
                conDataBase.Open();
                employeeNameComboBox.Text = new SqlCommand("select name from employeesTable where id=N'" + this.employeeCodeTextBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();


                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from employeesGeneralSettingsTable) BEGIN SELECT MAX(Id) FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();

                if (maxBill != "")
                {
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string specifiedSettings = new SqlCommand("IF EXISTS (select 1 from employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "') BEGIN select 1 from employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END Else SELECT 0 ", conDataBase).ExecuteScalar().ToString();
                    if (specifiedSettings == "0")
                    {
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string workingDays = new SqlCommand("BEGIN select workingDays FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        workingDays = (string.IsNullOrEmpty(workingDays)) ? "0" : workingDays;
                        this.workingDaysComboBox.Text = workingDays;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string vacation1 = new SqlCommand("BEGIN select vacation1 FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        vacation1 = (string.IsNullOrEmpty(vacation1)) ? "0" : vacation1;
                        this.vacationDay1ComboBox.Text = vacation1;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string vacation2 = new SqlCommand("BEGIN select vacation2 FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        vacation2 = (string.IsNullOrEmpty(vacation2)) ? "0" : vacation2;
                        this.vacationDay2ComboBox.Text = vacation2;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string startTime = new SqlCommand("BEGIN select startTime FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        this.startDate.Text = startTime;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string endTime = new SqlCommand("BEGIN select endTime FROM employeesGeneralSettingsTable END ", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        this.endDate.Text = endTime;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string range = new SqlCommand("BEGIN select range FROM employeesGeneralSettingsTable END ", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        range = (string.IsNullOrEmpty(range)) ? "0" : range;
                        startRangeTextBox.Text = range;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string accountingType = new SqlCommand("BEGIN select accountingType FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        accountingType = (string.IsNullOrEmpty(accountingType)) ? "0" : accountingType;
                        accountingTypeTextBox.Text = accountingType;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string workingHours = new SqlCommand("BEGIN select workingHours FROM employeesGeneralSettingsTable END ", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        workingHours = (string.IsNullOrEmpty(workingHours)) ? "0" : workingHours;
                        workingHoursTextBox.Text = workingHours;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string workingDaysMonth = new SqlCommand("BEGIN select workingDaysMonth FROM employeesGeneralSettingsTable END ", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        workingDaysMonth = (string.IsNullOrEmpty(workingDaysMonth)) ? "0" : workingDaysMonth;
                        accountingBasedOnDaysTextBox.Text = workingDaysMonth;

                        if (accountingTypeTextBox.Text == "أيام")
                        {

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string missingDay = new SqlCommand("BEGIN select missingDay FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            missingDay = (string.IsNullOrEmpty(missingDay)) ? "0" : missingDay;
                            missingDayEqualsTextBox.Text = missingDay;

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string lateHour = new SqlCommand("BEGIN select lateHour FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            lateHour = (string.IsNullOrEmpty(lateHour)) ? "0" : lateHour;
                            lateHourEqualsTextBox.Text = lateHour;

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string additionalHour = new SqlCommand("BEGIN select additionalHour FROM employeesGeneralSettingsTable END ", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            additionalHour = (string.IsNullOrEmpty(additionalHour)) ? "0" : additionalHour;
                            additionHourEquals.Text = additionalHour;
                        }
                        else
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string missingDay = new SqlCommand("BEGIN select missingDay FROM employeesGeneralSettingsTable END ", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            missingDay = (string.IsNullOrEmpty(missingDay)) ? "0" : missingDay;
                            missingDayEqualsAmountTextBox.Text = missingDay;

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string lateHour = new SqlCommand("BEGIN select lateHour FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            lateHour = (string.IsNullOrEmpty(lateHour)) ? "0" : lateHour;
                            lateHourEqualsAmountTextBox.Text = lateHour;

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string additionalHour = new SqlCommand("BEGIN select additionalHour FROM employeesGeneralSettingsTable END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            additionalHour = (string.IsNullOrEmpty(additionalHour)) ? "0" : additionalHour;
                            additionHourEqualsAmountTextBox.Text = additionalHour;
                        }
                    }
                    else
                    {

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string workingDays = new SqlCommand("BEGIN select workingDays FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "'  END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        workingDays = (string.IsNullOrEmpty(workingDays)) ? "0" : workingDays;
                        workingDaysComboBox.Text = workingDays;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string vacation1 = new SqlCommand("BEGIN select vacation1 FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        vacation1 = (string.IsNullOrEmpty(vacation1)) ? "0" : vacation1;
                        vacationDay1ComboBox.Text = vacation1;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string vacation2 = new SqlCommand("BEGIN select vacation2 FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        vacation2 = (string.IsNullOrEmpty(vacation2)) ? "0" : vacation2;
                        vacationDay2ComboBox.Text = vacation2;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string startTime = new SqlCommand("BEGIN select startTime FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        startDate.Text = startTime;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string endTime = new SqlCommand("BEGIN select endTime FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END ", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        endDate.Text = endTime;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string range = new SqlCommand("BEGIN select range FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END ", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        range = (string.IsNullOrEmpty(range)) ? "0" : range;
                        startRangeTextBox.Text = range;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string accountingType = new SqlCommand("BEGIN select accountingType FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        accountingType = (string.IsNullOrEmpty(accountingType)) ? "0" : accountingType;
                        accountingTypeTextBox.Text = accountingType;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string workingHours = new SqlCommand("BEGIN select workingHours FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END ", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        workingHours = (string.IsNullOrEmpty(workingHours)) ? "0" : workingHours;
                        workingHoursTextBox.Text = workingHours;

                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        string workingDaysMonth = new SqlCommand("BEGIN select workingDaysMonth FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END ", conDataBase).ExecuteScalar().ToString();
                        conDataBase.Close();
                        workingDaysMonth = (string.IsNullOrEmpty(workingDaysMonth)) ? "0" : workingDaysMonth;
                        accountingBasedOnDaysTextBox.Text = workingDaysMonth;

                        if (accountingTypeTextBox.Text == "أيام")
                        {

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string missingDay = new SqlCommand("BEGIN select missingDay FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            missingDay = (string.IsNullOrEmpty(missingDay)) ? "0" : missingDay;
                            missingDayEqualsTextBox.Text = missingDay;

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string lateHour = new SqlCommand("BEGIN select lateHour FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            lateHour = (string.IsNullOrEmpty(lateHour)) ? "0" : lateHour;
                            lateHourEqualsTextBox.Text = lateHour;

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string additionalHour = new SqlCommand("BEGIN select additionalHour FROM employeesSpecifiedTable  where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END ", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            additionalHour = (string.IsNullOrEmpty(additionalHour)) ? "0" : additionalHour;
                            additionHourEquals.Text = additionalHour;
                        }
                        else
                        {
                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string missingDay = new SqlCommand("BEGIN select missingDay FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END ", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            missingDay = (string.IsNullOrEmpty(missingDay)) ? "0" : missingDay;
                            missingDayEqualsAmountTextBox.Text = missingDay;

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string lateHour = new SqlCommand("BEGIN select lateHour FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            lateHour = (string.IsNullOrEmpty(lateHour)) ? "0" : lateHour;
                            lateHourEqualsAmountTextBox.Text = lateHour;

                            conDataBase = new SqlConnection(constring);
                            conDataBase.Open();
                            string additionalHour = new SqlCommand("BEGIN select additionalHour FROM employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                            conDataBase.Close();
                            additionalHour = (string.IsNullOrEmpty(additionalHour)) ? "0" : additionalHour;
                            additionHourEqualsAmountTextBox.Text = additionalHour;
                        }
                    }

                }

                else
                {

                }

            }
            catch
            {

            }

           
        }

        private void accountingTypeTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (accountingTypeTextBox.Text == "أيام")
            {
                missingDayEqualsAmountTextBox.BackColor = Color.FromArgb(64, 64, 64);
                missingDayEqualsAmountTextBox.ReadOnly = true;
                missingDayEqualsAmountTextBox.Text = "";

                lateHourEqualsAmountTextBox.BackColor = Color.FromArgb(64, 64, 64);
                lateHourEqualsAmountTextBox.ReadOnly = true;
                lateHourEqualsAmountTextBox.Text = "";

                additionHourEqualsAmountTextBox.BackColor = Color.FromArgb(64, 64, 64);
                additionHourEqualsAmountTextBox.ReadOnly = true;
                additionHourEqualsAmountTextBox.Text = "";


                missingDayEqualsTextBox.BackColor = Color.FromArgb(41, 44, 51);
                missingDayEqualsTextBox.ReadOnly = false;

                lateHourEqualsTextBox.BackColor = Color.FromArgb(41, 44, 51);
                lateHourEqualsTextBox.ReadOnly = false;

                additionHourEquals.BackColor = Color.FromArgb(41, 44, 51);
                additionHourEquals.ReadOnly = false;

            }
            else
            {
                missingDayEqualsTextBox.BackColor = Color.FromArgb(64, 64, 64);
                missingDayEqualsTextBox.ReadOnly = true;
                missingDayEqualsTextBox.Text = "";

                lateHourEqualsTextBox.BackColor = Color.FromArgb(64, 64, 64);
                lateHourEqualsTextBox.ReadOnly = true;
                lateHourEqualsTextBox.Text = "";

                additionHourEquals.BackColor = Color.FromArgb(64, 64, 64);
                additionHourEquals.ReadOnly = true;
                additionHourEquals.Text = "";


                missingDayEqualsAmountTextBox.BackColor = Color.FromArgb(41, 44, 51);
                missingDayEqualsAmountTextBox.ReadOnly = false;

                lateHourEqualsAmountTextBox.BackColor = Color.FromArgb(41, 44, 51);
                lateHourEqualsAmountTextBox.ReadOnly = false;

                additionHourEqualsAmountTextBox.BackColor = Color.FromArgb(41, 44, 51);
                additionHourEqualsAmountTextBox.ReadOnly = false;

            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

            if (accountingTypeTextBox.Text == "أيام")
            {
                s1 = missingDayEqualsTextBox.Text;
                s2 = lateHourEqualsTextBox.Text;
                s3 = additionHourEquals.Text;
            }

            else
            {
                s1 = missingDayEqualsAmountTextBox.Text;
                s2 = lateHourEqualsAmountTextBox.Text;
                s3 = additionHourEqualsAmountTextBox.Text;
            }


            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select 1 from employeesSpecifiedTable where employeeNumber=N'"+this.employeeCodeTextBox.Text+ "') BEGIN SELECT 1 FROM  employeesSpecifiedTable where employeeNumber=N'" + this.employeeCodeTextBox.Text + "' END ELSE SELECT 0" , conDataBase).ExecuteScalar().ToString();

            if (maxBill =="1")
            {
                string Query = "BEGIN UPDATE employeesSpecifiedTable SET workingDays = N'" + this.workingDaysComboBox.Text + "',vacation1=N'" + this.vacationDay1ComboBox.Text + "',vacation2=N'" + this.vacationDay2ComboBox.Text + "',startTime=N'" + this.startDate.Value.ToString("hh:mm:ss tt") + "',endTime=N'" + this.endDate.Value.ToString("hh:mm:ss tt") + "',range=N'" + this.startRangeTextBox.Text + "', accountingType=N'" + this.accountingTypeTextBox.Text + "',workingHours=N'" + this.workingHoursTextBox.Text + "',missingDay=N'" + s1 + "',lateHour=N'" + s2 + "', additionalHour=N'" + s3 + "',workingDaysMonth=N'" + this.accountingBasedOnDaysTextBox.Text + "' where employeeNumber=N'" + this.employeeCodeTextBox.Text + "' END";
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
                string update = this.employeeCodeTextBox.Text;
                this.employeeCodeTextBox.Text = "";
                this.employeeCodeTextBox.Text = update;
            }
            else
            {
                string Query = "INSERT INTO employeesSpecifiedTable(workingDays,vacation1,vacation2,startTime,endTime,range,accountingType,workingHours,missingDay,lateHour,additionalHour,workingDaysMonth,employeeNumber) VALUES (N'" + this.workingDaysComboBox.Text + "',N'" + this.vacationDay1ComboBox.Text + "',N'" + this.vacationDay2ComboBox.Text + "',N'" + this.startDate.Value.ToString("hh:mm:ss tt") + "',N'" + this.endDate.Value.ToString("hh:mm:ss tt") + "', N'" + this.startRangeTextBox.Text + "',N'" + this.accountingTypeTextBox.Text + "',N'" + this.workingHoursTextBox.Text + "',N'" + s1 + "',N'" + s2 + "','" + s3 + "',N'" + this.accountingBasedOnDaysTextBox.Text + "',N'"+this.employeeCodeTextBox.Text+"')  ";
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
                MessageBox.Show("حفظ");
                string update = this.employeeCodeTextBox.Text;
                this.employeeCodeTextBox.Text = "";
                this.employeeCodeTextBox.Text = update;
            }
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select 1 from employeesSpecifiedTable where employeeNumber=N'" + this.employeeCodeTextBox.Text + "') BEGIN SELECT 1 FROM  employeesSpecifiedTable where employeeNumber=N'" + this.employeeCodeTextBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();

            if (maxBill == "0")
            {
                MessageBox.Show("الإعدادات المحفوظة لهذا الموظف هي الإعدادات العامة");
            }

            else
            {

                DialogResult dialogResult = MessageBox.Show("هل تريد استرداد الإعدادات العامة لهذا الموظف؟", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string Query = "DELETE FROM employeesSpecifiedTable where employeeNumber=N'" + this.employeeCodeTextBox.Text + "' ;";
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

                    string update = this.employeeCodeTextBox.Text;
                    this.employeeCodeTextBox.Text = "";                 
                   this.employeeCodeTextBox.Text = update;

                    MessageBox.Show("حُفظ");
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
        }

        public void refreshLocal()
        {
            fill();
        }
    }
}
