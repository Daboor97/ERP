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

namespace SofterFertilizers.calculations.loans
{
    public partial class loansReport : UserControl
    {
        public loansReport()
        {
            InitializeComponent();
            reportComboBox.Items.Add("مفصّل");
            reportComboBox.Items.Add("إجمالي");
            reportComboBox.Text = "إجمالي";
            label6.Visible = false;
            loanNumberComboBox.Visible = false;
            detailsDGV.Visible = false;
            fill();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

    void fill()
        {
            //supplier ComboBox
            loanNumberComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct Id from loansMainTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    loanNumberComboBox.Items.Add(dr["Id"].ToString());
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (loanNumberComboBox.Items.Count > 0)
            {
                loanNumberComboBox.Text = loanNumberComboBox.Items[0].ToString();
            }
        }

        private void reportComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(reportComboBox.Text== "مفصّل")
            {
                detailsDGV.DataSource = null;
                categoryDGV.DataSource = null;
                loanNumberComboBox.Text = "";

                label6.Visible = true;
                loanNumberComboBox.Visible = true;
                detailsDGV.Visible = true;

            }
            else
            {
                detailsDGV.DataSource = null;
                categoryDGV.DataSource = null;
                loanNumberComboBox.Text = "";

                label6.Visible = false;
                loanNumberComboBox.Visible = false;
                detailsDGV.Visible = false;

            }
        }
        private void loanNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            detailsDGV.DataSource = null;
            categoryDGV.DataSource = null;
            loanNumberComboBox.Text = "";
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            if(reportComboBox.Text == "مفصّل")
            {
                //category DGV
                categoryDGV.DataSource = null;

                string Query = "select Id as 'رقم القرض', name as 'اسم المُقرض' ,telephone as 'الهاتف',  mobile as 'الموبايل', fax as 'الفاكس',amount as 'المبلغ' , debtsNumber as 'عدد الأقساط', debtAmount as 'مبلغ القسط', concat(time,' ', timeType) as 'قسط كل' , startDate as 'تاريخ البدء',endDate as 'تاريخ الانتهاء'  from loansMainTable where Id = N'"+this.loanNumberComboBox.Text+"' ;";

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
                conDataBase.Close();

                detailsDGV.DataSource = null;

                 Query = "select debtorder as 'رقم القسط'  ,debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط',status as 'الحالة',paidAmount as 'قيمة المدفوع', paidDate as 'تاريخ الدفع' from loansTable where loanMainTableNumber =N'" + this.loanNumberComboBox.Text + "' ;";

                conDataBase = new SqlConnection(constring);
                cmdDataBase = new SqlCommand(Query, conDataBase);

                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
                    DataTable dbdataset = new DataTable();
                    sda.Fill(dbdataset);
                    BindingSource bSource = new BindingSource();

                    bSource.DataSource = dbdataset;
                    detailsDGV.DataSource = bSource;
                    sda.Update(dbdataset);

                }
                catch (Exception ex)
                {
 
                }
                conDataBase.Close();
            }
            else
            {
                //category DGV
                categoryDGV.DataSource = null;

                string Query = "select Id as 'رقم القرض', name as 'اسم المُقرض' ,telephone as 'الهاتف',  mobile as 'الموبايل', fax as 'الفاكس',amount as 'المبلغ' , debtsNumber as 'عدد الأقساط', debtAmount as 'مبلغ القسط', concat(time,' ', timeType) as 'قسط كل' , startDate as 'تاريخ البدء',endDate as 'تاريخ الانتهاء'  from loansMainTable;";

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
                conDataBase.Close();
            }
        }

      public void refreshLoacl()
        {
            label6.Visible = false;
            loanNumberComboBox.Visible = false;
            detailsDGV.Visible = false;
            fill();
        }
    }
}
