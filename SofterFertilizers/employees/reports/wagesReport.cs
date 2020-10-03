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

namespace SofterFertilizers.employees.reports
{
    public partial class wagesReport : UserControl
    {
        public wagesReport()
        {
            InitializeComponent();
            fill();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {

            //supplier ComboBox
            employeeNameComboBox.Items.Clear();
            employeeNameComboBox.Items.Add("كل الموظفين");

            //supplier ComboBox
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
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();
            if (employeeNameComboBox.Text == "كل الموظفين")
            {
                employeeCodeTextBox.Text = "0";
            }
            else
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

        }

        private void employeeCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();

            if (employeeCodeTextBox.Text == "0")
            {
                employeeNameComboBox.Text = "كل الموظفين";
            }
            else
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
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            selectedDGV.DataSource = null;
            if (employeeCodeTextBox.Text == "0")
            {

                string Query = "select wagesTable.Id as 'رقم المرتب' , name as 'اسم الموظّف' ,fromDate as 'من تاريخ', toDate as 'إلى تاريخ' , date as 'تاريخ التسجيل' ,mainSalary as 'الأساسي',advances as 'السلف',abscentDays as 'عدد أيام الغياب',abscentValue as 'قيمة أيام الغياب',additionalHours as 'عدد الساعات الإضافية',AdditionalHoursValue as 'قيمة الساعات الإضافية',lateHours as 'عدد ساعات التأخير',lateHoursValue as 'قيمة ساعات التأخير',food as 'بدل وجبات',transportation as 'بدل انتقال',bonuses as 'مكافئات أخرى',reduction as 'خصومات أخرى',safe as 'الخزنة',remainder as 'صافي المرتب' from wagesTable,employeesTable where employeesTable.Id = wagesTable.employeeNumber and  date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ; ";

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
                    selectedDGV.DataSource = bSource;
                    sda.Update(dbdataset);

                }
                catch (Exception ex)
                {
 
                }

                conDataBase.Close();
            }
            else
            {
                string Query = "select wagesTable.Id as 'رقم المرتب' , name as 'اسم الموظّف' ,fromDate as 'من تاريخ', toDate as 'إلى تاريخ' , date as 'تاريخ التسجيل' ,mainSalary as 'الأساسي',advances as 'السلف',abscentDays as 'عدد أيام الغياب',abscentValue as 'قيمة أيام الغياب',additionalHours as 'عدد الساعات الإضافية',AdditionalHoursValue as 'قيمة الساعات الإضافية',lateHours as 'عدد ساعات التأخير',lateHoursValue as 'قيمة ساعات التأخير',food as 'بدل وجبات',transportation as 'بدل انتقال',bonuses as 'مكافئات أخرى',reduction as 'خصومات أخرى',safe as 'الخزنة',remainder as 'صافي المرتب' from wagesTable,employeesTable where employeesTable.Id = wagesTable.employeeNumber and employeeNumber=N'" + this.employeeCodeTextBox.Text + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ; ";

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
                    selectedDGV.DataSource = bSource;
                    sda.Update(dbdataset);

                }
                catch (Exception ex)
                {
 
                }

                conDataBase.Close();

            }
        }
    }
}
