namespace WorldRepr.Repr;

internal class DirectionTests
{
    [TestCaseSource(nameof(TestDecodeFromStringCases))]
    public void TestGetters(Directions d)
    {
        Assert.True(d.Top());
        Assert.False(d.Right());
        Assert.True(d.Bottom());
        Assert.False(d.Left());
    }

    [TestCaseSource(nameof(TestDecodeFromStringCases))]
    public void TestStringEncoder(Directions d)
    {
        Assert.That(d.EncodeAsString(), Is.EqualTo("1010"));
    }

    public static IEnumerable<Directions> TestDecodeFromStringCases()
    {
        yield return Directions.DecodeFromString("1010");
        yield return new Directions(true, false, true, false);
    }
}