
namespace PhoneWebShop.WinForms
{
    partial class PhoneOverview
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddPhone = new System.Windows.Forms.Button();
            this.btnRemovePhone = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.PhoneListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnAddPhone);
            this.flowLayoutPanel1.Controls.Add(this.btnRemovePhone);
            this.flowLayoutPanel1.Controls.Add(this.ExitButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 356);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(649, 31);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnAddPhone
            // 
            this.btnAddPhone.Location = new System.Drawing.Point(3, 3);
            this.btnAddPhone.Name = "btnAddPhone";
            this.btnAddPhone.Size = new System.Drawing.Size(25, 25);
            this.btnAddPhone.TabIndex = 1;
            this.btnAddPhone.Text = "+";
            this.btnAddPhone.UseVisualStyleBackColor = true;
            this.btnAddPhone.Click += new System.EventHandler(this.btnAddPhone_Click);
            // 
            // btnRemovePhone
            // 
            this.btnRemovePhone.Enabled = false;
            this.btnRemovePhone.Location = new System.Drawing.Point(34, 3);
            this.btnRemovePhone.Name = "btnRemovePhone";
            this.btnRemovePhone.Size = new System.Drawing.Size(25, 25);
            this.btnRemovePhone.TabIndex = 2;
            this.btnRemovePhone.Text = "-";
            this.btnRemovePhone.UseVisualStyleBackColor = true;
            this.btnRemovePhone.Click += new System.EventHandler(this.btnRemovePhone_Click);
            // 
            // ExitButton
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.ExitButton, true);
            this.ExitButton.Location = new System.Drawing.Point(65, 3);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 25);
            this.ExitButton.TabIndex = 0;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(649, 387);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.searchBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.PhoneListBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(215, 387);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // searchBox
            // 
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBox.Location = new System.Drawing.Point(3, 3);
            this.searchBox.Name = "searchBox";
            this.searchBox.PlaceholderText = "Search...";
            this.searchBox.Size = new System.Drawing.Size(209, 23);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.SearchTextChanged);
            // 
            // PhoneListBox
            // 
            this.PhoneListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PhoneListBox.FormattingEnabled = true;
            this.PhoneListBox.ItemHeight = 15;
            this.PhoneListBox.Location = new System.Drawing.Point(3, 32);
            this.PhoneListBox.Name = "PhoneListBox";
            this.PhoneListBox.Size = new System.Drawing.Size(209, 352);
            this.PhoneListBox.TabIndex = 1;
            this.PhoneListBox.SelectedIndexChanged += new System.EventHandler(this.PhoneListBox_SelectedIndexChanged);
            this.PhoneListBox.SelectedValueChanged += new System.EventHandler(this.SelectionChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(430, 387);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblStock);
            this.panel1.Controls.Add(this.lblPrice);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblType);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblBrand);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 187);
            this.panel1.TabIndex = 0;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStock.Location = new System.Drawing.Point(258, 31);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(40, 17);
            this.lblStock.TabIndex = 7;
            this.lblStock.Text = "label5";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrice.Location = new System.Drawing.Point(258, 6);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(40, 17);
            this.lblPrice.TabIndex = 6;
            this.lblPrice.Text = "label5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Stock:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Price:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblType.Location = new System.Drawing.Point(99, 29);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(40, 17);
            this.lblType.TabIndex = 3;
            this.lblType.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Text:";
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBrand.Location = new System.Drawing.Point(99, 6);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(40, 17);
            this.lblBrand.TabIndex = 1;
            this.lblBrand.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Brand:";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtDescription, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 196);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(424, 188);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(3, 18);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(418, 167);
            this.txtDescription.TabIndex = 1;
            // 
            // PhoneOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 387);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PhoneOverview";
            this.Text = "PhoneOverview";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ListBox PhoneListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button btnAddPhone;
        private System.Windows.Forms.Button btnRemovePhone;
    }
}