
namespace Omgevingsmonitor_configurator
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.createNewAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configPortBox = new System.Windows.Forms.ToolStripComboBox();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBoxBtn = new System.Windows.Forms.Button();
            this.userBoxesListView = new System.Windows.Forms.ListView();
            this.NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExposureColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ModelColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastMeasurementColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SensorsColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.configBtn = new System.Windows.Forms.Button();
            this.idGridView = new System.Windows.Forms.DataGridView();
            this.functionCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.functionsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.loginToolStrilLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.generalProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.configPort = new System.IO.Ports.SerialPort(this.components);
            this.deleteConfigBtn = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.WiFiBox = new System.Windows.Forms.GroupBox();
            this.WiFiConfigButton = new System.Windows.Forms.Button();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.SSIDLabel = new System.Windows.Forms.Label();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.SSIDBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.WiFiBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.configPortBox,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1162, 32);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.loginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginAccountToolStripMenuItem,
            this.toolStripSeparator1,
            this.createNewAccountToolStripMenuItem});
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(138, 28);
            this.loginToolStripMenuItem.Text = "OpenSense Login";
            // 
            // loginAccountToolStripMenuItem
            // 
            this.loginAccountToolStripMenuItem.Name = "loginAccountToolStripMenuItem";
            this.loginAccountToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.loginAccountToolStripMenuItem.Text = "Login";
            this.loginAccountToolStripMenuItem.Click += new System.EventHandler(this.loginAccountToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(224, 6);
            // 
            // createNewAccountToolStripMenuItem
            // 
            this.createNewAccountToolStripMenuItem.Name = "createNewAccountToolStripMenuItem";
            this.createNewAccountToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.createNewAccountToolStripMenuItem.Text = "Create New Account";
            this.createNewAccountToolStripMenuItem.Click += new System.EventHandler(this.createNewAccountToolStripMenuItem_Click);
            // 
            // configPortBox
            // 
            this.configPortBox.BackColor = System.Drawing.Color.Red;
            this.configPortBox.Name = "configPortBox";
            this.configPortBox.Size = new System.Drawing.Size(121, 28);
            this.configPortBox.DropDown += new System.EventHandler(this.configPortBox_DropDown);
            this.configPortBox.SelectedIndexChanged += new System.EventHandler(this.configPortBox_SelectedIndexChanged);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(47, 28);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // addBoxBtn
            // 
            this.addBoxBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addBoxBtn.BackColor = System.Drawing.SystemColors.GrayText;
            this.addBoxBtn.Enabled = false;
            this.addBoxBtn.Location = new System.Drawing.Point(12, 366);
            this.addBoxBtn.Name = "addBoxBtn";
            this.addBoxBtn.Size = new System.Drawing.Size(582, 46);
            this.addBoxBtn.TabIndex = 4;
            this.addBoxBtn.Text = "Add a omgevingsmonitor";
            this.addBoxBtn.UseVisualStyleBackColor = false;
            this.addBoxBtn.Click += new System.EventHandler(this.addBoxBtn_Click);
            // 
            // userBoxesListView
            // 
            this.userBoxesListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.userBoxesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.ExposureColumn,
            this.ModelColumn,
            this.LastMeasurementColumn,
            this.SensorsColumn});
            this.userBoxesListView.FullRowSelect = true;
            this.userBoxesListView.HideSelection = false;
            this.userBoxesListView.Location = new System.Drawing.Point(12, 39);
            this.userBoxesListView.Name = "userBoxesListView";
            this.userBoxesListView.Size = new System.Drawing.Size(582, 325);
            this.userBoxesListView.TabIndex = 5;
            this.userBoxesListView.UseCompatibleStateImageBehavior = false;
            this.userBoxesListView.View = System.Windows.Forms.View.Details;
            this.userBoxesListView.Click += new System.EventHandler(this.userBoxesListView_Click);
            this.userBoxesListView.DoubleClick += new System.EventHandler(this.userBoxesListView_DoubleClick_1);
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Name";
            this.NameColumn.Width = 100;
            // 
            // ExposureColumn
            // 
            this.ExposureColumn.Text = "Exposure";
            this.ExposureColumn.Width = 70;
            // 
            // ModelColumn
            // 
            this.ModelColumn.Text = "Model";
            this.ModelColumn.Width = 70;
            // 
            // LastMeasurementColumn
            // 
            this.LastMeasurementColumn.Text = "Last Measurement";
            this.LastMeasurementColumn.Width = 120;
            // 
            // SensorsColumn
            // 
            this.SensorsColumn.Text = "Sensors";
            this.SensorsColumn.Width = 70;
            // 
            // configBtn
            // 
            this.configBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.configBtn.BackColor = System.Drawing.SystemColors.GrayText;
            this.configBtn.Enabled = false;
            this.configBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configBtn.Location = new System.Drawing.Point(50, 664);
            this.configBtn.Name = "configBtn";
            this.configBtn.Size = new System.Drawing.Size(206, 45);
            this.configBtn.TabIndex = 21;
            this.configBtn.Text = "Configure gadget";
            this.configBtn.UseVisualStyleBackColor = false;
            this.configBtn.Click += new System.EventHandler(this.configBtn_Click);
            // 
            // idGridView
            // 
            this.idGridView.AllowUserToAddRows = false;
            this.idGridView.AllowUserToDeleteRows = false;
            this.idGridView.AllowUserToResizeColumns = false;
            this.idGridView.AllowUserToResizeRows = false;
            this.idGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.idGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.idGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.functionCol,
            this.functionsCol});
            this.idGridView.Location = new System.Drawing.Point(12, 418);
            this.idGridView.Name = "idGridView";
            this.idGridView.RowHeadersVisible = false;
            this.idGridView.RowHeadersWidth = 51;
            this.idGridView.RowTemplate.Height = 24;
            this.idGridView.Size = new System.Drawing.Size(582, 221);
            this.idGridView.TabIndex = 26;
            // 
            // functionCol
            // 
            this.functionCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.functionCol.FillWeight = 40F;
            this.functionCol.HeaderText = "Function";
            this.functionCol.MinimumWidth = 6;
            this.functionCol.Name = "functionCol";
            this.functionCol.ReadOnly = true;
            this.functionCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.functionCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.functionCol.Width = 150;
            // 
            // functionsCol
            // 
            this.functionsCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.functionsCol.FillWeight = 60F;
            this.functionsCol.HeaderText = "ID";
            this.functionsCol.MinimumWidth = 6;
            this.functionsCol.Name = "functionsCol";
            this.functionsCol.ReadOnly = true;
            this.functionsCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.functionsCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStrilLbl,
            this.generalProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 727);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1162, 44);
            this.statusStrip1.TabIndex = 27;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // loginToolStrilLbl
            // 
            this.loginToolStrilLbl.AutoSize = false;
            this.loginToolStrilLbl.BackColor = System.Drawing.Color.Red;
            this.loginToolStrilLbl.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.loginToolStrilLbl.Name = "loginToolStrilLbl";
            this.loginToolStrilLbl.Size = new System.Drawing.Size(450, 38);
            this.loginToolStrilLbl.Text = "Not logged in";
            // 
            // generalProgressBar
            // 
            this.generalProgressBar.Margin = new System.Windows.Forms.Padding(10, 4, 1, 4);
            this.generalProgressBar.Name = "generalProgressBar";
            this.generalProgressBar.Size = new System.Drawing.Size(200, 36);
            this.generalProgressBar.Step = 1;
            // 
            // deleteConfigBtn
            // 
            this.deleteConfigBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteConfigBtn.BackColor = System.Drawing.SystemColors.GrayText;
            this.deleteConfigBtn.Enabled = false;
            this.deleteConfigBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteConfigBtn.Location = new System.Drawing.Point(301, 664);
            this.deleteConfigBtn.Name = "deleteConfigBtn";
            this.deleteConfigBtn.Size = new System.Drawing.Size(206, 45);
            this.deleteConfigBtn.TabIndex = 28;
            this.deleteConfigBtn.Text = "Delete Config";
            this.deleteConfigBtn.UseVisualStyleBackColor = false;
            this.deleteConfigBtn.Click += new System.EventHandler(this.deleteConfigBtn_Click);
            // 
            // outputBox
            // 
            this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.outputBox.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.outputBox.Location = new System.Drawing.Point(3, 359);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputBox.Size = new System.Drawing.Size(556, 321);
            this.outputBox.TabIndex = 30;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.outputBox, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.WiFiBox, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(600, 39);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(562, 703);
            this.tableLayoutPanel2.TabIndex = 34;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Omgevingsmonitor_configurator.Properties.Resources.Omgevingsmonitor_in_Stedelijk_Landschap;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(556, 212);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // WiFiBox
            // 
            this.WiFiBox.Controls.Add(this.WiFiConfigButton);
            this.WiFiBox.Controls.Add(this.PasswordLabel);
            this.WiFiBox.Controls.Add(this.SSIDLabel);
            this.WiFiBox.Controls.Add(this.PasswordBox);
            this.WiFiBox.Controls.Add(this.SSIDBox);
            this.WiFiBox.Location = new System.Drawing.Point(3, 221);
            this.WiFiBox.Name = "WiFiBox";
            this.WiFiBox.Size = new System.Drawing.Size(556, 132);
            this.WiFiBox.TabIndex = 36;
            this.WiFiBox.TabStop = false;
            this.WiFiBox.Text = "Wi-Fi Config";
            // 
            // WiFiConfigButton
            // 
            this.WiFiConfigButton.Location = new System.Drawing.Point(372, 56);
            this.WiFiConfigButton.Name = "WiFiConfigButton";
            this.WiFiConfigButton.Size = new System.Drawing.Size(132, 33);
            this.WiFiConfigButton.TabIndex = 4;
            this.WiFiConfigButton.Text = "Set Wi-Fi";
            this.WiFiConfigButton.UseVisualStyleBackColor = true;
            this.WiFiConfigButton.Click += new System.EventHandler(this.WiFiConfigButton_Click);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(35, 82);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(67, 16);
            this.PasswordLabel.TabIndex = 3;
            this.PasswordLabel.Text = "Password";
            // 
            // SSIDLabel
            // 
            this.SSIDLabel.AutoSize = true;
            this.SSIDLabel.Location = new System.Drawing.Point(35, 56);
            this.SSIDLabel.Name = "SSIDLabel";
            this.SSIDLabel.Size = new System.Drawing.Size(38, 16);
            this.SSIDLabel.TabIndex = 2;
            this.SSIDLabel.Text = "SSID";
            // 
            // PasswordBox
            // 
            this.PasswordBox.HideSelection = false;
            this.PasswordBox.Location = new System.Drawing.Point(125, 82);
            this.PasswordBox.MaxLength = 64;
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.Size = new System.Drawing.Size(191, 22);
            this.PasswordBox.TabIndex = 1;
            // 
            // SSIDBox
            // 
            this.SSIDBox.Location = new System.Drawing.Point(125, 54);
            this.SSIDBox.MaxLength = 32;
            this.SSIDBox.Name = "SSIDBox";
            this.SSIDBox.Size = new System.Drawing.Size(191, 22);
            this.SSIDBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 771);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.deleteConfigBtn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.idGridView);
            this.Controls.Add(this.configBtn);
            this.Controls.Add(this.userBoxesListView);
            this.Controls.Add(this.addBoxBtn);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1180, 796);
            this.Name = "Form1";
            this.Text = "KITT Engineering Omgevingsmonitor Configurator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.WiFiBox.ResumeLayout(false);
            this.WiFiBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button addBoxBtn;
        private System.Windows.Forms.ListView userBoxesListView;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader ExposureColumn;
        private System.Windows.Forms.ColumnHeader ModelColumn;
        private System.Windows.Forms.ColumnHeader LastMeasurementColumn;
        private System.Windows.Forms.ColumnHeader SensorsColumn;
        private System.Windows.Forms.Button configBtn;
        private System.Windows.Forms.DataGridView idGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionsCol;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel loginToolStrilLbl;
        public System.IO.Ports.SerialPort configPort;
        private System.Windows.Forms.ToolStripComboBox configPortBox;
        private System.Windows.Forms.Button deleteConfigBtn;
        public System.Windows.Forms.ToolStripProgressBar generalProgressBar;
        public System.Windows.Forms.TextBox outputBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox WiFiBox;
        private System.Windows.Forms.TextBox SSIDBox;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label SSIDLabel;
        private System.Windows.Forms.Button WiFiConfigButton;
        private System.Windows.Forms.TextBox PasswordBox;
    }
}

