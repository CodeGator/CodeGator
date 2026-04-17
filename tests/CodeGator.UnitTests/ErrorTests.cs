using CodeGator;

namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies the <see cref="Error"/> record and sentinel values.
/// </summary>
[TestClass]
public sealed class ErrorTests
{
    /// <summary>
    /// This method verifies Error stores code and description values.
    /// </summary>
    [TestMethod]
    public void Error_holds_code_and_description()
    {
        var err = new Error("E1", "Something failed.");

        Assert.AreEqual("E1", err.Code);
        Assert.AreEqual("Something failed.", err.Description);
    }

    /// <summary>
    /// This method verifies Error.None exposes the expected sentinel text.
    /// </summary>
    [TestMethod]
    public void Error_None_has_expected_shape()
    {
        Assert.AreEqual("No error.", Error.None.Code);
        Assert.AreEqual(string.Empty, Error.None.Description);
    }
}
