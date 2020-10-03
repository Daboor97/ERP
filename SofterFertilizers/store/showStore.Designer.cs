namespace SofterFertilizers.store
{
    partial class showStore
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
            this.label16 = new System.Windows.Forms.Label();
            this.storeNameComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.categoryDGV = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.categoryStoreCodeSearchTextBox = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.categoryNameSearchTextBox = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.categoryCodeSearchTextBox = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.addQuantityToStore = new SofterFertilizers.roundedButton();
            this.totalSumLabel = new System.Windows.Forms.Label();
            this.sumBuyingPriceLabel = new System.Windows.Forms.Label();
            this.sumProfitLabel = new System.Windows.Forms.Label();
            this.storeCodeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.categoryDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tempus Sans ITC", 22F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label16.Location = new System.Drawing.Point(594, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(136, 39);
            this.label16.TabIndex = 533;
            this.label16.Text = "جرد مخزن";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // storeNameComboBox
            // 
            this.storeNameComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.storeNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.storeNameComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.storeNameComboBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.storeNameComboBox.ForeColor = System.Drawing.Color.White;
            this.storeNameComboBox.FormattingEnabled = true;
            this.storeNameComboBox.Location = new System.Drawing.Point(880, 69);
            this.storeNameComboBox.Name = "storeNameComboBox";
            this.storeNameComboBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.storeNameComboBox.Size = new System.Drawing.Size(197, 27);
            this.storeNameComboBox.TabIndex = 564;
            this.storeNameComboBox.SelectedIndexChanged += new System.EventHandler(this.storeNameComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label5.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(234)))));
            this.label5.Location = new System.Drawing.Point(1109, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 28);
            this.label5.TabIndex = 563;
            this.label5.Text = "المخزن";
            // 
            // categoryDGV
            // 
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
            this.categoryDGV.Location = new System.Drawing.Point(14, 112);
            this.categoryDGV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.categoryDGV.MultiSelect = false;
            this.categoryDGV.Name = "categoryDGV";
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
            this.categoryDGV.Size = new System.Drawing.Size(1210, 578);
            this.categoryDGV.TabIndex = 565;
            this.categoryDGV.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.categoryDGV_RowsAdded);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("Sitka Heading", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label1.Location = new System.Drawing.Point(764, 784);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 30);
            this.label1.TabIndex = 566;
            this.label1.Text = "إجمالي البضاعة";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label3.Font = new System.Drawing.Font("Sitka Heading", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label3.Location = new System.Drawing.Point(310, 784);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 30);
            this.label3.TabIndex = 568;
            this.label3.Text = "صافي الأرباح";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label4.Font = new System.Drawing.Font("Sitka Heading", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label4.Location = new System.Drawing.Point(517, 784);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 30);
            this.label4.TabIndex = 569;
            this.label4.Text = "صافي سعر الشراء";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tempus Sans ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label31.Location = new System.Drawing.Point(597, 698);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(107, 24);
            this.label31.TabIndex = 576;
            this.label31.Text = "الكود المخزني";
            // 
            // categoryStoreCodeSearchTextBox
            // 
            this.categoryStoreCodeSearchTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.categoryStoreCodeSearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.categoryStoreCodeSearchTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryStoreCodeSearchTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.categoryStoreCodeSearchTextBox.Location = new System.Drawing.Point(552, 724);
            this.categoryStoreCodeSearchTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.categoryStoreCodeSearchTextBox.Name = "categoryStoreCodeSearchTextBox";
            this.categoryStoreCodeSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.categoryStoreCodeSearchTextBox.Size = new System.Drawing.Size(197, 26);
            this.categoryStoreCodeSearchTextBox.TabIndex = 575;
            this.categoryStoreCodeSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.categoryStoreCodeSearchTextBox.TextChanged += new System.EventHandler(this.categoryStoreCodeSearchTextBox_TextChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tempus Sans ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label30.Location = new System.Drawing.Point(809, 698);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(88, 24);
            this.label30.TabIndex = 574;
            this.label30.Text = "اسم الصنف";
            // 
            // categoryNameSearchTextBox
            // 
            this.categoryNameSearchTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.categoryNameSearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.categoryNameSearchTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryNameSearchTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.categoryNameSearchTextBox.Location = new System.Drawing.Point(755, 724);
            this.categoryNameSearchTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.categoryNameSearchTextBox.Name = "categoryNameSearchTextBox";
            this.categoryNameSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.categoryNameSearchTextBox.Size = new System.Drawing.Size(197, 26);
            this.categoryNameSearchTextBox.TabIndex = 573;
            this.categoryNameSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.categoryNameSearchTextBox.TextChanged += new System.EventHandler(this.categoryNameSearchTextBox_TextChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tempus Sans ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label29.Location = new System.Drawing.Point(1012, 698);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(89, 24);
            this.label29.TabIndex = 572;
            this.label29.Text = "كود الصنف";
            // 
            // categoryCodeSearchTextBox
            // 
            this.categoryCodeSearchTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.categoryCodeSearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.categoryCodeSearchTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryCodeSearchTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.categoryCodeSearchTextBox.Location = new System.Drawing.Point(958, 724);
            this.categoryCodeSearchTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.categoryCodeSearchTextBox.Name = "categoryCodeSearchTextBox";
            this.categoryCodeSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.categoryCodeSearchTextBox.Size = new System.Drawing.Size(197, 26);
            this.categoryCodeSearchTextBox.TabIndex = 571;
            this.categoryCodeSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.categoryCodeSearchTextBox.TextChanged += new System.EventHandler(this.categoryCodeSearchTextBox_TextChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tempus Sans ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.label28.Location = new System.Drawing.Point(1159, 692);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(69, 24);
            this.label28.TabIndex = 570;
            this.label28.Text = "بحث بـــ";
            // 
            // addQuantityToStore
            // 
            this.addQuantityToStore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.addQuantityToStore.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.addQuantityToStore.FlatAppearance.BorderSize = 0;
            this.addQuantityToStore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.addQuantityToStore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.addQuantityToStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addQuantityToStore.Font = new System.Drawing.Font("Tempus Sans ITC", 13.8F, System.Drawing.FontStyle.Bold);
            this.addQuantityToStore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(27)))), ((int)(((byte)(55)))));
            this.addQuantityToStore.Location = new System.Drawing.Point(124, 65);
            this.addQuantityToStore.Name = "addQuantityToStore";
            this.addQuantityToStore.Size = new System.Drawing.Size(135, 38);
            this.addQuantityToStore.TabIndex = 577;
            this.addQuantityToStore.Text = "جرد";
            this.addQuantityToStore.UseVisualStyleBackColor = false;
            this.addQuantityToStore.Click += new System.EventHandler(this.addQuantityToStore_Click);
            // 
            // totalSumLabel
            // 
            this.totalSumLabel.AutoSize = true;
            this.totalSumLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.totalSumLabel.Font = new System.Drawing.Font("Sitka Heading", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalSumLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(234)))));
            this.totalSumLabel.Location = new System.Drawing.Point(825, 814);
            this.totalSumLabel.Name = "totalSumLabel";
            this.totalSumLabel.Size = new System.Drawing.Size(25, 30);
            this.totalSumLabel.TabIndex = 578;
            this.totalSumLabel.Text = "0";
            // 
            // sumBuyingPriceLabel
            // 
            this.sumBuyingPriceLabel.AutoSize = true;
            this.sumBuyingPriceLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.sumBuyingPriceLabel.Font = new System.Drawing.Font("Sitka Heading", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sumBuyingPriceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(234)))));
            this.sumBuyingPriceLabel.Location = new System.Drawing.Point(593, 814);
            this.sumBuyingPriceLabel.Name = "sumBuyingPriceLabel";
            this.sumBuyingPriceLabel.Size = new System.Drawing.Size(25, 30);
            this.sumBuyingPriceLabel.TabIndex = 579;
            this.sumBuyingPriceLabel.Text = "0";
            // 
            // sumProfitLabel
            // 
            this.sumProfitLabel.AutoSize = true;
            this.sumProfitLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.sumProfitLabel.Font = new System.Drawing.Font("Sitka Heading", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sumProfitLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(234)))));
            this.sumProfitLabel.Location = new System.Drawing.Point(361, 814);
            this.sumProfitLabel.Name = "sumProfitLabel";
            this.sumProfitLabel.Size = new System.Drawing.Size(25, 30);
            this.sumProfitLabel.TabIndex = 580;
            this.sumProfitLabel.Text = "0";
            // 
            // storeCodeTextBox
            // 
            this.storeCodeTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.storeCodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.storeCodeTextBox.Enabled = false;
            this.storeCodeTextBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeCodeTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.storeCodeTextBox.Location = new System.Drawing.Point(537, 69);
            this.storeCodeTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.storeCodeTextBox.Name = "storeCodeTextBox";
            this.storeCodeTextBox.ReadOnly = true;
            this.storeCodeTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.storeCodeTextBox.Size = new System.Drawing.Size(197, 26);
            this.storeCodeTextBox.TabIndex = 582;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.label2.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(234)))));
            this.label2.Location = new System.Drawing.Point(740, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 28);
            this.label2.TabIndex = 581;
            this.label2.Text = "كود المخزن";
            // 
            // showStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.storeCodeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sumProfitLabel);
            this.Controls.Add(this.sumBuyingPriceLabel);
            this.Controls.Add(this.totalSumLabel);
            this.Controls.Add(this.addQuantityToStore);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.categoryStoreCodeSearchTextBox);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.categoryNameSearchTextBox);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.categoryCodeSearchTextBox);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.categoryDGV);
            this.Controls.Add(this.storeNameComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label16);
            this.Name = "showStore";
            this.Size = new System.Drawing.Size(1245, 847);
            ((System.ComponentModel.ISupportInitialize)(this.categoryDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox storeNameComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView categoryDGV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox categoryStoreCodeSearchTextBox;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox categoryNameSearchTextBox;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox categoryCodeSearchTextBox;
        private System.Windows.Forms.Label label28;
        private roundedButton addQuantityToStore;
        private System.Windows.Forms.Label totalSumLabel;
        private System.Windows.Forms.Label sumBuyingPriceLabel;
        private System.Windows.Forms.Label sumProfitLabel;
        private System.Windows.Forms.TextBox storeCodeTextBox;
        private System.Windows.Forms.Label label2;
    }
}
