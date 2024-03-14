public sealed class TMXTile
{
    public static readonly uint kTMXTileHorizontalFlag = 0x80000000;
    public static readonly uint kTMXTileVerticalFlag = 0x40000000;
    public static readonly uint kTMXTileDiagonalFlag = 0x20000000;
    public static readonly uint kFlipedAll = (kTMXTileHorizontalFlag | kTMXTileVerticalFlag | kTMXTileDiagonalFlag);
    public static readonly uint kFlippedMask = ~(kFlipedAll);
}