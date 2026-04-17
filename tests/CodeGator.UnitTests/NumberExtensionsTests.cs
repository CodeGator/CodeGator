namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies <see cref="global::System.NumberExtensions"/> byte formatting helpers.
/// </summary>
[TestClass]
public sealed class NumberExtensionsTests
{
    /// <summary>
    /// This method verifies zero formats as byte units with the bytes suffix.
    /// </summary>
    [TestMethod]
    public void FormattedAsBytes_zero_bytes()
    {
        Assert.AreEqual("0 bytes", 0L.FormattedAsBytes());
    }

    /// <summary>
    /// This method verifies kilobyte-scale values include the KB suffix.
    /// </summary>
    [TestMethod]
    public void FormattedAsBytes_kb()
    {
        var s = 1024L.FormattedAsBytes(1);
        StringAssert.StartsWith(s, "1");
        StringAssert.Contains(s, "KB");
    }

    /// <summary>
    /// This method verifies negative values render with a leading minus sign.
    /// </summary>
    [TestMethod]
    public void FormattedAsBytes_negative_uses_prefix()
    {
        var s = (-512L).FormattedAsBytes(0);
        StringAssert.StartsWith(s, "-");
    }

    /// <summary>
    /// This method verifies the double overload delegates to the long formatter.
    /// </summary>
    [TestMethod]
    public void FormattedAsBytes_double_delegates_to_long()
    {
        Assert.AreEqual(1024L.FormattedAsBytes(1), ((double)1024).FormattedAsBytes(1));
    }

    /// <summary>
    /// This method verifies the float overload delegates to the long formatter.
    /// </summary>
    [TestMethod]
    public void FormattedAsBytes_float_delegates_to_long()
    {
        Assert.AreEqual(1024L.FormattedAsBytes(1), ((float)1024).FormattedAsBytes(1));
    }

    /// <summary>
    /// This method verifies int and short overloads match long formatting output.
    /// </summary>
    [TestMethod]
    public void FormattedAsBytes_int_and_short_overloads_match_long()
    {
        Assert.AreEqual(512L.FormattedAsBytes(0), ((int)512).FormattedAsBytes(0));
        Assert.AreEqual(100L.FormattedAsBytes(1), ((short)100).FormattedAsBytes(1));
    }

    /// <summary>
    /// This method verifies megabyte-scale values include the MB suffix.
    /// </summary>
    [TestMethod]
    public void FormattedAsBytes_uses_mb_suffix_for_megabyte_scale()
    {
        var s = (1024L * 1024L).FormattedAsBytes(1);

        StringAssert.Contains(s, "MB");
    }
}
