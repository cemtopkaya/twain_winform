using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Doviz
{
    public class ImageProc
    {
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        
        public static ImageFormat GetImageFormat(Image img)
        {
            if (img.RawFormat.Equals(ImageFormat.Jpeg))
                return ImageFormat.Jpeg;
            if (img.RawFormat.Equals(ImageFormat.Bmp))
                return ImageFormat.Bmp;
            if (img.RawFormat.Equals(ImageFormat.Png))
                return ImageFormat.Png;
            if (img.RawFormat.Equals(ImageFormat.Emf))
                return ImageFormat.Emf;
            if (img.RawFormat.Equals(ImageFormat.Exif))
                return ImageFormat.Exif;
            if (img.RawFormat.Equals(ImageFormat.Gif))
                return ImageFormat.Gif;
            if (img.RawFormat.Equals(ImageFormat.Icon))
                return ImageFormat.Icon;
            if (img.RawFormat.Equals(ImageFormat.MemoryBmp))
                return ImageFormat.MemoryBmp;
            if (img.RawFormat.Equals(ImageFormat.Tiff))
                return ImageFormat.Tiff;
            else
                return ImageFormat.Wmf;
        }

        public static string GetMimeType(ImageFormat imageFormat)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.First(codec => codec.FormatID == imageFormat.Guid).MimeType;
        }

        public static Image WatermarkImage(Image image)
        {
            var stamp = "BAÞKA BÝR AMAÇLA KULLANILAMAZ";
            var outputImageWidth = image.Width;
            var outputImageHeight = image.Height;
            var outputImage = new Bitmap(image);

            using (var g = Graphics.FromImage(outputImage))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TranslateTransform(dx: outputImageWidth / 2, dy: outputImageHeight / 2);
                g.RotateTransform((float) (Math.Atan2(outputImageHeight, outputImageWidth) * (180 / Math.PI)));

                var font = new Font("Tahoma", 40, FontStyle.Bold);
                var textSize = g.MeasureString(stamp, font);
                g.DrawString(stamp, font, Brushes.Red, -(textSize.Width / 2), -(textSize.Height / 2));

                g.Flush();
            }

            return outputImage;
        }

        public static Image MergeImagesVertical(Image image1, Image image2)
        {
            return null;
        }
        public static Image MergeImagesHorizontal(Image firstImage, Image secondImage)
        {
            if (firstImage == null)
            {
                throw new ArgumentNullException("firstImage");
            }

            if (secondImage == null)
            {
                throw new ArgumentNullException("secondImage");
            }

            int outputImageWidth = firstImage.Width + secondImage.Width + 1;

            int outputImageHeight = firstImage.Height;

            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(outputImage))
            {
                g.DrawImage(firstImage,
                    new System.Drawing.Rectangle(new System.Drawing.Point(), firstImage.Size),
                    new System.Drawing.Rectangle(new System.Drawing.Point(), firstImage.Size),
                    GraphicsUnit.Pixel);
                g.DrawImage(secondImage,
                    new System.Drawing.Rectangle(new System.Drawing.Point(firstImage.Width + 1, 0), secondImage.Size),
                    new System.Drawing.Rectangle(new System.Drawing.Point(), secondImage.Size),
                    GraphicsUnit.Pixel);


                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TranslateTransform(outputImage.Width / 2, outputImage.Height / 2);
                g.RotateTransform(30);
                String damga = "BAÞKA BÝR AMAÇLA KULLANILAMAZ";
                Font font = new Font("Tahoma", 40);
                SizeF textSize = g.MeasureString(damga, font);
                g.DrawString(damga, font, System.Drawing.Brushes.Red, -(textSize.Width / 2), -(textSize.Height / 2));

                g.Flush();
            }

            return outputImage;
        }

        public static Image ConvertImage(Image watermarkedImage, ImageFormat format)
        {
            var ms = new MemoryStream();
            watermarkedImage.Save(ms, format);
            return Image.FromStream(ms);
        }
    }
}