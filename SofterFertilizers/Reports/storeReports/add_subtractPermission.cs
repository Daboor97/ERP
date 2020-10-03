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


namespace SofterFertilizers.Reports.storeReports
{
    public partial class add_subtractPermission : UserControl
    {
        public add_subtractPermission()
        {
            InitializeComponent();
            permissionTypeComboBox.Items.Add("إذن إضافة");
            permissionTypeComboBox.Items.Add("إذن خصم");
            permissionTypeComboBox.Text = permissionTypeComboBox.Items[0].ToString();
            fill();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {

            //store Combo Boxes
            storeNameComboBox.Items.Clear();
            storeNameComboBox.Items.Add("كل المخازن");
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct storeName from storeTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    storeNameComboBox.Items.Add(dr["storeName"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (storeNameComboBox.Items.Count > 0)
            {
                storeNameComboBox.Text = storeNameComboBox.Items[0].ToString();
            }
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;
            categoryDGV.Refresh();

            if (permissionTypeComboBox.Text== "إذن إضافة")
            {
                if (storeNameComboBox.Text == "كل المخازن")
                {
                    string Query = "select Id as 'رقم الإذن', storeName as 'المخزن' , sum as 'المحموع' , status as 'الحالة' ,date as 'التاريخ' from permissionAdditionMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ;";

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
                        categoryDGV.DataSource = bSource;
                        sda.Update(dbdataset);
                    }
                    catch (Exception ex)
                    {
     
                    }  
                }
                else
                {
                    string Query = "select Id as 'رقم الإذن', storeName as 'المخزن' , sum as 'المحموع' , status as 'الحالة' ,date as 'التاريخ' from permissionAdditionMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  and storeName = N'"+this.storeNameComboBox.Text+"';";

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
                        categoryDGV.DataSource = bSource;
                        sda.Update(dbdataset);
                    }
                    catch (Exception ex)
                    {
     
                    }
                }
            }
            else
            {
                if (storeNameComboBox.Text == "كل المخازن")
                {
                    string Query = "select Id as 'رقم الإذن', storeName as 'المخزن' , sum as 'المحموع' , status as 'الحالة' ,date as 'التاريخ' from permissionSubtractionMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ;";

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
                        categoryDGV.DataSource = bSource;
                        sda.Update(dbdataset);
                    }
                    catch (Exception ex)
                    {
     
                    }

                }
                else
                {
                    string Query = "select Id as 'رقم الإذن', storeName as 'المخزن' , sum as 'المحموع' , status as 'الحالة' ,date as 'التاريخ' from permissionSubtractionMainTable where date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  and storeName = N'" + this.storeNameComboBox.Text + "';";

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
                        categoryDGV.DataSource = bSource;
                        sda.Update(dbdataset);
                    }
                    catch (Exception ex)
                    {
     
                    }
                }
            }
        }

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (permissionTypeComboBox.Text == "إذن إضافة")
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];

                        add_subtract_permission salesBill = new add_subtract_permission(0, Convert.ToInt32(row.Cells[0].Value.ToString()));
                        salesBill.Show();
                        salesBill.BringToFront();

                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                try
                {
                    if (e.RowIndex >= 0)
                    {
                        DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];

                        add_subtract_permission salesBill = new add_subtract_permission(1, Convert.ToInt32(row.Cells[0].Value.ToString()));
                        salesBill.Show();
                        salesBill.BringToFront();

                    }
                }
                catch (Exception ex)
                {
                }
            
        }
        }

        private void permissionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;
            categoryDGV.Refresh();
        }

        private void storeNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            categoryDGV.DataSource = null;
            categoryDGV.Refresh();
        }
    }
}
