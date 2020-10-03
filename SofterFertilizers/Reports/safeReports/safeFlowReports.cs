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
    public partial class safeFlowReports : UserControl
    {
        public safeFlowReports()
        {
            InitializeComponent();
            fill();
        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;

        void fill()
        {
            //safeComboBox
            safeComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select distinct name from safeMainTable;";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    safeComboBox.Items.Add(dr["name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (safeComboBox.Items.Count > 0)
            {
                safeComboBox.Text = safeComboBox.Items[0].ToString();
            }
        }

        private void safeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            outDGV.DataSource = null;
            outDGV.Refresh();
            inDGV.DataSource = null;
            inDGV.Refresh();
        }

        private void showFlowButton_Click(object sender, EventArgs e)
        {
            outDGV.Rows.Clear();
            outDGV.Refresh();
            inDGV.Rows.Clear();
            inDGV.Refresh();

            initialSafeDGV.DataSource = null;
            initialSafeDGV.Refresh();
            string Query = "select money ,safeTable.notes,date  from safeTable where name= N'" + this.safeComboBox.Text + "'  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and safeTable.notes=N'رصيد أول المدة' and type='initial safe' and details='in'; ";

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
                initialSafeDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

          

            advancePayDGV.DataSource = null;
            advancePayDGV.Refresh();
            Query = "select money ,safeTable.notes,date,billNo  from safeTable where name= N'" + this.safeComboBox.Text + "'  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and type='advancePay' and details='in'; ";

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
                advancePayDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

           

            anotherIncomeDGV.DataSource = null;
            anotherIncomeDGV.Refresh();
            Query = "select money ,safeTable.notes,date,billNo  from safeTable where name= N'" + this.safeComboBox.Text + "'  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and type='anothereIncome' and details='in'; ";

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
                anotherIncomeDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

      

            generalSpendngsDGV.DataSource = null;
            generalSpendngsDGV.Refresh();
            Query = "select money ,safeTable.notes,date,billNo  from safeTable where name= N'" + this.safeComboBox.Text + "'  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and type='generalSpendings' and details='out'; ";

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
                generalSpendngsDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

       


            loanPayDGV.DataSource = null;
            loanPayDGV.Refresh();
            Query = "select money ,safeTable.notes,date,billNo  from safeTable where name= N'" + this.safeComboBox.Text + "'  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and type='loanPay' and details='out'; ";

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
                loanPayDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

        

            partnerBalanceDGV.DataSource = null;
            partnerBalanceDGV.Refresh();
            Query = "select money ,safeTable.notes,date,billNo,details  from safeTable where name= N'" + this.safeComboBox.Text + "' and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and type='partnerBalance'; ";

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
                partnerBalanceDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

    

            initialDGV.DataSource = null;
            initialDGV.Refresh();
            Query = "select money ,safeTable.notes,date  from safeTable where name= N'" + this.safeComboBox.Text + "'  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and safeTable.notes=N'رصيد أول المدة' and type='initial' and details='in'; ";

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
                initialDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

       
            

            wagesDGV.DataSource = null;
            wagesDGV.Refresh();
            Query = "select money ,safeTable.notes,date,billNo,employeesTable.name from safeTable,employeesTable where safeTable.name= N'" + this.safeComboBox.Text + "' and employeesTable.Id = safeTable.clientcode and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and type='wages' and details='out'; ";

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
                wagesDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

         

            purchasesDGV.DataSource = null;
            purchasesDGV.Refresh();
            Query = "select money ,safeTable.notes,date,billNo,supplierTable.name from safeTable,supplierTable where safeTable.name= N'" + this.safeComboBox.Text + "' and  safeTable.clientcode = supplierTable.Id  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and type='purchases' and details='out'; ";
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
                purchasesDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

           

            supplierBalanceDGV.DataSource = null;
            supplierBalanceDGV.Refresh();
            Query = "select money ,safeTable.notes,date,billNo,supplierTable.name,details from safeTable,supplierTable where safeTable.name= N'" + this.safeComboBox.Text + "'  and   safeTable.clientcode =supplierTable.Id and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and type='supplierBalance' ; ";

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
                supplierBalanceDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();

        

            customerBalanceDGV.DataSource = null;
            customerBalanceDGV.Refresh();
            Query = "select money ,safeTable.notes,date,billNo,supplierTable.name,details from safeTable,supplierTable where safeTable.name= N'" + this.safeComboBox.Text + "'  and  safeTable.clientcode = supplierTable.Id  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and type='customerBalance' ; ";

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
                customerBalanceDGV.DataSource = bSource;
                sda.Update(dbdataset);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();


            divisionPayDGV.DataSource = null;
            divisionPayDGV.Refresh();
            Query = "select money ,safeTable.notes,date,billNo,customerTable.name from safeTable,customerTable where safeTable.name= N'" + this.safeComboBox.Text + "'  and  safeTable.clientcode = customerTable.Id  and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and type='divisionPay'and details='in' ; ";

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
                divisionPayDGV.DataSource = bSource;
                sda.Update(dbdataset);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conDataBase.Close();


            salesDGV.DataSource = null;
            salesDGV.Refresh();
            Query = "select money ,safeTable.notes,date,billNo,customerTable.name  from safeTable,customerTable where safeTable.name= N'" + this.safeComboBox.Text + "' and  customerTable.Id = safeTable.clientcode and date between '" + this.fromDate.Value.ToString("MM/dd/yyyy") + "' AND '" + this.toDate.Value.ToString("MM/dd/yyyy") + "' and type='sales' and details='in' ; ";

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
                salesDGV.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();


            if (salesDGV.Rows.Count > 0)
            {
                inDGV.Rows.Add();
                int row = inDGV.Rows.Count;
                inDGV.Rows.Add("", "", "المبيعات");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;

                for (int i = 0; i < salesDGV.Rows.Count; i++)
                {
                    try
                    {
                        inDGV.Rows.Add(salesDGV.Rows[i].Cells[0].Value.ToString(), salesDGV.Rows[i].Cells[1].Value.ToString(), salesDGV.Rows[i].Cells[3].Value.ToString(), salesDGV.Rows[i].Cells[4].Value.ToString(), salesDGV.Rows[i].Cells[2].Value.ToString());
                        sum += Convert.ToDouble(salesDGV.Rows[i].Cells[0].Value.ToString());
                    }

                    catch { }
                }
                row = inDGV.Rows.Count;
                inDGV.Rows.Add(sum.ToString(), "", "المجموع");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                inDGV.Rows.Add();
            }

            if (purchasesDGV.Rows.Count > 0)
            {
                outDGV.Rows.Add();
                int row = outDGV.Rows.Count;
                outDGV.Rows.Add("", "", "المشتريات");
                outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;
                for (int i = 0; i < purchasesDGV.Rows.Count; i++)
                {
                    try
                    {
                        outDGV.Rows.Add(purchasesDGV.Rows[i].Cells[0].Value.ToString(), purchasesDGV.Rows[i].Cells[1].Value.ToString(), purchasesDGV.Rows[i].Cells[3].Value.ToString(), purchasesDGV.Rows[i].Cells[4].Value.ToString(), purchasesDGV.Rows[i].Cells[2].Value.ToString());
                        sum += Convert.ToDouble(purchasesDGV.Rows[i].Cells[0].Value.ToString());
                    }

                    catch { }
                }
                row = outDGV.Rows.Count;
                outDGV.Rows.Add(sum.ToString(), "", "المجموع");
                outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                outDGV.Rows.Add();
            }

            if (divisionPayDGV.Rows.Count > 0)
            {
                inDGV.Rows.Add();
                int row = inDGV.Rows.Count;
                inDGV.Rows.Add("", "", "تسديد الأقساط");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;
                for (int i = 0; i < divisionPayDGV.Rows.Count; i++)
                {
                    try
                    {
                        inDGV.Rows.Add(divisionPayDGV.Rows[i].Cells[0].Value.ToString(), divisionPayDGV.Rows[i].Cells[1].Value.ToString(), divisionPayDGV.Rows[i].Cells[3].Value.ToString(), divisionPayDGV.Rows[i].Cells[4].Value.ToString(), divisionPayDGV.Rows[i].Cells[2].Value.ToString());
                        sum += Convert.ToDouble(divisionPayDGV.Rows[i].Cells[0].Value.ToString());
                    }

                    catch { }
                }
                row = inDGV.Rows.Count;
                inDGV.Rows.Add(sum.ToString(), "", "المجموع");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                inDGV.Rows.Add();
            }

            if (loanPayDGV.Rows.Count > 0)
            {
                outDGV.Rows.Add();
                int row = outDGV.Rows.Count;
                outDGV.Rows.Add("", "", "تسديدات القروض");
                outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;
                for (int i = 0; i < loanPayDGV.Rows.Count; i++)
                {
                    try
                    {
                        outDGV.Rows.Add(loanPayDGV.Rows[i].Cells[0].Value.ToString(), loanPayDGV.Rows[i].Cells[1].Value.ToString(), loanPayDGV.Rows[i].Cells[3].Value.ToString(), "", loanPayDGV.Rows[i].Cells[2].Value.ToString());
                        sum += Convert.ToDouble(loanPayDGV.Rows[i].Cells[0].Value.ToString());
                    }

                    catch { }
                }
                row = outDGV.Rows.Count;
                outDGV.Rows.Add(sum.ToString(), "", "المجموع");
                outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                outDGV.Rows.Add();

            }


            if (customerBalanceDGV.Rows.Count > 0)
            {
                bool inside = false;
                bool outside = false;
                double sumInside = 0;
                double sumOutside = 0;

                for (int i = 0; i < customerBalanceDGV.Rows.Count; i++)
                {
                    if (customerBalanceDGV.Rows[i].Cells[5].Value.ToString() == "in")
                    {
                        if (!inside)
                        {
                            inside = true;
                            inDGV.Rows.Add();
                            int row = inDGV.Rows.Count;
                            inDGV.Rows.Add("", "", "حسابات العملاء");
                            inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                            inDGV.Rows.Add();
                        }
                        try
                        {
                            inDGV.Rows.Add(customerBalanceDGV.Rows[i].Cells[0].Value.ToString(), customerBalanceDGV.Rows[i].Cells[1].Value.ToString(), customerBalanceDGV.Rows[i].Cells[3].Value.ToString(), customerBalanceDGV.Rows[i].Cells[4].Value.ToString(), customerBalanceDGV.Rows[i].Cells[2].Value.ToString());
                            sumInside += Convert.ToDouble(customerBalanceDGV.Rows[i].Cells[0].Value.ToString());
                        }
                        catch { }
                    }
                    else
                    {
                        if (!outside)
                        {
                            outside = true;
                            outDGV.Rows.Add();
                            int row = outDGV.Rows.Count;
                            outDGV.Rows.Add("", "", "حسابات العملاء");
                            outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                            outDGV.Rows.Add();
                        }
                        try
                        {
                            outDGV.Rows.Add(customerBalanceDGV.Rows[i].Cells[0].Value.ToString(), customerBalanceDGV.Rows[i].Cells[1].Value.ToString(), customerBalanceDGV.Rows[i].Cells[3].Value.ToString(), customerBalanceDGV.Rows[i].Cells[4].Value.ToString(), customerBalanceDGV.Rows[i].Cells[2].Value.ToString());
                            sumOutside += Convert.ToDouble(customerBalanceDGV.Rows[i].Cells[0].Value.ToString());
                        }
                        catch { }
                    }
                }
                if (inside)
                {
                    int row = inDGV.Rows.Count;
                    inDGV.Rows.Add(sumInside.ToString(), "", "المجموع");
                    inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                    inDGV.Rows.Add();
                }
                if (outside)
                {
                    int row = outDGV.Rows.Count;
                    outDGV.Rows.Add(sumOutside.ToString(), "", "المجموع");
                    outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                    outDGV.Rows.Add();

                }
            }

            if (supplierBalanceDGV.Rows.Count > 0)
            {
                bool inside = false;
                bool outside = false;
                double sumInside = 0;
                double sumOutside = 0;


                for (int i = 0; i < supplierBalanceDGV.Rows.Count; i++)
                {
                    if (supplierBalanceDGV.Rows[i].Cells[5].Value.ToString() == "in")
                    {
                        if (!inside)
                        {
                            inside = true;
                            inDGV.Rows.Add();
                            int row = inDGV.Rows.Count;
                            inDGV.Rows.Add("", "", "حسابات المورّدين");
                            inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                            inDGV.Rows.Add();
                        }
                        try
                        {

                            inDGV.Rows.Add(supplierBalanceDGV.Rows[i].Cells[0].Value.ToString(), supplierBalanceDGV.Rows[i].Cells[1].Value.ToString(), supplierBalanceDGV.Rows[i].Cells[3].Value.ToString(), supplierBalanceDGV.Rows[i].Cells[4].Value.ToString(), supplierBalanceDGV.Rows[i].Cells[2].Value.ToString());
                            sumInside += Convert.ToDouble(supplierBalanceDGV.Rows[i].Cells[0].Value.ToString());

                        }
                        catch { }
                    }
                    else
                    {
                        if (!outside)
                        {
                            outside = true;
                            outDGV.Rows.Add();
                            int row = outDGV.Rows.Count;
                            outDGV.Rows.Add("", "", "حسابات المورّدين");
                            outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                            outDGV.Rows.Add();
                        }
                        try
                        {
                            outDGV.Rows.Add(supplierBalanceDGV.Rows[i].Cells[0].Value.ToString(), supplierBalanceDGV.Rows[i].Cells[1].Value.ToString(), supplierBalanceDGV.Rows[i].Cells[3].Value.ToString(), "", supplierBalanceDGV.Rows[i].Cells[2].Value.ToString());
                            sumOutside += Convert.ToDouble(supplierBalanceDGV.Rows[i].Cells[0].Value.ToString());

                        }
                        catch { }
                    }
                }
                if (inside)
                {
                    int row = inDGV.Rows.Count;
                    inDGV.Rows.Add(sumInside.ToString(), "", "المجموع");
                    inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                    inDGV.Rows.Add();
                }
                if (outside)
                {
                    int row = outDGV.Rows.Count;
                    outDGV.Rows.Add(sumOutside.ToString(), "", "المجموع");
                    outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                    outDGV.Rows.Add();

                }
            }

            if (wagesDGV.Rows.Count > 0)
            {
                outDGV.Rows.Add();
                int row = outDGV.Rows.Count;
                outDGV.Rows.Add("", "", "المرتبات");
                outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;

                for (int i = 0; i < wagesDGV.Rows.Count; i++)
                {
                    try
                    {
                        outDGV.Rows.Add(wagesDGV.Rows[i].Cells[0].Value.ToString(), wagesDGV.Rows[i].Cells[1].Value.ToString(), wagesDGV.Rows[i].Cells[3].Value.ToString(), wagesDGV.Rows[i].Cells[4].Value.ToString(), wagesDGV.Rows[i].Cells[2].Value.ToString());
                        sum += Convert.ToDouble(wagesDGV.Rows[i].Cells[0].Value.ToString());
                    }

                    catch { }
                }
                row = outDGV.Rows.Count;
                outDGV.Rows.Add(sum.ToString(), "", "المجموع");
                outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                outDGV.Rows.Add();
            }

            if (initialSafeDGV.Rows.Count > 0)
            {
                inDGV.Rows.Add();
                int row = inDGV.Rows.Count;
                inDGV.Rows.Add("", "", "رصيد أول المدة");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;
                for (int i = 0; i < initialSafeDGV.Rows.Count; i++)
                {
                    try
                    {
                        inDGV.Rows.Add(initialSafeDGV.Rows[i].Cells[0].Value.ToString(), initialSafeDGV.Rows[i].Cells[1].Value.ToString(), "", "", initialSafeDGV.Rows[i].Cells[2].Value.ToString());
                        sum += Convert.ToDouble(initialSafeDGV.Rows[i].Cells[0].Value.ToString());
                    }
                    catch { }
                }
                row = inDGV.Rows.Count;
                inDGV.Rows.Add(sum.ToString(), "", "المجموع");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                inDGV.Rows.Add();

            }

            if (advancePayDGV.Rows.Count > 0)
            {
                inDGV.Rows.Add();
                int row = inDGV.Rows.Count;
                inDGV.Rows.Add("", "", "مدفوعات السُلف");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;

                for (int i = 0; i < advancePayDGV.Rows.Count; i++)
                {
                    try
                    {
                        inDGV.Rows.Add(advancePayDGV.Rows[i].Cells[0].Value.ToString(), advancePayDGV.Rows[i].Cells[1].Value.ToString(), advancePayDGV.Rows[i].Cells[3].Value.ToString(), "", advancePayDGV.Rows[i].Cells[2].Value.ToString());
                        sum += Convert.ToDouble(advancePayDGV.Rows[i].Cells[0].Value.ToString());
                    }
                    catch { }
                }
                row = inDGV.Rows.Count;
                inDGV.Rows.Add(sum.ToString(), "", "المجموع");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                inDGV.Rows.Add();

            }

            if (anotherIncomeDGV.Rows.Count > 0)
            {
                inDGV.Rows.Add();
                int row = inDGV.Rows.Count;
                inDGV.Rows.Add("", "", "مصادر دخل أُخرى");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;

                for (int i = 0; i < anotherIncomeDGV.Rows.Count; i++)
                {
                    try
                    {
                        inDGV.Rows.Add(anotherIncomeDGV.Rows[i].Cells[0].Value.ToString(), anotherIncomeDGV.Rows[i].Cells[1].Value.ToString(), anotherIncomeDGV.Rows[i].Cells[3].Value.ToString(), "", anotherIncomeDGV.Rows[i].Cells[2].Value.ToString());
                        sum += Convert.ToDouble(anotherIncomeDGV.Rows[i].Cells[0].Value.ToString());
                    }
                    catch { }
                }
                row = inDGV.Rows.Count;
                inDGV.Rows.Add(sum.ToString(), "", "المجموع");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                inDGV.Rows.Add();

            }

            if (generalSpendngsDGV.Rows.Count > 0)
            {
                outDGV.Rows.Add();
                int row = outDGV.Rows.Count;
                outDGV.Rows.Add("", "", "مصروفات عامة");
                outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;
                for (int i = 0; i < generalSpendngsDGV.Rows.Count; i++)
                {
                    try
                    {
                        outDGV.Rows.Add(generalSpendngsDGV.Rows[i].Cells[0].Value.ToString(), generalSpendngsDGV.Rows[i].Cells[1].Value.ToString(), generalSpendngsDGV.Rows[i].Cells[3].Value.ToString(), "", generalSpendngsDGV.Rows[i].Cells[2].Value.ToString());
                        sum += Convert.ToDouble(generalSpendngsDGV.Rows[i].Cells[0].Value.ToString());
                    }
                    catch { }
                }
                row = outDGV.Rows.Count;
                outDGV.Rows.Add(sum.ToString(), "", "المجموع");
                outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                outDGV.Rows.Add();

            }

            if (partnerBalanceDGV.Rows.Count > 0)
            {
                bool inside = false;
                bool outside = false;
                double sumInside = 0;
                double sumOutside = 0;


                for (int i = 0; i < partnerBalanceDGV.Rows.Count; i++)
                {
                    if (partnerBalanceDGV.Rows[i].Cells[5].Value.ToString() == "in")
                    {
                        if (!inside)
                        {
                            inside = true;
                            inDGV.Rows.Add();
                            int row = inDGV.Rows.Count;
                            inDGV.Rows.Add("", "", "حسابات الشركاء");
                            inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                        }
                        try
                        {
                            inDGV.Rows.Add(partnerBalanceDGV.Rows[i].Cells[0].Value.ToString(), partnerBalanceDGV.Rows[i].Cells[1].Value.ToString(), partnerBalanceDGV.Rows[i].Cells[3].Value.ToString(), "", partnerBalanceDGV.Rows[i].Cells[2].Value.ToString());
                            sumInside += Convert.ToDouble(initialSafeDGV.Rows[i].Cells[0].Value.ToString());
                        }

                        catch { }


                    }
                    else
                    {
                        if (!outside)
                        {
                            outside = true;
                            outDGV.Rows.Add();
                            int row = outDGV.Rows.Count;
                            outDGV.Rows.Add("", "", "حسابات الشركاء");
                            outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                        }
                        try
                        {

                            outDGV.Rows.Add(partnerBalanceDGV.Rows[i].Cells[0].Value.ToString(), partnerBalanceDGV.Rows[i].Cells[1].Value.ToString(), partnerBalanceDGV.Rows[i].Cells[3].Value.ToString(), "", partnerBalanceDGV.Rows[i].Cells[2].Value.ToString());
                            sumOutside += Convert.ToDouble(partnerBalanceDGV.Rows[i].Cells[0].Value.ToString());
                        }

                        catch { }
                    }
                }
                if (inside)
                {
                    int row = inDGV.Rows.Count;
                    inDGV.Rows.Add(sumInside.ToString(), "", "المجموع");
                    inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                    inDGV.Rows.Add();
                }
                if (outside)
                {
                    int row = outDGV.Rows.Count;
                    outDGV.Rows.Add(sumOutside.ToString(), "", "المجموع");
                    outDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                    outDGV.Rows.Add();

                }
            }

            if (initialDGV.Rows.Count > 0)
            {
                inDGV.Rows.Add();
                int row = inDGV.Rows.Count;
                inDGV.Rows.Add("", "", "رصيد أول المدة");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
                double sum = 0;
                for (int i = 0; i < initialDGV.Rows.Count; i++)
                {
                    try
                    {
                        inDGV.Rows.Add(initialDGV.Rows[i].Cells[0].Value.ToString(), initialDGV.Rows[i].Cells[1].Value.ToString(), "", "", initialDGV.Rows[i].Cells[2].Value.ToString());
                        sum += Convert.ToDouble(initialDGV.Rows[i].Cells[0].Value.ToString());
                    }

                    catch { }
                }
                row = inDGV.Rows.Count;
                inDGV.Rows.Add(sum.ToString(), "", "المجموع");
                inDGV.Rows[row].DefaultCellStyle.Font = new Font("Verdana", 12, FontStyle.Bold);
                inDGV.Rows.Add();
            }

            if (inDGV.Rows.Count > 1)
            {
                inDGV.Rows.RemoveAt(inDGV.Rows.Count - 1);
            }
            if (outDGV.Rows.Count > 1)
            {
                outDGV.Rows.RemoveAt(outDGV.Rows.Count - 1);
            }
            double income = 0;

            for (int i = 0; i < inDGV.Rows.Count; i++)
            {
                if (inDGV.Rows[i].Cells[2].Value != null) { 

                if (inDGV.Rows[i].Cells[2].Value.ToString() == "المجموع")
                {
                    income += Convert.ToDouble(inDGV.Rows[i].Cells[0].Value.ToString());
                }
                }
            }

            incomeSummationTextBox.Text = income.ToString();

            double outcome = 0;

            for (int i = 0; i < outDGV.Rows.Count; i++)
            {
                if (outDGV.Rows[i].Cells[2].Value != null)
                {
                    if (outDGV.Rows[i].Cells[2].Value.ToString() == "المجموع")
                    {
                        outcome += Convert.ToDouble(outDGV.Rows[i].Cells[0].Value.ToString());
                    }
                }
            }
            outcomeSummationTextBox.Text = outcome.ToString();


            resultTextBox.Text = (Convert.ToDouble(incomeSummationTextBox.Text) - Convert.ToDouble(outcomeSummationTextBox.Text)).ToString();




        }

        public void refreshLocal()
        {
            fill();
        }
    }
}
