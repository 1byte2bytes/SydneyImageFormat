namespace SIF
{
    public class Image
    {
        public Header head;
        public ColorPallete pallete;
        public Pixel[,] pixels;

        public Image(byte colors, byte x, byte y)
        {
            pallete = new ColorPallete(colors);
            pixels = new Pixel[x,y];
        }
    }
}