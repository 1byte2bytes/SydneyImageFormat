namespace SIF
{
    public class Pixel
    {
        public short x;
        public short y;
        public short colorindex;

        public Pixel(short inx, short iny, short incolorindex)
        {
            x = inx;
            y = iny;
            colorindex = incolorindex;
        }
    }
}