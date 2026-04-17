using System.Security.Cryptography;

namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies cryptographic <see cref="string"/> and
/// <see cref="StringBuilder"/> extension helpers.
/// </summary>
[TestClass]
public sealed class CryptographyStringExtensionsTests
{
    /// <summary>
    /// This method verifies ToSha256 returns the same digest for identical input.
    /// </summary>
    [TestMethod]
    public void ToSha256_is_deterministic()
    {
        var a = "hello".ToSha256();
        var b = "hello".ToSha256();

        Assert.AreEqual(a, b);
        Assert.IsFalse(string.IsNullOrEmpty(a));
    }

    /// <summary>
    /// This method verifies ToSha256 returns an empty string for empty input.
    /// </summary>
    [TestMethod]
    public void ToSha256_empty_returns_empty()
    {
        Assert.AreEqual("", "".ToSha256());
    }

    /// <summary>
    /// This method verifies ToSha512 returns the same digest for identical input.
    /// </summary>
    [TestMethod]
    public void ToSha512_is_deterministic()
    {
        var a = "hello".ToSha512();
        var b = "hello".ToSha512();

        Assert.AreEqual(a, b);
    }

    /// <summary>
    /// This method verifies ToSha512 returns an empty string for empty input.
    /// </summary>
    [TestMethod]
    public void ToSha512_empty_returns_empty()
    {
        Assert.AreEqual(string.Empty, string.Empty.ToSha512());
    }

    /// <summary>
    /// This method verifies Shuffle mutates the builder and preserves length.
    /// </summary>
    [TestMethod]
    public void StringBuilder_Shuffle_without_rng_preserves_length()
    {
        var sb = new StringBuilder("abcd");

        var same = sb.Shuffle();

        Assert.AreSame(sb, same);
        Assert.AreEqual(4, sb.Length);
    }

    /// <summary>
    /// This method verifies Shuffle with an RNG mutates the builder in place.
    /// </summary>
    [TestMethod]
    public void StringBuilder_Shuffle_with_rng_preserves_length()
    {
        var sb = new StringBuilder("xyz");
        using var rng = RandomNumberGenerator.Create();

        var same = sb.Shuffle(rng);

        Assert.AreSame(sb, same);
        Assert.AreEqual(3, sb.Length);
    }

    /// <summary>
    /// This method verifies Shuffle throws when the builder reference is null.
    /// </summary>
    [TestMethod]
    public void StringBuilder_Shuffle_throws_on_null()
    {
        StringBuilder? builder = null;

        Assert.ThrowsExactly<ArgumentNullException>(() => builder!.Shuffle());
    }
}
