namespace TranferUSDT
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            btnCheck = new Button();
            lblPrivateKey = new Label();
            txtPrivateKey = new TextBox();
            btnCreateAndTranferUsdt = new Button();
            btnCreateWallet = new Button();
            log = new Panel();
            txtLog = new RichTextBox();
            panel2 = new Panel();
            btnSendToBeoLon = new Button();
            panel1.SuspendLayout();
            log.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCheck);
            panel1.Controls.Add(lblPrivateKey);
            panel1.Controls.Add(txtPrivateKey);
            panel1.Controls.Add(btnCreateAndTranferUsdt);
            panel1.Controls.Add(btnCreateWallet);
            panel1.Location = new Point(12, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(1208, 65);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // btnCheck
            // 
            btnCheck.Location = new Point(1011, 14);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(94, 29);
            btnCheck.TabIndex = 4;
            btnCheck.Text = "Kiểm tra số dư";
            btnCheck.UseVisualStyleBackColor = true;
            btnCheck.Click += btnCheck_Click;
            // 
            // lblPrivateKey
            // 
            lblPrivateKey.AutoSize = true;
            lblPrivateKey.Location = new Point(283, 23);
            lblPrivateKey.Name = "lblPrivateKey";
            lblPrivateKey.Size = new Size(172, 20);
            lblPrivateKey.TabIndex = 3;
            lblPrivateKey.Text = "PrivatKey(Ví chuyển tiền)";
            // 
            // txtPrivateKey
            // 
            txtPrivateKey.Location = new Point(470, 16);
            txtPrivateKey.Name = "txtPrivateKey";
            txtPrivateKey.Size = new Size(294, 27);
            txtPrivateKey.TabIndex = 2;
            // 
            // btnCreateAndTranferUsdt
            // 
            btnCreateAndTranferUsdt.Location = new Point(781, 15);
            btnCreateAndTranferUsdt.Name = "btnCreateAndTranferUsdt";
            btnCreateAndTranferUsdt.Size = new Size(193, 29);
            btnCreateAndTranferUsdt.TabIndex = 1;
            btnCreateAndTranferUsdt.Text = "Tạo và chuyển USDT";
            btnCreateAndTranferUsdt.UseVisualStyleBackColor = true;
            btnCreateAndTranferUsdt.Click += btnCreateAndTranferUsdt_Click;
            // 
            // btnCreateWallet
            // 
            btnCreateWallet.Location = new Point(24, 15);
            btnCreateWallet.Name = "btnCreateWallet";
            btnCreateWallet.Size = new Size(94, 29);
            btnCreateWallet.TabIndex = 0;
            btnCreateWallet.Text = "Tạo ví";
            btnCreateWallet.UseVisualStyleBackColor = true;
            btnCreateWallet.Click += btnCreateWallet_Click;
            // 
            // log
            // 
            log.Controls.Add(txtLog);
            log.Location = new Point(12, 79);
            log.Name = "log";
            log.Size = new Size(1208, 359);
            log.TabIndex = 1;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(3, 6);
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(1205, 353);
            txtLog.TabIndex = 0;
            txtLog.Text = "";
            // 
            // panel2
            // 
            panel2.Controls.Add(btnSendToBeoLon);
            panel2.Location = new Point(12, 465);
            panel2.Name = "panel2";
            panel2.Size = new Size(1208, 384);
            panel2.TabIndex = 2;
            // 
            // btnSendToBeoLon
            // 
            btnSendToBeoLon.Location = new Point(283, 36);
            btnSendToBeoLon.Name = "btnSendToBeoLon";
            btnSendToBeoLon.Size = new Size(562, 29);
            btnSendToBeoLon.TabIndex = 0;
            btnSendToBeoLon.Text = "Chuyển tiền về ví của admin(BÉO LỒN_)";
            btnSendToBeoLon.UseVisualStyleBackColor = true;
            btnSendToBeoLon.Click += btnSendToBeoLon_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1232, 872);
            Controls.Add(panel2);
            Controls.Add(log);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            log.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel log;
        private RichTextBox txtLog;
        private Button btnCreateAndTranferUsdt;
        private Button btnCreateWallet;
        private Label lblPrivateKey;
        private TextBox txtPrivateKey;
        private Button btnCheck;
        private Panel panel2;
        private Button btnSendToBeoLon;
    }
}
