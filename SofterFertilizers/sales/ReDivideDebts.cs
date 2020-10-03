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

namespace SofterFertilizers.sales
{
    public partial class ReDivideDebts : UserControl
    {
        public ReDivideDebts()
        {
            InitializeComponent();
            fill();
            debtGrid.Visible = false;
            debtGrid.SendToBack();
            dayRadioButton.Checked = true;
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        int order;

        void clear()
        {
            restDebtTextBox.Text = "";
            DebtsNumberTextBox.Text = "";
            debtAmountTextBox.Text = "";
            debtTimeValueTextBox.Text = "";
            
        }

        void fill()
        {
            //supplier ComboBox
            supplierNameComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct name from customerTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    supplierNameComboBox.Items.Add(dr["name"].ToString());
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (supplierNameComboBox.Items.Count > 0)
            {
                supplierNameComboBox.Text = supplierNameComboBox.Items[0].ToString();
            } 
        }

        private void supplierNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //supplier Code
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                supplierCodeTextBox.Text = new SqlCommand("select Id from customerTable where name=N'" + this.supplierNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                //supplier Balance
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                balanceTextBox.Text = new SqlCommand("select balance from customerTable where name=N'" + this.supplierNameComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                //billNumber comboBox
                billNumberComboBox.Items.Clear();
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string Query = "select distinct billNumber from debtsMainTable where billNumber in (select Id from salesMainTable where customerName=N'" + this.supplierNameComboBox.Text + "')  ;";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
                da.Fill(dt);
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        billNumberComboBox.Items.Add(dr["billNumber"].ToString());
                    }
                }
                catch (Exception ex)
                {
 
                }
                conDataBase.Close();

                if (billNumberComboBox.Items.Count > 0)
                {
                    billNumberComboBox.Text = billNumberComboBox.Items[0].ToString();
                }

            }
            catch
            {

            }
        }

        private void supplierCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //supplierName
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                supplierNameComboBox.Text = new SqlCommand("IF EXISTS(select 1 from customerTable where Id=N'" + this.supplierCodeTextBox.Text + "') BEGIN select name from customerTable where Id=N'" + this.supplierCodeTextBox.Text + "' END ;", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();

                //category DGV
                categoryDGV.DataBindings.Clear();

                string Query = "select customerBalanceTable.Id as 'رقم الإيصال', customerTable.name as 'اسم العميل' , billNumber as 'رقم الفاتورة' ,billDetails as 'التفاصيل',amount as 'المبلغ', direction as 'مدفوع', safeName as 'الخزنة', date as 'التاريخ' from customerBalanceTable,customerTable where customerTable.Id=N'" + this.supplierCodeTextBox.Text + "' and customerBalanceTable.customerNumber = customerTable.Id;";

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
                    categoryDGV.DataSource = bSource;
                    sda.Update(dbdataset);

                }
                catch (Exception ex)
                {
 

                }

                conDataBase.Close();

                //billNumber comboBox
                billNumberComboBox.Items.Clear();
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                Query = "select distinct billNumber from debtsMainTable where billNumber in (select Id from salesMainTable where customerName=N'" + this.supplierNameComboBox.Text + "')  ;";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
                da.Fill(dt);
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        billNumberComboBox.Items.Add(dr["billNumber"].ToString());
                    }
                }
                catch (Exception ex)
                {
 
                }
                conDataBase.Close();

                if (billNumberComboBox.Items.Count > 0)
                {
                    billNumberComboBox.Text = billNumberComboBox.Items[0].ToString();
                }

            }
            catch
            {

            }
        }

        private void billNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear();
            debtGrid.Rows.Clear();
            debtGrid.Refresh();
            debtGrid.SendToBack();
            debtGrid.Visible = false;
            categoryDGV.BringToFront();
            categoryDGV.Visible = true;
            categoryDGV.DataSource = null;

            string Query = "select debtorder as 'رقم القسط'  ,debtAmount as 'قيمة القسط', debtDate as 'تاريخ القسط' from debtsTable where debtMainTableNumber =(select Id from debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "') and status = 'False' and paidAmount = '0';";
             
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


            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string debtTime = new SqlCommand("IF EXISTS (select 1 FROM debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "' ) BEGIN select time FROM debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
            debtTime = (string.IsNullOrEmpty(debtTime)) ? "0" : debtTime;
            debtTimeValueTextBox.Text = debtTime;
            conDataBase.Close();

           

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string timeType = new SqlCommand("IF EXISTS (select 1 FROM debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "') BEGIN select timeType FROM debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "' END ELSE SELECT 0", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            if (timeType == "days")
            {
                dayRadioButton.Checked = true;
            }
            else
            {
                monthRadioButton.Checked = true;
            }


            try
            {
                restDebtTextBox.Text = "0";
                DebtsNumberTextBox.Text = "0";
                debtAmountTextBox.Text = categoryDGV.Rows[0].Cells[1].Value.ToString();
                startDebtDate.Text = categoryDGV.Rows[0].Cells[2].Value.ToString();
                order = Convert.ToInt32(categoryDGV.Rows[0].Cells[0].Value.ToString());

                for (int i = 0; i <= categoryDGV.Rows.Count - 1; i++)
                {
                    restDebtTextBox.Text = Convert.ToString(Convert.ToInt32(restDebtTextBox.Text) + Convert.ToInt32(categoryDGV.Rows[i].Cells[1].Value));
                    DebtsNumberTextBox.Text = Convert.ToString(Convert.ToInt32(DebtsNumberTextBox.Text) + 1);
                }

            }
            catch
            {
            }


        }

        private void DebtsNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            double c, m, g = 0;

            double.TryParse(restDebtTextBox.Text, out c);
            double.TryParse(DebtsNumberTextBox.Text, out m);

            if (m == 0)
            {
                g = c;
            }
            else if (m > 0)
            {
                g = c / m;
            }
            if (g > 0)
            {
                debtAmountTextBox.Text = Convert.ToString(Convert.ToInt32(g));
            }
        }

        private void debtAmountTextBox_TextChanged(object sender, EventArgs e)
        {

            double c, m, g = 0;

            double.TryParse(restDebtTextBox.Text, out c);
            double.TryParse(debtAmountTextBox.Text, out m);

            if (m == 0)
            {

            }
            else if (m > 0)
            {
                g = c / m;
            }
            if (g > 0)
            {
                DebtsNumberTextBox.Text = Convert.ToString(Convert.ToInt32(g));
            }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            categoryDGV.Visible = false;
            categoryDGV.SendToBack();
            debtGrid.BringToFront();
            debtGrid.Visible = true;

            debtGrid.Rows.Clear();
            int row = 0;
            DateTime debtDate = this.startDebtDate.Value.Date;
            int newOrder = order;
            for (int i = 1; i <= (Convert.ToInt32(DebtsNumberTextBox.Text)); i++)
            {
                debtGrid.Rows.Add();
                row = debtGrid.Rows.Count - 1;
               
                debtGrid["debtNoColumn", row].Value = newOrder.ToString();
                newOrder++;
                debtGrid["debtAmountColumn", row].Value = debtAmountTextBox.Text;
                debtGrid["debtDateColumn", row].Value = debtDate.ToShortDateString();

                if (dayRadioButton.Checked)
                {
                    debtDate = debtDate.AddDays(Convert.ToInt32(debtTimeValueTextBox.Text));
                }
                else if (monthRadioButton.Checked)
                {
                    debtDate = debtDate.AddMonths(Convert.ToInt32(debtTimeValueTextBox.Text));
                }
            }
            startDebtDateDGV.Text = this.startDebtDate.Value.Date.ToShortDateString();
            endDebtDateDGV.Text = debtGrid["debtDateColumn", row].Value.ToString();
        }

        private void addButton_Click(object sender, EventArgs e)
        {


            string Query = "UPDATE salesMainTable SET returned='True' where Id=N'" + this.billNumberComboBox.Text + "'";
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
                MessageBox.Show(ex.Message);
            }

            Query = "DELETE FROM debtsTable where debtMainTableNumber = (select Id FROM debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "') and status = 'False' and paidAmount = '0';";
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
                MessageBox.Show(ex.Message);
            }


            //TODO Update the debts main table debt amount and time and timeType then Insert into the debt table the new divides
            string timeType;
            if (dayRadioButton.Checked)
                timeType = "days";
            else
                timeType = "month";

             Query = "UPDATE debtsMainTable SET debtsNumber =N'"+this.DebtsNumberTextBox.Text + "', debtAmount=N'" + this.debtAmountTextBox.Text + "', time=N'" + this.debtTimeValueTextBox.Text + "' , timeType= N'"+timeType+"' where billNumber=N'" + this.billNumberComboBox.Text + "'";
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
                MessageBox.Show(ex.Message);
            }

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string debtMainTableId = new SqlCommand("select Id FROM debtsMainTable where billNumber = N'" + this.billNumberComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            for (int i = 0; i < debtGrid.Rows.Count; i++)
            {
                Query = "INSERT INTO debtsTable (debtOrder,debtAmount,debtDate,status,paidDate,paidAmount,debtMainTableNumber) VALUES (N'" + this.debtGrid.Rows[i].Cells[0].Value + "',N'" + this.debtGrid.Rows[i].Cells[1].Value + "',N'" + this.debtGrid.Rows[i].Cells[2].Value + "','False',GETDATE(),'0',N'"+debtMainTableId+"');";
                conDataBase = new SqlConnection(constring);
                cmdDataBase = new SqlCommand(Query, conDataBase);

                try
                {
                    conDataBase.Open();
                    cmdDataBase.ExecuteReader();
                    conDataBase.Close();

                }
                catch (Exception ex)
                {
 
                }

            }

            MessageBox.Show("حفظ");
            clear();
            debtGrid.Rows.Clear();
            debtGrid.Refresh();
            debtGrid.SendToBack();
            debtGrid.Visible = false;
            categoryDGV.BringToFront();
            categoryDGV.Visible = true;
            categoryDGV.DataSource = null;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            fill();
            debtGrid.Visible = false;
            debtGrid.SendToBack();
            dayRadioButton.Checked = true;
            clear();
        }

        public void refreshLocal()
        {
            fill();
            debtGrid.Visible = false;
            debtGrid.SendToBack();
            dayRadioButton.Checked = true;
            clear();
        }
    }
}
