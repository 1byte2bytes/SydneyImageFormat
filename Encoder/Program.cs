using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
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
            SIF.Image outimage = new SIF.Image(255, (byte)inimage.Size.Width, (byte)inimage.Size.Height);
            
            int imheight = inimage.Size.Height;
            int imwidth = inimage.Size.Width;

            // Encode the image
            byte colorindex = 1;
            byte colindex = 1;
            bool newcolor;
            for (byte x = 0; x < imwidth; x++)
            {
                for (byte y = 0; y < imheight; y++)
                {
                    newcolor = false;
                    Color incolor = inimage.GetPixel(x, y);
                    SIF.Color outcolor = SysColorToSIFColor(incolor);
                    
                    try
                    {
                        colindex = SIF.ColorPallete.isColorInPallete(outimage.pallete, outcolor);
                    } catch {
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
                bw.Write(0x312E314D4953); // SIM1.0
                bw.Write(colorindex); // color size
                bw.Write((byte)inimage.Size.Height);
                bw.Write((byte)inimage.Size.Width);
                bw.Write((byte)0);

                foreach (SIF.Color color in outimage.pallete.colors)
                {
                    //bw.Write(color.red + color.green + color.green + color.alpha);
                    bw.Write(color.red);
                    bw.Write(color.green);
                    bw.Write(color.blue);
                    bw.Write(color.alpha);
                }

                for (int x = 0; x < inimage.Size.Width; x++)
                {
                    for (int y = 0; y < inimage.Size.Height; y++)
                    {
                        bw.Write(outimage.pixels[x,y].colorindex);
                    }
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