using System.Runtime.CompilerServices;
using System.Text;

namespace WorldRepr.Repr;

public struct Directions
{
    public readonly byte BitField;

    public Directions(bool top, bool right, bool bottom, bool left)
    {
        BitField |= Enc(top, 3);
        BitField |= Enc(right, 2);
        BitField |= Enc(bottom, 1);
        BitField |= Enc(left, 0);
    }

    public Directions(byte b)
    {
        BitField = b;
    }

    public bool Top() => Is(3);
    public bool Right() => Is(2);
    public bool Bottom() => Is(1);
    public bool Left() => Is(0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte Enc(bool b, byte shift)
    {
        var flag = b ? 1 : 0;
        return (byte)(flag << shift);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool Is(byte shift)
    {
        return (BitField & (1 << shift)) != 0;
    }

    public string EncodeAsString()
    {
        var s = new StringBuilder(4);
        s.Append(Top() ? "1" : "0");
        s.Append(Right() ? "1" : "0");
        s.Append(Bottom() ? "1" : "0");
        s.Append(Left() ? "1" : "0");
        return s.ToString();
    }

    public static Directions DecodeFromString(string s)
    {
        if (s.Length != 4)
        {
            throw new InvalidDataException("Must have length 4");
        }

        return new Directions(
            GetBoolAt(s, 0),
            GetBoolAt(s, 1),
            GetBoolAt(s, 2),
            GetBoolAt(s, 3));
    }

    private static bool GetBoolAt(string s, int pos)
    {
        switch (s[pos])
        {
            case '0': return false;
            case '1': return true;
            default:
                throw new InvalidDataException($"Invalid digit: `{s[pos]}`");
        }
    }
}