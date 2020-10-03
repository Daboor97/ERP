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

namespace SofterFertilizers.store
{
    public partial class transportStore : UserControl
    {
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        public transportStore()
        {
            InitializeComponent();
            fill();
            clear();
        }

        void clear()
        {
            categoryCodeTextBox.Text = "";
            categoryNameTextBox.Text = "";
            unitComboBox.Text = "";
            quantityTextBox.Text = "0";
            notesTextBox.Text = "";
            quantityLabel.Text = "0";
        }

        void fill()
        {
            //store Combo Boxes
            toStoreComboBox.Items.Clear();
            fromStoreComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct storeName from storeTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    toStoreComboBox.Items.Add(dr["storeName"].ToString());
                    fromStoreComboBox.Items.Add(dr["storeName"].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (toStoreComboBox.Items.Count > 1)
            {
                toStoreComboBox.Text = toStoreComboBox.Items[1].ToString();
                fromStoreComboBox.Text = toStoreComboBox.Items[0].ToString();
            }





            //permission Addittion Code
            conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string maxBill = new SqlCommand("IF EXISTS (select MAX(Id) from transportMainTable) BEGIN SELECT MAX(Id) FROM transportMainTable END", conDataBase).ExecuteScalar().ToString();

            if (maxBill != "")
            {
                int maxBillint = Convert.ToInt32(maxBill);
                transportCodeTextBox.Text = Convert.ToString(maxBillint + 1);
                conDataBase.Close();
            }

            else
            {
                transportCodeTextBox.Text = "1";
                conDataBase.Close();
            }

        }

        private void fromStoreComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //store Code
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            fromStoreCodeTextBox.Text = new SqlCommand("select Id from storeTable where storeName =N'" + this.fromStoreComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
            categoryDGV.DataSource = null;

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', quantity as 'الكمية' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.fromStoreComboBox.Text + "';";

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
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();
            clear();
            transportDGV.Rows.Clear();
            transportDGV.Refresh();
        }

        private void toStoreComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            toStoreCodeTextBox.Text = new SqlCommand("select Id from storeTable where storeName =N'" + this.toStoreComboBox.Text + "';", conDataBase).ExecuteScalar().ToString();
            conDataBase.Close();
        }

        private void categoryCodeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', quantity as 'الكمية' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.fromStoreComboBox.Text + "' and categoryTable.Id like N'%" + this.categoryCodeSearchTextBox.Text + "%';";

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

        private void categoryNameSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', quantity as 'الكمية' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.fromStoreComboBox.Text + "' and categoryTable.categoryName like N'%" + this.categoryNameSearchTextBox.Text + "%';";

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

        private void categoryStoreCodeSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            categoryDGV.DataBindings.Clear();

            string Query = "select categoryTable.Id as 'كود الصنف', categoryName as 'اسم الصنف' , companyName as 'الشركة' ,mainUnit as 'الوحدة', mainType as 'النوع', storeCode as 'الكود المخزني', notes as 'ملاحظات', sellingPrice as 'السعر', packagePrice as 'سعر الجملة', halfPackagePrice as 'نص جملة', buyingPrice as 'سعر الشراء', quantity as 'الكمية' from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber and quantity > 0.0 and storeName=N'" + this.fromStoreComboBox.Text + "' and categoryTable.storeCode like N'%" + this.categoryStoreCodeSearchTextBox.Text + "%';";

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

        private void categoryDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.categoryDGV.Rows[e.RowIndex];
                    this.categoryCodeTextBox.Text = row.Cells[0].Value.ToString();
                    this.categoryNameTextBox.Text = row.Cells[1].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void categoryCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //categoryName in mainUnit
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string categoryName = new SqlCommand("IF EXISTS(Select 1 from categoryTable where Id=N'" + this.categoryCodeTextBox.Text + "' ) BEGIN Select categoryName from categoryTable where Id=N'" + this.categoryCodeTextBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                categoryNameTextBox.Text = categoryName.ToString();

                //Unit Combo Boxes
                unitComboBox.Items.Clear();
                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string Query = "select distinct mainUnit from categoryTable where Id=N'" + this.categoryCodeTextBox.Text + "';";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
                da.Fill(dt);
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["mainUnit"].ToString() != "")
                            unitComboBox.Items.Add(dr["mainUnit"].ToString());
                    }
                }
                catch
                {
                }
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                Query = "select distinct subUnit from subUnitTable where categorycode=N'" + this.categoryCodeTextBox.Text + "';";
                dt = new DataTable();
                da = new SqlDataAdapter(Query, conDataBase);
                da.Fill(dt);
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["subUnit"].ToString() != "")
                            unitComboBox.Items.Add(dr["subUnit"].ToString());
                    }
                }
                catch
                {

                }
                conDataBase.Close();


            }
            catch
            {
            }

            if (unitComboBox.Items.Count > 0)
            {
                unitComboBox.Text = unitComboBox.Items[0].ToString();
            }
        }

     

        private void unitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Quantity in mainUnit
                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                float quantity = float.Parse(new SqlCommand("Select quantity from CategoryQuantityTable where categoryNumber=N'" + this.categoryCodeTextBox.Text + "' and storeName=N'" + this.fromStoreComboBox.Text + "'", conDataBase).ExecuteScalar().ToString());
                conDataBase.Close();

                //If the mainUnit is used => use the quantity Straight Away, if the subUnit is used then multiply the quantity by the ratio in the subUnit Table 

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                string mainUnit = new SqlCommand("IF EXISTS (select 1 from categoryTable where Id = N'" + this.categoryCodeTextBox.Text + "') BEGIN select mainUnit from categoryTable where Id = N'" + this.categoryCodeTextBox.Text + "' END", conDataBase).ExecuteScalar().ToString();
                conDataBase.Close();
                if (mainUnit == unitComboBox.Text)
                {
                    quantityLabel.Text = quantity.ToString();
                }

                else
                {
                    //ratio
                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where categoryCode=N'" + this.categoryCodeTextBox.Text + "'", conDataBase).ExecuteScalar().ToString());
                    conDataBase.Close();

                    quantityLabel.Text = (ratio * quantity).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void addQuantityToStore_Click(object sender, EventArgs e)
        {
            string categoryCode = categoryCodeTextBox.Text;
            string categoryName = categoryNameTextBox.Text;
            string unit = unitComboBox.Text;
            string quantity = quantityTextBox.Text;
            string notes = notesTextBox.Text;

            string[] row = { categoryCode, categoryName, unit, quantity, notes };
            transportDGV.Rows.Add(row);
            clear();
        }

        private void saveCategoryButton_Click(object sender, EventArgs e)
        {
            string Query = "INSERT INTO transportMainTable(fromStoreName,toStoreName,notes,date) VALUES (N'" + this.fromStoreComboBox.Text + "',N'" + this.toStoreComboBox.Text + "',N'" + this.mainNotesTextBox.Text + "',N'" + this.dateDTP.Value.ToString("MM/dd/yyyy") + "')";
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {

                for (int i = 0; i < transportDGV.Rows.Count - 1; i++)
                {
                    //First See the unit if it was the mainUnit, insert it directly to the quantity Table if it wasn't divide by the ratio and then inserrt it to the quantity Table


                    conDataBase = new SqlConnection(constring);
                    conDataBase.Open();
                    string mainUnit = new SqlCommand("IF EXISTS (select mainUnit from categoryTable where Id=N'" + this.transportDGV.Rows[i].Cells[0].Value.ToString() + "') BEGIN select mainUnit from categoryTable where Id =N'" + this.transportDGV.Rows[i].Cells[0].Value.ToString() + "' END", conDataBase).ExecuteScalar().ToString();
                    float fromStore = float.Parse(new SqlCommand("select quantity from categoryQuantityTable where CategoryNumber=N'" + this.transportDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.fromStoreComboBox.Text + "'", conDataBase).ExecuteScalar().ToString());
                    float toStore = float.Parse(new SqlCommand("IF EXISTS(SELECT 1 from categoryQuantityTable where CategoryNumber=N'" + this.transportDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.toStoreComboBox.Text + "') BEGIN select quantity from categoryQuantityTable where CategoryNumber=N'" + this.transportDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.toStoreComboBox.Text + "' END ELSE BEGIN SELECT 0 END", conDataBase).ExecuteScalar().ToString());

                    float fromStoreAfter=0;
                    float toStoreAfter=0;


                    if (mainUnit == this.transportDGV.Rows[i].Cells[2].Value.ToString())
                    {
                        fromStoreAfter = fromStore - float.Parse(this.transportDGV.Rows[i].Cells[3].Value.ToString());
                        toStoreAfter = toStore + float.Parse(this.transportDGV.Rows[i].Cells[3].Value.ToString());
                    }

                    else
                    {
                        //ratio
                        conDataBase = new SqlConnection(constring);
                        conDataBase.Open();
                        float ratio = float.Parse(new SqlCommand("Select ratio from subUnitTable where categoryCode=N'" + this.transportDGV.Rows[i].Cells[0].Value.ToString() + "'", conDataBase).ExecuteScalar().ToString());
                        conDataBase.Close();

                        fromStoreAfter = ((fromStore + float.Parse(this.transportDGV.Rows[i].Cells[3].Value.ToString())) / ratio);
                        toStoreAfter = ((toStore + float.Parse(this.transportDGV.Rows[i].Cells[3].Value.ToString())) / ratio);
                    }

                    Query = "IF NOT EXISTS (SELECT 1 from transportSubTable where transportCode=N'"+this.transportCodeTextBox.Text+ "') BEGIN INSERT INTO transportSubTable(transportCode,categoryCode,unit,quantity,notes) VALUES(N'" + this.transportCodeTextBox.Text + "',N'" + this.transportDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.transportDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.transportDGV.Rows[i].Cells[3].Value.ToString() + "',N'" + this.transportDGV.Rows[i].Cells[4].Value.ToString() + "') END";

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

                    Query = "IF EXISTS (select 1 from CategoryQuantityTable where storeName=N'" + this.fromStoreComboBox.Text + "' and categoryNumber=N'" + this.transportDGV.Rows[i].Cells[0].Value.ToString() + "') BEGIN UPDATE categoryQuantityTable SET quantity = N'" + fromStoreAfter + "' WHERE storeName =N'" + this.fromStoreComboBox.Text + "' AND categoryNumber=N'" + this.transportDGV.Rows[i].Cells[0].Value.ToString() + "' END ";

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

                    Query = "IF EXISTS (select 1 from CategoryQuantityTable where storeName=N'" + this.toStoreComboBox.Text + "' and categoryNumber=N'" + this.transportDGV.Rows[i].Cells[0].Value.ToString() + "') BEGIN UPDATE categoryQuantityTable SET quantity = N'" + toStoreAfter + "' WHERE storeName =N'" + this.toStoreComboBox.Text + "' AND categoryNumber=N'" + this.transportDGV.Rows[i].Cells[0].Value.ToString() + "' END ";

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
                }

                MessageBox.Show("حفظ");
                fill();
                clear();
                transportDGV.Rows.Clear();
                transportDGV.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void refreshLocal()
        {
            fill();
            clear();
            this.ActiveControl = categoryCodeTextBox;

        }

        private void transportStore_Load(object sender, EventArgs e)
        {
            this.ActiveControl = categoryCodeTextBox;

        }
    }
}