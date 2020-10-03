using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Configuration;

namespace SofterFertilizers
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.6
        /// </summary>
        [STAThread]

        static void Main()
        {
          /*  string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string code = new SqlCommand("Select securityCode from patchCodeTable", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            if (settings.FingerPrint.Value() == code)
            {*/
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new mainForm("admin"));
            /*}
            else
            {
                MessageBox.Show("كلمني على 01120951496");
            }
        */
        }
    }
}
