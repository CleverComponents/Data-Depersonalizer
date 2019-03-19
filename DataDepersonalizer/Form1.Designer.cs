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
			this.tabDataTypes = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.txtXmlReplaceWith = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtXmlNodes = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.label8 = new System.Windows.Forms.Label();
			this.cbDataType = new System.Windows.Forms.ComboBox();
			this.txtPairReplaceWith = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.txtPairNames = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.txtRegexReplaceWith = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.txtRegexPatterns = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.tabDataTypes.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
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
			this.btnDepersonalize.Location = new System.Drawing.Point(29, 390);
			this.btnDepersonalize.Name = "btnDepersonalize";
			this.btnDepersonalize.Size = new System.Drawing.Size(131, 23);
			this.btnDepersonalize.TabIndex = 36;
			this.btnDepersonalize.Text = "Depersonalize Data";
			this.btnDepersonalize.UseVisualStyleBackColor = true;
			this.btnDepersonalize.Click += new System.EventHandler(this.btnDepersonalize_Click);
			// 
			// txtLog
			// 
			this.txtLog.Location = new System.Drawing.Point(29, 426);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog.Size = new System.Drawing.Size(516, 113);
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
			this.label5.Location = new System.Drawing.Point(26, 157);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Encoding";
			// 
			// txtEncoding
			// 
			this.txtEncoding.Location = new System.Drawing.Point(136, 154);
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
			this.cbWriteBom.Location = new System.Drawing.Point(275, 156);
			this.cbWriteBom.Name = "cbWriteBom";
			this.cbWriteBom.Size = new System.Drawing.Size(78, 17);
			this.cbWriteBom.TabIndex = 13;
			this.cbWriteBom.Text = "Write BOM";
			this.cbWriteBom.UseVisualStyleBackColor = true;
			// 
			// tabDataTypes
			// 
			this.tabDataTypes.Controls.Add(this.tabPage1);
			this.tabDataTypes.Controls.Add(this.tabPage2);
			this.tabDataTypes.Controls.Add(this.tabPage3);
			this.tabDataTypes.Location = new System.Drawing.Point(29, 212);
			this.tabDataTypes.Name = "tabDataTypes";
			this.tabDataTypes.SelectedIndex = 0;
			this.tabDataTypes.Size = new System.Drawing.Size(516, 163);
			this.tabDataTypes.TabIndex = 35;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.txtXmlReplaceWith);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.txtXmlNodes);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(508, 137);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "XML Document";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// txtXmlReplaceWith
			// 
			this.txtXmlReplaceWith.Location = new System.Drawing.Point(242, 41);
			this.txtXmlReplaceWith.Multiline = true;
			this.txtXmlReplaceWith.Name = "txtXmlReplaceWith";
			this.txtXmlReplaceWith.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtXmlReplaceWith.Size = new System.Drawing.Size(248, 84);
			this.txtXmlReplaceWith.TabIndex = 23;
			this.txtXmlReplaceWith.Text = "+12345678901\r\n+12345678902\r\n+12345678903\r\nLastname{0}\r\nCompany Name {0} Inc.\r\nLak" +
    "eside Street {0}\r\nLakeside Street {0}\r\n1234-567\r\nCompany Name {0} Inc.\r\n12345678" +
    "901";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(239, 11);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(116, 13);
			this.label7.TabIndex = 22;
			this.label7.Text = "Replace Content Mask";
			// 
			// txtXmlNodes
			// 
			this.txtXmlNodes.Location = new System.Drawing.Point(15, 41);
			this.txtXmlNodes.Multiline = true;
			this.txtXmlNodes.Name = "txtXmlNodes";
			this.txtXmlNodes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtXmlNodes.Size = new System.Drawing.Size(207, 84);
			this.txtXmlNodes.TabIndex = 21;
			this.txtXmlNodes.Text = "Phone\r\nPhone2\r\nFax\r\nLastName\r\nCompany\r\nStreet1\r\nStreet2\r\nPostalCode\r\nRegName\r\nVat" +
    "Id";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 11);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(94, 13);
			this.label6.TabIndex = 20;
			this.label6.Text = "XML Node Names";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.txtPairReplaceWith);
			this.tabPage2.Controls.Add(this.label9);
			this.tabPage2.Controls.Add(this.txtPairNames);
			this.tabPage2.Controls.Add(this.label10);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(508, 137);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Name : Value Pairs";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(26, 188);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(57, 13);
			this.label8.TabIndex = 33;
			this.label8.Text = "Data Type";
			// 
			// cbDataType
			// 
			this.cbDataType.DisplayMember = "0";
			this.cbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDataType.FormattingEnabled = true;
			this.cbDataType.Items.AddRange(new object[] {
            "Text",
            "XML Document",
            "Name : Value Pairs"});
			this.cbDataType.Location = new System.Drawing.Point(136, 185);
			this.cbDataType.Name = "cbDataType";
			this.cbDataType.Size = new System.Drawing.Size(153, 21);
			this.cbDataType.TabIndex = 34;
			// 
			// txtPairReplaceWith
			// 
			this.txtPairReplaceWith.Location = new System.Drawing.Point(242, 41);
			this.txtPairReplaceWith.Multiline = true;
			this.txtPairReplaceWith.Name = "txtPairReplaceWith";
			this.txtPairReplaceWith.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtPairReplaceWith.Size = new System.Drawing.Size(248, 84);
			this.txtPairReplaceWith.TabIndex = 27;
			this.txtPairReplaceWith.Text = "1{0:D6}\r\nName {0}\r\nName {0}\r\nCompany Name {0} Inc.\r\nLakeside {0}\r\nStreet {0}\r\nDis" +
    "trict {0}\r\n123-456\r\n+12345678901";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(239, 11);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(116, 13);
			this.label9.TabIndex = 26;
			this.label9.Text = "Replace Content Mask";
			// 
			// txtPairNames
			// 
			this.txtPairNames.Location = new System.Drawing.Point(15, 41);
			this.txtPairNames.Multiline = true;
			this.txtPairNames.Name = "txtPairNames";
			this.txtPairNames.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtPairNames.Size = new System.Drawing.Size(207, 84);
			this.txtPairNames.TabIndex = 25;
			this.txtPairNames.Text = "Tracking ID\r\nName\r\nOrder Name\r\nCompany\r\nAddress1\r\nAddress2\r\nAddress3\r\nZip\r\nPhone";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(12, 11);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 13);
			this.label10.TabIndex = 24;
			this.label10.Text = "Names";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.txtRegexReplaceWith);
			this.tabPage3.Controls.Add(this.label11);
			this.tabPage3.Controls.Add(this.txtRegexPatterns);
			this.tabPage3.Controls.Add(this.label12);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(508, 137);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Custom Regex Patterns";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// txtRegexReplaceWith
			// 
			this.txtRegexReplaceWith.Location = new System.Drawing.Point(242, 41);
			this.txtRegexReplaceWith.Multiline = true;
			this.txtRegexReplaceWith.Name = "txtRegexReplaceWith";
			this.txtRegexReplaceWith.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtRegexReplaceWith.Size = new System.Drawing.Size(248, 84);
			this.txtRegexReplaceWith.TabIndex = 31;
			this.txtRegexReplaceWith.Text = "12345678-12345-{0}\r\ntrackingid=1{0:D6}&sid=2{0:D9}";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(239, 11);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(116, 13);
			this.label11.TabIndex = 30;
			this.label11.Text = "Replace Content Mask";
			// 
			// txtRegexPatterns
			// 
			this.txtRegexPatterns.Location = new System.Drawing.Point(15, 41);
			this.txtRegexPatterns.Multiline = true;
			this.txtRegexPatterns.Name = "txtRegexPatterns";
			this.txtRegexPatterns.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtRegexPatterns.Size = new System.Drawing.Size(207, 84);
			this.txtRegexPatterns.TabIndex = 29;
			this.txtRegexPatterns.Text = "[0-9]{8}-[0-9]{5}-[0-9]{1,3}\r\ntrackingid=[0-9]{7}&sid=[0-9]{10}";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(12, 11);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(80, 13);
			this.label12.TabIndex = 28;
			this.label12.Text = "Regex Patterns";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(248, 58);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(177, 13);
			this.label13.TabIndex = 35;
			this.label13.Text = "- is used as {0} replacement pattern.";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(567, 551);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.cbDataType);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.tabDataTypes);
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
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.tabDataTypes.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
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
		private System.Windows.Forms.TabControl tabDataTypes;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox txtXmlReplaceWith;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtXmlNodes;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cbDataType;
		private System.Windows.Forms.TextBox txtPairReplaceWith;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtPairNames;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TextBox txtRegexReplaceWith;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtRegexPatterns;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
	}
}

