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

namespace SofterFertilizers.Reports.safeReports
{
    public partial class cashBalance : UserControl
    {
        public cashBalance()
        {
            InitializeComponent();
        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string startIn = new SqlCommand("Select SUM(money) from safeTable where details ='in' and date between '01/01/2000' AND '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            startIn = (string.IsNullOrEmpty(startIn)) ? "0" : startIn;

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string startOut = new SqlCommand("Select SUM(money) from safeTable where details ='out' and date between '01/01/2000' AND '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            startOut = (string.IsNullOrEmpty(startOut)) ? "0" : startOut;

            startBalanceTextBox.Text = (Convert.ToDouble(startIn) - Convert.ToDouble(startOut)).ToString();

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string endIn = new SqlCommand("Select SUM(money) from safeTable where details ='in' and date between '01/01/2000' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            endIn = (string.IsNullOrEmpty(endIn)) ? "0" : endIn;

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string endOut = new SqlCommand("Select SUM(money) from safeTable where details ='out' and date between '01/01/2000' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            endOut = (string.IsNullOrEmpty(endOut)) ? "0" : endOut;

            endBalanceTextBox.Text = (Convert.ToDouble(endIn) - Convert.ToDouble(endOut)).ToString();


            //sales
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string salesSum = new SqlCommand("Select SUM(money) from safeTable where type='sales' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            salesSum = (string.IsNullOrEmpty(salesSum)) ? "0" : salesSum;

            salesTextBox.Text = salesSum.ToString();


            //purchases
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string purchasesSum = new SqlCommand("Select SUM(money) from safeTable where type='purchases' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            purchasesSum = (string.IsNullOrEmpty(purchasesSum)) ? "0" : purchasesSum;

            purchasesTextBox.Text = purchasesSum.ToString();


            //anotherIncome
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string anotherIncomeSum = new SqlCommand("Select SUM(money) from safeTable where type='anothereIncome' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            anotherIncomeSum = (string.IsNullOrEmpty(anotherIncomeSum)) ? "0" : anotherIncomeSum;

            anotherIncomeTextBox.Text = anotherIncomeSum.ToString();


            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string generalSpendingsSum = new SqlCommand("Select SUM(money) from safeTable where type='generalSpendings' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            generalSpendingsSum = (string.IsNullOrEmpty(generalSpendingsSum)) ? "0" : generalSpendingsSum;

            generalSpendingsTextBox.Text = generalSpendingsSum.ToString();


            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string salesDivisionSum = new SqlCommand("Select SUM(money) from safeTable where type='divisionPay' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            salesDivisionSum = (string.IsNullOrEmpty(salesDivisionSum)) ? "0" : salesDivisionSum;

            salesDivisionTextBox.Text = salesDivisionSum.ToString();

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string wagesSum = new SqlCommand("Select SUM(money) from safeTable where type='wages' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            wagesSum = (string.IsNullOrEmpty(wagesSum)) ? "0" : wagesSum;

            wagesTextBox.Text = wagesSum.ToString();

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string advanceSum = new SqlCommand("Select SUM(money) from safeTable where type='advancePay' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            advanceSum = (string.IsNullOrEmpty(advanceSum)) ? "0" : advanceSum;

            advanceTextBox.Text = advanceSum.ToString();

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string loanSum = new SqlCommand("Select SUM(money) from safeTable where type='loanPay' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            loanSum = (string.IsNullOrEmpty(loanSum)) ? "0" : loanSum;

            loanTextBox.Text = loanSum.ToString();

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string anotherIn = new SqlCommand("Select SUM(money) from safeTable where type!='sales' and type!='purchases' and type!='loanPay' and type!='advancePay' and type!='wages' and type!='divisionPay' and type!='generalSpendings' and type!='anothereIncome'  and details ='in' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            anotherIn = (string.IsNullOrEmpty(anotherIn)) ? "0" : anotherIn;
            inOtherTextBox.Text = anotherIn.ToString();

            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string anotherOut = new SqlCommand("Select SUM(money) from safeTable where type!='sales' and type!='purchases' and type!='loanPay' and type!='advancePay' and type!='wages' and type!='divisionPay' and type!='generalSpendings' and type!='anothereIncome'  and details ='out' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "'  ", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            anotherOut = (string.IsNullOrEmpty(anotherOut)) ? "0" : anotherOut;

            outOtherTextBox.Text = anotherOut.ToString();

            inSumTextBox.Text = (Convert.ToDouble(salesTextBox.Text)+ Convert.ToDouble(anotherIncomeTextBox.Text)+ Convert.ToDouble(salesDivisionTextBox.Text)+ Convert.ToDouble(advanceTextBox.Text)+Convert.ToDouble(inOtherTextBox.Text)).ToString();

            outSumTextBox.Text = (Convert.ToDouble(purchasesTextBox.Text) + Convert.ToDouble(generalSpendingsTextBox.Text) + Convert.ToDouble(wagesTextBox.Text) + Convert.ToDouble(loanTextBox.Text) + Convert.ToDouble(outOtherTextBox.Text)).ToString();

        }
    }
}
