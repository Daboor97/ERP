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
    public partial class wages : UserControl
    {
        public wages()
        {
            InitializeComponent();
            deleteButton.Visible = false;
            clear();
            fill();
            fillOthers();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string status = "new";
        string choicesType = "";
        string abscenceValue = "";
        string lateValue = "";
        string additionalValue = "";
        string assumedPresentDays = "";
        int presentDays;
        double mainSalary;
        double lateHours;
        double lateMinutes;
        double additionalHours;
        double additionalMinutes;
        int additionalAbscence;
        double workingHours;

        
        void clear()
        {
            try
            {
                this.remainderTextBox.Text = "0";
                this.mainSalaryTextBox.Text = "0";
                this.advanceTextBox.Text = "0";
                this.abscenceDaysTextBox.Text = "0";
                this.abscenceValueTextBox.Text = "0";
                this.additionalHoursTextBox.Text = "0";
                this.addditionalValueTextBox.Text = "0";
                this.lateHoursTextBox.Text = "0";
                this.lateValueTextBox.Text = "0";
                this.foodTextBox.Text = "0";
                this.transportTextBox.Text = "0";
                this.bonusesTextBox.Text = "0";
                this.reductionTextBox.Text = "0";
                this.remainderTextBox.Text = "0";
                this.date.Value = DateTime.Today;
            }
            catch
            {

            }

        }

        void fillOthers()
        {
            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from wagesTable) BEGIN SELECT MAX(Id) FROM wagesTable END", conDataBase).ExecuteScalar().ToString();

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

            //safeComboBox
            safeComboBox.Items.Clear();
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string  Query = "select distinct name from safeMainTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    safeComboBox.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (safeComboBox.Items.Count > 0)
            {
                safeComboBox.Text = safeComboBox.Items[0].ToString();
            }

            supplierDGV.DataSource = null;

             Query = "select wagesTable.Id as 'رقم المرتب' , name as 'اسم الموظّف' ,fromDate as 'من تاريخ', toDate as 'إلى تاريخ' , date as 'تاريخ التسجيل' ,mainSalary as 'الأساسي',advances as 'السلف',abscentDays as 'عدد أيام الغياب',abscentValue as 'قيمة أيام الغياب',additionalHours as 'عدد الساعات الإضافية',AdditionalHoursValue as 'قيمة الساعات الإضافية',lateHours as 'عدد ساعات التأخير',lateHoursValue as 'قيمة ساعات التأخير',food as 'بدل وجبات',transportation as 'بدل انتقال',bonuses as 'مكافئات أخرى',reduction as 'خصومات أخرى',safe as 'الخزنة',remainder as 'صافي المرتب' from wagesTable,employeesTable where employeesTable.Id = wagesTable.employeeNumber and employeeNumber=N'" + this.employeeCodeTextBox.Text + "' ; ";

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

                supplierDGV.DataSource = null;

               string Query = "select wagesTable.Id as 'رقم المرتب' , name as 'اسم الموظّف' ,fromDate as 'من تاريخ', toDate as 'إلى تاريخ' , date as 'تاريخ التسجيل' ,mainSalary as 'الأساسي',advances as 'السلف',abscentDays as 'عدد أيام الغياب',abscentValue as 'قيمة أيام الغياب',additionalHours as 'عدد الساعات الإضافية',AdditionalHoursValue as 'قيمة الساعات الإضافية',lateHours as 'عدد ساعات التأخير',lateHoursValue as 'قيمة ساعات التأخير',food as 'بدل وجبات',transportation as 'بدل انتقال',bonuses as 'مكافئات أخرى',reduction as 'خصومات أخرى',safe as 'الخزنة',remainder as 'صافي المرتب' from wagesTable,employeesTable where employeesTable.Id = wagesTable.employeeNumber and employeeNumber=N'" + this.employeeCodeTextBox.Text + "' ; ";

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
 
                }

                conDataBase.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        void sum()
        {
            //Abscence Number
            abscenceDaysTextBox.Text = (Convert.ToInt32(assumedPresentDays) - presentDays + additionalAbscence).ToString();

            //Additional Hours Number
            additionalHours += (additionalMinutes / 60);
            additionalHoursTextBox.Text = additionalHours.ToString();
           

            // LATE Hours Number
            lateHours += (lateMinutes / 60);
            lateHoursTextBox.Text = lateHours.ToString();

            double oneDayValue = mainSalary / Convert.ToInt32(assumedPresentDays);

            if (choicesType== "أيام")
            {

               //Values

                int abscence = Convert.ToInt32(abscenceValue) * (Convert.ToInt32(assumedPresentDays) - presentDays + additionalAbscence);

                abscenceValueTextBox.Text = (abscence * oneDayValue).ToString();

                double workingDaysToAdd = ((additionalHours *Convert.ToInt32(additionalValue))/ workingHours);
                addditionalValueTextBox.Text = (workingDaysToAdd * oneDayValue).ToString();

                double workingDaysToSubtracts = ((lateHours *Convert.ToInt32(lateValue)) / workingHours);
                lateValueTextBox.Text = (workingDaysToSubtracts * oneDayValue).ToString();

            }
            else
            {
                int abscence = Convert.ToInt32(abscenceValue) * (Convert.ToInt32(assumedPresentDays) - presentDays + additionalAbscence);
                abscenceValueTextBox.Text = abscence.ToString();
                addditionalValueTextBox.Text = (additionalHours * Convert.ToInt32(additionalValue)).ToString();
                lateValueTextBox.Text = (lateHours * Convert.ToInt32(lateValue)).ToString();
            }
            summation();
        }

        void summation()
        {
            try
            {
                double a, b, c, d, e, f, g, h, j;

                int m;

                double.TryParse(mainSalaryTextBox.Text, out a);
                double.TryParse(advanceTextBox.Text, out b);
                double.TryParse(abscenceValueTextBox.Text, out c);
                double.TryParse(addditionalValueTextBox.Text, out d);
                double.TryParse(lateValueTextBox.Text, out e);
                double.TryParse(foodTextBox.Text, out f);
                double.TryParse(transportTextBox.Text, out g);
                double.TryParse(bonusesTextBox.Text, out h);
                double.TryParse(reductionTextBox.Text, out j);

                m = Convert.ToInt32(a - b - c + d - e + f + g + h - j);
                remainderTextBox.Text = Convert.ToString(m);
            }
            catch (Exception ex)
            {
            }
        }

        private void mainSalaryTextBox_TextChanged(object sender, EventArgs e)
        {
            summation();
        }

        private void advanceTextBox_TextChanged(object sender, EventArgs e)
        {
            summation();
        }

        private void foodTextBox_TextChanged(object sender, EventArgs e)
        {
            summation();
        }

        private void transportTextBox_TextChanged(object sender, EventArgs e)
        {
            summation();
        }

        private void bonusesTextBox_TextChanged(object sender, EventArgs e)
        {
            summation();
        }

        private void reductionTextBox_TextChanged(object sender, EventArgs e)
        {
            summation();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (status == "new")
            {
                string Query = "INSERT INTO wagesTable(employeeNumber,fromDate,toDate,date,mainSalary,advances,abscentDays,abscentValue,additionalHours,AdditionalHoursValue,lateHours,lateHoursValue,food,transportation,bonuses,reduction,safe,remainder) VALUES (N'" + this.employeeCodeTextBox.Text + "',N'" + this.fromDate.Value.ToString("MM/dd/yyyy") + "',N'" + this.toDate.Value.ToString("MM/dd/yyyy") + "',N'" + this.date.Value.ToString("MM/dd/yyyy") + "',N'"+this.mainSalaryTextBox.Text+ "',N'" + this.advanceTextBox.Text + "',N'" + this.abscenceDaysTextBox.Text + "',N'" + this.abscenceValueTextBox.Text + "',N'" + this.additionalHoursTextBox.Text + "',N'" + this.addditionalValueTextBox.Text + "',N'" + this.lateHoursTextBox.Text + "',N'" + this.lateValueTextBox.Text + "',N'" + this.foodTextBox.Text + "',N'" + this.transportTextBox.Text + "',N'" + this.bonusesTextBox.Text + "',N'" + this.reductionTextBox.Text + "',N'" + this.safeComboBox.Text + "',N'" + this.remainderTextBox.Text + "')  ";
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

                //الخزنة

                Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.remainderTextBox.Text + "','wages',N'" + this.date.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.codeTextBox.Text + "','cash',N'" + this.employeeCodeTextBox.Text + "')";
                conDataBase = new SqlConnection(constring);
                cmdDataBase = new SqlCommand(Query, conDataBase);
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

                conDataBase.Close();
                clear();
                deleteButton.Visible = false;
                status = "new";
                MessageBox.Show("حفظ");
            }
            
            
            else
            {
              
                    string Query = "BEGIN UPDATE wagesTable SET fromDate= N'" + this.fromDate.Value.ToString("MM/dd/yyyy") + "',toDate=N'" + this.toDate.Value.ToString("MM/dd/yyyy") + "',date=N'" + this.date.Value.ToString("MM/dd/yyyy") + "',mainSalary=N'" + this.mainSalaryTextBox.Text + "',advances=N'" + this.advanceTextBox.Text + "',abscentDays=N'" + this.abscenceDaysTextBox.Text + "',abscentValue=N'" + this.abscenceValueTextBox.Text + "',additionalHours=N'" + this.additionalHoursTextBox.Text + "',AdditionalHoursValue=N'" + this.addditionalValueTextBox.Text + "',lateHours=N'" + this.lateHoursTextBox.Text + "',lateHoursValue=N'" + this.lateValueTextBox.Text + "',food=N'" + this.foodTextBox.Text + "',transportation=N'" + this.transportTextBox.Text + "',bonuses=N'" + this.bonusesTextBox.Text + "',reduction=N'" + this.reductionTextBox.Text + "',safe=N'" + this.safeComboBox.Text + "',remainder=N'" + this.remainderTextBox.Text + "' where Id =N'" + this.codeTextBox.Text + "' END";
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

                Query = "DELETE FROM safeTable where type='wages' and billNo=N'"+this.codeTextBox.Text+ "' and clientCode=N'"+this.employeeCodeTextBox.Text+"';";
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

                //الخزنة
                Query = "INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'" + this.safeComboBox.Text + "' ,'',N'" + this.remainderTextBox.Text + "','wages',N'" + this.date.Value.ToString("MM/dd/yyyy") + "','out',N'" + this.codeTextBox.Text + "','cash',N'" + this.employeeCodeTextBox.Text + "')";
                conDataBase = new SqlConnection(constring);
                cmdDataBase = new SqlCommand(Query, conDataBase);
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


                MessageBox.Show("انتهى التعديل");


                    clear();
                    deleteButton.Visible = false;
                    status = "new";

            }
            fillOthers();
        }

        private void supplierDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            deleteButton.Visible = true;
            status = "adjust";
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.supplierDGV.Rows[e.RowIndex];

                this.codeTextBox.Text = row.Cells[0].Value.ToString();
                this.employeeNameComboBox.Text = row.Cells[1].Value.ToString();
                this.fromDate.Text = row.Cells[2].Value.ToString();
                this.toDate.Text = row.Cells[3].Value.ToString();
                this.date.Text = row.Cells[4].Value.ToString();
                this.mainSalaryTextBox.Text = row.Cells[5].Value.ToString();
                this.advanceTextBox.Text = row.Cells[6].Value.ToString();
                this.abscenceDaysTextBox.Text = row.Cells[7].Value.ToString();
                this.abscenceValueTextBox.Text = row.Cells[8].Value.ToString();
                this.additionalHoursTextBox.Text = row.Cells[9].Value.ToString();
                this.addditionalValueTextBox.Text = row.Cells[10].Value.ToString();
                this.lateHoursTextBox.Text = row.Cells[11].Value.ToString();
                this.lateValueTextBox.Text = row.Cells[12].Value.ToString();
                this.foodTextBox.Text = row.Cells[13].Value.ToString();
                this.transportTextBox.Text = row.Cells[14].Value.ToString();
                this.bonusesTextBox.Text = row.Cells[15].Value.ToString();
                this.reductionTextBox.Text = row.Cells[16].Value.ToString();
                this.safeComboBox.Text = row.Cells[17].Value.ToString();
                this.remainderTextBox.Text = row.Cells[18].Value.ToString();
              
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("هل تريد حذف الاختيار؟", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string Query = "DELETE FROM wagesTable where Id =N'" + this.codeTextBox.Text + "' ;";
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

                Query = "DELETE FROM safeTable where type='wages' and billNo=N'" + this.codeTextBox.Text + "' and clientCode=N'" + this.employeeCodeTextBox.Text + "';";
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

                fillOthers();
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

        private void searchButton_Click(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);

            try
            {
                //Base Salary Code
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                mainSalaryTextBox.Text = new SqlCommand("select salary from employeesTable where name=N'" + this.employeeNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                mainSalary = Convert.ToDouble(mainSalaryTextBox.Text);

                //Base Salary Code
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                presentDays = Convert.ToInt32(new SqlCommand("select Count(employeeName) from employeeComeGoingTable where employeeName=N'" + this.employeeNameComboBox.Text + "' and come IS NOT NULL and go IS NOT NULL and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "';", conDataBase).ExecuteScalar().ToString());
                conDataBase.Close();


                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string addAbscenceString = new SqlCommand("select Sum(days) from employeeAbscenceTable where employeeName=N'" + this.employeeNameComboBox.Text + "' and N'" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' <= CAST(fromDate AS DATE) and N'" + this.toDate.Value.ToString("MM/dd/yyyy") + "' >= CAST(toDate AS DATE);", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                addAbscenceString = (string.IsNullOrEmpty(addAbscenceString)) ? "0" : addAbscenceString;
                additionalAbscence = Convert.ToInt32(addAbscenceString);

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                addAbscenceString = new SqlCommand("select Sum(hours) from employeeLateTable where employeeName=N'" + this.employeeNameComboBox.Text + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                addAbscenceString = (string.IsNullOrEmpty(addAbscenceString)) ? "0" : addAbscenceString;
                lateHours = Convert.ToInt32(addAbscenceString);


                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                addAbscenceString = new SqlCommand("select Sum(minutes) from employeeLateTable where employeeName=N'" + this.employeeNameComboBox.Text + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                addAbscenceString = (string.IsNullOrEmpty(addAbscenceString)) ? "0" : addAbscenceString;
                lateMinutes = Convert.ToInt32(addAbscenceString);



                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                addAbscenceString = new SqlCommand("select Sum(hours) from employeeAdditionalTable where employeeName=N'" + this.employeeNameComboBox.Text + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                addAbscenceString = (string.IsNullOrEmpty(addAbscenceString)) ? "0" : addAbscenceString;
                additionalHours = Convert.ToInt32(addAbscenceString);

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                addAbscenceString = new SqlCommand("select Sum(minutes) from employeeAdditionalTable where employeeName=N'" + this.employeeNameComboBox.Text + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                addAbscenceString = (string.IsNullOrEmpty(addAbscenceString)) ? "0" : addAbscenceString;
                additionalMinutes = Convert.ToInt32(addAbscenceString);

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string specifiedSettings = new SqlCommand("IF EXISTS (select 1 from employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "') BEGIN select 1 from employeesSpecifiedTable where employeeNumber =N'" + this.employeeCodeTextBox.Text + "' END Else SELECT 0 ", conDataBase).ExecuteScalar().ToString();
                if (specifiedSettings == "1")
                {
                    //Type Salary Code
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    choicesType = new SqlCommand("select accountingType from employeesSpecifiedTable where employeeNumber=N'" + this.employeeCodeTextBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //abscence Value
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    abscenceValue = new SqlCommand("select missingDay from employeesSpecifiedTable where employeeNumber=N'" + this.employeeCodeTextBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //late Value
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    lateValue = new SqlCommand("select lateHour from employeesSpecifiedTable where employeeNumber=N'" + this.employeeCodeTextBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //additional Value
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    additionalValue = new SqlCommand("select additionalHour from employeesSpecifiedTable where employeeNumber=N'" + this.employeeCodeTextBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //assumedPresentDays Value
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    assumedPresentDays = new SqlCommand("select workingDaysMonth from employeesSpecifiedTable where employeeNumber=N'" + this.employeeCodeTextBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //workingHours Value
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    workingHours = Convert.ToInt32(new SqlCommand("select workingHours from employeesSpecifiedTable where employeeNumber=N'" + this.employeeCodeTextBox.Text + "';", conDataBase).ExecuteScalar().ToString());
                    conDataBase.Close();
                }

                else
                {
                    //Type Salary Code
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    choicesType = new SqlCommand("select accountingType from employeesGeneralSettingsTable;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //abscence Value
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    abscenceValue = new SqlCommand("select missingDay from employeesGeneralSettingsTable;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //late Value
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    lateValue = new SqlCommand("select lateHour from employeesGeneralSettingsTable;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();


                    //additional Value
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    additionalValue = new SqlCommand("select additionalHour from employeesGeneralSettingsTable;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //assumedPresentDays Value
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    assumedPresentDays = new SqlCommand("select workingDaysMonth from employeesGeneralSettingsTable;", conDataBase).ExecuteScalar().ToString();
                    conDataBase.Close();

                    //workingHours Value
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    workingHours = Convert.ToInt32(new SqlCommand("select workingHours from employeesGeneralSettingsTable;", conDataBase).ExecuteScalar().ToString());
                    conDataBase.Close();
                }

                

                sum();


            }
            catch
            {

            }
        }
        public void refreshLocal()
        {
            deleteButton.Visible = false;
            clear();
            fill();
            fillOthers();
        }
    }
}