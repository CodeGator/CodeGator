using CodeGator;

namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies Result and generic result factory methods.
/// </summary>
/// <remarks>Covers <see cref="Result"/> and generic <c>Result&lt;T&gt;</c> outcomes.</remarks>
[TestClass]
public sealed class ResultTests
{
    /// <summary>
    /// This method verifies Success marks the result as successful with no error.
    /// </summary>
    [TestMethod]
    public void Result_Success_is_success_without_error()
    {
        var r = Result.Success();

        Assert.IsTrue(r.IsSuccess);
        Assert.IsFalse(r.IsFailure);
        Assert.AreSame(Error.None, r.Error);
    }

    /// <summary>
    /// This method verifies Failure carries the supplied error payload.
    /// </summary>
    [TestMethod]
    public void Result_Failure_is_failure_with_error()
    {
        var err = new Error("X", "msg");
        var r = Result.Failure(err);

        Assert.IsFalse(r.IsSuccess);
        Assert.IsTrue(r.IsFailure);
        Assert.AreSame(err, r.Error);
    }

    /// <summary>
    /// This method verifies typed Success attaches the expected data value.
    /// </summary>
    [TestMethod]
    public void Result_of_T_Success_carries_data()
    {
        var r = Result<int>.Success(42);

        Assert.IsTrue(r.IsSuccess);
        Assert.AreEqual(42, r.Data);
    }

    /// <summary>
    /// This method verifies typed Failure reports failure without success data.
    /// </summary>
    [TestMethod]
    public void Result_of_T_Failure_has_no_success_data()
    {
        var err = new Error("Y", "bad");
        var r = Result<int>.Failure(err);

        Assert.IsTrue(r.IsFailure);
        Assert.AreSame(err, r.Error);
    }
}
