namespace CodeGator.UnitTests;

[TestClass]
public sealed class StringExtensionsTests
{
    [TestMethod]
    public void IsMatch_literal_equal_returns_true()
    {
        Assert.IsTrue("hello".IsMatch("hello"));
        Assert.IsFalse("hello".IsMatch("world"));
    }

    [TestMethod]
    public void IsMatch_wildcard_on_rhs_matches()
    {
        Assert.IsTrue("hello world".IsMatch("hello*"));
        Assert.IsTrue("abc".IsMatch("a?c"));
        Assert.IsFalse("ab".IsMatch("a?c"));
    }

    [TestMethod]
    public void IsMatch_wildcard_on_lhs_matches()
    {
        Assert.IsTrue("hello*".IsMatch("hello world"));
        Assert.IsTrue("xay".IsMatch("?a?"));
    }

    [TestMethod]
    public void ReplaceFriendlyNameToken_replaces_token_with_domain_name()
    {
        var name = AppDomain.CurrentDomain.FriendlyName;
        var result = "app=[FriendlyName]".ReplaceFriendlyNameToken();

        StringAssert.Contains(result, name);
    }

    [TestMethod]
    public void ReplaceTimeToken_replaces_when_whole_string_is_token()
    {
        var result = "[Now]".ReplaceTimeToken();

        Assert.IsFalse(result.Contains("[Now]", StringComparison.OrdinalIgnoreCase));
        Assert.IsTrue(result.Length > 0);
    }

    [TestMethod]
    public void ReplaceTimeToken_leaves_non_token_unchanged()
    {
        var s = "prefix [Now] suffix";
        Assert.AreEqual(s, s.ReplaceTimeToken());
    }

    [TestMethod]
    public void ReplaceTimeUtcToken_replaces_when_whole_string_is_token()
    {
        var result = "[NowUtc]".ReplaceTimeUtcToken();

        Assert.IsFalse(result.Contains("[NowUtc]", StringComparison.OrdinalIgnoreCase));
    }

    [TestMethod]
    public void ReplaceDriveToken_replaces_prefix()
    {
        var result = "[Drive]/folder".ReplaceDriveToken();

        Assert.IsTrue(
            result.StartsWith("C:", StringComparison.OrdinalIgnoreCase) ||
            result.StartsWith("D:", StringComparison.OrdinalIgnoreCase),
            result);
    }

    [TestMethod]
    public void Obfuscate_short_or_equal_length_returns_original()
    {
        Assert.AreEqual("ab", "ab".Obfuscate(4));
        Assert.AreEqual("abcd", "abcd".Obfuscate(4));
    }

    [TestMethod]
    public void Obfuscate_masks_suffix()
    {
        Assert.AreEqual("abcd**********", "abcdefghijklmn".Obfuscate(4));
    }
}
