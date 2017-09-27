# Sydney Image Format

Sydney Image Format is a small, simple, work in progress image format.

## Format

### Some General Notes

All numbers are stored in hex, and are shorts (2 bytes) unless stated otherwise.

### Header

The header of any SIF file should always be as follows. It should always be 8 bytes long.

```
53 49 4D 31 2E 30 00 00
```

Which is hex for

```
SIM1.0  
```

### Color Table

This should have a header as follows, and should be 8 bytes long.

```
43 4F 4C 4F 52 79 0D 00
```

Which is hex for

```
COLOR1949
```

The length of the color table in entries (not bytes) should be appended to the text `COLOR`.

A color entry looks like this, after the header:

```
DF 3B 26 FE
```

It trasnlates to

```
[1 BYTE RED][1 BYTE GREEN][1 BYTE BLUE][1 BYTE ALPHA]
```

### Pixel Data

Now each pixel is written.

```
[2 BYTES X][2 BYTES Y][2 BYTES COLOR]
```
