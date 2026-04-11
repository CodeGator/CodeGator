namespace CodeGator.UnitTests;

[TestClass]
public sealed class AppDomainExtensionsTests
{
    [TestMethod]
    public void FriendlyNameEx_returns_non_empty()
    {
        var name = AppDomain.CurrentDomain.FriendlyNameEx();

        Assert.IsFalse(string.IsNullOrWhiteSpace(name));
    }

    [TestMethod]
    public void FriendlyNameEx_stripTrailingExtension_removes_dll()
    {
        var name = AppDomain.CurrentDomain.FriendlyNameEx(stripTrailingExtension: true);

        Assert.IsFalse(name.EndsWith(".dll", StringComparison.OrdinalIgnoreCase));
    }
}
