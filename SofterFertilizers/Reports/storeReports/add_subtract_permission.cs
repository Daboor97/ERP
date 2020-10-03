using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class add_subtract_permission : Form
    {
        int billNumber;
        int billType;

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        public add_subtract_permission(int billT,int billN)
        {
            InitializeComponent();
            billNumber = billN;
            billType = billT;
            fill();
        }


        void fill()
        {
            if(billType == 1)
            {

                string Query = "select Id as 'رقم الإذن', storeName as 'المخزن' , sum as 'المحموع' , status as 'الحالة' ,date as 'التاريخ' from permissionSubtractionMainTable where Id = N'"+this.billNumber+"' ;";

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
                    mainDetailsDGV.DataSource = bSource;
                    sda.Update(dbdataset);
                }
                catch (Exception ex)
                {
 
                }

                 Query = "select categoryName as 'اسم الصنف' , categoryCode as 'كود الصنف' , unit as 'الوحدة' ,price as 'السعر' , quantity as 'الكمية' , categorySum as 'المجموع'  from permissionSubtractionSubTable where permissionSubtractionCode = N'" + this.billNumber + "' ;";

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
                    contentDGV.DataSource = bSource;
                    sda.Update(dbdataset);
                }
                catch (Exception ex)
                {
 
                }


            }


            else
            {
                string Query = "select Id as 'رقم الإذن', storeName as 'المخزن' , sum as 'المحموع' , status as 'الحالة' ,date as 'التاريخ' from permissionAdditionMainTable where Id = N'" + this.billNumber + "' ;";

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
                    mainDetailsDGV.DataSource = bSource;
                    sda.Update(dbdataset);
                }
                catch (Exception ex)
                {
 
                }

                Query = "select categoryName as 'اسم الصنف' , categoryCode as 'كود الصنف' , unit as 'الوحدة' ,price as 'السعر' , quantity as 'الكمية' , categorySum as 'المجموع'  from permissionAdditionSubTable where permissionAdditionCode = N'" + this.billNumber + "' ;";

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
                    contentDGV.DataSource = bSource;
                    sda.Update(dbdataset);
                }
                catch (Exception ex)
                {
 
                }
            }
        }
    }
}
