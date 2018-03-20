using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TwainDotNet;

namespace Doviz
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            fillDummyData();
        }

        private Twain _twain;
        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                var lstImages = new List<Bitmap>();
                _twain = new Twain(new TwainDotNet.WinFroms.WinFormsWindowMessageHook(this));
                _twain.TransferImage += (snd, transferImage) =>
                                        {
                                            if (transferImage.Image != null) lstImages.Add(transferImage.Image);
                                        };
                _twain.ScanningComplete += delegate { pbScannedImage.Image = ImageProc.MergeImagesHorizontal(lstImages[0], lstImages[1]); };

                var scanSettings = new ScanSettings
                {
                    UseDuplex = true,
                    Rotation = new RotationSettings { AutomaticRotate = true },
                    Resolution = new ResolutionSettings { ColourSetting = ColourSetting.Colour, Dpi = 72 }
                };
                //ss.ShowTwainUI = true;

                _twain.StartScanning(scanSettings);
            }
            catch (TwainDotNet.TwainException tex)
            {
                MessageBox.Show("Tarama işlemi yapılamadı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tarama işlemi sırasında istisna alındı!"+ex.StackTrace);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var imageFormat = ImageProc.GetImageFormat(pbScannedImage.Image);
            var watermarkedImage = ImageProc.WatermarkImage(pbScannedImage.Image);
            var jpegImage = ImageProc.ConvertImage(watermarkedImage, imageFormat);
            var barrImage = ImageProc.ImageToByte(jpegImage);

            var musteri = new Musteri
            {
                Date = tbDate.Text,
                ScannedImage = Convert.ToBase64String(barrImage),
                ScannedImageFormat = ImageProc.GetMimeType(imageFormat),
                Address = tbAddress.Text,
                DocumentNo = tbDocNo.Text,
                RecNo = tbRecNo.Text,
                Tel = tbTel.Text
            };

            using (var frmPrint = new frmPrint(musteri))
            {
                frmPrint.ShowDialog();
            }
        }

        void fillDummyData()
        {
            tbDate.Text = "10.12.2018";
            tbAddress.Text = "Saçma bir adres bile olsa burada olacak";
            tbDocNo.Text = "DOC001";
            tbRecNo.Text = "REC001";
            tbTel.Text = "5334816380";
        }
    }
}
