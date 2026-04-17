namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies <see cref="global::System.AppDomainExtensions"/> helpers.
/// </summary>
[TestClass]
public sealed class AppDomainExtensionsTests
{
    /// <summary>
    /// This method verifies FriendlyNameEx returns a non-empty display name.
    /// </summary>
    [TestMethod]
    public void FriendlyNameEx_returns_non_empty()
    {
        var name = AppDomain.CurrentDomain.FriendlyNameEx();

        Assert.IsFalse(string.IsNullOrWhiteSpace(name));
    }

    /// <summary>
    /// This method verifies FriendlyNameEx can strip trailing executable extensions.
    /// </summary>
    [TestMethod]
    public void FriendlyNameEx_stripTrailingExtension_removes_dll()
    {
        var name = AppDomain.CurrentDomain.FriendlyNameEx(stripTrailingExtension: true);

        Assert.IsFalse(name.EndsWith(".dll", StringComparison.OrdinalIgnoreCase));
    }
}
