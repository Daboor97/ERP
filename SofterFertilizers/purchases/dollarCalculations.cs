using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SofterFertilizers.purchases
{
    public partial class dollarCalculations : UserControl
    {
        public dollarCalculations()
        {
            InitializeComponent();
            expenseAmountTextBox.Text = "0";
            sumExpensesTextBox.Text = "0";
            cargoPriceInDollarTextBox.Text = "0";
            dollarDifferenceTextBox.Text = "0";
            dollarCurrentPriceTextBox.Text = "0";
            finalDollarPriceTextBox.Text = "0";
        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            int row = 0;
            expensesGridView.Rows.Add();
            row = expensesGridView.Rows.Count - 2;
            expensesGridView["expensesNameColumn", row].Value = expenseNameTextBox.Text;
            expensesGridView["expensesAmountColumn", row].Value = expenseAmountTextBox.Text;
        }

        private void expensesGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            sumExpensesTextBox.Text = "0";
            for (int i = 0; i <= expensesGridView.Rows.Count - 1; i++)
            {
                try
                {
                    sumExpensesTextBox.Text = Convert.ToString(Convert.ToInt32(sumExpensesTextBox.Text) + Convert.ToInt32(expensesGridView.Rows[i].Cells[1].Value));
                }
                catch { }
            }

        }

        private void expensesGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            sumExpensesTextBox.Text = "0";
            for (int i = 0; i <= expensesGridView.Rows.Count - 1; i++)
            {
                try
                {
                    sumExpensesTextBox.Text = Convert.ToString(Convert.ToInt32(sumExpensesTextBox.Text) + Convert.ToInt32(expensesGridView.Rows[i].Cells[1].Value));
                }
                catch { }
            }
        }

        private void expensesGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            sumExpensesTextBox.Text = "0";
            for (int i = 0; i <= expensesGridView.Rows.Count - 1; i++)
            {
                try
                {
                    sumExpensesTextBox.Text = Convert.ToString(Convert.ToInt32(sumExpensesTextBox.Text) + Convert.ToInt32(expensesGridView.Rows[i].Cells[1].Value));
                }
                catch { }
            }
        }

        private void sumFunction()
        {
            // sumPartTextBox.ResetText();
            double a, b, c, d, e;

            double.TryParse(sumExpensesTextBox.Text, out a);
            double.TryParse(cargoPriceInDollarTextBox.Text, out b);
            double.TryParse(dollarCurrentPriceTextBox.Text, out c);

            d = a / b;
            e = c + d;

            if (d > 0)
            {
                dollarDifferenceTextBox.Text = string.Format("{0:N2}", d);
            }
            if (e > 0)
            {
                finalDollarPriceTextBox.Text = string.Format("{0:N2}", e);
            }
        }

        private void sumExpensesTextBox_TextChanged(object sender, EventArgs e)
        {

            if (cargoPriceInDollarTextBox.Text != "0")
            {
                sumFunction();
            }
        }

        private void cargoPriceInDollarTextBox_TextChanged(object sender, EventArgs e)
        {
            sumFunction();
        }

        private void dollarDifferenceTextBox_TextChanged(object sender, EventArgs e)
        {
            sumFunction();
        }

        private void dollarCurrentPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            sumFunction();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            expenseAmountTextBox.Text = "0";
            sumExpensesTextBox.Text = "0";
            cargoPriceInDollarTextBox.Text = "0";
            dollarDifferenceTextBox.Text = "0";
            dollarCurrentPriceTextBox.Text = "0";
            finalDollarPriceTextBox.Text = "0";
        }

        public void refreshLocal()
        {
            expenseAmountTextBox.Text = "0";
            sumExpensesTextBox.Text = "0";
            cargoPriceInDollarTextBox.Text = "0";
            dollarDifferenceTextBox.Text = "0";
            dollarCurrentPriceTextBox.Text = "0";
            finalDollarPriceTextBox.Text = "0";
        }
    }
}
