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
using ExcelLibrary.BinaryDrawingFormat;
using ExcelLibrary.SpreadSheet;
using ExcelLibrary.CompoundDocumentFormat;


namespace SofterFertilizers.sales
{
    public partial class exportCustomers : UserControl
    {
        public exportCustomers()
        {
            InitializeComponent();
            fillCategoryDGV();
        }

        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fillCategoryDGV()
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select id as 'كود العميل', name as 'اسم العميل' , telephone as 'الشركة' ,mobile as 'الموبايل', fax as 'فاكس', notes as 'الملاحظات', governorate as 'المحافظة', center as 'المركز', address as 'عنوان العميل', balance as 'الرصيد', active as 'عميل نشط' from customerTable;";

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

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = "unknown.xls";
            // set filters - this can be done in properties as well
            savefile.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                string path = savefile.FileName;
                string Query = "select id as 'كود العميل', name as 'اسم العميل' , telephone as 'الشركة' ,mobile as 'الموبايل', fax as 'فاكس', notes as 'الملاحظات', governorate as 'المحافظة', center as 'المركز', address as 'عنوان العميل', balance as 'الرصيد' from customerTable;";

                SqlConnection conDataBase = new SqlConnection(constring);
                SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
                    DataTable dbdataset = new DataTable();
                    sda.Fill(dbdataset);
                    BindingSource bSource = new BindingSource();


                    DataSet ds = new DataSet();
                    sda.Fill(dbdataset);
                    ds.Tables.Add(dbdataset);
                    ExcelLibrary.DataSetHelper.CreateWorkbook(path, ds);

                }
                catch (Exception ex)
                {
 
                }
            }
        }
    }
}
