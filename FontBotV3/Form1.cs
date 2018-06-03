using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FontBotV3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var maximumCharsPerLine = 30;
            var currentLineWidth = 0;
            var currentLineYValue = 0;
            var verticalHeightOffset = 0;

            // Big bitmap we're drawing to
            Bitmap bmp = new Bitmap(1000, 1000);

            // Create string formatting options (used for alignment)
            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };

            // Create a temporary graphics object for sizing the word
            Bitmap tempBmp = new Bitmap(1000, 1000);
            Graphics graphics = Graphics.FromImage(tempBmp);

            using (var g = Graphics.FromImage(bmp))
            {
                #region
                //Assigning rendering options
                // ------------------------------------------
                // Ensure the best possible quality rendering
                // ------------------------------------------
                // The smoothing mode specifies whether lines, curves, and the edges of filled areas use smoothing (also called antialiasing). One exception is that path gradient brushes do not obey the smoothing mode. Areas filled using a PathGradientBrush are rendered the same way (aliased) regardless of the SmoothingMode property.
                g.SmoothingMode = SmoothingMode.AntiAlias;
                // The interpolation mode determines how intermediate values between two endpoints are calculated.
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // Use this property to specify either higher quality, slower rendering, or lower quality, faster rendering of the contents of this Graphics object.
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                // This one is important
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                #endregion
                Array values = Enum.GetValues(typeof(Fonts));
                Random random = new Random();

                var charCount = 0;
                string[] words = textBox1.Text.Split(' ');
                foreach (string word in words)
                {
                    currentLineWidth += 20;
                    charCount += word.Length;
                    if (charCount > maximumCharsPerLine)
                    {
                        verticalHeightOffset += currentLineYValue + 50;
                        currentLineWidth = 0;
                        charCount = 0;
                    }

                    foreach (char c in word)
                    {
                        if (string.IsNullOrWhiteSpace(c.ToString()))
                        {
                            currentLineWidth += 10;
                            continue;
                        }

                        Fonts randomBar = (Fonts)values.GetValue(random.Next(values.Length));
                        Font stringFont = returnFont(randomBar);

                        using (var foreBrush = new SolidBrush(Color.Black))
                        {
                            SizeF stringSize = new SizeF();
                            stringSize = graphics.MeasureString(c.ToString(), stringFont);

                            // Add logic here to check to see if we need to carriage return

                            // If this is higher than our lines max height thus far, update it
                            if ((int)stringSize.Height > currentLineYValue)
                            {
                                currentLineYValue = (int)stringSize.Height;
                            }

                            RectangleF rectOne = new RectangleF(currentLineWidth, verticalHeightOffset, stringSize.Width, stringSize.Height);
                            g.DrawString(c.ToString(), stringFont, Brushes.Black, rectOne, format);
                            currentLineWidth += (int)stringSize.Width - 5;
                            g.Flush();
                        }
                    }

                    
                }
            }


            // Now save or use the bitmap
            bmp.Save("C:\\data\\templates\\output.png", ImageFormat.Png);
            pictureBox1.Image = bmp;

        }

        enum Fonts
        {
            //nick_ml_12,
            //nick_ml_24,
            nick_001,
            nick_002,
            nick_003,
            //nick_ml_48,
            //nick_ml_60
        };

        private Font returnFont (Fonts font)
        {
            var returnFont = new Font("Gunny Rewritten", 12);

            if (font == Fonts.nick_002)
            {
                returnFont = new Font("Gunny Rewritten", 13);
            }

            if (font == Fonts.nick_003)
            {
                returnFont = new Font("Gunny Rewritten", 14);
            }




            //if (font == Fonts.nick_ml_12)
            //{
            //    returnFont = new Font("nick_ml", 12);
            //}

            //if (font == Fonts.nick_ml_24)
            //{
            //    returnFont = new Font("nick_ml", 24);
            //}

            //if (font == Fonts.nick_ml_36)
            //{
            //    returnFont = new Font("nick_ml", 36);
            //}

            //if (font == Fonts.nick_ml_48)
            //{
            //    returnFont = new Font("nick_ml", 48);
            //}

            //if (font == Fonts.nick_ml_60)
            //{
            //    returnFont = new Font("nick_ml", 60);
            //}

            return returnFont;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
