using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Red.BitmapHelpers;

namespace Red.BitmapHelper.Test
{
    [TestClass]
    public class BitmapTest
    {
        [TestMethod]
        public void LoadBmp()
        {
            var bmp = RedHelper.LoadBitmap("Test.bmp");
            Assert.IsNotNull(bmp);
        }

        [TestMethod]
        public void GetScreen()
        {
            var bmp = RedHelper.GetCopyFromScreen(0, 0, 2, 2);
            Assert.IsNotNull(bmp);
        }

        [TestMethod]
        public void GetColor()
        {
            var bmp = RedHelper.LoadBitmap("Test.bmp");
            var pixels = bmp.BitmapToByteRgbQ();
            var bmpWidth = bmp.Width;
            var bmpHeight = bmp.Height;
            for (var x = 0; x < bmpWidth; x++)
            {
                for (var y = 0; y < bmpHeight; y++)
                {
                    var color = pixels.GetColor(x, y);
                    Assert.IsTrue(color.R == Color.Red.R);
                }
            }
        }
    }
}
