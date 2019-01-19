namespace DataDepersonalizer {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.txtEmailFolder = new System.Windows.Forms.TextBox();
			this.btnOpenEmailFolder = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.label2 = new System.Windows.Forms.Label();
			this.txtReplaceMask = new System.Windows.Forms.TextBox();
			this.btnDepersonalize = new System.Windows.Forms.Button();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtStartFrom = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtReplaceIpAddr = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtEncoding = new System.Windows.Forms.TextBox();
			this.cbWriteBom = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtXmlNodes = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtReplaceWith = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(26, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Source Folder";
			// 
			// txtEmailFolder
			// 
			this.txtEmailFolder.Location = new System.Drawing.Point(136, 22);
			this.txtEmailFolder.Name = "txtEmailFolder";
			this.txtEmailFolder.Size = new System.Drawing.Size(379, 20);
			this.txtEmailFolder.TabIndex = 1;
			this.txtEmailFolder.Text = ".\\data";
			// 
			// btnOpenEmailFolder
			// 
			this.btnOpenEmailFolder.Location = new System.Drawing.Point(521, 20);
			this.btnOpenEmailFolder.Name = "btnOpenEmailFolder";
			this.btnOpenEmailFolder.Size = new System.Drawing.Size(24, 23);
			this.btnOpenEmailFolder.TabIndex = 2;
			this.btnOpenEmailFolder.Text = "...";
			this.btnOpenEmailFolder.UseVisualStyleBackColor = true;
			this.btnOpenEmailFolder.Click += new System.EventHandler(this.btnOpenEmailFolder_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(26, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Email Replace Mask";
			// 
			// txtReplaceMask
			// 
			this.txtReplaceMask.Location = new System.Drawing.Point(136, 89);
			this.txtReplaceMask.Name = "txtReplaceMask";
			this.txtReplaceMask.Size = new System.Drawing.Size(379, 20);
			this.txtReplaceMask.TabIndex = 4;
			this.txtReplaceMask.Text = "recipient{0}@example.com";
			// 
			// btnDepersonalize
			// 
			this.btnDepersonalize.Location = new System.Drawing.Point(29, 321);
			this.btnDepersonalize.Name = "btnDepersonalize";
			this.btnDepersonalize.Size = new System.Drawing.Size(131, 23);
			this.btnDepersonalize.TabIndex = 30;
			this.btnDepersonalize.Text = "Depersonalize Data";
			this.btnDepersonalize.UseVisualStyleBackColor = true;
			this.btnDepersonalize.Click += new System.EventHandler(this.btnDepersonalize_Click);
			// 
			// txtLog
			// 
			this.txtLog.Location = new System.Drawing.Point(29, 361);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog.Size = new System.Drawing.Size(516, 155);
			this.txtLog.TabIndex = 31;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(26, 58);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Start from Number";
			// 
			// txtStartFrom
			// 
			this.txtStartFrom.Location = new System.Drawing.Point(136, 55);
			this.txtStartFrom.Name = "txtStartFrom";
			this.txtStartFrom.Size = new System.Drawing.Size(100, 20);
			this.txtStartFrom.TabIndex = 3;
			this.txtStartFrom.Text = "1";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(26, 127);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(89, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "IP Replace Mask";
			// 
			// txtReplaceIpAddr
			// 
			this.txtReplaceIpAddr.Location = new System.Drawing.Point(136, 124);
			this.txtReplaceIpAddr.Name = "txtReplaceIpAddr";
			this.txtReplaceIpAddr.Size = new System.Drawing.Size(379, 20);
			this.txtReplaceIpAddr.TabIndex = 9;
			this.txtReplaceIpAddr.Text = "127.0.0.{0}";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(26, 162);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Encoding";
			// 
			// txtEncoding
			// 
			this.txtEncoding.Location = new System.Drawing.Point(136, 159);
			this.txtEncoding.Name = "txtEncoding";
			this.txtEncoding.Size = new System.Drawing.Size(100, 20);
			this.txtEncoding.TabIndex = 10;
			this.txtEncoding.Text = "ASCII";
			// 
			// cbWriteBom
			// 
			this.cbWriteBom.AutoSize = true;
			this.cbWriteBom.Checked = true;
			this.cbWriteBom.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbWriteBom.Location = new System.Drawing.Point(273, 161);
			this.cbWriteBom.Name = "cbWriteBom";
			this.cbWriteBom.Size = new System.Drawing.Size(78, 17);
			this.cbWriteBom.TabIndex = 13;
			this.cbWriteBom.Text = "Write BOM";
			this.cbWriteBom.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(26, 190);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(94, 13);
			this.label6.TabIndex = 16;
			this.label6.Text = "XML Node Names";
			// 
			// txtXmlNodes
			// 
			this.txtXmlNodes.Location = new System.Drawing.Point(29, 220);
			this.txtXmlNodes.Multiline = true;
			this.txtXmlNodes.Name = "txtXmlNodes";
			this.txtXmlNodes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtXmlNodes.Size = new System.Drawing.Size(207, 84);
			this.txtXmlNodes.TabIndex = 17;
			this.txtXmlNodes.Text = "Phone\r\nPhone2\r\nFax\r\nLastName\r\nCompany\r\nStreet1\r\nStreet2\r\nPostalCode\r\nRegName\r\nVat" +
    "Id";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(270, 190);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(116, 13);
			this.label7.TabIndex = 18;
			this.label7.Text = "Replace Content Mask";
			// 
			// txtReplaceWith
			// 
			this.txtReplaceWith.Location = new System.Drawing.Point(273, 220);
			this.txtReplaceWith.Multiline = true;
			this.txtReplaceWith.Name = "txtReplaceWith";
			this.txtReplaceWith.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtReplaceWith.Size = new System.Drawing.Size(272, 84);
			this.txtReplaceWith.TabIndex = 19;
			this.txtReplaceWith.Text = "+12345678901\r\n+12345678902\r\n+12345678903\r\nLastname{0}\r\nCompany Name {0} Inc.\r\nLak" +
    "eside Street {0}\r\nLakeside Street {0}\r\n1234-567\r\nCompany Name {0} Inc.\r\n12345678" +
    "901";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(567, 528);
			this.Controls.Add(this.txtReplaceWith);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.txtXmlNodes);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.cbWriteBom);
			this.Controls.Add(this.txtEncoding);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtReplaceIpAddr);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtStartFrom);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.btnDepersonalize);
			this.Controls.Add(this.txtReplaceMask);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnOpenEmailFolder);
			this.Controls.Add(this.txtEmailFolder);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "Data Depersonalizer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtEmailFolder;
		private System.Windows.Forms.Button btnOpenEmailFolder;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtReplaceMask;
		private System.Windows.Forms.Button btnDepersonalize;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtStartFrom;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtReplaceIpAddr;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtEncoding;
		private System.Windows.Forms.CheckBox cbWriteBom;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtXmlNodes;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtReplaceWith;
	}
}

