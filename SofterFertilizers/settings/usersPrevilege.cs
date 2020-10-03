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

namespace SofterFertilizers.settings
{
    public partial class usersPrevilege : UserControl
    {
        public usersPrevilege()
        {
            InitializeComponent();
            fill();
        }
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
      
        void fill()
        {
            //supplier ComboBox
            userNameComboBox.Items.Clear();
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            string Query = "select userName from usersMainTable where owner='false';";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, conDataBase);
            da.Fill(dt);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    userNameComboBox.Items.Add(dr["userName"].ToString());
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDataBase.Close();

            if (userNameComboBox.Items.Count > 0)
            {
                userNameComboBox.Text = userNameComboBox.Items[0].ToString();
            }
        }

        private void BASICSelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (BASICSelectAllCheckBox.Checked)
            {
                storesCheckBox.Checked = true;
                companiesCheckBox.Checked = true;
                unitCheckBox.Checked = true;
                typeCheckBox.Checked = true;
                categoriesCheckBox.Checked = true;
                subUnitsCheckBox.Checked = true;
                safeCheckBox.Checked = true;
                exportCategoriesCheckBox.Checked = true;
                importCAtegoriesCheckBox.Checked = true;

            }
            else
            {
                storesCheckBox.Checked = false;
                companiesCheckBox.Checked = false;
                unitCheckBox.Checked = false;
                typeCheckBox.Checked = false;
                categoriesCheckBox.Checked = false;
                subUnitsCheckBox.Checked = false;
                safeCheckBox.Checked = false;
                exportCategoriesCheckBox.Checked = false;
                importCAtegoriesCheckBox.Checked = false;
            }
        }

        private void STORESSelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (STORESSelectAllCheckBox.Checked)
            {

                settleQuantitiesCheckBox.Checked = true;
                subtractPermissionCheckBox.Checked = true;
                addPermissionCheckBox.Checked = true;
                destroyedCheckBox.Checked = true;
                storeTransportCheckBox.Checked = true;
                showStoreCheckBox.Checked = true;
            }
            else
            {
                settleQuantitiesCheckBox.Checked = false;
                subtractPermissionCheckBox.Checked = false;
                addPermissionCheckBox.Checked = false;
                destroyedCheckBox.Checked = false;
                storeTransportCheckBox.Checked = false;
                showStoreCheckBox.Checked = false;
            }

        }

        private void PURCHASESSelectALLCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PURCHASESSelectALLCheckBox.Checked)
            {
                suppliersCheckBox.Checked = true;
                purchasesBillCheckBox.Checked = true;
                returnedPurchasesCheckBox.Checked = true;
                supplierBalanceCheckBox.Checked = true;
                dollarCheckBox.Checked = true;
            }
            else
            {
                suppliersCheckBox.Checked = false;
                purchasesBillCheckBox.Checked = false;
                returnedPurchasesCheckBox.Checked = false;
                supplierBalanceCheckBox.Checked = false;
                dollarCheckBox.Checked = false;
            }
        }

        private void SALESSelcetAllcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SALESSelcetAllcheckBox.Checked)
            {

                customerCheckBox.Checked = true;
                salesBillCheckBox.Checked = true;
                statementCheckBox.Checked = true;
                customerBalancesCheckBox.Checked = true;
                duesCheckBox.Checked = true;
                payivisionCheckBox.Checked = true;
                reDivideLoansCheckBox.Checked = true;
                exportCustomersCheckBox.Checked = true;
                importCustomersCheckBox.Checked = true;
              
            }
            else
            {
                customerCheckBox.Checked = false;
                salesBillCheckBox.Checked = false;
                statementCheckBox.Checked = false;
                customerBalancesCheckBox.Checked = false;
                duesCheckBox.Checked = false;
                payivisionCheckBox.Checked = false;
                reDivideLoansCheckBox.Checked = false;
                exportCustomersCheckBox.Checked = false;
                importCustomersCheckBox.Checked = false;
            }
        }

        private void CALCULATIONSSelectALLCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CALCULATIONSSelectALLCheckBox.Checked)
            {

                transferSafeCheckBox.Checked = true;
                InittialBalanceCheckBox.Checked = true;
                potentialsCheckBox.Checked = true;
                potentialsDamagedCheckBox.Checked = true;
                partnersCheckBox.Checked = true;
                generalSpendingsCheckBox.Checked = true;
                anotherIncomeCheckBox.Checked = true;
                loansCheckBox.Checked = true;
                advancesCheckBox.Checked = true;

            }
            else
            {
                transferSafeCheckBox.Checked = false;
                InittialBalanceCheckBox.Checked = false;
                potentialsCheckBox.Checked = false;
                potentialsDamagedCheckBox.Checked = false;
                partnersCheckBox.Checked = false;
                generalSpendingsCheckBox.Checked = false;
                anotherIncomeCheckBox.Checked = false;
                loansCheckBox.Checked = false;
                advancesCheckBox.Checked = false;
            }
        }

        private void EMPLOYEESSelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (EMPLOYEESSelectAllCheckBox.Checked)
            {
                generalSettingsCheckBox.Checked = true;
                employeesCheckBox.Checked = true;
                specifiedSettingsCheckBox.Checked = true;
                GoComeCheckBox.Checked = true;
                importEmployessFileCheckBox.Checked = true;
                AbscenceCheckBox.Checked = true;
                lateCheckBox.Checked = true;
                additionalCheckBox.Checked = true;
                wagesCheckBox.Checked = true;
                employessReports.Checked = true;

            }
            else
            {
                generalSettingsCheckBox.Checked = false;
                employeesCheckBox.Checked = false;
                specifiedSettingsCheckBox.Checked = false;
                GoComeCheckBox.Checked = false;
                importEmployessFileCheckBox.Checked = false;
                AbscenceCheckBox.Checked = false;
                lateCheckBox.Checked = false;
                additionalCheckBox.Checked = false;
                wagesCheckBox.Checked = false;
                employessReports.Checked = false;
            }
        }

        private void REPORTSSelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (REPORTSSelectAllCheckBox.Checked)
            {

                salesReportsCheckBox.Checked = true;
                customersReportsCheckBox.Checked = true;
                purchasesReportsCheckBox.Checked = true;
                suppliersReportsCheckBox.Checked = true;
                balanceReportsCheckBox.Checked = true;
                storesReportsCheckBox.Checked = true;
                calculationsReportsCheckBox.Checked = true;
                userReportsCheckBox.Checked = true;
                

            }
            else
            {
                salesReportsCheckBox.Checked = false;
                customersReportsCheckBox.Checked = false;
                purchasesReportsCheckBox.Checked = false;
                suppliersReportsCheckBox.Checked = false;
                balanceReportsCheckBox.Checked = false;
                storesReportsCheckBox.Checked = false;
                calculationsReportsCheckBox.Checked = false;
                userReportsCheckBox.Checked = false;
            }
        }

        private void SETTINGSSelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (SETTINGSSelectAllCheckBox.Checked)
            {
                addUsersCheckBox.Checked = true;
                userPrevilegeCheckBox.Checked = true;
            }
            else
            {
               addUsersCheckBox.Checked = false;
               userPrevilegeCheckBox.Checked = false;
            }
        }

        char[] categories = new char[9];
        char[] store = new char[6];
        char[] purchases = new char[5];
        char[] sales = new char[9];
        char[] calculations = new char[10];
        char[] employees = new char[10];
        char[] reports = new char[8];
        char[] settings = new char[2];

       

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            string _categories = "";
            string _store = "";
            string _purchases = "";
            string _sales = "";
            string _calculations = "";
            string _employees = "";
            string _reports = "";
            string _settings = "";

            categories[0] = storesCheckBox.Checked ? '1' : '0';
            categories[1] =  companiesCheckBox.Checked ? '1' : '0' ;
            categories[2] =  unitCheckBox.Checked ? '1' : '0' ;
            categories[3] =  typeCheckBox.Checked ? '1' : '0' ;
            categories[4] =  categoriesCheckBox.Checked ? '1' : '0' ;
            categories[5] =  subUnitsCheckBox.Checked ? '1' : '0' ;
            categories[6] =  safeCheckBox.Checked ? '1' : '0' ;
            categories[7] =  exportCategoriesCheckBox.Checked ? '1' : '0' ;
            categories[8] =  importCAtegoriesCheckBox.Checked ? '1' : '0' ;


            store[0] =  settleQuantitiesCheckBox.Checked ? '1' : '0' ;
            store[1] =  subtractPermissionCheckBox.Checked ? '1' : '0' ;
            store[2] =  addPermissionCheckBox.Checked ? '1' : '0' ;
            store[3] =  destroyedCheckBox.Checked ? '1' : '0' ;
            store[4] =  storeTransportCheckBox.Checked ? '1' : '0' ;
            store[5] =  showStoreCheckBox.Checked ? '1' : '0' ;


            purchases[0] =  suppliersCheckBox.Checked ? '1' : '0' ;
            purchases[1] =  purchasesBillCheckBox.Checked ? '1' : '0' ;
            purchases[2] =  returnedPurchasesCheckBox.Checked ? '1' : '0' ;
            purchases[3] =  supplierBalanceCheckBox.Checked ? '1' : '0' ;
            purchases[4] =  dollarCheckBox.Checked ? '1' : '0' ;

         
            sales[0] =  customerCheckBox.Checked ? '1' : '0' ;
            sales[1] =  salesBillCheckBox.Checked ? '1' : '0' ;
            sales[2] =  statementCheckBox.Checked ? '1' : '0' ;
            sales[3] =  customerBalancesCheckBox.Checked ? '1' : '0' ;
            sales[4] =  duesCheckBox.Checked ? '1' : '0' ;
            sales[5] =  payivisionCheckBox.Checked ? '1' : '0' ;
            sales[6] =  reDivideLoansCheckBox.Checked ? '1' : '0' ;
            sales[7] =  exportCustomersCheckBox.Checked ? '1' : '0' ;
            sales[8] =  importCustomersCheckBox.Checked ? '1' : '0' ;

            calculations[0] =   '0' ;
            calculations[1] =  transferSafeCheckBox.Checked ? '1' : '0' ;
            calculations[2] =  InittialBalanceCheckBox.Checked ? '1' : '0' ;
            calculations[3] =  potentialsCheckBox.Checked ? '1' : '0' ;
            calculations[4] =  potentialsDamagedCheckBox.Checked ? '1' : '0' ;
            calculations[5] =  partnersCheckBox.Checked ? '1' : '0' ;
            calculations[6] =  generalSpendingsCheckBox.Checked ? '1' : '0' ;
            calculations[7] =  anotherIncomeCheckBox.Checked ? '1' : '0' ;
            calculations[8] =  loansCheckBox.Checked ? '1' : '0' ;
            calculations[9] =  advancesCheckBox.Checked ? '1' : '0' ;

            employees[0] =  generalSettingsCheckBox.Checked ? '1' : '0' ;
            employees[1] =  employeesCheckBox.Checked ? '1' : '0' ;
            employees[2] =  specifiedSettingsCheckBox.Checked ? '1' : '0' ;
            employees[3] =  GoComeCheckBox.Checked ? '1' : '0' ;
            employees[4] =  importEmployessFileCheckBox.Checked ? '1' : '0' ;
            employees[5] =  AbscenceCheckBox.Checked ? '1' : '0' ;
            employees[6] =  lateCheckBox.Checked ? '1' : '0' ;
            employees[7] =  additionalCheckBox.Checked ? '1' : '0' ;
            employees[8] =  wagesCheckBox.Checked ? '1' : '0' ;
            employees[9] =  employessReports.Checked ? '1' : '0' ;

            reports[0] =  salesReportsCheckBox.Checked ? '1' : '0' ;
            reports[1] =  customersReportsCheckBox.Checked ? '1' : '0' ;
            reports[2] =  purchasesReportsCheckBox.Checked ? '1' : '0' ;
            reports[3] =  suppliersReportsCheckBox.Checked ? '1' : '0' ;
            reports[4] =  balanceReportsCheckBox.Checked ? '1' : '0' ;
            reports[5] =  storesReportsCheckBox.Checked ? '1' : '0' ;
            reports[6] =  calculationsReportsCheckBox.Checked ? '1' : '0' ;
            reports[7] =  userReportsCheckBox.Checked ? '1' : '0' ;

            settings[0] =  addUsersCheckBox.Checked ? '1' : '0' ;
            settings[1] =  userPrevilegeCheckBox.Checked ? '1' : '0' ;


            for(int i=0;i< 2; i++)
            {
                _settings += settings[i];
            }

            for(int i =0; i < 5; i++)
            {
                _purchases += purchases[i];
            }

            for(int i = 0; i < 6; i++)
            {
                _store += store[i];
            }

            for (int i = 0; i < 8; i++)
            {
                _reports += reports[i];
            }

            for (int i = 0; i < 9; i++)
            {
                _categories += categories[i];
                _sales += sales[i];
            }

            for (int i = 0; i < 10; i++)
            {
                _calculations += calculations[i];
                _employees += employees[i];
            }


            string Query = "IF NOT EXISTS (select 1 FROM usersPrevilege where userName = N'" + this.userNameComboBox.Text + "') BEGIN INSERT INTO usersPrevilege (category,store,purchases,sales,calculations,employees,reports, settings,userName) VALUES (N'" + _categories + "',N'" + _store+ "',N'" + _purchases+ "',N'" + _sales + "',N'" + _calculations + "',N'" + _employees + "',N'" + _reports + "',N'" + _settings + "',N'"+this.userNameComboBox.Text+"') END ELSE UPDATE usersPrevilege SET category =N'" + _categories + "',store =N'" + _store + "',purchases =N'" + _purchases + "',sales =N'" + _sales + "',calculations =N'" + _calculations+ "',employees =N'" + _employees + "',reports=N'" + _reports + "',settings=N'" + _settings + "'  where userName=N'"+this.userNameComboBox.Text+"' ";
            SqlConnection conDataBase = new SqlConnection(constring);
            SqlCommand cmdDataBase = new SqlCommand(Query, conDataBase);
            SqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                MessageBox.Show("حُفظ");
                while (myReader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            fill();

        }

        private void userNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(userNameComboBox.Text == "admin")
            {
                storesCheckBox.Checked = true;
                companiesCheckBox.Checked = true;
                unitCheckBox.Checked = true;
                typeCheckBox.Checked = true;
                categoriesCheckBox.Checked = true;
                subUnitsCheckBox.Checked = true;
                safeCheckBox.Checked = true;
                exportCategoriesCheckBox.Checked = true;
                importCAtegoriesCheckBox.Checked = true;
                settleQuantitiesCheckBox.Checked = true;
                subtractPermissionCheckBox.Checked = true;
                addPermissionCheckBox.Checked = true;
                destroyedCheckBox.Checked = true;
                storeTransportCheckBox.Checked = true;
                showStoreCheckBox.Checked = true;
                suppliersCheckBox.Checked = true;
                purchasesBillCheckBox.Checked = true;
                returnedPurchasesCheckBox.Checked = true;
                supplierBalanceCheckBox.Checked = true;
                dollarCheckBox.Checked = true;
                customerCheckBox.Checked = true;
                salesBillCheckBox.Checked = true;
                statementCheckBox.Checked = true;
                customerBalancesCheckBox.Checked = true;
                duesCheckBox.Checked = true;
                payivisionCheckBox.Checked = true;
                reDivideLoansCheckBox.Checked = true;
                exportCustomersCheckBox.Checked = true;
                importCustomersCheckBox.Checked = true;
                transferSafeCheckBox.Checked = true;
                InittialBalanceCheckBox.Checked = true;
                potentialsCheckBox.Checked = true;
                potentialsDamagedCheckBox.Checked = true;
                partnersCheckBox.Checked = true;
                generalSpendingsCheckBox.Checked = true;
                anotherIncomeCheckBox.Checked = true;
                loansCheckBox.Checked = true;
                advancesCheckBox.Checked = true;
                generalSettingsCheckBox.Checked = true;
                employeesCheckBox.Checked = true;
                specifiedSettingsCheckBox.Checked = true;
                GoComeCheckBox.Checked = true;
                importEmployessFileCheckBox.Checked = true;
                AbscenceCheckBox.Checked = true;
                lateCheckBox.Checked = true;
                additionalCheckBox.Checked = true;
                wagesCheckBox.Checked = true;
                employessReports.Checked = true;
                salesReportsCheckBox.Checked = true;
                customersReportsCheckBox.Checked = true;
                purchasesReportsCheckBox.Checked = true;
                suppliersReportsCheckBox.Checked = true;
                balanceReportsCheckBox.Checked = true;
                storesReportsCheckBox.Checked = true;
                calculationsReportsCheckBox.Checked = true;
                userReportsCheckBox.Checked = true;
                addUsersCheckBox.Checked = true;
                userPrevilegeCheckBox.Checked = true;

                storesCheckBox.Enabled = false;
                companiesCheckBox.Enabled = false;
                unitCheckBox.Enabled = false;
                typeCheckBox.Enabled = false;
                categoriesCheckBox.Enabled = false;
                subUnitsCheckBox.Enabled = false;
                safeCheckBox.Enabled = false;
                exportCategoriesCheckBox.Enabled = false;
                importCAtegoriesCheckBox.Enabled = false;
                settleQuantitiesCheckBox.Enabled = false;
                subtractPermissionCheckBox.Enabled = false;
                addPermissionCheckBox.Enabled = false;
                destroyedCheckBox.Enabled = false;
                storeTransportCheckBox.Enabled = false;
                showStoreCheckBox.Enabled = false;
                suppliersCheckBox.Enabled = false;
                purchasesBillCheckBox.Enabled = false;
                returnedPurchasesCheckBox.Enabled = false;
                supplierBalanceCheckBox.Enabled = false;
                dollarCheckBox.Enabled = false;
                customerCheckBox.Enabled = false;
                salesBillCheckBox.Enabled = false;
                statementCheckBox.Enabled = false;
                customerBalancesCheckBox.Enabled = false;
                duesCheckBox.Enabled = false;
                payivisionCheckBox.Enabled = false;
                reDivideLoansCheckBox.Enabled = false;
                exportCustomersCheckBox.Enabled = false;
                importCustomersCheckBox.Enabled = false;
                transferSafeCheckBox.Enabled = false;
                InittialBalanceCheckBox.Enabled = false;
                potentialsCheckBox.Enabled = false;
                potentialsDamagedCheckBox.Enabled = false;
                partnersCheckBox.Enabled = false;
                generalSpendingsCheckBox.Enabled = false;
                anotherIncomeCheckBox.Enabled = false;
                loansCheckBox.Enabled = false;
                advancesCheckBox.Enabled = false;
                generalSettingsCheckBox.Enabled = false;
                employeesCheckBox.Enabled = false;
                specifiedSettingsCheckBox.Enabled = false;
                GoComeCheckBox.Enabled = false;
                importEmployessFileCheckBox.Enabled = false;
                AbscenceCheckBox.Enabled = false;
                lateCheckBox.Enabled = false;
                additionalCheckBox.Enabled = false;
                wagesCheckBox.Enabled = false;
                employessReports.Enabled = false;
                salesReportsCheckBox.Enabled = false;
                customersReportsCheckBox.Enabled = false;
                purchasesReportsCheckBox.Enabled = false;
                suppliersReportsCheckBox.Enabled = false;
                balanceReportsCheckBox.Enabled = false;
                storesReportsCheckBox.Enabled = false;
                calculationsReportsCheckBox.Enabled = false;
                userReportsCheckBox.Enabled = false;
                addUsersCheckBox.Enabled = false;
                userPrevilegeCheckBox.Enabled = false;

            }
            else
            {
                storesCheckBox.Enabled = true;
                companiesCheckBox.Enabled = true;
                unitCheckBox.Enabled = true;
                typeCheckBox.Enabled = true;
                categoriesCheckBox.Enabled = true;
                subUnitsCheckBox.Enabled = true;
                safeCheckBox.Enabled = true;
                exportCategoriesCheckBox.Enabled = true;
                importCAtegoriesCheckBox.Enabled = true;
                settleQuantitiesCheckBox.Enabled = true;
                subtractPermissionCheckBox.Enabled = true;
                addPermissionCheckBox.Enabled = true;
                destroyedCheckBox.Enabled = true;
                storeTransportCheckBox.Enabled = true;
                showStoreCheckBox.Enabled = true;
                suppliersCheckBox.Enabled = true;
                purchasesBillCheckBox.Enabled = true;
                returnedPurchasesCheckBox.Enabled = true;
                supplierBalanceCheckBox.Enabled = true;
                dollarCheckBox.Enabled = true;
                customerCheckBox.Enabled = true;
                salesBillCheckBox.Enabled = true;
                statementCheckBox.Enabled = true;
                customerBalancesCheckBox.Enabled = true;
                duesCheckBox.Enabled = true;
                payivisionCheckBox.Enabled = true;
                reDivideLoansCheckBox.Enabled = true;
                exportCustomersCheckBox.Enabled = true;
                importCustomersCheckBox.Enabled = true;
                transferSafeCheckBox.Enabled = true;
                InittialBalanceCheckBox.Enabled = true;
                potentialsCheckBox.Enabled = true;
                potentialsDamagedCheckBox.Enabled = true;
                partnersCheckBox.Enabled = true;
                generalSpendingsCheckBox.Enabled = true;
                anotherIncomeCheckBox.Enabled = true;
                loansCheckBox.Enabled = true;
                advancesCheckBox.Enabled = true;
                generalSettingsCheckBox.Enabled = true;
                employeesCheckBox.Enabled = true;
                specifiedSettingsCheckBox.Enabled = true;
                GoComeCheckBox.Enabled = true;
                importEmployessFileCheckBox.Enabled = true;
                AbscenceCheckBox.Enabled = true;
                lateCheckBox.Enabled = true;
                additionalCheckBox.Enabled = true;
                wagesCheckBox.Enabled = true;
                employessReports.Enabled = true;
                salesReportsCheckBox.Enabled = true;
                customersReportsCheckBox.Enabled = true;
                purchasesReportsCheckBox.Enabled = true;
                suppliersReportsCheckBox.Enabled = true;
                balanceReportsCheckBox.Enabled = true;
                storesReportsCheckBox.Enabled = true;
                calculationsReportsCheckBox.Enabled = true;
                userReportsCheckBox.Enabled = true;
                addUsersCheckBox.Enabled = true;
                userPrevilegeCheckBox.Enabled = true;

                SqlConnection conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                char[] categories = (new SqlCommand("select category from usersPrevilege where userName='" + this.userNameComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
                conDataBase.Close();



                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                char[] store = (new SqlCommand("select store from usersPrevilege where userName='" + this.userNameComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                char[] purchases = (new SqlCommand("select purchases from usersPrevilege where userName='" + this.userNameComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                char[] sales = (new SqlCommand("select sales from usersPrevilege where userName='" + this.userNameComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                char[] calculations = (new SqlCommand("select calculations from usersPrevilege where userName='" + this.userNameComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                char[] employees = (new SqlCommand("select employees from usersPrevilege where userName='" + this.userNameComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                char[] reports = (new SqlCommand("select reports from usersPrevilege where userName='" + this.userNameComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
                conDataBase.Close();

                conDataBase = new SqlConnection(constring);
                conDataBase.Open();
                char[] settings = (new SqlCommand("select settings from usersPrevilege where userName='" + this.userNameComboBox.Text + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
                conDataBase.Close();

                if (categories[0] == '1')
                {
                    storesCheckBox.Checked = true;
                }
                else
                {
                    storesCheckBox.Checked = true;
                }

                if (categories[1] == '1')
                {

                    companiesCheckBox.Checked = true;
                }
                else
                {
                    companiesCheckBox.Checked = false;
                }


                if (categories[2] == '1')
                {
                    unitCheckBox.Checked = true;
                }
                else
                {
                    unitCheckBox.Checked = false;
                }


                if (categories[3] == '1')
                {
                    typeCheckBox.Checked = true;
                }
                else
                {
                    typeCheckBox.Checked = false;
                }


                if (categories[4] == '1')
                {
                    categoriesCheckBox.Checked = true;
                }
                else
                {
                    categoriesCheckBox.Checked = false;
                }


                if (categories[5] == '1')
                {
                    subUnitsCheckBox.Checked = true;
                }
                else
                {
                    subUnitsCheckBox.Checked = false;
                }

                if (categories[6] == '1')
                {
                    safeCheckBox.Checked = true;
                }
                else
                {
                    safeCheckBox.Checked = false;
                }

                if (categories[7] == '1')
                {
                    exportCategoriesCheckBox.Checked = true;
                }
                else
                {
                    exportCategoriesCheckBox.Checked = false;
                }

                if (categories[8] == '1')
                {
                    importCAtegoriesCheckBox.Checked = true;
                }
                else
                {
                    importCAtegoriesCheckBox.Checked = false;
                }


                if (store[0] == '1')
                {
                    settleQuantitiesCheckBox.Checked = true;
                }
                else
                {
                    settleQuantitiesCheckBox.Checked = false;
                }

                if (store[1] == '1')
                {
                    subtractPermissionCheckBox.Checked = true;
                }
                else
                {
                    subtractPermissionCheckBox.Checked = false;
                }


                if (store[2] == '1')
                {
                    addPermissionCheckBox.Checked = true;
                }
                else
                {
                    addPermissionCheckBox.Checked = false;
                }


                if (store[3] == '1')
                {
                    destroyedCheckBox.Checked = true;
                }
                else
                {
                    destroyedCheckBox.Checked = false;
                }


                if (store[4] == '1')
                {
                    storeTransportCheckBox.Checked = true;
                }
                else
                {
                    storeTransportCheckBox.Checked = false;
                }


                if (store[5] == '1')
                {
                    showStoreCheckBox.Checked = true;
                }
                else
                {
                    showStoreCheckBox.Checked = false;
                }

               

                if (purchases[0] == '1')
                {
                    suppliersCheckBox.Checked = true;
                }
                else
                {
                    suppliersCheckBox.Checked = false;
                }

                if (purchases[1] == '1')
                {
                    purchasesBillCheckBox.Checked = true;
                }
                else
                {
                    purchasesBillCheckBox.Checked = false;
                }


                if (purchases[2] == '1')
                {
                    returnedPurchasesCheckBox.Checked = true;
                }
                else
                {
                    returnedPurchasesCheckBox.Checked = false;
                }


                if (purchases[3] == '1')
                {
                    supplierBalanceCheckBox.Checked = true;
                }
                else
                {
                    supplierBalanceCheckBox.Checked = false;
                }


                if (purchases[4] == '1')
                {
                    dollarCheckBox.Checked = true;
                }
                else
                {
                    dollarCheckBox.Checked = false;
                }

              


                if (sales[0] == '1')
                {
                    customerCheckBox.Checked = true;
                }
                else
                {
                    customerCheckBox.Checked = false;
                }


                if (sales[1] == '1')
                {
                    salesBillCheckBox.Checked = true;
                }
                else
                {
                    salesBillCheckBox.Checked = false;
                }



                if (sales[2] == '1')
                {
                    statementCheckBox.Checked = true;
                }
                else
                {
                    statementCheckBox.Checked = false;
                }



                if (sales[3] == '1')
                {
                    customerBalancesCheckBox.Checked = true;
                }
                else
                {
                    customerBalancesCheckBox.Checked = false;
                }



                if (sales[4] == '1')
                {
                    duesCheckBox.Checked = true;
                }
                else
                {
                    duesCheckBox.Checked = false;
                }



                if (sales[5] == '1')
                {
                    payivisionCheckBox.Checked = true;
                }
                else
                {
                    payivisionCheckBox.Checked = false;
                }


                if (sales[6] == '1')
                {
                    reDivideLoansCheckBox.Checked = true;
                }
                else
                {
                    reDivideLoansCheckBox.Checked = false;
                }


                if (sales[7] == '1')
                {
                    exportCustomersCheckBox.Checked = true;
                }
                else
                {
                    exportCustomersCheckBox.Checked = false;
                }

                if (sales[8] == '1')
                {
                    importCustomersCheckBox.Checked = true;
                }
                else
                {
                    importCustomersCheckBox.Checked = false;
                }



              

                if (calculations[1] == '1')
                {
                    transferSafeCheckBox.Checked = true;
                }
                else
                {
                    transferSafeCheckBox.Checked = false;
                }


                if (calculations[2] == '1')
                {
                    InittialBalanceCheckBox.Checked = true;
                }
                else
                {
                    InittialBalanceCheckBox.Checked = false;
                }


                if (calculations[3] == '1')
                {
                    potentialsCheckBox.Checked = true;
                }
                else
                {
                    potentialsCheckBox.Checked = false;
                }


                if (calculations[4] == '1')
                {
                    potentialsDamagedCheckBox.Checked = true;
                }
                else
                {
                    potentialsDamagedCheckBox.Checked = false;
                }


                if (calculations[5] == '1')
                {
                    partnersCheckBox.Checked = true;
                }
                else
                {
                    partnersCheckBox.Checked = false;
                }

                if (calculations[6] == '1')
                {
                    generalSpendingsCheckBox.Checked = true;
                }
                else
                {
                    generalSpendingsCheckBox.Checked = false;
                }

                if (calculations[7] == '1')
                {
                    anotherIncomeCheckBox.Checked = true;
                }
                else
                {
                    anotherIncomeCheckBox.Checked = false;
                }

                if (calculations[8] == '1')
                {
                    loansCheckBox.Checked = true;
                }
                else
                {
                    loansCheckBox.Checked = false;
                }

                if (calculations[9] == '1')
                {
                    advancesCheckBox.Checked = true;
                }
                else
                {
                    advancesCheckBox.Checked = false;
                }

               

                if (employees[0] == '1')
                {
                    generalSettingsCheckBox.Checked = true;
                }
                else
                {
                    generalSettingsCheckBox.Checked = false;
                }

                if (employees[1] == '1')
                {
                    employeesCheckBox.Checked = true;
                }
                else
                {
                    employeesCheckBox.Checked = false;
                }


                if (employees[2] == '1')
                {
                    specifiedSettingsCheckBox.Checked = true;
                }
                else
                {
                    specifiedSettingsCheckBox.Checked = false;
                }


                if (employees[3] == '1')
                {
                    GoComeCheckBox.Checked = true;
                }
                else
                {
                    GoComeCheckBox.Checked = false;
                }


                if (employees[4] == '1')
                {
                    importEmployessFileCheckBox.Checked = true;
                }
                else
                {
                    importEmployessFileCheckBox.Checked = false;
                }


                if (employees[5] == '1')
                {
                    AbscenceCheckBox.Checked = true;
                }
                else
                {
                    AbscenceCheckBox.Checked = false;
                }

                if (employees[6] == '1')
                {
                    lateCheckBox.Checked = true;
                }
                else
                {
                    lateCheckBox.Checked = false;
                }

                if (employees[7] == '1')
                {
                    additionalCheckBox.Checked = true;
                }
                else
                {
                    additionalCheckBox.Checked = false;
                }

                if (employees[8] == '1')
                {
                    wagesCheckBox.Checked = true;
                }
                else
                {
                    wagesCheckBox.Checked = false;
                }

                if (employees[9] == '1')
                {
                    employessReports.Checked = true;
                }
                else
                {
                    employessReports.Checked = false;
                }

               

                if (reports[0] == '1')
                {
                    salesReportsCheckBox.Checked = true;
                }
                else
                {
                    salesReportsCheckBox.Checked = false;
                }

                if (reports[1] == '1')
                {
                    customersReportsCheckBox.Checked = true;
                }
                else
                {
                    customersReportsCheckBox.Checked = false;
                }


                if (reports[2] == '1')
                {
                    purchasesReportsCheckBox.Checked = true;
                }
                else
                {
                    purchasesReportsCheckBox.Checked = false;
                }


                if (reports[3] == '1')
                {
                    suppliersReportsCheckBox.Checked = true;
                }
                else
                {
                    suppliersReportsCheckBox.Checked = false;
                }


                if (reports[4] == '1')
                {
                    balanceReportsCheckBox.Checked = true;
                }
                else
                {
                    balanceReportsCheckBox.Checked = false;
                }


                if (reports[5] == '1')
                {
                    storesReportsCheckBox.Checked = true;
                }
                else
                {
                    storesReportsCheckBox.Checked = false;
                }

                if (reports[6] == '1')
                {
                    calculationsReportsCheckBox.Checked = true;
                }
                else
                {
                    calculationsReportsCheckBox.Checked = false;
                }

                if (reports[7] == '1')
                {
                    userReportsCheckBox.Checked = true;
                }
                else
                {
                    userReportsCheckBox.Checked = false;
                }


              


                if (settings[0] == '1')
                {
                    addUsersCheckBox.Checked = true;
                }
                else
                {
                    addUsersCheckBox.Checked = false;
                }

                if (settings[1] == '1')
                {
                    userPrevilegeCheckBox.Checked = true;
                }
                else
                {
                    userPrevilegeCheckBox.Checked = false;
                }

             
            }
        }
    }
}
