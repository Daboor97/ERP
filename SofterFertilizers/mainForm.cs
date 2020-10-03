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

namespace SofterFertilizers
{
    public partial class mainForm : Form
    {
        private bool shortcutsIsCollapsed = true;
        
        //Basic Data Staic Pages Starter
        static BasicData.storeAddingUC SAUC = new BasicData.storeAddingUC();
        static BasicData.companiesUC CUC = new BasicData.companiesUC();
        static BasicData.typeUC TUC = new BasicData.typeUC();
        static BasicData.unitsUC UUC = new BasicData.unitsUC();
        static BasicData.sortUC SUC = new BasicData.sortUC();
        static BasicData.subUnit SUUC = new BasicData.subUnit();
        static BasicData.safeUC SfUC = new BasicData.safeUC();
        static BasicData.saveToExcel STEXC = new BasicData.saveToExcel();
        static BasicData.importExcel IEXC = new BasicData.importExcel();

        //Store Static Pages Starter
        static store.categoryAddPermission CAPUC = new store.categoryAddPermission();
        static store.categorySubtractPermission CSPUC = new store.categorySubtractPermission();
        static store.categoryQuantities CQUC = new store.categoryQuantities();
        static store.showStore SSUC = new store.showStore();
        static store.destroyedAdding DAUC = new store.destroyedAdding();
        static store.transportStore TSUC = new store.transportStore();

        //purchases Static Pages Starter
        static purchases.Suppilers SpUC = new purchases.Suppilers(); 
        static purchases.purchases PUC = new purchases.purchases(); 
        static purchases.returnedPurchasesUC RPUC = new purchases.returnedPurchasesUC(); 
        static purchases.suppliersBalances SBUC = new purchases.suppliersBalances(); 
        static purchases.dollarCalculations DCUC = new purchases.dollarCalculations();

        //sales static pages Starter

        static sales.customers CustUC = new sales.customers();
        static sales.sales SalesUC = new sales.sales();
        static sales.statement StatementUC = new sales.statement();
        static sales.customesrBalances CustBalancesUC = new sales.customesrBalances();
        static sales.duesUC duesUC = new sales.duesUC();
        static sales.payDivision payDivisionUC = new sales.payDivision();
        static sales.ReDivideDebts RDDUC = new sales.ReDivideDebts();
        static sales.exportCustomers ECUC = new sales.exportCustomers();

        //Calculation static pages starter
        static calculations.transferBetweenSafe TBS = new calculations.transferBetweenSafe();
        static calculations.potentials.initialMoney IMUC = new calculations.potentials.initialMoney();
        static calculations.potentials.bankAccounts BAUC = new calculations.potentials.bankAccounts();
        static calculations.fixedPotentials FPUC = new calculations.fixedPotentials();
        static calculations.potentailDamage PDUC = new calculations.potentailDamage();
        static calculations.partners.partnersData PDataUC = new calculations.partners.partnersData();
        static calculations.partners.partnersOperations POUC = new calculations.partners.partnersOperations();
        static calculations.generalSpendings GSUC = new calculations.generalSpendings();
        static calculations.anotherIncomes AIUC = new calculations.anotherIncomes();
        static calculations.loans.loansOwners AddLoanUC = new calculations.loans.loansOwners();
        static calculations.loans.loansDetails LDUC = new calculations.loans.loansDetails();
        static calculations.loans.payLoans PLUC = new calculations.loans.payLoans();
        static calculations.loans.loansReport LRUC = new calculations.loans.loansReport();
        static calculations.advance.advanceOwners AOUC= new calculations.advance.advanceOwners();
        static calculations.advance.advanceDetails ADUC= new calculations.advance.advanceDetails();
        static calculations.advance.payAdvances PAUC= new calculations.advance.payAdvances();
        static calculations.advance.advanceReport ARUC = new calculations.advance.advanceReport();

        //employees static pages starter
        static employees.employeesGeneralSettings EGSUC = new employees.employeesGeneralSettings();
        static employees.employees EAUC = new employees.employees();
        static employees.employeeSpecifiedSettings ESSUC = new employees.employeeSpecifiedSettings();
        static employees.comeGoing CGUC = new employees.comeGoing();
        static employees.abscence empAUC = new employees.abscence();
        static employees.late LEUC= new employees.late();
        static employees.additional AEUC= new employees.additional();
        static employees.wages EWUC= new employees.wages();
        static employees.reports.comeGoingReports CGRUC= new employees.reports.comeGoingReports();
        static employees.reports.abscenceReports EARUC= new employees.reports.abscenceReports();
        static employees.reports.lateReport LERUC= new employees.reports.lateReport();
        static employees.reports.additionalReport AERUC= new employees.reports.additionalReport();
        static employees.reports.wagesReport WRUC= new employees.reports.wagesReport();
        static employees.reports.employeesVisulaizationUC EVUC = new employees.reports.employeesVisulaizationUC();

        //Setitngs static pages starter
        static settings.addUsers AUUC = new settings.addUsers();
        static settings.usersPrevilege UPUC = new settings.usersPrevilege();

        // Reports Static pages Starter
        static Reports.usersTrafficReport UTUC = new Reports.usersTrafficReport();

        // Sales Reports Static pages Starter
        static Reports.salesFlow SFRUC = new Reports.salesFlow();
        static Reports.companySalesFlow CSFRUC = new Reports.companySalesFlow();
        static Reports.typeSalesFlow TSFRUC = new Reports.typeSalesFlow();
        static Reports.overallSaleFlow OSFRUC = new Reports.overallSaleFlow();
        static Reports.highestBought HBSFRUC = new Reports.highestBought();
        static Reports.soloPackage SPSFRUC = new Reports.soloPackage();
        static Reports.soloPackageCategory SPCSFRUC = new Reports.soloPackageCategory();
        static Reports.storeCategoryReport SCSFRUC = new Reports.storeCategoryReport();
        static Reports.salesReport.salesVisualization SVR = new Reports.salesReport.salesVisualization();

        //customers Reports Static pages Starter
        static Reports.customersReport.customersDetail CDRUC = new Reports.customersReport.customersDetail();
        static Reports.customersReport.customerBalanceDetails CBDRUC = new Reports.customersReport.customerBalanceDetails();
        static Reports.customersReport.customerSalesBillsResport CSBR = new Reports.customersReport.customerSalesBillsResport();
        static Reports.customersReport.customersCategoryReport CCR = new Reports.customersReport.customersCategoryReport();
        static Reports.customersReport.customerCompanyReport CCompR = new Reports.customersReport.customerCompanyReport();
        static Reports.customersReport.customerTypeReport CTR = new Reports.customersReport.customerTypeReport();
        static Reports.customersReport.customerCategorySumReport CCSR = new Reports.customersReport.customerCategorySumReport();
        static Reports.customersReport.customerHghestsales CHSR = new Reports.customersReport.customerHghestsales();
        static Reports.customersReport.customerQuantityCompare CQCR = new Reports.customersReport.customerQuantityCompare();
        static Reports.customersReport.customerDivision CDR = new Reports.customersReport.customerDivision();
        static Reports.customersReport.customerDivisonDate CDDR = new Reports.customersReport.customerDivisonDate();
        static Reports.customersReport.customerDivisionDetails CDDetailsR = new Reports.customersReport.customerDivisionDetails();
        static Reports.customersReport.customerDivisionPayment CDPR = new Reports.customersReport.customerDivisionPayment();
        static Reports.customersReport.customers_Visualization CVR = new Reports.customersReport.customers_Visualization();

        //purchases Reports Static pages Starter
        static Reports.purchasesReport.purchasesFlow PFR = new Reports.purchasesReport.purchasesFlow();
        static Reports.purchasesReport.returnedPurchasesReport RPFR = new Reports.purchasesReport.returnedPurchasesReport();
        static Reports.purchasesReport.purchasesCompanyFlow PCFR = new Reports.purchasesReport.purchasesCompanyFlow();
        static Reports.purchasesReport.overAllPurchasesFlow OAPFR = new Reports.purchasesReport.overAllPurchasesFlow();
        static Reports.purchasesReport.transportReport TCR = new Reports.purchasesReport.transportReport();
        static Reports.purchasesReport.Purchases_Visualization PVR = new Reports.purchasesReport.Purchases_Visualization();

        //supplier Reports static pages starter
        static Reports.suppliersReport.supplierDetails SDR = new Reports.suppliersReport.supplierDetails();
        static Reports.suppliersReport.supplierCategoryReport SCR = new Reports.suppliersReport.supplierCategoryReport();
        static Reports.suppliersReport.supplierTypeReports STR = new Reports.suppliersReport.supplierTypeReports();
        static Reports.suppliersReport.supplierBalance SBR = new Reports.suppliersReport.supplierBalance();
        static Reports.suppliersReport.supplierCategorySumReport SCSR = new Reports.suppliersReport.supplierCategorySumReport();
        static Reports.suppliersReport.supplierReturnedReport SRR = new Reports.suppliersReport.supplierReturnedReport();
        static Reports.suppliersReport.suppliersVisualizationUC SVUC = new Reports.suppliersReport.suppliersVisualizationUC();

        //Store Reports static pages starter
        static Reports.storeReports.categoryList CLR = new Reports.storeReports.categoryList();
        static Reports.storeReports.storeCategoryReport SCLR = new Reports.storeReports.storeCategoryReport();
        static Reports.storeReports.categoryStoreReport CSR = new Reports.storeReports.categoryStoreReport();
        static Reports.storeReports.storeCompanyReport SComR = new Reports.storeReports.storeCompanyReport();
        static Reports.storeReports.StoreMinimumReport SMR= new Reports.storeReports.StoreMinimumReport();
        static Reports.storeReports.storeMaximumReport SMaxR= new Reports.storeReports.storeMaximumReport();
        static store.storeDestroyedReport SDesR = new store.storeDestroyedReport();
        static Reports.storeReports.storeTransformReport STranR= new Reports.storeReports.storeTransformReport();
        static Reports.storeReports.storeBalance SBalR= new Reports.storeReports.storeBalance();
        static Reports.storeReports.categoryFlow CFRUC= new Reports.storeReports.categoryFlow();
        static Reports.storeReports.typeList TLRUC= new Reports.storeReports.typeList();
        static Reports.storeReports.minusCategoryReports MCRUC = new Reports.storeReports.minusCategoryReports();
        static Reports.storeReports.historyCategory HCRUC = new Reports.storeReports.historyCategory();
        static Reports.storeReports.add_subtractPermission ASRUC = new Reports.storeReports.add_subtractPermission();

        //safe Reports static pages starter
        static Reports.safeReports.safeFlowReports SafeFRUC = new Reports.safeReports.safeFlowReports();
        static Reports.safeReports.cashBalance CBRUC = new Reports.safeReports.cashBalance();
        static Reports.safeReports.safeTransferReport STRUC = new Reports.safeReports.safeTransferReport();
        static Reports.safeReports.SafeVisualization SDVUC = new Reports.safeReports.SafeVisualization();

        //safe Reports static pages starter
        static Reports.calculationsReport.spendingReports GSRUC= new Reports.calculationsReport.spendingReports();
        static Reports.calculationsReport.partnerBalance PBRUC= new Reports.calculationsReport.partnerBalance();
        static Reports.calculationsReport.anotherIncomeReports AIRUC= new Reports.calculationsReport.anotherIncomeReports();
        static Reports.calculationsReport.potentialDamageReprt PDRUC= new Reports.calculationsReport.potentialDamageReprt();


        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["constring"].ConnectionString;


        void clearMain()
        {
            SAUC.Visible = false;
            CUC.Visible = false;
            TUC.Visible = false;
            UUC.Visible = false;
            SUC.Visible = false;
            SUUC.Visible = false;
            SfUC.Visible = false;
            STEXC.Visible = false;
            IEXC.Visible = false;
            CAPUC.Visible = false;
            CSPUC.Visible = false;
            CQUC.Visible = false;
            SSUC.Visible = false;
            DAUC.Visible = false;
            TSUC.Visible = false;
            SpUC.Visible = false;
            PUC.Visible = false;
            RPUC.Visible = false;
            SBUC.Visible = false;
            DCUC.Visible = false;
            CustUC.Visible = false;
            SalesUC.Visible = false;
            StatementUC.Visible = false;
            CustBalancesUC.Visible = false;
            duesUC.Visible = false;
            payDivisionUC.Visible = false;
            RDDUC.Visible = false;
            ECUC.Visible = false;
            SFRUC.Visible = false;
            CSFRUC.Visible = false;
            TSFRUC.Visible = false;
            OSFRUC.Visible = false;
            HBSFRUC.Visible = false;
            SPSFRUC.Visible = false;
            SPCSFRUC.Visible = false;
            SCSFRUC.Visible = false;
            CDRUC.Visible = false;
            CBDRUC.Visible = false;
            CSBR.Visible = false;
            CCR.Visible = false;
            CCompR.Visible = false;
            CTR.Visible = false;
            CCSR.Visible = false;
            CHSR.Visible = false;
            CQCR.Visible = false;
            CDR.Visible = false;
            CDDR.Visible = false;
            CDPR.Visible = false;
            CDDetailsR.Visible = false;
            PFR.Visible = false;
            RPFR.Visible = false;
            PCFR.Visible = false;
            OAPFR.Visible = false;
            TCR.Visible = false;
            SDR.Visible = false;
            SCR.Visible = false;
            STR.Visible = false;
            SBR.Visible = false;
            SCSR.Visible = false;
            SRR.Visible = false;
            CLR.Visible = false;
            SCLR.Visible = false;
            CSR.Visible = false;
            SComR.Visible = false;
            SMR.Visible = false;
            SMaxR.Visible = false;
            SDesR.Visible = false;
            STranR.Visible = false;
            SBalR.Visible = false;
            TBS.Visible = false;
            IMUC.Visible = false;
            BAUC.Visible = false;
            FPUC.Visible = false;
            PDUC.Visible = false;
            PDataUC.Visible = false;
            POUC.Visible = false;
            GSUC.Visible = false;
            AIUC.Visible = false;
            AddLoanUC.Visible = false;
            LDUC.Visible = false;
            PLUC.Visible = false;
            LRUC.Visible = false;
            AOUC.Visible = false;
            ADUC.Visible = false;
            PAUC.Visible = false;
            ARUC.Visible = false;
            EGSUC.Visible = false;
            EAUC.Visible = false;
            ESSUC.Visible = false;
            CGUC.Visible = false;
            empAUC.Visible = false;
            LEUC.Visible = false;
            AEUC.Visible = false;
            EWUC.Visible = false;
            EARUC.Visible = false;
            CGRUC.Visible = false;
            LERUC.Visible = false;
            AERUC.Visible = false;
            WRUC.Visible = false;
            SafeFRUC.Visible = false;
            CBRUC.Visible = false;
            STRUC.Visible = false;
            CFRUC.Visible = false;
            TLRUC.Visible = false;
            MCRUC.Visible = false;
            HCRUC.Visible = false;
            ASRUC.Visible = false;
            GSRUC.Visible = false;
            PBRUC.Visible = false;
            AIRUC.Visible = false;
            PDRUC.Visible = false;
            AUUC.Visible = false;
            UPUC.Visible = false;
            UTUC.Visible = false;
            PVR.Visible = false;
            SVR.Visible = false;
            CVR.Visible = false;
            EVUC.Visible = false;
            SVUC.Visible = false;
            SDVUC.Visible = false;
        }
        void closeForms()
        {
            HomeButton.BringToFront();
            HomeButton.Visible = true;

            shortcutsDropDownPanel.Visible = false;
            softerPicture.Visible = false;

        }
        string userNameLoged;

        public mainForm(string userName)
        {
            InitializeComponent();
            userNameLoged = userName;
            loginFunction();
            clearMain();
            HomeButton.Visible = false;
            timer1.Start();   
        }
        void loginFunction()
        {
            int categoriesInt = 0;
            int storeInt = 0;
            int purchasesInt = 0;
            int salesInt = 0;
            int calculationsInt = 0;
            int employeesInt = 0;
            int reportsInt = 0;
            int settingsInt = 0;
            
            
            SqlConnection conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            char[] categories = (new SqlCommand("select category from usersPrevilege where userName='"+this.userNameLoged +"' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
            conDataBase.Close();



             conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            char[] store = (new SqlCommand("select store from usersPrevilege where userName='" + this.userNameLoged + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
            conDataBase.Close();

             conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            char[] purchases = (new SqlCommand("select purchases from usersPrevilege where userName='" + this.userNameLoged + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
            conDataBase.Close();

             conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            char[] sales = (new SqlCommand("select sales from usersPrevilege where userName='" + this.userNameLoged + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
            conDataBase.Close();

             conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            char[] calculations = (new SqlCommand("select calculations from usersPrevilege where userName='" + this.userNameLoged + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
            conDataBase.Close();

             conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            char[] employees = (new SqlCommand("select employees from usersPrevilege where userName='" + this.userNameLoged + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
            conDataBase.Close();

             conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            char[] reports = (new SqlCommand("select reports from usersPrevilege where userName='" + this.userNameLoged + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
            conDataBase.Close();

             conDataBase = new SqlConnection(constring);
            conDataBase.Open();
            char[] settings = (new SqlCommand("select settings from usersPrevilege where userName='" + this.userNameLoged + "' ", conDataBase).ExecuteScalar().ToString()).ToCharArray();
            conDataBase.Close();
            
            if (categories[0] == '1')
            {
                storesButton.Visible = true;
                categoriesInt++;
            }
            else
            {
                storesButton.Visible = false;
            }

            if (categories[1] == '1')
            {

                companyAddingButton.Visible = true;
                categoriesInt++;

            }
            else
            {
                companyAddingButton.Visible = false;
            }


            if (categories[2] == '1')
            {
                categoriesInt++;

                unitsButton.Visible = true;
                categoriesInt++;

            }
            else
            {
                unitsButton.Visible = false;
            }


            if (categories[3] == '1')
            {
                categoriesInt++;

                typeButton.Visible = true;
                categoriesInt++;

            }
            else
            {
                typeButton.Visible = false;
            }


            if (categories[4] == '1')
            {
                categoriesInt++;

                sortButton.Visible = true;
            }
            else
            {
                sortButton.Visible = false;
            }


            if (categories[5] == '1')
            {
                categoriesInt++;

                subUnitButton.Visible = true;
            }
            else
            {
                subUnitButton.Visible = false;
            }

            if (categories[6] == '1')
            {
                categoriesInt++;

                safeButton.Visible = true;
            }
            else
            {
                safeButton.Visible = false;
            }

            if (categories[7] == '1')
            {
                categoriesInt++;

                exportExcelButton.Visible = true;
            }
            else
            {
                exportExcelButton.Visible = false;
            }

            if (categories[8] == '1')
            {
                categoriesInt++;

                importExcelButton.Visible = true;
            }
            else
            {
                importExcelButton.Visible = false;
            }

            if (categoriesInt > 0)
            {
                الأصنافToolStripMenuItem.Visible = true;
            }
            else
            {
                الأصنافToolStripMenuItem.Visible = false;

            }


            if (store[0] == '1')
            {
                storeInt++;
                categoryQuantityButton.Visible = true;
            }
            else
            {
                categoryQuantityButton.Visible = false;
            }

            if (store[1] == '1')
            {
                storeInt++;
                permissionSubtractionButton.Visible = true;
            }
            else
            {
                permissionSubtractionButton.Visible = false;
            }


            if (store[2] == '1')
            {
                storeInt++;
                permissionAdditionButton.Visible = true;
            }
            else
            {
                permissionAdditionButton.Visible = false;
            }


            if (store[3] == '1')
            {
                storeInt++;
                destroyedButton.Visible = true;
            }
            else
            {
                destroyedButton.Visible = false;
            }


            if (store[4] == '1')
            {
                storeInt++;
                transportButton.Visible = true;
            }
            else
            {
                transportButton.Visible = false;
            }


            if (store[5] == '1')
            {
                storeInt++;
                showStoreButton.Visible = true;
            }
            else
            {
                showStoreButton.Visible = false;
            }

            if(storeInt > 0)
            {
                المخزنToolStripMenuItem.Visible = true;
            }
            else
            {
                المخزنToolStripMenuItem.Visible = false;
            }

            if (purchases[0] == '1')
            {
                purchasesInt++;
                supplierButton.Visible = true;
            }
            else
            {
                supplierButton.Visible = false;
            }

            if (purchases[1] == '1')
            {
                purchasesInt++;
                purchasesButton.Visible = true;
            }
            else
            {
                purchasesButton.Visible = false;
            }


            if (purchases[2] == '1')
            {
                purchasesInt++;
                returnedPurchasesButton.Visible = true;
            }
            else
            {
                returnedPurchasesButton.Visible = false;
            }


            if (purchases[3] == '1')
            {
                purchasesInt++;
                supplierBalanceButton.Visible = true;
            }
            else
            {
                supplierBalanceButton.Visible = false;
            }


            if (purchases[4] == '1')
            {
                purchasesInt++;
                dollarCalculationsButton.Visible = true;
            }
            else
            {
                dollarCalculationsButton.Visible = false;
            }

            if(purchasesInt > 0)
            {
                المشترياتToolStripMenuItem.Visible = true;
            }
            else
            {
                المشترياتToolStripMenuItem.Visible = false;
            }


            if (sales[0] == '1')
            {
                salesInt++;
                customersButton.Visible = true;
            }
            else
            {
                customersButton.Visible = false;
            }

            if (sales[1] == '1')
            {
                salesInt++;
                salesButton.Visible = true;
            }
            else
            {
                salesButton.Visible = false;
            }


            if (sales[2] == '1')
            {
                salesInt++;
                statmentButton.Visible = true;
            }
            else
            {
                statmentButton.Visible = false;
            }


            if (sales[3] == '1')
            {
                salesInt++;
                customerBalanceButton.Visible = true;
            }
            else
            {
                customerBalanceButton.Visible = false;
            }


            if (sales[4] == '1')
            {
                salesInt++;
                duesButton.Visible = true;
            }
            else
            {
                duesButton.Visible = false;
            }


            if (sales[5] == '1')
            {
                salesInt++;
                payDivisionButton.Visible = true;
            }
            else
            {
                payDivisionButton.Visible = false;
            }

            if (sales[6] == '1')
            {
                salesInt++;
                reDivideDebtsButton.Visible = true;
            }
            else
            {
                reDivideDebtsButton.Visible = false;
            }

            if (sales[7] == '1')
            {
                salesInt++;
                exportCustomersButton.Visible = true;
            }
            else
            {
                exportCustomersButton.Visible = false;
            }

            if (sales[8] == '1')
            {
                salesInt++;
                importCustomersButton.Visible = true;
            }
            else
            {
                importCustomersButton.Visible = false;
            }

            if(salesInt > 0)
            {
                المبيعاتToolStripMenuItem.Visible = true;
            }
            else
            {
                المبيعاتToolStripMenuItem.Visible = false;
            }

            if (calculations[0] == '1')
            {
                calculationsInt++;
                addStoreToUser.Visible = true;
            }
            else
            {
                addStoreToUser.Visible = false;
            }

            if (calculations[1] == '1')
            {
                calculationsInt++;
                safeTransferButton.Visible = true;
            }
            else
            {
                safeTransferButton.Visible = false;
            }


            if (calculations[2] == '1')
            {
                calculationsInt++;
                initialsButton.Visible = true;
            }
            else
            {
                initialsButton.Visible = false;
            }


            if (calculations[3] == '1')
            {
                calculationsInt++;
                fixedPotentialsButton.Visible = true;
            }
            else
            {
                fixedPotentialsButton.Visible = false;
            }


            if (calculations[4] == '1')
            {
                calculationsInt++;
                potentialDamgeButton.Visible = true;
            }
            else
            {
                potentialDamgeButton.Visible = false;
            }


            if (calculations[5] == '1')
            {
                calculationsInt++;
                partnersButton.Visible = true;
            }
            else
            {
                partnersButton.Visible = false;
            }

            if (calculations[6] == '1')
            {
                calculationsInt++;
                generalSpendingButton.Visible = true;
            }
            else
            {
                generalSpendingButton.Visible = false;
            }

            if (calculations[7] == '1')
            {
                calculationsInt++;
                anotherIncomeButton.Visible = true;
            }
            else
            {
                anotherIncomeButton.Visible = false;
            }

            if (calculations[8] == '1')
            {
                calculationsInt++;
                loansButton.Visible = true;
            }
            else
            {
                loansButton.Visible = false;
            }

            if (calculations[9] == '1')
            {
                calculationsInt++;
                advanceButton.Visible = true;
            }
            else
            {
                advanceButton.Visible = false;
            }

            if(calculationsInt > 0)
            {
                الحساباتToolStripMenuItem.Visible = true;
            }
            else
            {
                الحساباتToolStripMenuItem.Visible = false;
            }

            if (employees[0] == '1')
            {
                employeesInt++;
                employeesGeneralSettingsButton.Visible = true;
            }
            else
            {
                employeesGeneralSettingsButton.Visible = false;
            }

            if (employees[1] == '1')
            {
                employeesInt++;
                employeesAdditionButton.Visible = true;
            }
            else
            {
                employeesAdditionButton.Visible = false;
            }


            if (employees[2] == '1')
            {
                employeesInt++;
                employeeSpecifiedSettingsButton.Visible = true;
            }
            else
            {
                employeeSpecifiedSettingsButton.Visible = false;
            }


            if (employees[3] == '1')
            {
                employeesInt++;
                comeGoingButton.Visible = true;
            }
            else
            {
                comeGoingButton.Visible = false;
            }


            if (employees[4] == '1')
            {
                employeesInt++;
                getComeGoFileButton.Visible = true;
            }
            else
            {
                getComeGoFileButton.Visible = false;
            }


            if (employees[5] == '1')
            {
                employeesInt++;
                employeeAbscenceButton.Visible = true;
            }
            else
            {
                employeeAbscenceButton.Visible = false;
            }

            if (employees[6] == '1')
            {
                employeesInt++;
                lateButton.Visible = true;
            }
            else
            {
                lateButton.Visible = false;
            }

            if (employees[7] == '1')
            {
                employeesInt++;
                employeeAdditionalButton.Visible = true;
            }
            else
            {
                employeeAdditionalButton.Visible = false;
            }

            if (employees[8] == '1')
            {
                employeesInt++;
                wagesButton.Visible = true;
            }
            else
            {
                wagesButton.Visible = false;
            }

            if (employees[9] == '1')
            {
                employeesInt++;
                employeesReportsButton.Visible = true;
            }
            else
            {
                employeesReportsButton.Visible = false;
            }

            if(employeesInt > 0)
            {
                الموظفينToolStripMenuItem.Visible = true;
            }
            else
            {
                الموظفينToolStripMenuItem.Visible = false;
            }

            if (reports[0] == '1')
            {
                reportsInt++;
                salesReportsButton.Visible = true;
            }
            else
            {
                salesReportsButton.Visible = false;
            }

            if (reports[1] == '1')
            {
                reportsInt++;
                customersReportsButton.Visible = true;
            }
            else
            {
                customersReportsButton.Visible = false;
            }


            if (reports[2] == '1')
            {
                reportsInt++;
                purchasesReportsButton.Visible = true;
            }
            else
            {
                purchasesReportsButton.Visible = false;
            }


            if (reports[3] == '1')
            {
                reportsInt++;
                suppliersReportsButton.Visible = true;
            }
            else
            {
                suppliersReportsButton.Visible = false;
            }


            if (reports[4] == '1')
            {
                reportsInt++;
                cashReportsButton.Visible = true;
            }
            else
            {
                cashReportsButton.Visible = false;
            }


            if (reports[5] == '1')
            {
                reportsInt++;
                storesReportsButton.Visible = true;
            }
            else
            {
                storesReportsButton.Visible = false;
            }

            if (reports[6] == '1')
            {
                reportsInt++;
                calculationsReportsbutton.Visible = true;
            }
            else
            {
                calculationsReportsbutton.Visible = false;
            }

            if (reports[7] == '1')
            {
                reportsInt++;
                usersLoginsReportsButton.Visible = true;
            }
            else
            {
                usersLoginsReportsButton.Visible = false;
            }


            if(reportsInt > 0)
            {
                التقاريرToolStripMenuItem.Visible = true;
            }
            else
            {
                التقاريرToolStripMenuItem.Visible = false;
            }


            if (settings[0] == '1')
            {
                settingsInt++;
                addUsersBuuton.Visible = true;
            }
            else
            {
                addUsersBuuton.Visible = false;
            }

            if (settings[1] == '1')
            {
                settingsInt++;
                userPrivilegeButton.Visible = true;
            }
            else
            {
                userPrivilegeButton.Visible = false;
            }

            if (settingsInt>0)
            {
                الإعدادتToolStripMenuItem.Visible = true;
            }
            else
            {
                الإعدادتToolStripMenuItem.Visible = false;
            }

            string Query = "INSERT INTO usersLoginTable(userName,login) VALUES (N'" + this.userNameLoged+ "',GetDate())  ";
            conDataBase = new SqlConnection(constring);
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
 
            }
            conDataBase.Close();

        }

        private void shortcutsCollapseButton_Click(object sender, EventArgs e)
        {
            if (shortcutsIsCollapsed)
            {
                shortcutsCollapseButton.Image = SofterFertilizers.Properties.Resources.icons8_chevron_down_32___Copy;
                shortcutsDropDownPanel.Size = shortcutsDropDownPanel.MaximumSize;
                shortcutsIsCollapsed=false;


            }
            else
            {
                shortcutsCollapseButton.Image = SofterFertilizers.Properties.Resources.icons8_chevron_down_32;
                shortcutsDropDownPanel.Size = shortcutsDropDownPanel.MinimumSize;
                shortcutsIsCollapsed = true;
            }
            
        }

        public class TestColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(192, 27, 55); }
            }

            public override Color MenuBorder
            {
                get { return Color.FromArgb(192, 27, 55); }
            }
            public override Color MenuItemBorder
            {
                get { return Color.FromArgb(192, 27, 55); }
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(192, 27, 55); }
            }

            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(192, 27, 55); }
            }

            public override Color MenuItemPressedGradientBegin
            {
                
                get { return Color.FromArgb(192, 27, 55); }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.FromArgb(192, 27, 55); }
            }
        }
        //TODO user Log
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            categoryDGV.DataBindings.Clear();
            SqlDataReader myReader;

            string Query = "select categoryTable.Id,  sellingPrice , packagePrice , buyingPrice, storeName ,quantity from CategoryQuantityTable, CategoryTable where categoryTable.Id = CategoryQuantityTable.CategoryNumber;";

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

            for (int i = 0; i < categoryDGV.Rows.Count - 1; i++)
            {

                Query = "IF NOT EXISTS (select 1 from categoryUpdateMainTable where date = CONVERT(date, getdate()) and categoryNumber=N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.categoryDGV.Rows[i].Cells[4].Value.ToString() + "' ) INSERT INTO categoryUpdateMainTable(categoryNumber,sellingPrice,packagePrice,buyingPrice,date,storeName,quantity) VALUES (N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "',N'" + this.categoryDGV.Rows[i].Cells[1].Value.ToString() + "',N'" + this.categoryDGV.Rows[i].Cells[2].Value.ToString() + "',N'" + this.categoryDGV.Rows[i].Cells[3].Value.ToString() + "',CONVERT(date, getdate()),N'" + this.categoryDGV.Rows[i].Cells[4].Value.ToString() + "',N'" + this.categoryDGV.Rows[i].Cells[5].Value.ToString() + "') ELSE UPDATE categoryUpdateMainTable SET categoryNumber=N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "',sellingPrice=N'" + this.categoryDGV.Rows[i].Cells[1].Value.ToString() + "',packagePrice=N'" + this.categoryDGV.Rows[i].Cells[2].Value.ToString() + "', buyingPrice=N'" + this.categoryDGV.Rows[i].Cells[3].Value.ToString() + "',storeName=N'" + this.categoryDGV.Rows[i].Cells[4].Value.ToString() + "',quantity= N'" + this.categoryDGV.Rows[i].Cells[5].Value.ToString() + "' where date = CONVERT(date, getdate()) and categoryNumber=N'" + this.categoryDGV.Rows[i].Cells[0].Value.ToString() + "' and storeName=N'" + this.categoryDGV.Rows[i].Cells[4].Value.ToString() + "'";
                conDataBase = new SqlConnection(constring);
                cmdDataBase = new SqlCommand(Query, conDataBase);
                
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
     
                }
            }

            Query = "Update usersLoginTable Set logout = GetDate() where userName=N'"+this.userNameLoged+"' and logout IS NULL  ";
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
            conDataBase.Close();
            */
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.label2.Text = dateTime.ToString();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            mainMenuStrip.Renderer = new ToolStripProfessionalRenderer(new TestColorTable());
            mainMenuStrip.BackColor = Color.FromArgb(41, 44, 51);
            mainMenuStrip.ForeColor = Color.White;
        }

        private void storesButton_Click(object sender, EventArgs e)
        {
            SAUC.refreshLocal();
            clearMain();
            closeForms();
            SAUC.Visible = true;

            if (!this.Controls.Contains(SAUC))
            {
                SAUC.Left = (this.ClientSize.Width - SAUC.Width) / 2;
                SAUC.Top = (this.ClientSize.Height - SAUC.Height) / 2;
                this.Controls.Add(SAUC);
                SAUC.Visible = true;
            }
        }

        private void companyAddingButton_Click(object sender, EventArgs e)
        {
            CUC.refreshButton();
            clearMain();
            closeForms();
            CUC.Visible = true;

            if (!this.Controls.Contains(CUC))
            {
                CUC.Left = (this.ClientSize.Width - CUC.Width) / 2;
                CUC.Top = (this.ClientSize.Height - CUC.Height) / 2;
                this.Controls.Add(CUC);
                CUC.Visible = true;
            }
        }

        private void typeButton_Click(object sender, EventArgs e)
        {
            TUC.refreshLocal();
            clearMain();
            closeForms();
            TUC.Visible = true;

            if (!this.Controls.Contains(TUC))
            {
                TUC.Left = (this.ClientSize.Width - TUC.Width) / 2;
                TUC.Top = (this.ClientSize.Height - TUC.Height) / 2;
                this.Controls.Add(TUC);
                TUC.Visible = true;
            }
        }

        private void unitsButton_Click(object sender, EventArgs e)
        {
            UUC.refreshLoacl();
            clearMain();
            closeForms();
            UUC.Visible = true;

            if (!this.Controls.Contains(UUC))
            {
                UUC.Left = (this.ClientSize.Width - UUC.Width) / 2;
                UUC.Top = (this.ClientSize.Height - UUC.Height) / 2;
                this.Controls.Add(UUC);
                UUC.Visible = true;
            }
        }

        private void sortButton_Click(object sender, EventArgs e)
        {

            SUC.refresh();

            clearMain();
            closeForms();
            SUC.Visible = true;

            if (!this.Controls.Contains(SUC))
            {
                SUC.Left = (this.ClientSize.Width - SUC.Width) / 2;
                SUC.Top = (this.ClientSize.Height - SUC.Height) / 2;
                this.Controls.Add(SUC);
                SUC.Visible = true;
            }
        }

       

        private void subUnitButton_Click(object sender, EventArgs e)
        {
            SUUC.refreshLocal();
            clearMain();
            closeForms();
            SUUC.Visible = true;

            if (!this.Controls.Contains(SUUC))
            {
                SUUC.Left = (this.ClientSize.Width - SUUC.Width) / 2;
                SUUC.Top = (this.ClientSize.Height - SUUC.Height) / 2;
                this.Controls.Add(SUUC);
                SUUC.Visible = true;
            }
        }



        private void safeButton_Click(object sender, EventArgs e)
        {
            SfUC.refreshLocal();
            clearMain();
            closeForms();
            SfUC.Visible = true;

            if (!this.Controls.Contains(SfUC))
            {
                SfUC.Left = (this.ClientSize.Width - SfUC.Width) / 2;
                SfUC.Top = (this.ClientSize.Height - SfUC.Height) / 2;
                this.Controls.Add(SfUC);
                SfUC.Visible = true;
            }
        }

        private void exportExcelButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            STEXC.Visible = true;

            if (!this.Controls.Contains(STEXC))
            {
                STEXC.Left = (this.ClientSize.Width - STEXC.Width) / 2;
                STEXC.Top = (this.ClientSize.Height - STEXC.Height) / 2;
                this.Controls.Add(STEXC);
                STEXC.Visible = true;
            }
        }

        private void importExcelButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            IEXC.Visible = true;

            if (!this.Controls.Contains(IEXC))
            {
                IEXC.Left = (this.ClientSize.Width - IEXC.Width) / 2;
                IEXC.Top = (this.ClientSize.Height - IEXC.Height) / 2;
                this.Controls.Add(IEXC);
                IEXC.Visible = true;
            }
        }

        //Store UCs

        private void permissionAdditionButton_Click(object sender, EventArgs e)
        {
            CAPUC.refreshLocal();
            clearMain();
            closeForms();
            CAPUC.Visible = true;

            if (!this.Controls.Contains(CAPUC))
            {
                CAPUC.Left = (this.ClientSize.Width - CAPUC.Width) / 2;
                CAPUC.Top = (this.ClientSize.Height - CAPUC.Height) / 2;
                this.Controls.Add(CAPUC);
                CAPUC.Visible = true;
            }
        }

        private void permissionSubtractionButton_Click(object sender, EventArgs e)
        {
            CSPUC.refreshLoacl();
            clearMain();
            closeForms();
            CSPUC.Visible = true;

            if (!this.Controls.Contains(CSPUC))
            {
                CSPUC.Left = (this.ClientSize.Width - CSPUC.Width) / 2;
                CSPUC.Top = (this.ClientSize.Height - CSPUC.Height) / 2;
                this.Controls.Add(CSPUC);
                CSPUC.Visible = true;
            }
        }

        private void categoryQuantityButton_Click(object sender, EventArgs e)
        {
            CQUC.refreshLocal();
            clearMain();
            closeForms();
            CQUC.Visible = true;

            if (!this.Controls.Contains(CQUC))
            {
                CQUC.Left = (this.ClientSize.Width - CQUC.Width) / 2;
                CQUC.Top = (this.ClientSize.Height - CQUC.Height) / 2;
                this.Controls.Add(CQUC);
                CQUC.Visible = true;
            }
        }

        private void showStoreButton_Click(object sender, EventArgs e)
        {
            SSUC.refreshLoacl();
            clearMain();
            closeForms();
            SSUC.Visible = true;

            if (!this.Controls.Contains(SSUC))
            {
                SSUC.Left = (this.ClientSize.Width - SSUC.Width) / 2;
                SSUC.Top = (this.ClientSize.Height - SSUC.Height) / 2;
                this.Controls.Add(SSUC);
                SSUC.Visible = true;
            }
        }

        private void destroyedButton_Click(object sender, EventArgs e)
        {
            DAUC.refreshLocal();
            clearMain();
            closeForms();
            DAUC.Visible = true;

            if (!this.Controls.Contains(DAUC))
            {
                DAUC.Left = (this.ClientSize.Width - DAUC.Width) / 2;
                DAUC.Top = (this.ClientSize.Height - DAUC.Height) / 2;
                this.Controls.Add(DAUC);
                DAUC.Visible = true;
            }

        }

        private void transportButton_Click(object sender, EventArgs e)
        {
            TSUC.refreshLocal();
            clearMain();
            closeForms();
            TSUC.Visible = true;

            if (!this.Controls.Contains(TSUC))
            {
                TSUC.Left = (this.ClientSize.Width - TSUC.Width) / 2;
                TSUC.Top = (this.ClientSize.Height - TSUC.Height) / 2;
                this.Controls.Add(TSUC);
                TSUC.Visible = true;
            }
        }

        private void supplierButton_Click(object sender, EventArgs e)
        {
            SpUC.refreshLoacl();
            clearMain();
            closeForms();
            SpUC.Visible = true;

            if (!this.Controls.Contains(SpUC))
            {
                SpUC.Left = (this.ClientSize.Width - SpUC.Width) / 2;
                SpUC.Top = (this.ClientSize.Height - SpUC.Height) / 2;
                this.Controls.Add(SpUC);
                SpUC.Visible = true;
            }
        }

        private void purchasesButton_Click(object sender, EventArgs e)
        {
            PUC.refreshLocal();
            clearMain();
            closeForms();
            PUC.Visible = true;

            if (!this.Controls.Contains(PUC))
            {
                PUC.Left = (this.ClientSize.Width - PUC.Width) / 2;
                PUC.Top = (this.ClientSize.Height - PUC.Height) / 2;
                this.Controls.Add(PUC);
                PUC.Visible = true;
            }

        }
       

        private void returnedPurchasesButton_Click(object sender, EventArgs e)
        {
            RPUC.refreshLocal();
            clearMain();
            closeForms();
            RPUC.Visible = true;

            if (!this.Controls.Contains(RPUC))
            {
                RPUC.Left = (this.ClientSize.Width - RPUC.Width) / 2;
                RPUC.Top = (this.ClientSize.Height - RPUC.Height) / 2;
                this.Controls.Add(RPUC);
                RPUC.Visible = true;
            }
        }

        private void supplierBalanceButton_Click(object sender, EventArgs e)
        {
            SBUC.refreshLocal();
            clearMain();
            closeForms();
            SBUC.Visible = true;

            if (!this.Controls.Contains(SBUC))
            {
                SBUC.Left = (this.ClientSize.Width - SBUC.Width) / 2;
                SBUC.Top = (this.ClientSize.Height - SBUC.Height) / 2;
                this.Controls.Add(SBUC);
                SBUC.Visible = true;
            }
        }

        private void dollarCalculationsButton_Click(object sender, EventArgs e)
        {
            DCUC.refreshLocal();
            clearMain();
            closeForms();
            DCUC.Visible = true;

            if (!this.Controls.Contains(DCUC))
            {
                DCUC.Left = (this.ClientSize.Width - DCUC.Width) / 2;
                DCUC.Top = (this.ClientSize.Height - DCUC.Height) / 2;
                this.Controls.Add(DCUC);
                DCUC.Visible = true;
            }
        }

        private void customersButton_Click(object sender, EventArgs e)
        {
            CustUC.refreshLocal();
            clearMain();
            closeForms();
            CustUC.Visible = true;

            if (!this.Controls.Contains(CustUC))
            {
                CustUC.Left = (this.ClientSize.Width - CustUC.Width) / 2;
                CustUC.Top = (this.ClientSize.Height - CustUC.Height) / 2;
                this.Controls.Add(CustUC);
                CustUC.Visible = true;
            }
        }

        private void salesButton_Click(object sender, EventArgs e)
        {
           
            SalesUC.refreshLocal();

            clearMain();
            closeForms();
            SalesUC.Visible = true;

            if (!this.Controls.Contains(SalesUC))
            {
                SalesUC.Left = (this.ClientSize.Width - SalesUC.Width) / 2;
                SalesUC.Top = (this.ClientSize.Height - SalesUC.Height) / 2;
                this.Controls.Add(SalesUC);
                SalesUC.Visible = true;
            }

        }

        private void statmentButton_Click(object sender, EventArgs e)
        {
            StatementUC.refreshLocal();
            clearMain();
            closeForms();
            StatementUC.Visible = true;

            if (!this.Controls.Contains(StatementUC))
            {
                StatementUC.Left = (this.ClientSize.Width - StatementUC.Width) / 2;
                StatementUC.Top = (this.ClientSize.Height - StatementUC.Height) / 2;
                this.Controls.Add(StatementUC);
                StatementUC.Visible = true;
            }
        }

        private void customerBalanceButton_Click(object sender, EventArgs e)
        {
            CustBalancesUC.refreshLocal();
            clearMain();
            closeForms();
            CustBalancesUC.Visible = true;

            if (!this.Controls.Contains(CustBalancesUC))
            {
                CustBalancesUC.Left = (this.ClientSize.Width - CustBalancesUC.Width) / 2;
                CustBalancesUC.Top = (this.ClientSize.Height - CustBalancesUC.Height) / 2;
                this.Controls.Add(CustBalancesUC);
                CustBalancesUC.Visible = true;
            }
        }

        private void duesButton_Click(object sender, EventArgs e)
        {
            duesUC.refreshLocal();
            clearMain();
            closeForms();
            duesUC.Visible = true;

            if (!this.Controls.Contains(duesUC))
            {
                duesUC.Left = (this.ClientSize.Width - duesUC.Width) / 2;
                duesUC.Top = (this.ClientSize.Height - duesUC.Height) / 2;
                this.Controls.Add(duesUC);
                duesUC.Visible = true;
            }
        }

        private void payDivisionButton_Click(object sender, EventArgs e)
        {
            payDivisionUC.refreshLocal();

            clearMain();
            closeForms();
            payDivisionUC.Visible = true;

            if (!this.Controls.Contains(payDivisionUC))
            {
                payDivisionUC.Left = (this.ClientSize.Width - payDivisionUC.Width) / 2;
                payDivisionUC.Top = (this.ClientSize.Height - payDivisionUC.Height) / 2;
                this.Controls.Add(payDivisionUC);
                payDivisionUC.Visible = true;
            }
        }

        private void reDivideDebtsButton_Click(object sender, EventArgs e)
        {
            RDDUC.refreshLocal();
            clearMain();
            closeForms();
            RDDUC.Visible = true;

            if (!this.Controls.Contains(RDDUC))
            {
                RDDUC.Left = (this.ClientSize.Width - RDDUC.Width) / 2;
                RDDUC.Top = (this.ClientSize.Height - RDDUC.Height) / 2;
                this.Controls.Add(RDDUC);
                RDDUC.Visible = true;
            }
        }

        private void exportCustomersButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            ECUC.Visible = true;

            if (!this.Controls.Contains(ECUC))
            {
                ECUC.Left = (this.ClientSize.Width - ECUC.Width) / 2;
                ECUC.Top = (this.ClientSize.Height - ECUC.Height) / 2;
                this.Controls.Add(ECUC);
                ECUC.Visible = true;
            }

        }

        private void salesFlowReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SFRUC.Visible = true;

            if (!this.Controls.Contains(SFRUC))
            {
                SFRUC.Left = (this.ClientSize.Width - SFRUC.Width) / 2;
                SFRUC.Top = (this.ClientSize.Height - SFRUC.Height) / 2;
                this.Controls.Add(SFRUC);
                SFRUC.Visible = true;
            }
        }

        private void companySalesFlowButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CSFRUC.Visible = true;

            if (!this.Controls.Contains(CSFRUC))
            {
                CSFRUC.Left = (this.ClientSize.Width - CSFRUC.Width) / 2;
                CSFRUC.Top = (this.ClientSize.Height - CSFRUC.Height) / 2;
                this.Controls.Add(CSFRUC);
                CSFRUC.Visible = true;
            }
        }

        private void typeSalesFlowButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            TSFRUC.Visible = true;

            if (!this.Controls.Contains(TSFRUC))
            {
                TSFRUC.Left = (this.ClientSize.Width - TSFRUC.Width) / 2;
                TSFRUC.Top = (this.ClientSize.Height - TSFRUC.Height) / 2;
                this.Controls.Add(TSFRUC);
                TSFRUC.Visible = true;
            }
        }

        private void overallSalesFlowButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            OSFRUC.Visible = true;

            if (!this.Controls.Contains(OSFRUC))
            {
                OSFRUC.Left = (this.ClientSize.Width - OSFRUC.Width) / 2;
                OSFRUC.Top = (this.ClientSize.Height - OSFRUC.Height) / 2;
                this.Controls.Add(OSFRUC);
                OSFRUC.Visible = true;
            }
        }

        private void highestBoughtButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            HBSFRUC.Visible = true;

            if (!this.Controls.Contains(HBSFRUC))
            {
                HBSFRUC.Left = (this.ClientSize.Width - HBSFRUC.Width) / 2;
                HBSFRUC.Top = (this.ClientSize.Height - HBSFRUC.Height) / 2;
                this.Controls.Add(HBSFRUC);
                HBSFRUC.Visible = true;
            }
        }

       

        private void soloPackageButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SPSFRUC.Visible = true;

            if (!this.Controls.Contains(SPSFRUC))
            {
                SPSFRUC.Left = (this.ClientSize.Width - SPSFRUC.Width) / 2;
                SPSFRUC.Top = (this.ClientSize.Height - SPSFRUC.Height) / 2;
                this.Controls.Add(SPSFRUC);
                SPSFRUC.Visible = true;
            }
        }

        private void soloPackageCategoryButton_Click(object sender, EventArgs e)
        {

            clearMain();
            closeForms();
            SPCSFRUC.Visible = true;

            if (!this.Controls.Contains(SPCSFRUC))
            {
                SPCSFRUC.Left = (this.ClientSize.Width - SPCSFRUC.Width) / 2;
                SPCSFRUC.Top = (this.ClientSize.Height - SPCSFRUC.Height) / 2;
                this.Controls.Add(SPCSFRUC);
                SPCSFRUC.Visible = true;
            }
        }

        private void storeCategoryButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SCSFRUC.Visible = true;

            if (!this.Controls.Contains(SCSFRUC))
            {
                SCSFRUC.Left = (this.ClientSize.Width - SCSFRUC.Width) / 2;
                SCSFRUC.Top = (this.ClientSize.Height - SCSFRUC.Height) / 2;
                this.Controls.Add(SCSFRUC);
                SCSFRUC.Visible = true;
            }
        }

        private void customersDetailsReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CDRUC.Visible = true;

            if (!this.Controls.Contains(CDRUC))
            {
                CDRUC.Left = (this.ClientSize.Width - CDRUC.Width) / 2;
                CDRUC.Top = (this.ClientSize.Height - CDRUC.Height) / 2;
                this.Controls.Add(CDRUC);
                CDRUC.Visible = true;
            }
        }

        

        private void customerBalanceDetailsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CBDRUC.Visible = true;

            if (!this.Controls.Contains(CBDRUC))
            {
                CBDRUC.Left = (this.ClientSize.Width - CBDRUC.Width) / 2;
                CBDRUC.Top = (this.ClientSize.Height - CBDRUC.Height) / 2;
                this.Controls.Add(CBDRUC);
                CBDRUC.Visible = true;
            }
        }

        private void customersSalesBillsReportButtton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CSBR.Visible = true;

            if (!this.Controls.Contains(CSBR))
            {
                CSBR.Left = (this.ClientSize.Width - CSBR.Width) / 2;
                CSBR.Top = (this.ClientSize.Height - CSBR.Height) / 2;
                this.Controls.Add(CSBR);
                CSBR.Visible = true;
            }
        }

        private void customerCategoryReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CCR.Visible = true;

            if (!this.Controls.Contains(CCR))
            {
                CCR.Left = (this.ClientSize.Width - CCR.Width) / 2;
                CCR.Top = (this.ClientSize.Height - CCR.Height) / 2;
                this.Controls.Add(CCR);
                CCR.Visible = true;
            }

        }

        private void customerCompanyReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CCompR.Visible = true;

            if (!this.Controls.Contains(CCompR))
            {
                CCompR.Left = (this.ClientSize.Width - CCompR.Width) / 2;
                CCompR.Top = (this.ClientSize.Height - CCompR.Height) / 2;
                this.Controls.Add(CCompR);
                CCompR.Visible = true;
            }
        }

        private void customerTypeReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CTR.Visible = true;

            if (!this.Controls.Contains(CTR))
            {
                CTR.Left = (this.ClientSize.Width - CTR.Width) / 2;
                CTR.Top = (this.ClientSize.Height - CTR.Height) / 2;
                this.Controls.Add(CTR);
                CTR.Visible = true;
            }
        }

        private void customerCategorySumReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CCSR.Visible = true;

            if (!this.Controls.Contains(CCSR))
            {
                CCSR.Left = (this.ClientSize.Width - CCSR.Width) / 2;
                CCSR.Top = (this.ClientSize.Height - CCSR.Height) / 2;
                this.Controls.Add(CCSR);
                CCSR.Visible = true;
            }
        }

        private void customerHighestSales_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CHSR.Visible = true;

            if (!this.Controls.Contains(CHSR))
            {
                CHSR.Left = (this.ClientSize.Width - CHSR.Width) / 2;
                CHSR.Top = (this.ClientSize.Height - CHSR.Height) / 2;
                this.Controls.Add(CHSR);
                CHSR.Visible = true;
            }
        }

        private void customerQuantityCompareButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CQCR.Visible = true;

            if (!this.Controls.Contains(CQCR))
            {
                CQCR.Left = (this.ClientSize.Width - CQCR.Width) / 2;
                CQCR.Top = (this.ClientSize.Height - CQCR.Height) / 2;
                this.Controls.Add(CQCR);
                CQCR.Visible = true;
            }
        }

        private void customerDivisionReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CDR.Visible = true;

            if (!this.Controls.Contains(CDR))
            {
                CDR.Left = (this.ClientSize.Width - CDR.Width) / 2;
                CDR.Top = (this.ClientSize.Height - CDR.Height) / 2;
                this.Controls.Add(CDR);
                CDR.Visible = true;
            }
        }

        private void customerDivisionDateButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CDDR.Visible = true;

            if (!this.Controls.Contains(CDDR))
            {
                CDDR.Left = (this.ClientSize.Width - CDDR.Width) / 2;
                CDDR.Top = (this.ClientSize.Height - CDDR.Height) / 2;
                this.Controls.Add(CDDR);
                CDDR.Visible = true;
            }
        }

        private void customerDivisionPaymentButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CDPR.Visible = true;

            if (!this.Controls.Contains(CDPR))
            {
                CDPR.Left = (this.ClientSize.Width - CDPR.Width) / 2;
                CDPR.Top = (this.ClientSize.Height - CDPR.Height) / 2;
                this.Controls.Add(CDPR);
                CDPR.Visible = true;
            }
        }

        private void customerDivisionDetailsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CDDetailsR.Visible = true;

            if (!this.Controls.Contains(CDDetailsR))
            {
                CDDetailsR.Left = (this.ClientSize.Width - CDDetailsR.Width) / 2;
                CDDetailsR.Top = (this.ClientSize.Height - CDDetailsR.Height) / 2;
                this.Controls.Add(CDDetailsR);
                CDDetailsR.Visible = true;
            }
        }

        private void purchasesFlowReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            PFR.Visible = true;

            if (!this.Controls.Contains(PFR))
            {
                PFR.Left = (this.ClientSize.Width - PFR.Width) / 2;
                PFR.Top = (this.ClientSize.Height - PFR.Height) / 2;
                this.Controls.Add(PFR);
                PFR.Visible = true;
            }
        }

        private void returnedPurchasesReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            RPFR.Visible = true;

            if (!this.Controls.Contains(RPFR))
            {
                RPFR.Left = (this.ClientSize.Width - RPFR.Width) / 2;
                RPFR.Top = (this.ClientSize.Height - RPFR.Height) / 2;
                this.Controls.Add(RPFR);
                RPFR.Visible = true;
            }
        }

        private void companyPurchasesReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            PCFR.Visible = true;

            if (!this.Controls.Contains(PCFR))
            {
                PCFR.Left = (this.ClientSize.Width - PCFR.Width) / 2;
                PCFR.Top = (this.ClientSize.Height - PCFR.Height) / 2;
                this.Controls.Add(PCFR);
                PCFR.Visible = true;
            }
        }

        private void overAllPurchasesReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            OAPFR.Visible = true;

            if (!this.Controls.Contains(OAPFR))
            {
                OAPFR.Left = (this.ClientSize.Width - OAPFR.Width) / 2;
                OAPFR.Top = (this.ClientSize.Height - OAPFR.Height) / 2;
                this.Controls.Add(OAPFR);
                OAPFR.Visible = true;
            }
        }

        private void transportCostREportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            TCR.Visible = true;

            if (!this.Controls.Contains(TCR))
            {
                TCR.Left = (this.ClientSize.Width - TCR.Width) / 2;
                TCR.Top = (this.ClientSize.Height - TCR.Height) / 2;
                this.Controls.Add(TCR);
                TCR.Visible = true;
            }
        }

        private void supplierDetailsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SDR.Visible = true;

            if (!this.Controls.Contains(SDR))
            {
                SDR.Left = (this.ClientSize.Width - SDR.Width) / 2;
                SDR.Top = (this.ClientSize.Height - SDR.Height) / 2;
                this.Controls.Add(SDR);
                SDR.Visible = true;
            }
        }

        private void supplierCategoryReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SCR.Visible = true;

            if (!this.Controls.Contains(SCR))
            {
                SCR.Left = (this.ClientSize.Width - SCR.Width) / 2;
                SCR.Top = (this.ClientSize.Height - SCR.Height) / 2;
                this.Controls.Add(SCR);
                SCR.Visible = true;
            }
        }

        private void supplierTypeReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            STR.Visible = true;

            if (!this.Controls.Contains(STR))
            {
                STR.Left = (this.ClientSize.Width - STR.Width) / 2;
                STR.Top = (this.ClientSize.Height - STR.Height) / 2;
                this.Controls.Add(STR);
                STR.Visible = true;
            }
        }

        private void supplierBalanceReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SBR.Visible = true;

            if (!this.Controls.Contains(SBR))
            {
                SBR.Left = (this.ClientSize.Width - SBR.Width) / 2;
                SBR.Top = (this.ClientSize.Height - SBR.Height) / 2;
                this.Controls.Add(SBR);
                SBR.Visible = true;
            }
        }

        private void supplierCategorySumReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SCSR.Visible = true;

            if (!this.Controls.Contains(SCSR))
            {
                SCSR.Left = (this.ClientSize.Width - SCSR.Width) / 2;
                SCSR.Top = (this.ClientSize.Height - SCSR.Height) / 2;
                this.Controls.Add(SCSR);
                SCSR.Visible = true;
            }

        }

        private void supplierReturnedReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SRR.Visible = true;

            if (!this.Controls.Contains(SRR))
            {
                SRR.Left = (this.ClientSize.Width - SRR.Width) / 2;
                SRR.Top = (this.ClientSize.Height - SRR.Height) / 2;
                this.Controls.Add(SRR);
                SRR.Visible = true;
            }
        }

        private void categoryListReportButtton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CLR.Visible = true;

            if (!this.Controls.Contains(CLR))
            {
                CLR.Left = (this.ClientSize.Width - CLR.Width) / 2;
                CLR.Top = (this.ClientSize.Height - CLR.Height) / 2;
                this.Controls.Add(CLR);
                CLR.Visible = true;
            }
        }

        private void storeCategoryReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SCLR.Visible = true;

            if (!this.Controls.Contains(SCLR))
            {
                SCLR.Left = (this.ClientSize.Width - SCLR.Width) / 2;
                SCLR.Top = (this.ClientSize.Height - SCLR.Height) / 2;
                this.Controls.Add(SCLR);
                SCLR.Visible = true;
            }
        }

        private void categoryStoreReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CSR.Visible = true;

            if (!this.Controls.Contains(CSR))
            {
                CSR.Left = (this.ClientSize.Width - CSR.Width) / 2;
                CSR.Top = (this.ClientSize.Height - CSR.Height) / 2;
                this.Controls.Add(CSR);
                CSR.Visible = true;
            }
        }

        private void storeCompanyReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SComR.Visible = true;

            if (!this.Controls.Contains(SComR))
            {
                SComR.Left = (this.ClientSize.Width - SComR.Width) / 2;
                SComR.Top = (this.ClientSize.Height - SComR.Height) / 2;
                this.Controls.Add(SComR);
                SComR.Visible = true;
            }
        }

        private void storeMinimumReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SMR.Visible = true;

            if (!this.Controls.Contains(SMR))
            {
                SMR.Left = (this.ClientSize.Width - SMR.Width) / 2;
                SMR.Top = (this.ClientSize.Height - SMR.Height) / 2;
                this.Controls.Add(SMR);
                SMR.Visible = true;
            }
        }

        private void storeMaximumReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SMaxR.Visible = true;

            if (!this.Controls.Contains(SMaxR))
            {
                SMaxR.Left = (this.ClientSize.Width - SMaxR.Width) / 2;
                SMaxR.Top = (this.ClientSize.Height - SMaxR.Height) / 2;
                this.Controls.Add(SMaxR);
                SMaxR.Visible = true;
            }
        }

        private void storeDestroyedReport_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SDesR.Visible = true;

            if (!this.Controls.Contains(SDesR))
            {
                SDesR.Left = (this.ClientSize.Width - SDesR.Width) / 2;
                SDesR.Top = (this.ClientSize.Height - SDesR.Height) / 2;
                this.Controls.Add(SDesR);
                SDesR.Visible = true;
            }
        }

        private void storeTransportReportButton_Click(object sender, EventArgs e)
        {
            
            clearMain();
            closeForms();
            STranR.Visible = true;

            if (!this.Controls.Contains(STranR))
            {
                STranR.Left = (this.ClientSize.Width - STranR.Width) / 2;
                STranR.Top = (this.ClientSize.Height - STranR.Height) / 2;
                this.Controls.Add(STranR);
                STranR.Visible = true;
            }
        }

        private void storeBalanceReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SBalR.Visible = true;

            if (!this.Controls.Contains(SBalR))
            {
                SBalR.Left = (this.ClientSize.Width - SBalR.Width) / 2;
                SBalR.Top = (this.ClientSize.Height - SBalR.Height) / 2;
                this.Controls.Add(SBalR);
                SBalR.Visible = true;
            }
        }

        private void safeTransferButton_Click(object sender, EventArgs e)
        {
            TBS.refreshLocal();
            clearMain();
            closeForms();
            TBS.Visible = true;

            if (!this.Controls.Contains(TBS))
            {
                TBS.Left = (this.ClientSize.Width - TBS.Width) / 2;
                TBS.Top = (this.ClientSize.Height - TBS.Height) / 2;
                this.Controls.Add(TBS);
                TBS.Visible = true;
            }
        }

        private void initialMoneyButton_Click(object sender, EventArgs e)
        {
            IMUC.refreshLocal();
            clearMain();
            closeForms();
            IMUC.Visible = true;

            if (!this.Controls.Contains(IMUC))
            {
                IMUC.Left = (this.ClientSize.Width - IMUC.Width) / 2;
                IMUC.Top = (this.ClientSize.Height - IMUC.Height) / 2;
                this.Controls.Add(IMUC);
                IMUC.Visible = true;
            }
        }

        private void bankAccountsButton_Click(object sender, EventArgs e)
        {
            BAUC.refreshLocal();
            clearMain();
            closeForms();
            BAUC.Visible = true;

            if (!this.Controls.Contains(BAUC))
            {
                BAUC.Left = (this.ClientSize.Width - BAUC.Width) / 2;
                BAUC.Top = (this.ClientSize.Height - BAUC.Height) / 2;
                this.Controls.Add(BAUC);
                BAUC.Visible = true;
            }
        }

        private void fixedPotentialsButton_Click(object sender, EventArgs e)
        {
            FPUC.refreshLocal();
            clearMain();
            closeForms();
            FPUC.Visible = true;

            if (!this.Controls.Contains(FPUC))
            {
                FPUC.Left = (this.ClientSize.Width - FPUC.Width) / 2;
                FPUC.Top = (this.ClientSize.Height - FPUC.Height) / 2;
                this.Controls.Add(FPUC);
                FPUC.Visible = true;
            }
        }

        private void potentialDamgeButton_Click(object sender, EventArgs e)
        {
            PDUC.refreshLoacl();
            clearMain();
            closeForms();
            PDUC.Visible = true;

            if (!this.Controls.Contains(PDUC))
            {
                PDUC.Left = (this.ClientSize.Width - PDUC.Width) / 2;
                PDUC.Top = (this.ClientSize.Height - PDUC.Height) / 2;
                this.Controls.Add(PDUC);
                PDUC.Visible = true;
            }
        }

        private void partnersData_Click(object sender, EventArgs e)
        {
            PDataUC.clear();
            clearMain();
            closeForms();
            PDataUC.Visible = true;

            if (!this.Controls.Contains(PDataUC))
            {
                PDataUC.Left = (this.ClientSize.Width - PDataUC.Width) / 2;
                PDataUC.Top = (this.ClientSize.Height - PDataUC.Height) / 2;
                this.Controls.Add(PDataUC);
                PDataUC.Visible = true;
            }
        }

        private void partnersBalanceButton_Click(object sender, EventArgs e)
        {
            POUC.refreshLocal();
            clearMain();
            closeForms();
            POUC.Visible = true;

            if (!this.Controls.Contains(POUC))
            {
                POUC.Left = (this.ClientSize.Width - POUC.Width) / 2;
                POUC.Top = (this.ClientSize.Height - POUC.Height) / 2;
                this.Controls.Add(POUC);
                POUC.Visible = true;
            }

        }

        private void generalSpendingButton_Click(object sender, EventArgs e)
        {
            GSUC.refreshLocal();
            clearMain();
            closeForms();
            GSUC.Visible = true;

            if (!this.Controls.Contains(GSUC))
            {
                GSUC.Left = (this.ClientSize.Width - GSUC.Width) / 2;
                GSUC.Top = (this.ClientSize.Height - GSUC.Height) / 2;
                this.Controls.Add(GSUC);
                GSUC.Visible = true;
            }
        }

        private void anotherIncomeButton_Click(object sender, EventArgs e)
        {
            AIUC.refreshLocal();
            clearMain();
            closeForms();
            AIUC.Visible = true;

            if (!this.Controls.Contains(AIUC))
            {
                AIUC.Left = (this.ClientSize.Width - AIUC.Width) / 2;
                AIUC.Top = (this.ClientSize.Height - AIUC.Height) / 2;
                this.Controls.Add(AIUC);
                AIUC.Visible = true;
            }

        }

        private void addLoanButton_Click(object sender, EventArgs e)
        {
            AddLoanUC.refreshLocal();
            clearMain();
            closeForms();
            AddLoanUC.Visible = true;

            if (!this.Controls.Contains(AddLoanUC))
            {
                AddLoanUC.Left = (this.ClientSize.Width - AddLoanUC.Width) / 2;
                AddLoanUC.Top = (this.ClientSize.Height - AddLoanUC.Height) / 2;
                this.Controls.Add(AddLoanUC);
                AddLoanUC.Visible = true;
            }
        }

        private void loanetailsButton_Click(object sender, EventArgs e)
        {
            LDUC.refreshLocal();
            clearMain();
            closeForms();
            LDUC.Visible = true;

            if (!this.Controls.Contains(LDUC))
            {
                LDUC.Left = (this.ClientSize.Width - LDUC.Width) / 2;
                LDUC.Top = (this.ClientSize.Height - LDUC.Height) / 2;
                this.Controls.Add(LDUC);
                LDUC.Visible = true;
            }
        }

        private void payloansButton_Click(object sender, EventArgs e)
        {
            PLUC.refreshLoacl();
            clearMain();
            closeForms();
            PLUC.Visible = true;

            if (!this.Controls.Contains(PLUC))
            {
                PLUC.Left = (this.ClientSize.Width - PLUC.Width) / 2;
                PLUC.Top = (this.ClientSize.Height - PLUC.Height) / 2;
                this.Controls.Add(PLUC);
                PLUC.Visible = true;
            }
        }

        private void loansReportButton_Click(object sender, EventArgs e)
        {
            LRUC.refreshLoacl();
            clearMain();
            closeForms();
            LRUC.Visible = true;

            if (!this.Controls.Contains(LRUC))
            {
                LRUC.Left = (this.ClientSize.Width - LRUC.Width) / 2;
                LRUC.Top = (this.ClientSize.Height - LRUC.Height) / 2;
                this.Controls.Add(LRUC);
                LRUC.Visible = true;
            }
        }

        private void addAdvanceButton_Click(object sender, EventArgs e)
        {
            AOUC.refreshLocal();
            clearMain();
            closeForms();
            AOUC.Visible = true;

            if (!this.Controls.Contains(AOUC))
            {
                AOUC.Left = (this.ClientSize.Width - AOUC.Width) / 2;
                AOUC.Top = (this.ClientSize.Height - AOUC.Height) / 2;
                this.Controls.Add(AOUC);
                AOUC.Visible = true;
            }
        }

        private void advancesDetailsButton_Click(object sender, EventArgs e)
        {
            ADUC.refreshLocal();
            clearMain();
            closeForms();
            ADUC.Visible = true;

            if (!this.Controls.Contains(ADUC))
            {
                ADUC.Left = (this.ClientSize.Width - ADUC.Width) / 2;
                ADUC.Top = (this.ClientSize.Height - ADUC.Height) / 2;
                this.Controls.Add(ADUC);
                ADUC.Visible = true;
            }
        }

        private void payAdvancesButton_Click(object sender, EventArgs e)
        {
            PAUC.refreshLocal();
            clearMain();
            closeForms();
            PAUC.Visible = true;

            if (!this.Controls.Contains(PAUC))
            {
                PAUC.Left = (this.ClientSize.Width - PAUC.Width) / 2;
                PAUC.Top = (this.ClientSize.Height - PAUC.Height) / 2;
                this.Controls.Add(PAUC);
                PAUC.Visible = true;
            }
        }

        private void advancesReport_Click(object sender, EventArgs e)
        {
            ARUC.refreshLocal();
            clearMain();
            closeForms();
            ARUC.Visible = true;

            if (!this.Controls.Contains(ARUC))
            {
                ARUC.Left = (this.ClientSize.Width - ARUC.Width) / 2;
                ARUC.Top = (this.ClientSize.Height - ARUC.Height) / 2;
                this.Controls.Add(ARUC);
                ARUC.Visible = true;
            }
        }

        private void employeesGeneralSettingsButton_Click(object sender, EventArgs e)
        {
            EGSUC.refreshLocal();
            clearMain();
            closeForms();
            EGSUC.Visible = true;

            if (!this.Controls.Contains(EGSUC))
            {
                EGSUC.Left = (this.ClientSize.Width - EGSUC.Width) / 2;
                EGSUC.Top = (this.ClientSize.Height - EGSUC.Height) / 2;
                this.Controls.Add(EGSUC);
                EGSUC.Visible = true;
            }
        }

        private void employeesAdditionButton_Click(object sender, EventArgs e)
        {
            EAUC.refreshLocal();
            clearMain();
            closeForms();
            EAUC.Visible = true;

            if (!this.Controls.Contains(EAUC))
            {
                EAUC.Left = (this.ClientSize.Width - EAUC.Width) / 2;
                EAUC.Top = (this.ClientSize.Height - EAUC.Height) / 2;
                this.Controls.Add(EAUC);
                EAUC.Visible = true;
            }
        }

        private void employeeSpecifiedSettingsButton_Click(object sender, EventArgs e)
        {
            ESSUC.refreshLocal();
            clearMain();
            closeForms();
            ESSUC.Visible = true;

            if (!this.Controls.Contains(ESSUC))
            {
                ESSUC.Left = (this.ClientSize.Width - ESSUC.Width) / 2;
                ESSUC.Top = (this.ClientSize.Height - ESSUC.Height) / 2;
                this.Controls.Add(ESSUC);
                ESSUC.Visible = true;
            }
        }

        private void comeGoingButton_Click(object sender, EventArgs e)
        {
            CGUC.localRefresh();
            clearMain();
            closeForms();
            CGUC.Visible = true;

            if (!this.Controls.Contains(CGUC))
            {
                CGUC.Left = (this.ClientSize.Width - CGUC.Width) / 2;
                CGUC.Top = (this.ClientSize.Height - CGUC.Height) / 2;
                this.Controls.Add(CGUC);
                CGUC.Visible = true;
            }
        }

        private void employeeAbscenceButton_Click(object sender, EventArgs e)
        {
            empAUC.refreshLocal();
            clearMain();
            closeForms();
            empAUC.Visible = true;

            if (!this.Controls.Contains(empAUC))
            {
                empAUC.Left = (this.ClientSize.Width - empAUC.Width) / 2;
                empAUC.Top = (this.ClientSize.Height - empAUC.Height) / 2;
                this.Controls.Add(empAUC);
                empAUC.Visible = true;
            }
        }

        private void lateButton_Click(object sender, EventArgs e)
        {
            LEUC.refreshLoacl();
            clearMain();
            closeForms();
            LEUC.Visible = true;

            if (!this.Controls.Contains(LEUC))
            {
                LEUC.Left = (this.ClientSize.Width - LEUC.Width) / 2;
                LEUC.Top = (this.ClientSize.Height - LEUC.Height) / 2;
                this.Controls.Add(LEUC);
                LEUC.Visible = true;
            }
        }

        private void employeeAdditionalButton_Click(object sender, EventArgs e)
        {
            AEUC.refreshLocal();
            clearMain();
            closeForms();
            AEUC.Visible = true;

            if (!this.Controls.Contains(AEUC))
            {
                AEUC.Left = (this.ClientSize.Width - AEUC.Width) / 2;
                AEUC.Top = (this.ClientSize.Height - AEUC.Height) / 2;
                this.Controls.Add(AEUC);
                AEUC.Visible = true;
            }
        }

        private void wagesButton_Click(object sender, EventArgs e)
        {
            EWUC.refreshLocal();
            clearMain();
            closeForms();
            EWUC.Visible = true;

            if (!this.Controls.Contains(EWUC))
            {
                EWUC.Left = (this.ClientSize.Width - EWUC.Width) / 2;
                EWUC.Top = (this.ClientSize.Height - EWUC.Height) / 2;
                this.Controls.Add(EWUC);
                EWUC.Visible = true;
            }
        }

        private void abscenceReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            EARUC.Visible = true;

            if (!this.Controls.Contains(EARUC))
            {
                EARUC.Left = (this.ClientSize.Width - EARUC.Width) / 2;
                EARUC.Top = (this.ClientSize.Height - EARUC.Height) / 2;
                this.Controls.Add(EARUC);
                EARUC.Visible = true;
            }
        }

        private void comeGoingReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CGRUC.Visible = true;

            if (!this.Controls.Contains(CGRUC))
            {
                CGRUC.Left = (this.ClientSize.Width - CGRUC.Width) / 2;
                CGRUC.Top = (this.ClientSize.Height - CGRUC.Height) / 2;
                this.Controls.Add(CGRUC);
                CGRUC.Visible = true;
            }
        }

        private void lateReportsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            LERUC.Visible = true;

            if (!this.Controls.Contains(LERUC))
            {
                LERUC.Left = (this.ClientSize.Width - LERUC.Width) / 2;
                LERUC.Top = (this.ClientSize.Height - LERUC.Height) / 2;
                this.Controls.Add(LERUC);
                LERUC.Visible = true;
            }
        }

        private void additionalReportsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            AERUC.Visible = true;

            if (!this.Controls.Contains(AERUC))
            {
                AERUC.Left = (this.ClientSize.Width - AERUC.Width) / 2;
                AERUC.Top = (this.ClientSize.Height - AERUC.Height) / 2;
                this.Controls.Add(AERUC);
                AERUC.Visible = true;
            }
        }

        private void wagesRepotButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            WRUC.Visible = true;

            if (!this.Controls.Contains(WRUC))
            {
                WRUC.Left = (this.ClientSize.Width - WRUC.Width) / 2;
                WRUC.Top = (this.ClientSize.Height - WRUC.Height) / 2;
                this.Controls.Add(WRUC);
                WRUC.Visible = true;
            }
        }

        private void safeFlowReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SafeFRUC.Visible = true;

            if (!this.Controls.Contains(SafeFRUC))
            {
                SafeFRUC.Left = (this.ClientSize.Width - SafeFRUC.Width) / 2;
                SafeFRUC.Top = (this.ClientSize.Height - SafeFRUC.Height) / 2;
                this.Controls.Add(SafeFRUC);
                SafeFRUC.Visible = true;
            }
        }

        private void cashBalanceReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CBRUC.Visible = true;

            if (!this.Controls.Contains(CBRUC))
            {
                CBRUC.Left = (this.ClientSize.Width - CBRUC.Width) / 2;
                CBRUC.Top = (this.ClientSize.Height - CBRUC.Height) / 2;
                this.Controls.Add(CBRUC);
                CBRUC.Visible = true;
            }
        }

        private void safeTransferReportsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            STRUC.Visible = true;

            if (!this.Controls.Contains(STRUC))
            {
                STRUC.Left = (this.ClientSize.Width - STRUC.Width) / 2;
                STRUC.Top = (this.ClientSize.Height - STRUC.Height) / 2;
                this.Controls.Add(STRUC);
                STRUC.Visible = true;
            }
        }

        private void categortyFlowReportsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CFRUC.Visible = true;

            if (!this.Controls.Contains(CFRUC))
            {
                CFRUC.Left = (this.ClientSize.Width - CFRUC.Width) / 2;
                CFRUC.Top = (this.ClientSize.Height - CFRUC.Height) / 2;
                this.Controls.Add(CFRUC);
                CFRUC.Visible = true;
            }
        }

        private void typeListReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            TLRUC.Visible = true;

            if (!this.Controls.Contains(TLRUC))
            {
                TLRUC.Left = (this.ClientSize.Width - TLRUC.Width) / 2;
                TLRUC.Top = (this.ClientSize.Height - TLRUC.Height) / 2;
                this.Controls.Add(TLRUC);
                TLRUC.Visible = true;
            }

        }

        private void minusCategoryReportsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            MCRUC.Visible = true;

            if (!this.Controls.Contains(MCRUC))
            {
                MCRUC.Left = (this.ClientSize.Width - MCRUC.Width) / 2;
                MCRUC.Top = (this.ClientSize.Height - MCRUC.Height) / 2;
                this.Controls.Add(MCRUC);
                MCRUC.Visible = true;
            }
        }

        private void historyCategoryReportsButton_Click(object sender, EventArgs e)
        {
             clearMain();
            closeForms();
            HCRUC.Visible = true;

            if (!this.Controls.Contains(HCRUC))
            {
                HCRUC.Left = (this.ClientSize.Width - HCRUC.Width) / 2;
                HCRUC.Top = (this.ClientSize.Height - HCRUC.Height) / 2;
                this.Controls.Add(HCRUC);
                HCRUC.Visible = true;
            }

        }

        private void addSubtactPermissionReportsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            ASRUC.Visible = true;

            if (!this.Controls.Contains(ASRUC))
            {
                ASRUC.Left = (this.ClientSize.Width - ASRUC.Width) / 2;
                ASRUC.Top = (this.ClientSize.Height - ASRUC.Height) / 2;
                this.Controls.Add(ASRUC);
                ASRUC.Visible = true;
            }
        }

        private void spendingsReportButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            GSRUC.Visible = true;

            if (!this.Controls.Contains(GSRUC))
            {
                GSRUC.Left = (this.ClientSize.Width - GSRUC.Width) / 2;
                GSRUC.Top = (this.ClientSize.Height - GSRUC.Height) / 2;
                this.Controls.Add(GSRUC);
                GSRUC.Visible = true;
            }
        }

        private void partnerBalanceReportsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            PBRUC.Visible = true;

            if (!this.Controls.Contains(PBRUC))
            {
                PBRUC.Left = (this.ClientSize.Width - PBRUC.Width) / 2;
                PBRUC.Top = (this.ClientSize.Height - PBRUC.Height) / 2;
                this.Controls.Add(PBRUC);
                PBRUC.Visible = true;
            }
        }

        private void anotherIncomeReportsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            AIRUC.Visible = true;

            if (!this.Controls.Contains(AIRUC))
            {
                AIRUC.Left = (this.ClientSize.Width - AIRUC.Width) / 2;
                AIRUC.Top = (this.ClientSize.Height - AIRUC.Height) / 2;
                this.Controls.Add(AIRUC);
                AIRUC.Visible = true;
            }
        }

        private void damagedPotentialsReportsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            PDRUC.Visible = true;

            if (!this.Controls.Contains(PDRUC))
            {
                PDRUC.Left = (this.ClientSize.Width - PDRUC.Width) / 2;
                PDRUC.Top = (this.ClientSize.Height - PDRUC.Height) / 2;
                this.Controls.Add(PDRUC);
                PDRUC.Visible = true;
            }
        }

        private void addUsersBuuton_Click(object sender, EventArgs e)
        {
            AUUC.refreshLocal();
            clearMain();
            closeForms();
            AUUC.Visible = true;

            if (!this.Controls.Contains(AUUC))
            {
                AUUC.Left = (this.ClientSize.Width - AUUC.Width) / 2;
                AUUC.Top = (this.ClientSize.Height - AUUC.Height) / 2;
                this.Controls.Add(AUUC);
                AUUC.Visible = true;
            }
        }

        private void userPrivilegeButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            UPUC.Visible = true;

            if (!this.Controls.Contains(UPUC))
            {
                UPUC.Left = (this.ClientSize.Width - UPUC.Width) / 2;
                UPUC.Top = (this.ClientSize.Height - UPUC.Height) / 2;
                this.Controls.Add(UPUC);
                UPUC .Visible = true;
            }
        }

        private void usersLoginsReportsButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            UTUC.Visible = true;

            if (!this.Controls.Contains(UTUC))
            {
                UTUC.Left = (this.ClientSize.Width - UTUC.Width) / 2;
                UTUC.Top = (this.ClientSize.Height - UTUC.Height) / 2;
                this.Controls.Add(UTUC);
                UTUC.Visible = true;
            }
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            HomeButton.Visible = false;
            panel1.Visible = true;
            softerPicture.Visible = true;
            shortcutsDropDownPanel.Visible = true;
        }

        private void salesShortcutButon_Click(object sender, EventArgs e)
        {
            SalesUC.refreshLocal();

            clearMain();
            closeForms();
            SalesUC.Visible = true;

            if (!this.Controls.Contains(SalesUC))
            {
                SalesUC.Left = (this.ClientSize.Width - SalesUC.Width) / 2;
                SalesUC.Top = (this.ClientSize.Height - SalesUC.Height) / 2;
                this.Controls.Add(SalesUC);
                SalesUC.Visible = true;
            }
        }

        private void purchasesShortcutButton_Click(object sender, EventArgs e)
        {
            PUC.refreshLocal();
            clearMain();
            closeForms();
            PUC.Visible = true;

            if (!this.Controls.Contains(PUC))
            {
                PUC.Left = (this.ClientSize.Width - PUC.Width) / 2;
                PUC.Top = (this.ClientSize.Height - PUC.Height) / 2;
                this.Controls.Add(PUC);
                PUC.Visible = true;
            }
        }

        private void dailyReportShortcutButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SFRUC.Visible = true;

            if (!this.Controls.Contains(SFRUC))
            {
                SFRUC.Left = (this.ClientSize.Width - SFRUC.Width) / 2;
                SFRUC.Top = (this.ClientSize.Height - SFRUC.Height) / 2;
                this.Controls.Add(SFRUC);
                SFRUC.Visible = true;
            }
        }

        private void safeTrafficShortcutButton_Click(object sender, EventArgs e)
        {
            SafeFRUC.refreshLocal();
            clearMain();
            closeForms();
            SafeFRUC.Visible = true;

            if (!this.Controls.Contains(SafeFRUC))
            {
                SafeFRUC.Left = (this.ClientSize.Width - SafeFRUC.Width) / 2;
                SafeFRUC.Top = (this.ClientSize.Height - SafeFRUC.Height) / 2;
                this.Controls.Add(SafeFRUC);
                SafeFRUC.Visible = true;
            }
        }

        private void customerBalanceShortcutButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CBDRUC.Visible = true;

            if (!this.Controls.Contains(CBDRUC))
            {
                CBDRUC.Left = (this.ClientSize.Width - CBDRUC.Width) / 2;
                CBDRUC.Top = (this.ClientSize.Height - CBDRUC.Height) / 2;
                this.Controls.Add(CBDRUC);
                CBDRUC.Visible = true;
            }
        }

        private void supplierShortcutButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SBR.Visible = true;

            if (!this.Controls.Contains(SBR))
            {
                SBR.Left = (this.ClientSize.Width - SBR.Width) / 2;
                SBR.Top = (this.ClientSize.Height - SBR.Height) / 2;
                this.Controls.Add(SBR);
                SBR.Visible = true;
            }
        }

        private void purchasesvisualizationReport_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            PVR.Visible = true;

            if (!this.Controls.Contains(PVR))
            {
                PVR.Left = (this.ClientSize.Width - PVR.Width) / 2;
                PVR.Top = (this.ClientSize.Height - PVR.Height) / 2;
                PVR.Visible = true;
            }
        }

        private void salesVisualizationButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            SVR.Visible = true;

            if (!this.Controls.Contains(SVR))
            {
                SVR.Left = (this.ClientSize.Width - SVR.Width) / 2;
                SVR.Top = (this.ClientSize.Height - SVR.Height) / 2;
                SVR.Visible = true;
            }
        }

        private void customersVisualizationButton_Click(object sender, EventArgs e)
        {
            clearMain();
            closeForms();
            CVR.Visible = true;

            if (!this.Controls.Contains(CVR))
            {
                CVR.Left = (this.ClientSize.Width - CVR.Width) / 2;
                CVR.Top = (this.ClientSize.Height - CVR.Height) / 2;
                CVR.Visible = true;
            }
        }

        private void employeesVisualizationButton(object sender, EventArgs e)
        {
   clearMain();
            closeForms();
            EVUC.Visible = true;

            if (!this.Controls.Contains(EVUC))
            {
                EVUC.Left = (this.ClientSize.Width - EVUC.Width) / 2;
                EVUC.Top = (this.ClientSize.Height - EVUC.Height) / 2;
                EVUC.Visible = true;
            }
        }

        private void SuppliersVisualizationButton_Click(object sender, EventArgs e)
        {
clearMain();
            closeForms();
            SVUC.Visible = true;

            if (!this.Controls.Contains(SVUC))
            {
                SVUC.Left = (this.ClientSize.Width - SVUC.Width) / 2;
                SVUC.Top = (this.ClientSize.Height - SVUC.Height) / 2;
                SVUC.Visible = true;
            }
        }

        private void safeVisualizationButton_Click(object sender, EventArgs e)
        {
clearMain();
            closeForms();
            SDVUC.Visible = true;

            if (!this.Controls.Contains(SDVUC))
            {
                SDVUC.Left = (this.ClientSize.Width - SDVUC.Width) / 2;
                SDVUC.Top = (this.ClientSize.Height - SDVUC.Height) / 2;
                SDVUC.Visible = true;
            }
        }
    }
}