namespace Omgevingsmonitor_configurator
{
    partial class AddSenseBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSenseBoxForm));
            this.locatieControl = new GMap.NET.WindowsForms.GMapControl();
            this.label1 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.latitudeBox = new System.Windows.Forms.TextBox();
            this.exposureBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.longitudeBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sensorGridView = new System.Windows.Forms.DataGridView();
            this.enableCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.functionsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.submitBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.heightBox = new System.Windows.Forms.TextBox();
            this.sensirionCheckBox = new System.Windows.Forms.CheckBox();
            this.sen50Select = new System.Windows.Forms.RadioButton();
            this.sen55Select = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.sensorGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // locatieControl
            // 
            this.locatieControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.locatieControl.Bearing = 0F;
            this.locatieControl.CanDragMap = true;
            this.locatieControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.locatieControl.GrayScaleMode = false;
            this.locatieControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.locatieControl.LevelsKeepInMemory = 5;
            this.locatieControl.Location = new System.Drawing.Point(388, 12);
            this.locatieControl.MarkersEnabled = true;
            this.locatieControl.MaxZoom = 2;
            this.locatieControl.MinZoom = -10;
            this.locatieControl.MouseWheelZoomEnabled = true;
            this.locatieControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.locatieControl.Name = "locatieControl";
            this.locatieControl.NegativeMode = false;
            this.locatieControl.PolygonsEnabled = true;
            this.locatieControl.RetryLoadTile = 0;
            this.locatieControl.RoutesEnabled = true;
            this.locatieControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.locatieControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.locatieControl.ShowTileGridLines = false;
            this.locatieControl.Size = new System.Drawing.Size(544, 570);
            this.locatieControl.TabIndex = 3;
            this.locatieControl.Zoom = -4D;
            this.locatieControl.OnPositionChanged += new GMap.NET.PositionChanged(this.locatieControl_OnPositionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Name:";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(118, 12);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(264, 22);
            this.nameBox.TabIndex = 1;
            // 
            // latitudeBox
            // 
            this.latitudeBox.Enabled = false;
            this.latitudeBox.Location = new System.Drawing.Point(118, 71);
            this.latitudeBox.Name = "latitudeBox";
            this.latitudeBox.Size = new System.Drawing.Size(264, 22);
            this.latitudeBox.TabIndex = 11;
            // 
            // exposureBox
            // 
            this.exposureBox.FormattingEnabled = true;
            this.exposureBox.Items.AddRange(new object[] {
            "indoor",
            "outdoor",
            "mobile"});
            this.exposureBox.Location = new System.Drawing.Point(118, 41);
            this.exposureBox.Name = "exposureBox";
            this.exposureBox.Size = new System.Drawing.Size(264, 24);
            this.exposureBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Exposure:";
            // 
            // longitudeBox
            // 
            this.longitudeBox.Enabled = false;
            this.longitudeBox.Location = new System.Drawing.Point(118, 99);
            this.longitudeBox.Name = "longitudeBox";
            this.longitudeBox.Size = new System.Drawing.Size(264, 22);
            this.longitudeBox.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Latitude:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Longlitude:";
            // 
            // sensorGridView
            // 
            this.sensorGridView.AllowUserToAddRows = false;
            this.sensorGridView.AllowUserToDeleteRows = false;
            this.sensorGridView.AllowUserToResizeColumns = false;
            this.sensorGridView.AllowUserToResizeRows = false;
            this.sensorGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sensorGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enableCol,
            this.functionsCol});
            this.sensorGridView.Location = new System.Drawing.Point(118, 268);
            this.sensorGridView.Name = "sensorGridView";
            this.sensorGridView.RowHeadersVisible = false;
            this.sensorGridView.RowHeadersWidth = 51;
            this.sensorGridView.RowTemplate.Height = 24;
            this.sensorGridView.Size = new System.Drawing.Size(264, 313);
            this.sensorGridView.TabIndex = 17;
            // 
            // enableCol
            // 
            this.enableCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.enableCol.HeaderText = "Enabled";
            this.enableCol.MinimumWidth = 6;
            this.enableCol.Name = "enableCol";
            this.enableCol.ReadOnly = true;
            this.enableCol.Width = 66;
            // 
            // functionsCol
            // 
            this.functionsCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.functionsCol.HeaderText = "Functions";
            this.functionsCol.MinimumWidth = 6;
            this.functionsCol.Name = "functionsCol";
            this.functionsCol.ReadOnly = true;
            // 
            // submitBtn
            // 
            this.submitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.submitBtn.Location = new System.Drawing.Point(528, 588);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(404, 66);
            this.submitBtn.TabIndex = 9;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelBtn.Location = new System.Drawing.Point(118, 588);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(404, 66);
            this.cancelBtn.TabIndex = 10;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Sensors:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Height:";
            // 
            // heightBox
            // 
            this.heightBox.Location = new System.Drawing.Point(118, 127);
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(264, 22);
            this.heightBox.TabIndex = 4;
            this.heightBox.Text = "1.0";
            // 
            // sensirionCheckBox
            // 
            this.sensirionCheckBox.AutoSize = true;
            this.sensirionCheckBox.Location = new System.Drawing.Point(118, 187);
            this.sensirionCheckBox.Name = "sensirionCheckBox";
            this.sensirionCheckBox.Size = new System.Drawing.Size(177, 21);
            this.sensirionCheckBox.TabIndex = 6;
            this.sensirionCheckBox.Text = "Added Sensirion Sen5x";
            this.sensirionCheckBox.UseVisualStyleBackColor = true;
            this.sensirionCheckBox.CheckedChanged += new System.EventHandler(this.sensirionCheckBox_CheckedChanged);
            // 
            // sen50Select
            // 
            this.sen50Select.AutoSize = true;
            this.sen50Select.Checked = true;
            this.sen50Select.Enabled = false;
            this.sen50Select.Location = new System.Drawing.Point(139, 214);
            this.sen50Select.Name = "sen50Select";
            this.sen50Select.Size = new System.Drawing.Size(178, 21);
            this.sen50Select.TabIndex = 7;
            this.sen50Select.TabStop = true;
            this.sen50Select.Text = "Sensirion Sen50/Sen54";
            this.sen50Select.UseVisualStyleBackColor = true;
            this.sen50Select.CheckedChanged += new System.EventHandler(this.sen50Select_CheckedChanged);
            // 
            // sen55Select
            // 
            this.sen55Select.AutoSize = true;
            this.sen55Select.Enabled = false;
            this.sen55Select.Location = new System.Drawing.Point(139, 241);
            this.sen55Select.Name = "sen55Select";
            this.sen55Select.Size = new System.Drawing.Size(133, 21);
            this.sen55Select.TabIndex = 8;
            this.sen55Select.Text = "Sensirion Sen55";
            this.sen55Select.UseVisualStyleBackColor = true;
            this.sen55Select.CheckedChanged += new System.EventHandler(this.sen55Select_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(118, 159);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 21);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Default";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // AddSenseBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 664);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.sen55Select);
            this.Controls.Add(this.sen50Select);
            this.Controls.Add(this.sensirionCheckBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.heightBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.sensorGridView);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.longitudeBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.exposureBox);
            this.Controls.Add(this.latitudeBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.locatieControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddSenseBoxForm";
            this.Text = "AddSenseBox";
            ((System.ComponentModel.ISupportInitialize)(this.sensorGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl locatieControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox latitudeBox;
        private System.Windows.Forms.ComboBox exposureBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox longitudeBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView sensorGridView;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox heightBox;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enableCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionsCol;
        private System.Windows.Forms.CheckBox sensirionCheckBox;
        private System.Windows.Forms.RadioButton sen50Select;
        private System.Windows.Forms.RadioButton sen55Select;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}