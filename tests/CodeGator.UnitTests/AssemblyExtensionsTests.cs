namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies <see cref="global::System.Reflection.AssemblyExtensions"/> helpers.
/// </summary>
[TestClass]
public sealed class AssemblyExtensionsTests
{
    /// <summary>
    /// This method verifies ReadFileVersion returns metadata for the CodeGator assembly.
    /// </summary>
    [TestMethod]
    public void ReadFileVersion_returns_non_empty_for_current_assembly()
    {
        var asm = typeof(global::System.Collections.Generic.EnumerableExtensions).Assembly;
        var v = asm.ReadFileVersion();

        Assert.IsFalse(string.IsNullOrEmpty(v));
    }
}
