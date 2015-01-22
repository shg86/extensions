using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Extensions
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Scales an image according to height and width.
        /// </summary>
        public static Image ScaleImage(this Image img, int height, int width)
        {
            if (img == null || height <= 0 || width <= 0)
            {
                return null;
            }
            int newWidth = (img.Width * height) / (img.Height);
            int newHeight = (img.Height * width) / (img.Width);
            int x = 0;
            int y = 0;

            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

            // use this when debugging.
            //g.FillRectangle(Brushes.Aqua, 0, 0, bmp.Width - 1, bmp.Height - 1);
            if (newWidth > width)
            {
                // use new height
                x = (bmp.Width - width) / 2;
                y = (bmp.Height - newHeight) / 2;
                g.DrawImage(img, x, y, width, newHeight);
            }
            else
            {
                // use new width
                x = (bmp.Width / 2) - (newWidth / 2);
                y = (bmp.Height / 2) - (height / 2);
                g.DrawImage(img, x, y, newWidth, height);
            }
            // use this when debugging.
            //g.DrawRectangle(new Pen(Color.Red, 1), 0, 0, bmp.Width - 1, bmp.Height - 1);
            return bmp;
        }
    }
}
