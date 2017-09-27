using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using SIF;
using Color = System.Drawing.Color;

namespace Encoder
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid arguments");
                return;
            }

            if (File.Exists(args[0]) != true)
            {
                Console.WriteLine("Input file does not exist");
                return;
            }
            
            Bitmap inimage = new Bitmap(args[0]);
            SIF.Image outimage = new SIF.Image(8191, (short)inimage.Size.Height, (short)inimage.Size.Width);
            
            int imheight = inimage.Size.Height;
            int imwidth = inimage.Size.Width;

            // Encode the image
            short colorindex = 0;
            bool newcolor;
            for (short x = 0; x < imheight; x++)
            {
                for (short y = 0; y < imwidth; y++)
                {
                    newcolor = false;
                    Color incolor = inimage.GetPixel(y, x);
                    SIF.Color outcolor = SysColorToSIFColor(incolor);

                    short colindex = SIF.ColorPallete.isColorInPallete(outimage.pallete, outcolor);
                    
                    if (colindex == -1)
                    {
                        SIF.ColorPallete.addColor(outimage.pallete, outcolor, colorindex);
                        colorindex++;
                        newcolor = true;
                    }
                    
                    SIF.Pixel outpixel = new SIF.Pixel(
                        x, y,
                        newcolor ? colorindex : colindex
                    );
                    outimage.pixels[x, y] = outpixel;
                }
            }
            
            // Decode the image
            using (FileStream fs = File.Open(args[1], FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                //bw.Write(0x53494D312E30); // SIM1.0
                bw.Write(0x302E314D4953); // SIM1.0
                
                // Color
                bw.Write((byte)0x43);
                bw.Write((byte)0x4F);
                bw.Write((byte)0x4C);
                bw.Write((byte)0x4F);
                bw.Write((byte)0x52);
                bw.Write(colorindex);
                bw.Write((byte)0x0);

                foreach (SIF.Color color in outimage.pallete.colors)
                {
                    bw.Write(color.red + color.green + color.green + color.alpha);
                }

                foreach (SIF.Pixel pixel in outimage.pixels)
                {
                    bw.Write(pixel.x + pixel.y + pixel.colorindex);
                }
            }
            
            Console.WriteLine("encoded");
        }
        
        private static SIF.Color SysColorToSIFColor(Color col)
        {
            SIF.Color temp = new SIF.Color(
                col.R,
                col.G,
                col.B,
                col.A
            );
            return temp;
        }
    }
}