using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Doviz
{
    public partial class frmPrint : Form
    {
        private Musteri _musteri;
        public frmPrint(Musteri musteri)
        {
            InitializeComponent();
            _musteri = musteri;
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            var pDate = new ReportParameter("Date", _musteri.Date);
            var pDocumentNo = new ReportParameter("DocumentNo", _musteri.DocumentNo);
            var pRecNo = new ReportParameter("RecNo", _musteri.RecNo);
            var pAddress = new ReportParameter("Address", _musteri.Address);
            var pTel = new ReportParameter("Tel", _musteri.Tel);
            var pScannedImage = new ReportParameter("ScannedImage", _musteri.ScannedImage, true);
            var pScannedImageMimeType = new ReportParameter("MimeType", _musteri.ScannedImageFormat, true);
            report.LocalReport.SetParameters(new[] { pDate, pDocumentNo, pRecNo, pAddress, pTel, pScannedImage, pScannedImageMimeType });
            report.RenderingComplete += (snd, earg) =>
                                        {
                                            var a = report.PrintDialog();
                                            switch (a)
                                            {
                                                case DialogResult.Abort:
                                                case DialogResult.Cancel:
                                                case DialogResult.Ignore:
                                                case DialogResult.No:
                                                case DialogResult.None:
                                                case DialogResult.OK:
                                                case DialogResult.Retry:
                                                case DialogResult.Yes:
                                                    this.Close();
                                                    break;
                                            }
                                        };
            report.RefreshReport();
        }
    }
}
