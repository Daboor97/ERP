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

namespace SofterFertilizers.calculations.potentials
{
    public partial class initialMoney : UserControl
    {
        public initialMoney()
        {
            InitializeComponent();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        private void addButton_Click(object sender, EventArgs e)
        {
           string Query = "IF NOT EXISTS (SELECT name from safeTable where name=N'رأس المال') BEGIN INSERT INTO safeTable(name,notes,money,type,date,details,billNo,paymentType,clientCode) VALUES (N'رأس المال',N'رصيد أول المدة',N'" + this.amountTransferredTextbox.Text + "' ,'initialMoney',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "','in','','','') END ELSE UPDATE safeTable SET money = N'" + this.amountTransferredTextbox.Text + "' where name =N'رأس المال' ";
            SqlConnection conDataBase = new SqlConnection(constring);
            SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
            SqlDataReader myReader;

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

            amountTransferredTextbox.Text = "0";
            MessageBox.Show("حُفظ");
        }

        public void refreshLocal()
        {
            amountTransferredTextbox.Text = "0";

        }
    }
}
