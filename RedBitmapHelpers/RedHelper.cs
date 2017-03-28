using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Red.BitmapHelpers
{
    public static class RedHelper
    {
        internal static Bitmap LoadBitmap(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return new Bitmap(fs);
            }
        }

        internal static Bitmap GetCopyFromScreen(int screenPositionX, int screenPositionY, int width, int height)
        {
            using (var bmpScreenCapture = new Bitmap(width, height))
            {
                using (var g = Graphics.FromImage(bmpScreenCapture))
                {
                    g.CopyFromScreen(screenPositionX, screenPositionY, 0, 0, bmpScreenCapture.Size, CopyPixelOperation.SourceCopy);
                }
                return bmpScreenCapture.Clone() as Bitmap;
            }
        }

        internal static Color GetColor(this byte[,,] data, int x, int y)
        {
            return Color.FromArgb(data[0, y, x], data[1, y, x], data[2, y, x]);
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "TooWideLocalVariableScope")]
        internal static unsafe byte[,,] BitmapToByteRgbQ(this Bitmap bmp)
        {
            var width = bmp.Width;
            var height = bmp.Height;
            var res = new byte[3, height, width];
            var bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                byte* curpos;
                fixed (byte* _res = res)
                {
                    byte* _r = _res, _g = _res + width * height, _b = _res + 2 * width * height;
                    for (var h = 0; h < height; h++)
                    {
                        curpos = ((byte*)bd.Scan0) + h * bd.Stride;
                        for (var w = 0; w < width; w++)
                        {
                            *_b = *(curpos++); ++_b;
                            *_g = *(curpos++); ++_g;
                            *_r = *(curpos++); ++_r;
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bd);
            }
            return res;
        }
    }
}
