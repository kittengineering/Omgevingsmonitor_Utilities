
namespace Omgevingsmonitor_configurator
{
    partial class Beurs_configurator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Beurs_configurator));
            this.configBtn = new System.Windows.Forms.Button();
            this.customNameBox = new System.Windows.Forms.TextBox();
            this.configProgressBar = new System.Windows.Forms.ProgressBar();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // configBtn
            // 
            this.configBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.configBtn.Location = new System.Drawing.Point(12, 161);
            this.configBtn.Name = "configBtn";
            this.configBtn.Size = new System.Drawing.Size(776, 187);
            this.configBtn.TabIndex = 0;
            this.configBtn.Text = "Start config";
            this.configBtn.UseVisualStyleBackColor = true;
            this.configBtn.Click += new System.EventHandler(this.configBtn_Click);
            // 
            // customNameBox
            // 
            this.customNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customNameBox.Location = new System.Drawing.Point(12, 12);
            this.customNameBox.MaxLength = 30;
            this.customNameBox.Name = "customNameBox";
            this.customNameBox.Size = new System.Drawing.Size(776, 143);
            this.customNameBox.TabIndex = 1;
            // 
            // configProgressBar
            // 
            this.configProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.configProgressBar.Location = new System.Drawing.Point(12, 354);
            this.configProgressBar.Name = "configProgressBar";
            this.configProgressBar.Size = new System.Drawing.Size(776, 84);
            this.configProgressBar.TabIndex = 2;
            // 
            // outputBox
            // 
            this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.outputBox.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputBox.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.outputBox.Location = new System.Drawing.Point(12, 444);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputBox.Size = new System.Drawing.Size(776, 245);
            this.outputBox.TabIndex = 31;
            // 
            // Beurs_configurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 701);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.configProgressBar);
            this.Controls.Add(this.customNameBox);
            this.Controls.Add(this.configBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Beurs_configurator";
            this.Text = "Beurs_configurator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Beurs_configurator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button configBtn;
        private System.Windows.Forms.TextBox customNameBox;
        private System.Windows.Forms.ProgressBar configProgressBar;
        public System.Windows.Forms.TextBox outputBox;
    }
}