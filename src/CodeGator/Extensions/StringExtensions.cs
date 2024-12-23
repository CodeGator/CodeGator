
#pragma warning disable IDE0130
namespace System;
#pragma warning restore IDE0130

/// <summary>
/// This class utility contains extension methods for the <see cref="string"/> type.
/// </summary>
public static partial class StringExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method performs a simple equality match of the two strings that 
    /// allows for the possibility of wildcard characters, in either string.
    /// </summary>
    /// <param name="lhs">The left-hand string to use for the comparison.</param>
    /// <param name="rhs">The right-hand string to use for the comparison.</param>
    /// <returns><c>true</c> if a match was found; <c>false</c> otherwise.</returns>
    /// <remarks>
    /// <para>
    /// The possible wild cards are as follows:
    /// <list type="bullet">
    /// <item>* - matches all characters</item>
    /// <item>? - matches a single character</item>
    /// </list>
    /// </para>
    /// <para>
    /// For a more robust comparison, try the <see cref="Regex.IsMatch(string, string)"/>
    /// method.
    /// </para>
    /// </remarks>
    public static bool IsMatch(
        [NotNull] this string lhs,
        [NotNull] string rhs
        )
    {
        var result = false;

        if (rhs.Contains("*") || rhs.Contains("?"))
        {
            var regex = "^" + Regex.Escape(rhs).Replace("\\?", ".").Replace("\\*", ".*") + "$";
            result = Regex.IsMatch(lhs, regex);
        }
        else if (lhs.Contains("*") || lhs.Contains("?"))
        {
            var regex = "^" + Regex.Escape(lhs).Replace("\\?", ".").Replace("\\*", ".*") + "$";
            result = Regex.IsMatch(rhs, regex);
        }
        else
        {
            result = lhs.Equals(rhs);
        }
        return result;
    }

    // *******************************************************************

    /// <summary>
    /// This method replaces a <c>[FriendlyName]</c> token in the <paramref name="value"/>
    /// parameter, with the current application domain's friendly name.
    /// </summary>
    /// <param name="value">The string to use for the operation.</param>
    /// <returns>The converted string.</returns>
    /// <remarks>
    /// <para>
    /// NOTE: The token is not case sensitive.
    /// </para>
    /// </remarks>
    public static string ReplaceFriendlyNameToken(
        [NotNull] this string value
        )
    {
        if (value.Contains("[FriendlyName]", StringComparison.InvariantCultureIgnoreCase))
        {
            value = value.Replace(
                "[FriendlyName]",
                AppDomain.CurrentDomain.FriendlyName,
                StringComparison.InvariantCultureIgnoreCase
                );
        }
        return value;
    }

    // *******************************************************************

    /// <summary>
    /// This method replaces a <c>[Now]</c> token in the <paramref name="value"/>
    /// parameter, with the current date/time.
    /// </summary>
    /// <param name="value">The string to use for the operation.</param>
    /// <returns>The converted string.</returns>
    /// <remarks>
    /// <para>
    /// NOTE: The token is not case sensitive.
    /// </para>
    /// </remarks>
    public static string ReplaceTimeToken(
        [NotNull] this string value
        )
    {
        if (value.Equals("[Now]", StringComparison.InvariantCultureIgnoreCase))
        {
            value = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        }
        return value;
    }

    // *******************************************************************

    /// <summary>
    /// This method replaces a <c>[NowUtc]</c> token in the <paramref name="value"/>
    /// parameter, with the current (UTC) date/time.
    /// </summary>
    /// <param name="value">The string to use for the operation.</param>
    /// <returns>The converted string.</returns>
    /// <remarks>
    /// <para>
    /// NOTE: The token is not case sensitive.
    /// </para>
    /// </remarks>
    public static string ReplaceTimeUtcToken(
        [NotNull] this string value
        )
    {
        if (value.Equals("[NowUtc]", StringComparison.InvariantCultureIgnoreCase))
        {
            value = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");
        }
        return value;
    }

    // *******************************************************************

    /// <summary>
    /// This method replaces a <c>[Drive]</c> token in the <paramref name="value"/>
    /// parameter, with a drive letter that is appropriate for use on the current
    /// machine
    /// </summary>
    /// <param name="value">The string to use for the operation.</param>
    /// <returns>The converted string.</returns>
    /// <remarks>
    /// <para>
    /// The contents of the <c>[Drive]</c> token are replaced with the drive letter
    /// <c>C:</c> on machines without a <c>D</c> drive; otherwise, the token is 
    /// replaced with the drive letter <c>D:</c>. This is to adjust for variations 
    /// in local development environments, where some laptops have two drives, while
    /// others have one. rVetLink always runs on the D drive, in production.
    /// </para>
    /// <para>
    /// NOTE: The token is not case sensitive.
    /// </para>
    /// </remarks>
    public static string ReplaceDriveToken(
        [NotNull] this string value
        )
    {
        if (value.StartsWith("[Drive]", StringComparison.InvariantCultureIgnoreCase))
        {
            var replacementValue = "C:";
            if (Environment.GetLogicalDrives().Contains("D:\\"))
            {
                replacementValue = "D:";
            }

            value = value.Replace(
                value.StartsWith("[Drive]:", StringComparison.InvariantCultureIgnoreCase)
                    ? "[Drive]:"
                    : "[Drive]",
                replacementValue,
                StringComparison.InvariantCultureIgnoreCase
                );
        }
        return value;
    }

    // *******************************************************************

    /// <summary>
    /// This method exposes the given number of characters in the specified string, while 
    /// converting all other characters to asterisks. 
    /// </summary>
    /// <param name="value">The value to be obfuscated.</param>
    /// <param name="visibleCharacters">The number of characters to expose.</param>
    /// <returns>An obfuscated version of the <paramref name="value"/> parameter.</returns>
    public static string Obfuscate(
        [NotNull] this string value,
        int visibleCharacters = 4
        )
    {
        var length = value.Length;
        if (length <= visibleCharacters)
        {
            return value;
        }

        var exposed = value[0 .. visibleCharacters];
        var obfuscated = new string('*', length - visibleCharacters);   
        return $"{exposed}{obfuscated}";
    }

    #endregion
}
