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
    public partial class abscenceReports : UserControl
    {
        public abscenceReports()
        {
            InitializeComponent();
            fill();
        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {

            //supplier ComboBox
            employeeNameComboBox.Items.Clear();
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
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();
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
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();

           
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

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            selectedDGV.DataSource = null;
            selectedDGV.Refresh();

           
            DGV.Rows.Clear();
            DGV.Refresh();

            selectedComboBox.Items.Clear();
            requiredDate.Items.Clear();
            dateComboBox.Items.Clear();


            string Query = "select employeeName as 'اسم الموظّف' ,fromDate as 'من تاريخ', toDate as 'إلى تاريخ' , days as 'عدد الأيام', permission as 'بإذن' ,today as 'تاريخ تسجيل الغياب' from employeeAbscenceTable where employeeName = N'" + this.employeeNameComboBox.Text + "'  and N'" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' <= CAST(fromDate AS DATE) and N'" + this.toDate.Value.ToString("MM/dd/yyyy") + "' >= CAST(toDate AS DATE); ";

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
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            Query = "select date from employeeComeGoingTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and employeeName = N'" + this.employeeNameComboBox.Text + "' and come IS NOT NULL and go IS NOT NULL ;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                DateTime Received;

                foreach (DataRow dr in dt.Rows)
                {
                    DateTime.TryParse(dr["date"].ToString(), out Received);
                    selectedComboBox.Items.Add(Received.Date);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            DateTime from = fromDate.Value.Date;
            DateTime to = toDate.Value.Date;

            var dates = new List<DateTime>();

            for (var xdcv = from; xdcv<= to; xdcv = xdcv.AddDays(1))
            {
                dates.Add(xdcv);
            }

            for (int i= 0;i< dates.Count; i++)
            {
                dateComboBox.Items.Add(dates[i]);
            }
        
            for(int i = 0; i < dateComboBox.Items.Count; i++)
            {
                for(int j = 0; j < selectedComboBox.Items.Count; j++)
                {
                    if(dateComboBox.Items[i].ToString() == selectedComboBox.Items[j].ToString())
                    {
                        break;
                    }

                    else if(j == selectedComboBox.Items.Count - 1)
                    {
                        requiredDate.Items.Add(dateComboBox.Items[i]);
                    }
                    
                }
            }

            for (int i = 0; i < selectedDGV.Rows.Count ; i++)
            {
                try
                {
                    DGV.Rows.Add(selectedDGV.Rows[i].Cells[0].Value.ToString(), selectedDGV.Rows[i].Cells[1].Value.ToString(), selectedDGV.Rows[i].Cells[2].Value.ToString(),
                        selectedDGV.Rows[i].Cells[3].Value.ToString(), selectedDGV.Rows[i].Cells[4].Value.ToString(), selectedDGV.Rows[i].Cells[5].Value.ToString(),
                        "غياب مدخل يدويًا");
                }
                catch { }
            }
            
            for(int i = 0; i < requiredDate.Items.Count; i++)
            {
                try
                {
                    DGV.Rows.Add(employeeNameComboBox.Text, "", "","1", "False", Convert.ToDateTime(requiredDate.Items[i].ToString()).Date,"من الحضور والانصراف");
                }
                catch { }
            }
            int sum = 0;
            int sumPermission = 0;
            int sumWithout = 0;

            for (int i = 0; i < DGV.Rows.Count - 1; i++)
            {
                try
                {
                    sum +=Convert.ToInt32(DGV.Rows[i].Cells[3].Value.ToString());
                    if (Convert.ToBoolean(DGV.Rows[i].Cells[4].Value.ToString()))
                    {
                        sumPermission += Convert.ToInt32(DGV.Rows[i].Cells[3].Value.ToString());
                    }
                    else
                    {
                        sumWithout += Convert.ToInt32(DGV.Rows[i].Cells[3].Value.ToString());

                    }
                }
                catch
                {

                }
            }


            abscentDaysNumberTextBox.Text = sum.ToString();
            sumPermissionTextBox.Text = sumPermission.ToString();
            sumWithoutTextBox.Text = sumWithout.ToString();

        }
    }
}
