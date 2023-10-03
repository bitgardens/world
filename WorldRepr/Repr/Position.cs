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

    public Position WithX(short deltaX)
    {
        return new Position((ushort)(X + deltaX), Y);
    }
    
    public Position WithY(short deltaY)
    {
        return new Position(X, (ushort)(Y + deltaY));
    }

    public Position WithXY(short deltaX, short deltaY)
    {
        return new Position((ushort)(X + deltaX), (ushort)(Y + deltaY));
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

    public override string ToString()
    {
        return $"Position(x={X}, y={Y})";
    }
}