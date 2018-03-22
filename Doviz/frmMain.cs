using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using TwainDotNet;

namespace Doviz
{
    public partial class frmMain : Form
    {
        private Twain _twain;
        private ScanSettings _scanSettings;
        List<Bitmap> _lstImages = new List<Bitmap>();

        public frmMain()
        {
            InitializeComponent();

            //_twain = new Twain(new TwainDotNet.Wpf.WpfWindowMessageHook(this));
            _twain = new Twain(new TwainDotNet.WinFroms.WinFormsWindowMessageHook(this));

            _twain.TransferImage += evt_transferImage;
            _twain.ScanningComplete += evt_scanComplete;

            fillDummyData();
            SetTwainSettings();
        }


        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                _lstImages.Clear();
                //resetInputs();
                _twain.StartScanning(_scanSettings);
            }
            catch (TwainException tex)
            {
                MessageBox.Show("Tarama işlemi yapılamadı!" + tex.StackTrace);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tarama işlemi sırasında istisna alındı!" + ex.StackTrace);
            }
        }

        private void evt_scanComplete(object sender, ScanningCompleteEventArgs e)
        {
            pbScannedImage.Image = ImageProc.MergeImagesHorizontal(_lstImages[0], _lstImages[1]);
        }

        private void evt_transferImage(object sender, TransferImageEventArgs e)
        {
            if (e.Image != null) _lstImages.Add(e.Image);
        }

        private void SetTwainSettings()
        {
            var scannerSource = GetScannerSource();
            if (string.IsNullOrEmpty(scannerSource))
            {
                SetScannerSource();
            }
            else
            {
                _twain.SelectSource(scannerSource);
            }

            _scanSettings = new ScanSettings
            {
                UseDuplex = true,
                Rotation = new RotationSettings { AutomaticRotate = true, AutomaticBorderDetection = true },
                Resolution = new ResolutionSettings { ColourSetting = ColourSetting.GreyScale, Dpi = 100 }
            };
            //_scanSettings.ShowTwainUI = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var imageFormat = ImageProc.GetImageFormat(pbScannedImage.Image);
            imageFormat = ImageFormat.Jpeg;
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

        void resetInputs()
        {
            tbDate.Text = "";
            tbAddress.Text = "";
            tbDocNo.Text = "";
            tbRecNo.Text = "";
            tbTel.Text = "";
        }

        void fillDummyData()
        {
            tbDate.Text = "10.12.2018";
            tbAddress.Text = "Saçma bir adres bile olsa burada olacak";
            tbDocNo.Text = "DOC001";
            tbRecNo.Text = "REC001";
            tbTel.Text = "5334816380";
        }

        private void ayarlarToolStrip_Click(object sender, EventArgs e) => SetScannerSource();
        private string GetScannerSource() => ConfigurationSettings.AppSettings["defaultSource"];
        private void SetScannerSource()
        {
            _twain.SelectSource();
            if (_twain.DefaultSourceName != "")
                ConfigurationSettings.AppSettings["defaultSource"] = _twain.DefaultSourceName;
        }


        private void btnBgWorkerScan_Click(object sender, EventArgs e)
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                WorkerArgs wArgs = new WorkerArgs
                {
                    _bitmap = _lstImages,
                    _twain = _twain,
                    _settings = _scanSettings
                };

                worker.DoWork += bgWorker_DoWork;
                worker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
                worker.RunWorkerAsync(wArgs);
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            => MessageBox.Show("BG Worker taraması tamamlandır");

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkerArgs thisArgs = (WorkerArgs)e.Argument as WorkerArgs;
                if (thisArgs != null)
                {
                    AutoResetEvent waitHandle = new AutoResetEvent(false);
                    thisArgs._twain.ScanningComplete += (se, ev) =>
                                                        {
                                                            evt_scanComplete(se, ev);
                                                            waitHandle.Set();
                                                        };
                    thisArgs._twain.StartScanning(_scanSettings);
                    waitHandle.WaitOne();

                    //if (thisArgs._twain.Images.Count < 0)
                    //{
                    //    foreach (var image in twain.Images)
                    //    {
                    //        BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(new Bitmap(image).GetHbitmap(),
                    //            IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                    //        thisArgs._bitmapSources.Add(bitmapSource);
                    //    }
                    //}
                }
            }
            catch (Exception up)
            {
                throw up; // :P
            }
        }

        private void hakkındaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmHakkinda().Show();
        }
    }
    public class WorkerArgs
    {
        public List<Bitmap> _bitmap;
        public Twain _twain;
        public ScanSettings _settings;
    }
}
