using System.Security.Claims;

namespace CodeGator.UnitTests;

[TestClass]
public sealed class ClaimsPrincipalExtensionsTests
{
    [TestMethod]
    public void GetEmail_returns_claim_value()
    {
        var p = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Email, "a@b.c"),
        }));

        Assert.AreEqual("a@b.c", p.GetEmail());
    }

    [TestMethod]
    public void GetNameIdentifier_returns_claim_value()
    {
        var p = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "id-1"),
        }));

        Assert.AreEqual("id-1", p.GetNameIdentifier());
    }

    [TestMethod]
    public void GetNickName_returns_claim_value()
    {
        var p = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("nickname", "bob"),
        }));

        Assert.AreEqual("bob", p.GetNickName());
    }

    [TestMethod]
    public void GetName_returns_claim_value()
    {
        var p = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "n"),
        }));

        Assert.AreEqual("n", p.GetName());
    }

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
