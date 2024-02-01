
using System;
using System.IO;
using System.Drawing;
using Microsoft.WindowsAPICodePack.Shell;

namespace SIMS.Conversion
{
    public class Thumbnails
    {

        public Bitmap GetThumbnail(string _sourcePath)
        {
            const int bmpW = 150;
            const int bmpH = 180;
            Bitmap upBmp = null;

            if (!string.IsNullOrEmpty(_sourcePath))
            {
                if (Path.GetExtension(_sourcePath).ToLower() == ".pdf")
                {
                    ShellFile shellFile = ShellFile.FromFilePath(_sourcePath);
                    upBmp = shellFile.Thumbnail.ExtraLargeBitmap;
                }
            }

            if (upBmp != null)
            {
                Int32 newWidth = bmpW;
                Int32 newHeight = bmpH;

                Bitmap newBmp = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                newBmp.SetResolution(72, 72);
                Double upWidth = upBmp.Width;
                Double upHeight = upBmp.Height;

                int newX = 0;
                int newY = 0;
                Double reDuce;
                if (upWidth > upHeight)
                {
                    reDuce = newWidth / upWidth;
                    newHeight = ((Int32)(upHeight * reDuce));
                    newY = ((Int32)((bmpH - newHeight) / 2));
                    newX = 0;
                }
                else if (upWidth < upHeight)
                {
                    reDuce = newHeight / upHeight;
                    newWidth = ((Int32)(upWidth * reDuce));
                    newX = ((Int32)((bmpW - newWidth) / 2));
                    newY = 0;
                }
                else if (upWidth == upHeight)
                {
                    reDuce = newHeight / upHeight;
                    newWidth = ((Int32)(upWidth * reDuce));
                    newX = ((Int32)((bmpW - newWidth) / 2));
                    newY = ((Int32)((bmpH - newHeight) / 2));
                }
                Graphics newGraphic = Graphics.FromImage(newBmp);
                try
                {
                    newGraphic.Clear(Color.White);
                    newGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    newGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    newGraphic.DrawImage(upBmp, newX, newY, newWidth, newHeight);
                    return newBmp;
                }
                catch (Exception ex)
                {
                    string newError = ex.Message;
                    // lblError.Text = newError;  
                }
                finally
                {
                    upBmp.Dispose();
                    // newBmp.Dispose();  
                    newGraphic.Dispose();
                }


            }

            return upBmp;
        }

        public void SavethumbnailToImage(Bitmap _bitmapImage, string _imagePath, string _typeOfImage)
        {
            if (_typeOfImage == "JPEG")
                _bitmapImage.Save(_imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            else
                _bitmapImage.Save(_imagePath, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}