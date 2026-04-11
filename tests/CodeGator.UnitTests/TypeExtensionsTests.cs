namespace CodeGator.UnitTests;

[TestClass]
public sealed class TypeExtensionsTests
{
    public abstract class SampleBase { }

    public sealed class SampleDerived : SampleBase { }

    [TestMethod]
    public void DerivedTypes_finds_concrete_subtypes_in_whitelisted_assembly()
    {
        var types = typeof(SampleBase).DerivedTypes(assemblyWhiteList: "CodeGator.UnitTests");

        CollectionAssert.Contains(types, typeof(SampleDerived));
    }
}
