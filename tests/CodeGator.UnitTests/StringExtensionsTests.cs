namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies <see cref="global::System.StringExtensions"/> helpers.
/// </summary>
[TestClass]
public sealed class StringExtensionsTests
{
    /// <summary>
    /// This method verifies IsMatch treats literal equality as expected.
    /// </summary>
    [TestMethod]
    public void IsMatch_literal_equal_returns_true()
    {
        Assert.IsTrue("hello".IsMatch("hello"));
        Assert.IsFalse("hello".IsMatch("world"));
    }

    /// <summary>
    /// This method verifies IsMatch applies wildcards on the right-hand pattern.
    /// </summary>
    [TestMethod]
    public void IsMatch_wildcard_on_rhs_matches()
    {
        Assert.IsTrue("hello world".IsMatch("hello*"));
        Assert.IsTrue("abc".IsMatch("a?c"));
        Assert.IsFalse("ab".IsMatch("a?c"));
    }

    /// <summary>
    /// This method verifies IsMatch applies wildcards on the left-hand pattern.
    /// </summary>
    [TestMethod]
    public void IsMatch_wildcard_on_lhs_matches()
    {
        Assert.IsTrue("hello*".IsMatch("hello world"));
        Assert.IsTrue("xay".IsMatch("?a?"));
    }

    /// <summary>
    /// This method verifies ReplaceFriendlyNameToken substitutes the friendly name token.
    /// </summary>
    [TestMethod]
    public void ReplaceFriendlyNameToken_replaces_token_with_domain_name()
    {
        var name = AppDomain.CurrentDomain.FriendlyName;
        var result = "app=[FriendlyName]".ReplaceFriendlyNameToken();

        StringAssert.Contains(result, name);
    }

    /// <summary>
    /// This method verifies ReplaceTimeToken replaces a whole-string time token.
    /// </summary>
    [TestMethod]
    public void ReplaceTimeToken_replaces_when_whole_string_is_token()
    {
        var result = "[Now]".ReplaceTimeToken();

        Assert.IsFalse(result.Contains("[Now]", StringComparison.OrdinalIgnoreCase));
        Assert.IsTrue(result.Length > 0);
    }

    /// <summary>
    /// This method verifies ReplaceTimeToken leaves embedded tokens unchanged.
    /// </summary>
    [TestMethod]
    public void ReplaceTimeToken_leaves_non_token_unchanged()
    {
        var s = "prefix [Now] suffix";
        Assert.AreEqual(s, s.ReplaceTimeToken());
    }

    /// <summary>
    /// This method verifies ReplaceTimeUtcToken replaces a whole-string UTC token.
    /// </summary>
    [TestMethod]
    public void ReplaceTimeUtcToken_replaces_when_whole_string_is_token()
    {
        var result = "[NowUtc]".ReplaceTimeUtcToken();

        Assert.IsFalse(result.Contains("[NowUtc]", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// This method verifies ReplaceDriveToken substitutes the drive letter token prefix.
    /// </summary>
    [TestMethod]
    public void ReplaceDriveToken_replaces_prefix()
    {
        var result = "[Drive]/folder".ReplaceDriveToken();

        Assert.IsTrue(
            result.StartsWith("C:", StringComparison.OrdinalIgnoreCase) ||
            result.StartsWith("D:", StringComparison.OrdinalIgnoreCase),
            result);
    }

    /// <summary>
    /// This method verifies Obfuscate returns short strings unchanged for wide masks.
    /// </summary>
    [TestMethod]
    public void Obfuscate_short_or_equal_length_returns_original()
    {
        Assert.AreEqual("ab", "ab".Obfuscate(4));
        Assert.AreEqual("abcd", "abcd".Obfuscate(4));
    }

    /// <summary>
    /// This method verifies Obfuscate masks characters beyond the visible prefix length.
    /// </summary>
    [TestMethod]
    public void Obfuscate_masks_suffix()
    {
        Assert.AreEqual("abcd**********", "abcdefghijklmn".Obfuscate(4));
    }
}
