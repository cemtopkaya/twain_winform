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

            //fillDummyData();
            resetInputs();
            SetTwainSettings();
        }


        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                _lstImages.Clear();
                resetInputs();
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
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ImageFormat imageFormat = ImageProc.GetImageFormat(this.pbScannedImage.Image);
            imageFormat = ImageFormat.Jpeg;
            Image watermarkedImage = ImageProc.WatermarkImage(this.pbScannedImage.Image);
            Image jpegImage = ImageProc.ConvertImage(watermarkedImage, imageFormat);
            byte[] barrImage = ImageProc.ImageToByte(jpegImage);
            Musteri musteri = new Musteri
            {
                Date = this.tbDate.Text,
                ScannedImage = Convert.ToBase64String(barrImage),
                ScannedImageFormat = ImageProc.GetMimeType(imageFormat),
                Address = this.tbAddress.Text,
                DocumentNo = this.tbDocNo.Text,
                RecNo = this.tbRecNo.Text,
                Tel = this.tbTel.Text
            };
            using (frmPrint frmPrint = new frmPrint(musteri))
            {
                frmPrint.ShowDialog();
            }
        }

        void resetInputs()
        {
            tbDate.Text = DateTime.Now.ToShortDateString();
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
                    _bitmap = this._lstImages,
                    _twain = this._twain,
                    _settings = this._scanSettings
                };
                worker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
                worker.RunWorkerAsync(wArgs);
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            => MessageBox.Show("BG Worker taraması tamamlandı");

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkerArgs thisArgs = (WorkerArgs)e.Argument;
                bool flag = thisArgs != null;
                if (flag)
                {
                    AutoResetEvent waitHandle = new AutoResetEvent(false);
                    thisArgs._twain.ScanningComplete += delegate (object se, ScanningCompleteEventArgs ev)
                    {
                        this.evt_scanComplete(se, ev);
                        waitHandle.Set();
                    };
                    thisArgs._twain.StartScanning(this._scanSettings);
                    waitHandle.WaitOne();
                }
            }
            catch (Exception up)
            {
                throw up;
            }
        }

        private void hakkındaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmHakkinda().Show();
        }


        private void tbDate_KeyDown(object sender, KeyEventArgs e)
        {
            bool flag = this.tbDate.Text == "  .  .";
            if (flag)
            {
                e.SuppressKeyPress = true;
                switch (e.KeyValue)
                {
                    case 48:
                    case 49:
                    case 50:
                    case 51:
                        this.tbDate.Text = ((char)e.KeyValue).ToString();
                        break;
                }
            }
        }
        private void tbTel_KeyDown(object sender, KeyEventArgs e)
        {
            bool flag = this.tbTel.Text == "   -   -  -";
            if (flag)
            {
                e.SuppressKeyPress = true;
                int pressedNumber = -1;
                switch (e.KeyCode)
                {
                    case Keys.NumPad0:
                        pressedNumber = 0;
                        break;
                    case Keys.NumPad1:
                        pressedNumber = 1;
                        break;
                    case Keys.NumPad2:
                        pressedNumber = 2;
                        break;
                    case Keys.NumPad3:
                        pressedNumber = 3;
                        break;
                    case Keys.NumPad4:
                        pressedNumber = 4;
                        break;
                    case Keys.NumPad5:
                        pressedNumber = 5;
                        break;
                    case Keys.NumPad6:
                        pressedNumber = 6;
                        break;
                    case Keys.NumPad7:
                        pressedNumber = 7;
                        break;
                    case Keys.NumPad8:
                        pressedNumber = 8;
                        break;
                    case Keys.NumPad9:
                        pressedNumber = 9;
                        break;
                }
                bool flag2 = pressedNumber > 0 && pressedNumber < 6;
                if (flag2)
                {
                    this.tbTel.Text = pressedNumber.ToString();
                }
                else
                {
                    switch (e.KeyValue)
                    {
                        case 49:
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                            this.tbTel.Text = ((char)e.KeyValue).ToString();
                            break;
                    }
                }
            }
        }
    }
    public class WorkerArgs
    {
        public List<Bitmap> _bitmap;
        public Twain _twain;
        public ScanSettings _settings;
    }
}
