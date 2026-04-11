namespace CodeGator.UnitTests;

[TestClass]
public sealed class CryptographyStringExtensionsTests
{
    [TestMethod]
    public void ToSha256_is_deterministic()
    {
        var a = "hello".ToSha256();
        var b = "hello".ToSha256();

        Assert.AreEqual(a, b);
        Assert.IsFalse(string.IsNullOrEmpty(a));
    }

    [TestMethod]
    public void ToSha256_empty_returns_empty()
    {
        Assert.AreEqual("", "".ToSha256());
    }

    [TestMethod]
    public void ToSha512_is_deterministic()
    {
        var a = "hello".ToSha512();
        var b = "hello".ToSha512();

        Assert.AreEqual(a, b);
    }
}
