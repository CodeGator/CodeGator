namespace CodeGator.UnitTests;

[TestClass]
public sealed class DiagnosticsObjectExtensionsTests
{
    private sealed class Holder
    {
        public int Id { get; set; }
        private readonly string _field = "f";

        public string GetFieldValue() => _field;
    }

    [TestMethod]
    public void GetPropertyValue_by_name()
    {
        var h = new Holder { Id = 9 };

        var v = h.GetPropertyValue(nameof(Holder.Id));

        Assert.AreEqual(9, v);
    }

    [TestMethod]
    public void SetPropertyValue_by_name()
    {
        var h = new Holder();

        h.SetPropertyValue(nameof(Holder.Id), 3);

        Assert.AreEqual(3, h.Id);
    }

    [TestMethod]
    public void TryGetPropertyValue_with_expression()
    {
        var h = new Holder { Id = 4 };

        var ok = h.TryGetPropertyValue(x => x.Id, out var value);

        Assert.IsTrue(ok);
        Assert.AreEqual(4, value);
    }
}
