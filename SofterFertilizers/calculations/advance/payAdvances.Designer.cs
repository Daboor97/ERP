namespace SofterFertilizers.calculations.advance
{
    partial class payAdvances
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.adjustCheckBox = new System.Windows.Forms.CheckBox();
            this.totalPaidTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.paidAmount = new System.Windows.Forms.TextBox();
            this.oldPaidAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.divisionAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.categoryDGV = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.dateDTP = new SofterFertilizers.BCDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.divisionNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addButton = new SofterFertilizers.roundedButton();
            this.loanNumberComboBox = new System.Windows.Forms.ComboBox();
            this.detailedDebtGroupBox = new System.Windows.Forms.GroupBox();
            this.safeComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.categoryDGV)).BeginInit();
            this.detailedDebtGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label3.Location = new System.Drawing.Point(796, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 28);
            this.label3.TabIndex = 926;
            this.label3.Text = "رقم السلفة";
            // 
            // adjustCheckBox
            // 
            this.adjustCheckBox.AutoSize = true;
            this.adjustCheckBox.Font = new System.Drawing.Font("Tempus Sans ITC", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adjustCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.adjustCheckBox.Location = new System.Drawing.Point(246, 90);
            this.adjustCheckBox.Name = "adjustCheckBox";
            this.adjustCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.adjustCheckBox.Size = new System.Drawing.Size(480, 31);
            this.adjustCheckBox.TabIndex = 902;
            this.adjustCheckBox.Text = "علّم لتعديل قسط ثم اختار القسط المراد تعديله من الجدول";
            this.adjustCheckBox.UseVisualStyleBackColor = true;
            this.adjustCheckBox.CheckedChanged += new System.EventHandler(this.adjustCheckBox_CheckedChanged);
            // 
            // totalPaidTextBox
            // 
            this.totalPaidTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.totalPaidTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.totalPaidTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalPaidTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.totalPaidTextBox.Location = new System.Drawing.Point(361, 54);
            this.totalPaidTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.totalPaidTextBox.Name = "totalPaidTextBox";
            this.totalPaidTextBox.ReadOnly = true;
            this.totalPaidTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.totalPaidTextBox.Size = new System.Drawing.Size(153, 26);
            this.totalPaidTextBox.TabIndex = 908;
            this.totalPaidTextBox.TextChanged += new System.EventHandler(this.totalPaidTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label7.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(234)))));
            this.label7.Location = new System.Drawing.Point(534, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 28);
            this.label7.TabIndex = 907;
            this.label7.Text = "إجمالي المدفوع";
            // 
            // paidAmount
            // 
            this.paidAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.paidAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paidAmount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paidAmount.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.paidAmount.Location = new System.Drawing.Point(716, 54);
            this.paidAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.paidAmount.Name = "paidAmount";
            this.paidAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.paidAmount.Size = new System.Drawing.Size(153, 26);
            this.paidAmount.TabIndex = 906;
            this.paidAmount.TextChanged += new System.EventHandler(this.paidAmount_TextChanged);
            // 
            // oldPaidAmount
            // 
            this.oldPaidAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.oldPaidAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.oldPaidAmount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oldPaidAmount.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.oldPaidAmount.Location = new System.Drawing.Point(6, 21);
            this.oldPaidAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.oldPaidAmount.Name = "oldPaidAmount";
            this.oldPaidAmount.ReadOnly = true;
            this.oldPaidAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.oldPaidAmount.Size = new System.Drawing.Size(153, 26);
            this.oldPaidAmount.TabIndex = 905;
            this.oldPaidAmount.TextChanged += new System.EventHandler(this.oldPaidAmount_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label5.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(234)))));
            this.label5.Location = new System.Drawing.Point(881, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 28);
            this.label5.TabIndex = 903;
            this.label5.Text = "المبلغ المدفوع";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label6.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(234)))));
            this.label6.Location = new System.Drawing.Point(165, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 28);
            this.label6.TabIndex = 901;
            this.label6.Text = "المبلغ المدفوع سابقًا";
            // 
            // divisionAmount
            // 
            this.divisionAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.divisionAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.divisionAmount.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.divisionAmount.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.divisionAmount.Location = new System.Drawing.Point(361, 19);
            this.divisionAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.divisionAmount.Name = "divisionAmount";
            this.divisionAmount.ReadOnly = true;
            this.divisionAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.divisionAmount.Size = new System.Drawing.Size(153, 26);
            this.divisionAmount.TabIndex = 900;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label9.Location = new System.Drawing.Point(493, 84);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 28);
            this.label9.TabIndex = 923;
            this.label9.Text = "الخزنة";
            // 
            // categoryDGV
            // 
            this.categoryDGV.AllowUserToAddRows = false;
            this.categoryDGV.AllowUserToDeleteRows = false;
            this.categoryDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.categoryDGV.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.categoryDGV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.categoryDGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.categoryDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.categoryDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.categoryDGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.categoryDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.categoryDGV.GridColor = System.Drawing.Color.White;
            this.categoryDGV.Location = new System.Drawing.Point(14, 378);
            this.categoryDGV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.categoryDGV.MultiSelect = false;
            this.categoryDGV.Name = "categoryDGV";
            this.categoryDGV.ReadOnly = true;
            this.categoryDGV.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.categoryDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.categoryDGV.RowHeadersWidth = 30;
            this.categoryDGV.RowTemplate.Height = 26;
            this.categoryDGV.Size = new System.Drawing.Size(987, 386);
            this.categoryDGV.TabIndex = 922;
            this.categoryDGV.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.categoryDGV_RowHeaderMouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label2.Location = new System.Drawing.Point(181, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 28);
            this.label2.TabIndex = 920;
            this.label2.Text = "التاريخ";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tempus Sans ITC", 22F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label16.Location = new System.Drawing.Point(407, 8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(206, 39);
            this.label16.TabIndex = 919;
            this.label16.Text = "دفع أقساط السلف";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dateDTP
            // 
            this.dateDTP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.dateDTP.BackDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.dateDTP.CalendarFont = new System.Drawing.Font("Tempus Sans ITC", 16F);
            this.dateDTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.dateDTP.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.dateDTP.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.dateDTP.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.dateDTP.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.dateDTP.CustomFormat = "yyyy-MM-dd";
            this.dateDTP.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dateDTP.Font = new System.Drawing.Font("Tempus Sans ITC", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateDTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateDTP.Location = new System.Drawing.Point(126, 115);
            this.dateDTP.Name = "dateDTP";
            this.dateDTP.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dateDTP.Size = new System.Drawing.Size(175, 39);
            this.dateDTP.TabIndex = 921;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label4.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(234)))));
            this.label4.Location = new System.Drawing.Point(534, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 28);
            this.label4.TabIndex = 899;
            this.label4.Text = "قيمة القسط";
            // 
            // divisionNumber
            // 
            this.divisionNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.divisionNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.divisionNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.divisionNumber.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.divisionNumber.Location = new System.Drawing.Point(716, 20);
            this.divisionNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.divisionNumber.Name = "divisionNumber";
            this.divisionNumber.ReadOnly = true;
            this.divisionNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.divisionNumber.Size = new System.Drawing.Size(153, 26);
            this.divisionNumber.TabIndex = 898;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(234)))));
            this.label1.Location = new System.Drawing.Point(904, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 28);
            this.label1.TabIndex = 897;
            this.label1.Text = "رقم القسط";
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.addButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.addButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Tempus Sans ITC", 13.8F, System.Drawing.FontStyle.Bold);
            this.addButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(27)))), ((int)(((byte)(55)))));
            this.addButton.Location = new System.Drawing.Point(6, 54);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(135, 38);
            this.addButton.TabIndex = 892;
            this.addButton.Text = "حفظ";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // loanNumberComboBox
            // 
            this.loanNumberComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.loanNumberComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loanNumberComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.loanNumberComboBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.loanNumberComboBox.ForeColor = System.Drawing.Color.White;
            this.loanNumberComboBox.FormattingEnabled = true;
            this.loanNumberComboBox.Location = new System.Drawing.Point(742, 118);
            this.loanNumberComboBox.Name = "loanNumberComboBox";
            this.loanNumberComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.loanNumberComboBox.Size = new System.Drawing.Size(181, 27);
            this.loanNumberComboBox.TabIndex = 927;
            this.loanNumberComboBox.SelectedIndexChanged += new System.EventHandler(this.loanNumberComboBox_SelectedIndexChanged);
            // 
            // detailedDebtGroupBox
            // 
            this.detailedDebtGroupBox.Controls.Add(this.adjustCheckBox);
            this.detailedDebtGroupBox.Controls.Add(this.totalPaidTextBox);
            this.detailedDebtGroupBox.Controls.Add(this.label7);
            this.detailedDebtGroupBox.Controls.Add(this.paidAmount);
            this.detailedDebtGroupBox.Controls.Add(this.oldPaidAmount);
            this.detailedDebtGroupBox.Controls.Add(this.label5);
            this.detailedDebtGroupBox.Controls.Add(this.label6);
            this.detailedDebtGroupBox.Controls.Add(this.divisionAmount);
            this.detailedDebtGroupBox.Controls.Add(this.label4);
            this.detailedDebtGroupBox.Controls.Add(this.divisionNumber);
            this.detailedDebtGroupBox.Controls.Add(this.label1);
            this.detailedDebtGroupBox.Controls.Add(this.addButton);
            this.detailedDebtGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.detailedDebtGroupBox.Location = new System.Drawing.Point(16, 223);
            this.detailedDebtGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.detailedDebtGroupBox.Name = "detailedDebtGroupBox";
            this.detailedDebtGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.detailedDebtGroupBox.Size = new System.Drawing.Size(981, 151);
            this.detailedDebtGroupBox.TabIndex = 925;
            this.detailedDebtGroupBox.TabStop = false;
            // 
            // safeComboBox
            // 
            this.safeComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.safeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.safeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.safeComboBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.safeComboBox.ForeColor = System.Drawing.Color.White;
            this.safeComboBox.FormattingEnabled = true;
            this.safeComboBox.Location = new System.Drawing.Point(431, 118);
            this.safeComboBox.Name = "safeComboBox";
            this.safeComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.safeComboBox.Size = new System.Drawing.Size(181, 27);
            this.safeComboBox.TabIndex = 924;
            // 
            // payAdvances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.categoryDGV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.dateDTP);
            this.Controls.Add(this.loanNumberComboBox);
            this.Controls.Add(this.detailedDebtGroupBox);
            this.Controls.Add(this.safeComboBox);
            this.Name = "payAdvances";
            this.Size = new System.Drawing.Size(1014, 773);
            ((System.ComponentModel.ISupportInitialize)(this.categoryDGV)).EndInit();
            this.detailedDebtGroupBox.ResumeLayout(false);
            this.detailedDebtGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox adjustCheckBox;
        private System.Windows.Forms.TextBox totalPaidTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox paidAmount;
        private System.Windows.Forms.TextBox oldPaidAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox divisionAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView categoryDGV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label16;
        private BCDateTimePicker dateDTP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox divisionNumber;
        private System.Windows.Forms.Label label1;
        private roundedButton addButton;
        private System.Windows.Forms.ComboBox loanNumberComboBox;
        private System.Windows.Forms.GroupBox detailedDebtGroupBox;
        private System.Windows.Forms.ComboBox safeComboBox;
    }
}
