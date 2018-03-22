using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
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
            setupPage();
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
                                                    //this.Close();
                                                    break;
                                            }
                                        };
            report.RefreshReport();
        }

        void setupPage()
        {
            report.SetDisplayMode(DisplayMode.PrintLayout);
            var pg = new PageSettings
                     {
                         Margins =
                         {
                             Left = 80,
                             Right = 80,
                             Top = 20,
                             Bottom = 20
                         }
                     };
            //var size = new PaperSize();
            //size.RawKind = (int)PaperKind.A4;
            //pg.PaperSize = size;
            report.SetPageSettings(pg);
            report.RefreshReport();
        }


        static float dotsPerInch;

        static float DotsPerInch
        {
            get
            {
                if (0 == dotsPerInch)
                {
                    using (Bitmap bitmap = new Bitmap(1, 1))
                    using (Graphics gr = Graphics.FromImage(bitmap))
                    {
                        dotsPerInch = gr.DpiX;
                    }
                }
                return dotsPerInch;
            }
        }

        public static object GetImage(byte[] value, Size size)
        {
            var stream = new MemoryStream(value);
            var image = Image.FromStream(stream);

            float destWidth = size.Width * DotsPerInch;
            ;
            float destHeight = size.Height * DotsPerInch;

            float srcWidth = image.Width;
            float srcHeight = image.Height;

            float dX = destWidth / srcWidth;
            float dY = destHeight / srcHeight;
            float ratio = Math.Min(dX, dY);

            srcWidth = srcWidth * ratio;
            srcHeight = srcHeight * ratio;

            var bigger = new Bitmap((int)(destWidth),
                (int)(destHeight));
            using (var g = Graphics.FromImage(bigger))
            {
                g.DrawImage(image, new Rectangle(0, 0, (int)srcWidth, (int)srcHeight));
                return bigger;
            }
        }
    }
}