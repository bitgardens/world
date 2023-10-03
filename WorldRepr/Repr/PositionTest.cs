using NUnit.Framework;

namespace WorldRepr.Repr;

internal class PositionTests
{
    [TestCaseSource(nameof(TestData))]
    public void TestStringEncoder((uint, (ushort, ushort)) tup)
    {
        var expectedEncoded = tup.Item1;
        var expectedX = tup.Item2.Item1;
        var expectedY = tup.Item2.Item2;

        var pos = Position.FromId(expectedEncoded);
        Assert.That(pos.X, Is.EqualTo(expectedX));
        Assert.That(pos.Y, Is.EqualTo(expectedY));

        var encoded = pos.ToId();
        Assert.That(encoded, Is.EqualTo(expectedEncoded));
    }

    public static IEnumerable<(uint, (ushort, ushort))> TestData()
    {
        yield return (1, (1, 0));
        yield return (2, (2, 0));
        yield return (0xFFFF, (0xFFFF, 0));
        yield return (0x0001_0000, (0, 1));
        yield return (0x0002_0000, (0, 2));
        yield return (0xFFFF_0000, (0, 0xFFFF));
        yield return (0xACDC_ABBA, (0xABBA, 0xACDC));
        yield return (0xFFFF_FFFF, (0xFFFF, 0xFFFF));
    }
}