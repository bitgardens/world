namespace WorldRepr.Repr;

/// <summary>
/// Encodes two unsigned short integers (u16) into a single unsigned integer (u32).
///
/// The X component is placed in the least significant portion.
/// The Y component is placed in the most significant portion.
///
/// See tests for usage examples.
/// </summary>
public readonly struct Position
{
    public ushort X { get; }
    public ushort Y { get; }

    public Position(ushort x, ushort y)
    {
        X = x;
        Y = y;
    }

    public uint ToId()
    {
        var y = (uint)Y << 16;
        return y | X;
    }

    public static Position FromId(uint id)
    {
        const uint hiMask = 0xFFFF0000;
        var x = id & (~hiMask);
        var y = (id & hiMask) >> 16;
        return new Position((ushort)x, (ushort)y);
    }
}