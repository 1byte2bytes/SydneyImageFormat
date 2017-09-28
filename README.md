# Sydney Image Format

Sydney Image Format is a small, simple, work in progress image format.

## Format

### Some General Notes

All numbers are stored in hex, and 1 byte long unless stated otherwise.

---
### Header

The header of any SIF file should always be as follows. It should always be 8 bytes long.

```
53 49 4D 31 2E 31 00 00
```

Which is hex for

```
SIM1.1  
```

---
### Size Table

This should have a header like below, and should be 8 bytes long.

```
F9 80 71 00
```

The first byte is the amount of color entries in our table. The second byte is the width of the image in pixels. The third byte is the height of the image in pixels. The fourth byte remains empty.

---

A color entry looks like this, after the header:

```
DF 3B 26 FE
```

It trasnlates to

```
[1 BYTE RED][1 BYTE GREEN][1 BYTE BLUE][1 BYTE ALPHA]
```

Every color will have it's own entry, back to back.

---
### Pixel Data

Instead of writing the X and Y value for every pixel, I've chosen to write each pixel in order. The logic for this is as below.

Each pixel is written in order on the image, from (0, 0) to the end of the image. Each pixel is written as 1 byte, and that byte being the number for the color to use in the color table.

Read the Y value, and go across until your index reaches that of the Y value. You now have read 1 row of the image. A hex editor can help show this on some images. 
