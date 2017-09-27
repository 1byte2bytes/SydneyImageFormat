namespace SIF
{
    public class Image
    {
        public Header head;
        public ColorPallete pallete;
        public Pixel[,] pixels;

        public Image(int colors, short x, short y)
        {
            pallete = new ColorPallete(colors);
            pixels = new Pixel[x,y];
        }
    }
}