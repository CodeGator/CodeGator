namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies <see cref="global::System.Diagnostics.ObjectExtensions"/> helpers.
/// </summary>
[TestClass]
public sealed class DiagnosticsObjectExtensionsTests
{
    private sealed class Holder
    {
        public int Id { get; set; }
        private readonly string _field = "f";

        public string GetFieldValue() => _field;
    }

    /// <summary>
    /// This method verifies GetPropertyValue reads a public property by name.
    /// </summary>
    [TestMethod]
    public void GetPropertyValue_by_name()
    {
        var h = new Holder { Id = 9 };

        var v = h.GetPropertyValue(nameof(Holder.Id));

        Assert.AreEqual(9, v);
    }

    /// <summary>
    /// This method verifies SetPropertyValue writes a public property by name.
    /// </summary>
    [TestMethod]
    public void SetPropertyValue_by_name()
    {
        var h = new Holder();

        h.SetPropertyValue(nameof(Holder.Id), 3);

        Assert.AreEqual(3, h.Id);
    }

    /// <summary>
    /// This method verifies TryGetPropertyValue succeeds for a simple expression.
    /// </summary>
    [TestMethod]
    public void TryGetPropertyValue_with_expression()
    {
        var h = new Holder { Id = 4 };

        var ok = h.TryGetPropertyValue(x => x.Id, out var value);

        Assert.IsTrue(ok);
        Assert.AreEqual(4, value);
    }
}
