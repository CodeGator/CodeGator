namespace CodeGator.UnitTests;

[TestClass]
public sealed class RandomNumberGeneratorExtensionsTests
{
    [TestMethod]
    public void NextString_respects_length()
    {
        using var rng = RandomNumberGenerator.Create();

        var s = rng.NextString(12);

        Assert.AreEqual(12, s.Length);
    }

    [TestMethod]
    public void NextRange_is_inclusive()
    {
        using var rng = RandomNumberGenerator.Create();

        for (var i = 0; i < 50; i++)
        {
            var n = rng.Next(2, 5);
            Assert.IsTrue(n >= 2 && n <= 4, n.ToString());
        }
    }

    [TestMethod]
    public void NextDigits_only_numeric_chars()
    {
        using var rng = RandomNumberGenerator.Create();

        var s = rng.NextDigits(20);

        Assert.IsTrue(s.All(char.IsDigit));
    }

    [TestMethod]
    public void Shuffle_string_preserves_length()
    {
        using var rng = RandomNumberGenerator.Create();

        var s = rng.Shuffle("abcdef");

        Assert.AreEqual(6, s.Length);
    }
}
