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

namespace SofterFertilizers.calculations.advance
{
    public partial class advanceDetails : UserControl
    {
        public advanceDetails()
        {
            InitializeComponent();
            fill();
            deleteButton.Visible = false;
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        string oldBill;

        void fill()
        {
            //category DGV
            categoryDGV.DataSource = null;

            string Query = "select Id as 'رقم القرض', name as 'اسم المُقرض' ,telephone as 'الهاتف',  mobile as 'الموبايل', fax as 'الفاكس',amount as 'المبلغ' , debtsNumber as 'عدد الأقساط', debtAmount as 'مبلغ القسط', concat(time,' ', timeType) as 'قسط كل' , startDate as 'تاريخ البدء',endDate as 'تاريخ الانتهاء'  from advancesMainTable;";

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
                MessageBox.Show(ex.Message);
            }


            conDataBase.Close();
        }

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
                    deleteButton.Visible = true;
                    oldBill = row.Cells[0].Value.ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string stringStatus = new SqlCommand("IF EXISTS(select 1 from advanceTable where advanceMainTableNumber = N'" + oldBill + "' and status='True') SELECT 0 ELSE SELECT 1 ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();

            stringStatus = (string.IsNullOrEmpty(stringStatus)) ? "0" : stringStatus;
            bool status;
            if (stringStatus == "0" || stringStatus == "False")
            {
                status = false;
            }
            else
            {
                status = true;
            }
            if (status)
            {

                string Query = "IF NOT EXISTS (select 1 from advanceTable where advanceMainTableNumber = N'" + oldBill + "' and status='True') BEGIN DELETE FROM advanceTable where advanceMainTableNumber = N'" + oldBill + "' END ";
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

                Query = "IF NOT EXISTS (select 1 from advanceTable where advanceMainTableNumber= N'" + oldBill + "' and status='True') BEGIN DELETE FROM advancesMainTable where Id = N'" + oldBill + "' END ";
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

                deleteButton.Visible = false;

                fill();
                MessageBox.Show("حُفظ");
            }
            else
            {
                deleteButton.Visible = false;
                MessageBox.Show("لا يمكن حذف بيانات قرض مدفوع أحد أقساطه");
            }
        }
        public void refreshLocal(){
            fill();
            deleteButton.Visible = false;
        }
    }
}
