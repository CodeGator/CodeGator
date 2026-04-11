namespace CodeGator.UnitTests;

[TestClass]
public sealed class NumberExtensionsTests
{
    [TestMethod]
    public void FormattedAsBytes_zero_bytes()
    {
        Assert.AreEqual("0 bytes", 0L.FormattedAsBytes());
    }

    [TestMethod]
    public void FormattedAsBytes_kb()
    {
        var s = 1024L.FormattedAsBytes(1);
        StringAssert.StartsWith(s, "1");
        StringAssert.Contains(s, "KB");
    }

    [TestMethod]
    public void FormattedAsBytes_negative_uses_prefix()
    {
        var s = (-512L).FormattedAsBytes(0);
        StringAssert.StartsWith(s, "-");
    }

    [TestMethod]
    public void FormattedAsBytes_double_delegates_to_long()
    {
        Assert.AreEqual(1024L.FormattedAsBytes(1), ((double)1024).FormattedAsBytes(1));
    }
}
