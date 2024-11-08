using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersRepairService
{
    internal static class Capcha
    {
        public static Bitmap CreateCapcha(int width, int height, out string text)
        {
            Random rnd = new Random();
            Bitmap result = new Bitmap(width, height);
            Brush[] colors = { Brushes.Black, Brushes.Red, Brushes.Aqua, Brushes.BlueViolet };

            Graphics graphics = Graphics.FromImage((System.Drawing.Image)result);
            graphics.Clear(Color.LightGray);

            string[] fonts = { "Arial", "Times New Roman", "Arial Black" };
            text = String.Empty;
            string Symbols = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            
            for (int counter = 0; counter < 4; ++counter)
                text += Symbols[rnd.Next(Symbols.Length)];
            for (int counter = 0; counter < text.Length; ++counter)
            {
                graphics.DrawString(text[counter].ToString(),
                             new System.Drawing.Font(fonts[rnd.Next(fonts.Length)], rnd.Next(12, 15)),
                             colors[rnd.Next(colors.Length)],
                             new PointF(counter * 15, rnd.Next(10, height / 2)));
            }

            for (int widthCounter = 0; widthCounter < width; ++widthCounter)
                for (int heightCounter = 0; heightCounter < height; ++heightCounter)
                    if (rnd.Next() % 10 == 0)
                        result.SetPixel(widthCounter, heightCounter, Color.White);
            for (int counter = 0; counter < 3; ++counter)
            {
                graphics.DrawLine(Pens.Black,
                           new Point(0, rnd.Next(height - 1)),
                           new Point(width - 1, rnd.Next(height - 1)));
            }
            return result;
        }

    }
}
