
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Security.Claims;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class contains extension methods related to the <see cref="ClaimsPrincipal"/>
/// type.
/// </summary>
public static partial class ClaimsPrincipalExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method returns the value of the email claim, if it exists, in
    /// the specified <see cref="ClaimsPrincipal"/> object.
    /// </summary>
    /// <param name="principal">The claims to use for the operation.</param>
    /// <returns>The value of the claim, or an empty string, if the claim
    /// wasn't found on the principal.</returns>
    public static string GetEmail(
        [NotNull] this ClaimsPrincipal principal
        )
    {
        Guard.Instance().ThrowIfNull(principal, nameof(principal));

        var claim = principal.Claims.FirstOrDefault(
            x => x.Type == ClaimTypes.Email
            );
        
        if (null != claim)
        {
            return claim.Value;
        }

        return string.Empty;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns the value of the name identifier claim, if it 
    /// exists, in the specified <see cref="ClaimsPrincipal"/> object.
    /// </summary>
    /// <param name="claimsPrincipal">The principal to use for the operation.</param>
    /// <returns>The value of the claim, or an empty string, if the claim
    /// wasn't found on the principal.</returns>
    public static string GetNameIdentifier(
        [NotNull] this ClaimsPrincipal claimsPrincipal
        )
    {
        var claim = claimsPrincipal.Claims.FirstOrDefault(
            x => x.Type == ClaimTypes.NameIdentifier
            );

        if (null != claim)
        {
            return claim.Value;
        }
        else
        {
            return String.Empty; 
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method returns the value of the nickname claim, if it 
    /// exists, in the specified <see cref="ClaimsPrincipal"/> object.
    /// </summary>
    /// <param name="claimsPrincipal">The principal to use for the operation.</param>
    /// <returns>The value of the claim, or an empty string, if the claim
    /// wasn't found on the principal.</returns>
    public static string GetNickName(
        [NotNull] this ClaimsPrincipal claimsPrincipal
        )
    {
        var claim = claimsPrincipal.Claims.FirstOrDefault(
            x => x.Type == "nickname"
            );

        if (null != claim)
        {
            return claim.Value;
        }
        else
        {
            return String.Empty; 
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method returns the value of the name claim, if it 
    /// exists, in the specified <see cref="ClaimsPrincipal"/> object.
    /// </summary>
    /// <param name="claimsPrincipal">The principal to use for the operation.</param>
    /// <returns>The value of the claim, or an empty string, if the claim
    /// wasn't found on the principal.</returns>
    public static string GetName(
        [NotNull] this ClaimsPrincipal claimsPrincipal
        )
    {
        var claim = claimsPrincipal.Claims.FirstOrDefault(
            x => x.Type == ClaimTypes.Name
            );

        if (null != claim)
        {
            return claim.Value;
        }
        else
        {
            return String.Empty; 
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method returns the value of the picture claim, if it 
    /// exists, in the specified <see cref="ClaimsPrincipal"/> object.
    /// </summary>
    /// <param name="claimsPrincipal">The principal to use for the operation.</param>
    /// <returns>The value of the claim, or an empty string, if the claim
    /// wasn't found on the principal.</returns>
    public static string GetPicture(
        [NotNull] this ClaimsPrincipal claimsPrincipal
        )
    {
        var claim = claimsPrincipal.Claims.FirstOrDefault(
            x => x.Type == "picture"
            );

        if (null != claim)
        {
            return claim.Value;
        }
        else
        {
            return String.Empty;
        }
    }

    #endregion
}
