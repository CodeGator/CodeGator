using System.Security.Claims;

namespace CodeGator.UnitTests;

/// <summary>
/// This class verifies ClaimsPrincipal extension claim readers.
/// </summary>
/// <remarks>
/// See <see cref="global::System.Security.Claims.ClaimsPrincipalExtensions"/>.
/// </remarks>
[TestClass]
public sealed class ClaimsPrincipalExtensionsTests
{
    /// <summary>
    /// This method verifies GetEmail returns the email claim when present.
    /// </summary>
    [TestMethod]
    public void GetEmail_returns_claim_value()
    {
        var p = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Email, "a@b.c"),
        }));

        Assert.AreEqual("a@b.c", p.GetEmail());
    }

    /// <summary>
    /// This method verifies GetNameIdentifier returns the name identifier claim.
    /// </summary>
    [TestMethod]
    public void GetNameIdentifier_returns_claim_value()
    {
        var p = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "id-1"),
        }));

        Assert.AreEqual("id-1", p.GetNameIdentifier());
    }

    /// <summary>
    /// This method verifies GetNickName returns the nickname claim when present.
    /// </summary>
    [TestMethod]
    public void GetNickName_returns_claim_value()
    {
        var p = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("nickname", "bob"),
        }));

        Assert.AreEqual("bob", p.GetNickName());
    }

    /// <summary>
    /// This method verifies GetName returns the name claim when present.
    /// </summary>
    [TestMethod]
    public void GetName_returns_claim_value()
    {
        var p = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "n"),
        }));

        Assert.AreEqual("n", p.GetName());
    }

    /// <summary>
    /// This method verifies GetPicture returns the picture claim when present.
    /// </summary>
    [TestMethod]
    public void GetPicture_returns_claim_value()
    {
        var p = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("picture", "https://example.com/p.png"),
        }));

        Assert.AreEqual("https://example.com/p.png", p.GetPicture());
    }
}
