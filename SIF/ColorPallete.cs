using System;

namespace SIF
{
    public class ColorPallete
    {
        public Color[] colors;

        public ColorPallete(int size)
        {
            colors = new Color[size];
            Color empty = new Color(255, 255, 255, 0);
            for (int i = 0; i < size; i++)
            {
                colors[i] = empty;
            }
        }

        public static byte isColorInPallete(ColorPallete pallete, Color col)
        {
            byte index = 0;
            foreach (Color color in pallete.colors)
            {
                if (color.red == col.red && color.green == col.green && color.red == col.red &&
                    color.alpha == col.alpha)
                {
                    return index;
                }
                index++;
            }
            throw new ColorNotFoundException("");
        }
        
        public static void addColor(ColorPallete pallete, Color col, int index)
        {
            pallete.colors[index] = col;
        }
    }
    
    public class ColorNotFoundException : Exception
    {
        public ColorNotFoundException(string message)
            : base(message)
        {
        }
    }
}