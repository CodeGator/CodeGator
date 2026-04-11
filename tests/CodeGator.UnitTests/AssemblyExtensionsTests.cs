namespace CodeGator.UnitTests;

[TestClass]
public sealed class AssemblyExtensionsTests
{
    [TestMethod]
    public void ReadFileVersion_returns_non_empty_for_current_assembly()
    {
        var asm = typeof(global::System.Collections.Generic.EnumerableExtensions).Assembly;
        var v = asm.ReadFileVersion();

        Assert.IsFalse(string.IsNullOrEmpty(v));
    }
}
