namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies RandomNumberGenerator extension helpers.
/// </summary>
/// <remarks>
/// See <see cref="global::System.Security.Cryptography.RandomNumberGeneratorExtensions"/>.
/// </remarks>
[TestClass]
public sealed class RandomNumberGeneratorExtensionsTests
{
    /// <summary>
    /// This method verifies NextString honors the requested character count.
    /// </summary>
    [TestMethod]
    public void NextString_respects_length()
    {
        using var rng = RandomNumberGenerator.Create();

        var s = rng.NextString(12);

        Assert.AreEqual(12, s.Length);
    }

    /// <summary>
    /// This method verifies Next stays within the inclusive lower and exclusive upper bounds.
    /// </summary>
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

    /// <summary>
    /// This method verifies NextDigits returns only decimal digit characters.
    /// </summary>
    [TestMethod]
    public void NextDigits_only_numeric_chars()
    {
        using var rng = RandomNumberGenerator.Create();

        var s = rng.NextDigits(20);

        Assert.IsTrue(s.All(char.IsDigit));
    }

    /// <summary>
    /// This method verifies Shuffle on strings preserves total length.
    /// </summary>
    [TestMethod]
    public void Shuffle_string_preserves_length()
    {
        using var rng = RandomNumberGenerator.Create();

        var s = rng.Shuffle("abcdef");

        Assert.AreEqual(6, s.Length);
    }

    /// <summary>
    /// This method verifies the parameterless Next overload returns a non-zero sample.
    /// </summary>
    [TestMethod]
    public void Next_parameterless_returns_int()
    {
        using var rng = RandomNumberGenerator.Create();

        var n = rng.Next();

        Assert.AreNotEqual(0, n);
    }

    /// <summary>
    /// This method verifies NextSymbols draws only characters from the symbol set.
    /// </summary>
    [TestMethod]
    public void NextSymbols_only_symbol_charset()
    {
        using var rng = RandomNumberGenerator.Create();

        var s = rng.NextSymbols(30);

        Assert.AreEqual(30, s.Length);
        Assert.IsTrue(s.All(c => "~!@#$%^&*()[];:<>,.-=_+".Contains(c)));
    }

    /// <summary>
    /// This method verifies NextUpper returns only uppercase ASCII letters.
    /// </summary>
    [TestMethod]
    public void NextUpper_only_uppercase_letters()
    {
        using var rng = RandomNumberGenerator.Create();

        var s = rng.NextUpper(25);

        Assert.AreEqual(25, s.Length);
        Assert.IsTrue(s.All(char.IsAsciiLetterUpper));
    }

    /// <summary>
    /// This method verifies NextLower returns only lowercase ASCII letters.
    /// </summary>
    [TestMethod]
    public void NextLower_only_lowercase_letters()
    {
        using var rng = RandomNumberGenerator.Create();

        var s = rng.NextLower(25);

        Assert.AreEqual(25, s.Length);
        Assert.IsTrue(s.All(char.IsAsciiLetterLower));
    }
}
