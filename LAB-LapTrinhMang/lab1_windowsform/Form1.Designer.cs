namespace lab1_windowsform
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblInput = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnResolve = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblHostNameTitle = new System.Windows.Forms.Label();
            this.lblHostNameValue = new System.Windows.Forms.Label();
            this.grpIPv4 = new System.Windows.Forms.GroupBox();
            this.lstIPv4 = new System.Windows.Forms.ListBox();
            this.grpIPv6 = new System.Windows.Forms.GroupBox();
            this.lstIPv6 = new System.Windows.Forms.ListBox();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpIPv4.SuspendLayout();
            this.grpIPv6.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(220, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(320, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PHÂN GIẢI TÊN MIỀN DNS";
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInput.Location = new System.Drawing.Point(30, 70);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(160, 19);
            this.lblInput.TabIndex = 1;
            this.lblInput.Text = "Nhập Domain hoặc IP:";
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtInput.Location = new System.Drawing.Point(200, 67);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(560, 27);
            this.txtInput.TabIndex = 2;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // btnResolve
            // 
            this.btnResolve.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnResolve.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnResolve.Location = new System.Drawing.Point(200, 110);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(120, 40);
            this.btnResolve.TabIndex = 3;
            this.btnResolve.Text = "Phân giải";
            this.btnResolve.UseVisualStyleBackColor = false;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClear.Location = new System.Drawing.Point(340, 110);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 40);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Xóa";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightCoral;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnExit.Location = new System.Drawing.Point(460, 110);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 40);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblHostNameTitle
            // 
            this.lblHostNameTitle.AutoSize = true;
            this.lblHostNameTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHostNameTitle.Location = new System.Drawing.Point(30, 175);
            this.lblHostNameTitle.Name = "lblHostNameTitle";
            this.lblHostNameTitle.Size = new System.Drawing.Size(85, 19);
            this.lblHostNameTitle.TabIndex = 6;
            this.lblHostNameTitle.Text = "HostName:";
            // 
            // lblHostNameValue
            // 
            this.lblHostNameValue.AutoSize = true;
            this.lblHostNameValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHostNameValue.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblHostNameValue.Location = new System.Drawing.Point(130, 173);
            this.lblHostNameValue.Name = "lblHostNameValue";
            this.lblHostNameValue.Size = new System.Drawing.Size(0, 20);
            this.lblHostNameValue.TabIndex = 7;
            // 
            // grpIPv4
            // 
            this.grpIPv4.Controls.Add(this.lstIPv4);
            this.grpIPv4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpIPv4.ForeColor = System.Drawing.Color.DarkBlue;
            this.grpIPv4.Location = new System.Drawing.Point(30, 210);
            this.grpIPv4.Name = "grpIPv4";
            this.grpIPv4.Size = new System.Drawing.Size(360, 200);
            this.grpIPv4.TabIndex = 8;
            this.grpIPv4.TabStop = false;
            this.grpIPv4.Text = "IPv4 Addresses";
            // 
            // lstIPv4
            // 
            this.lstIPv4.Font = new System.Drawing.Font("Consolas", 10F);
            this.lstIPv4.FormattingEnabled = true;
            this.lstIPv4.ItemHeight = 17;
            this.lstIPv4.Location = new System.Drawing.Point(15, 30);
            this.lstIPv4.Name = "lstIPv4";
            this.lstIPv4.Size = new System.Drawing.Size(330, 157);
            this.lstIPv4.TabIndex = 0;
            this.lstIPv4.DoubleClick += new System.EventHandler(this.lstIPv4_DoubleClick);
            // 
            // grpIPv6
            // 
            this.grpIPv6.Controls.Add(this.lstIPv6);
            this.grpIPv6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpIPv6.ForeColor = System.Drawing.Color.DarkRed;
            this.grpIPv6.Location = new System.Drawing.Point(410, 210);
            this.grpIPv6.Name = "grpIPv6";
            this.grpIPv6.Size = new System.Drawing.Size(360, 200);
            this.grpIPv6.TabIndex = 9;
            this.grpIPv6.TabStop = false;
            this.grpIPv6.Text = "IPv6 Addresses";
            // 
            // lstIPv6
            // 
            this.lstIPv6.Font = new System.Drawing.Font("Consolas", 9F);
            this.lstIPv6.FormattingEnabled = true;
            this.lstIPv6.ItemHeight = 15;
            this.lstIPv6.Location = new System.Drawing.Point(15, 30);
            this.lstIPv6.Name = "lstIPv6";
            this.lstIPv6.Size = new System.Drawing.Size(330, 154);
            this.lstIPv6.TabIndex = 0;
            this.lstIPv6.DoubleClick += new System.EventHandler(this.lstIPv6_DoubleClick);
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatusTitle.Location = new System.Drawing.Point(30, 430);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(75, 19);
            this.lblStatusTitle.TabIndex = 10;
            this.lblStatusTitle.Text = "Trạng thái:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblStatus.Location = new System.Drawing.Point(130, 430);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 19);
            this.lblStatus.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 470);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblStatusTitle);
            this.Controls.Add(this.grpIPv6);
            this.Controls.Add(this.grpIPv4);
            this.Controls.Add(this.lblHostNameValue);
            this.Controls.Add(this.lblHostNameTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnResolve);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bài 1 - Phân giải tên miền DNS";
            this.grpIPv4.ResumeLayout(false);
            this.grpIPv6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblHostNameTitle;
        private System.Windows.Forms.Label lblHostNameValue;
        private System.Windows.Forms.GroupBox grpIPv4;
        private System.Windows.Forms.ListBox lstIPv4;
        private System.Windows.Forms.GroupBox grpIPv6;
        private System.Windows.Forms.ListBox lstIPv6;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label lblStatus;
    }
}