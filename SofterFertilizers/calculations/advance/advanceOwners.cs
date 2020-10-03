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
    public partial class advanceOwners : UserControl
    {
        public advanceOwners()
        {
            InitializeComponent();
            fill();
            dayRadioButton.Checked = true;
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {
            // code textBox
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from advancesMainTable) BEGIN SELECT MAX(Id) FROM advancesMainTable END", conDataBase).ExecuteScalar().ToString();

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
        }

        void clear()
        {
            nameTextBox.Text = "";
            telephoneTextBox.Text = "";
            mobileTextBox.Text = "";
            faxTextBox.Text = "";
            restDebtTextBox.Text = "";
            debtAmountTextBox.Text = "";
            DebtsNumberTextBox.Text = "";
            debtTimeValueTextBox.Text = "";
            debtGrid.DataSource = null;
            startDebtDateDGV.Text = "";
            endDebtDateDGV.Text = "";
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            debtGrid.Rows.Clear();
            int row = 0;
            DateTime debtDate = this.startDebtDate.Value.Date;

            for (int i = 1; i <= (Convert.ToInt32(DebtsNumberTextBox.Text)); i++)
            {
                debtGrid.Rows.Add();
                row = debtGrid.Rows.Count - 1;


                debtGrid["debtNoColumn", row].Value = i.ToString();
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

        private void savedDivisionButton_Click(object sender, EventArgs e)
        {
            string Query = "INSERT INTO advancesMainTable (name,telephone,mobile,fax,amount,debtsNumber,debtAmount,time,timeType,startDate,endDate) VALUES (N'" + this.nameTextBox.Text + "',N'" + this.telephoneTextBox.Text + "',N'" + this.mobileTextBox.Text + "',N'" + this.faxTextBox.Text + "',N'" + this.restDebtTextBox.Text + "',N'" + this.DebtsNumberTextBox.Text + "',N'" + this.debtAmountTextBox.Text + "',N'" + this.debtTimeValueTextBox.Text + "',N'" + debtTime + "',N'" + this.startDebtDate.Value.ToString("MM/dd/yyyy") + "',N'" + this.endDebtDateDGV.Text + "') ;";
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


            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string debtId = new SqlCommand("SELECT Id FROM advancesMainTable WHERE name = N'" + this.nameTextBox.Text + "' AND telephone = N'" + this.telephoneTextBox.Text + "' AND mobile = N'" + this.mobileTextBox.Text + "'  AND fax = N'" + this.faxTextBox.Text + "' AND amount = N'" + this.restDebtTextBox.Text + "' AND debtsNumber = N'" + this.DebtsNumberTextBox.Text + "' AND debtAmount = N'" + this.debtAmountTextBox.Text + "'AND time = N'" + this.debtTimeValueTextBox.Text + "' AND timeType = N'" + debtTime + "'", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();


            for (int i = 0; i < debtGrid.Rows.Count; i++)
            {
                Query = "INSERT INTO advanceTable (debtOrder,debtAmount,debtDate,status,paidDate,paidAmount,advanceMainTableNumber) VALUES (N'" + this.debtGrid.Rows[i].Cells[0].Value + "',N'" + this.debtGrid.Rows[i].Cells[1].Value + "',N'" + this.debtGrid.Rows[i].Cells[2].Value + "','False',GETDATE(),'0',N'" + debtId + "') ;";
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

            fill();
            clear();
            MessageBox.Show("حفظ");
        }

        string debtTime;
        private void dayRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            debtTime = "days";
        }

        private void monthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            debtTime = "month";
        }

        public void refreshLocal()
        {
            fill();
            dayRadioButton.Checked = true;
            clear();
        }
    }
}
