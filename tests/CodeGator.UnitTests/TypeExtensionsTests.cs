namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies TypeExtensions DerivedTypes assembly filtering.
/// </summary>
/// <remarks>
/// See <see cref="global::System.TypeExtensions.DerivedTypes(System.Type,string?,string?)"/>.
/// </remarks>
[TestClass]
public sealed class TypeExtensionsTests
{
    /// <summary>
    /// This class provides a base type for DerivedTypes whitelist tests.
    /// </summary>
    public abstract class SampleBase { }

    /// <summary>
    /// This class provides a concrete derived type for DerivedTypes tests.
    /// </summary>
    public sealed class SampleDerived : SampleBase { }

    /// <summary>
    /// This method verifies DerivedTypes returns concrete subtypes from a whitelist.
    /// </summary>
    [TestMethod]
    public void DerivedTypes_finds_concrete_subtypes_in_whitelisted_assembly()
    {
        var types = typeof(SampleBase).DerivedTypes(assemblyWhiteList: "CodeGator.UnitTests");

        CollectionAssert.Contains(types, typeof(SampleDerived));
    }
}
