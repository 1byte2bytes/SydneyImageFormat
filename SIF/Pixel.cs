namespace SIF
{
    public class Pixel
    {
        public byte x;
        public byte y;
        public byte colorindex;

        public Pixel(byte inx, byte iny, byte incolorindex)
        {
            x = inx;
            y = iny;
            colorindex = incolorindex;
        }
    }
}