﻿namespace Doviz
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnScan = new System.Windows.Forms.Button();
            this.gbForm = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.tbTel = new System.Windows.Forms.TextBox();
            this.tbRecNo = new System.Windows.Forms.TextBox();
            this.tbDocNo = new System.Windows.Forms.TextBox();
            this.tbDate = new System.Windows.Forms.TextBox();
            this.lblAddres = new System.Windows.Forms.Label();
            this.lblTel = new System.Windows.Forms.Label();
            this.lblRecordNo = new System.Windows.Forms.Label();
            this.lblDocNo = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.pnlPicture = new System.Windows.Forms.Panel();
            this.pbScannedImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbForm.SuspendLayout();
            this.pnlPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbScannedImage)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnScan);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbForm);
            this.splitContainer1.Panel2.Controls.Add(this.pnlPicture);
            this.splitContainer1.Size = new System.Drawing.Size(661, 594);
            this.splitContainer1.SplitterDistance = 43;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(12, 12);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "Tara";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // gbForm
            // 
            this.gbForm.Controls.Add(this.btnPrint);
            this.gbForm.Controls.Add(this.tbAddress);
            this.gbForm.Controls.Add(this.tbTel);
            this.gbForm.Controls.Add(this.tbRecNo);
            this.gbForm.Controls.Add(this.tbDocNo);
            this.gbForm.Controls.Add(this.tbDate);
            this.gbForm.Controls.Add(this.lblAddres);
            this.gbForm.Controls.Add(this.lblTel);
            this.gbForm.Controls.Add(this.lblRecordNo);
            this.gbForm.Controls.Add(this.lblDocNo);
            this.gbForm.Controls.Add(this.lblDate);
            this.gbForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbForm.Location = new System.Drawing.Point(0, 265);
            this.gbForm.Name = "gbForm";
            this.gbForm.Size = new System.Drawing.Size(661, 282);
            this.gbForm.TabIndex = 2;
            this.gbForm.TabStop = false;
            this.gbForm.Text = "Müşteri Bilgi Formu";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(181, 208);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "Yazdır";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(93, 128);
            this.tbAddress.Multiline = true;
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(163, 74);
            this.tbAddress.TabIndex = 9;
            // 
            // tbTel
            // 
            this.tbTel.Location = new System.Drawing.Point(93, 104);
            this.tbTel.Name = "tbTel";
            this.tbTel.Size = new System.Drawing.Size(163, 20);
            this.tbTel.TabIndex = 8;
            // 
            // tbRecNo
            // 
            this.tbRecNo.Location = new System.Drawing.Point(93, 79);
            this.tbRecNo.Name = "tbRecNo";
            this.tbRecNo.Size = new System.Drawing.Size(163, 20);
            this.tbRecNo.TabIndex = 7;
            // 
            // tbDocNo
            // 
            this.tbDocNo.Location = new System.Drawing.Point(93, 55);
            this.tbDocNo.Name = "tbDocNo";
            this.tbDocNo.Size = new System.Drawing.Size(163, 20);
            this.tbDocNo.TabIndex = 6;
            // 
            // tbDate
            // 
            this.tbDate.Location = new System.Drawing.Point(93, 31);
            this.tbDate.Name = "tbDate";
            this.tbDate.Size = new System.Drawing.Size(163, 20);
            this.tbDate.TabIndex = 5;
            // 
            // lblAddres
            // 
            this.lblAddres.AutoSize = true;
            this.lblAddres.Location = new System.Drawing.Point(50, 131);
            this.lblAddres.Name = "lblAddres";
            this.lblAddres.Size = new System.Drawing.Size(37, 13);
            this.lblAddres.TabIndex = 4;
            this.lblAddres.Text = "Adres:";
            // 
            // lblTel
            // 
            this.lblTel.AutoSize = true;
            this.lblTel.Location = new System.Drawing.Point(41, 107);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(46, 13);
            this.lblTel.TabIndex = 3;
            this.lblTel.Text = "Telefon:";
            // 
            // lblRecordNo
            // 
            this.lblRecordNo.AutoSize = true;
            this.lblRecordNo.Location = new System.Drawing.Point(37, 82);
            this.lblRecordNo.Name = "lblRecordNo";
            this.lblRecordNo.Size = new System.Drawing.Size(50, 13);
            this.lblRecordNo.TabIndex = 2;
            this.lblRecordNo.Text = "Kayıt No:";
            // 
            // lblDocNo
            // 
            this.lblDocNo.AutoSize = true;
            this.lblDocNo.Location = new System.Drawing.Point(17, 58);
            this.lblDocNo.Name = "lblDocNo";
            this.lblDocNo.Size = new System.Drawing.Size(70, 13);
            this.lblDocNo.TabIndex = 1;
            this.lblDocNo.Text = "Fiş Belge No:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(53, 34);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(34, 13);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Tarih:";
            // 
            // pnlPicture
            // 
            this.pnlPicture.Controls.Add(this.pbScannedImage);
            this.pnlPicture.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPicture.Location = new System.Drawing.Point(0, 0);
            this.pnlPicture.Name = "pnlPicture";
            this.pnlPicture.Size = new System.Drawing.Size(661, 265);
            this.pnlPicture.TabIndex = 1;
            // 
            // pbScannedImage
            // 
            this.pbScannedImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbScannedImage.Image = ((System.Drawing.Image)(resources.GetObject("pbScannedImage.Image")));
            this.pbScannedImage.Location = new System.Drawing.Point(0, 0);
            this.pbScannedImage.Margin = new System.Windows.Forms.Padding(0);
            this.pbScannedImage.Name = "pbScannedImage";
            this.pbScannedImage.Size = new System.Drawing.Size(661, 265);
            this.pbScannedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbScannedImage.TabIndex = 1;
            this.pbScannedImage.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 594);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmMain";
            this.Text = "Exchange Bureau Scanner";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbForm.ResumeLayout(false);
            this.gbForm.PerformLayout();
            this.pnlPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbScannedImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.GroupBox gbForm;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.TextBox tbTel;
        private System.Windows.Forms.TextBox tbRecNo;
        private System.Windows.Forms.TextBox tbDocNo;
        private System.Windows.Forms.TextBox tbDate;
        private System.Windows.Forms.Label lblAddres;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label lblRecordNo;
        private System.Windows.Forms.Label lblDocNo;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Panel pnlPicture;
        private System.Windows.Forms.PictureBox pbScannedImage;
    }
}

