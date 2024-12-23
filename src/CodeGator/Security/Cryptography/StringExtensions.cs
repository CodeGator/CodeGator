
#pragma warning disable IDE0130
namespace System.Security.Cryptography;
#pragma warning restore IDE0130

/// <summary>
/// This class contains extension methods related to the <see cref="string"/>
/// type.
/// </summary>
public static partial class StringExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method randomly shuffles the characters in the given
    /// string builder.
    /// </summary>
    /// <param name="builder">The string builder to use for the operation.</param>
    /// <param name="rnd">The random number generator to use for the operation.</param>
    /// <returns>A randomly shuffled string.</returns>
    public static StringBuilder Shuffle(
        this StringBuilder builder,
        RandomNumberGenerator rnd
        )
    {
        for (var x = 0; x < builder.Length; x++)
        {
            var index = rnd.Next(0, builder.Length);
            var ch = builder[x];

            builder[x] = builder[index];
            builder[index] = ch;
        }
        return builder;
    }

    // *******************************************************************

    /// <summary>
    /// This method reverses the characters in a string builder.
    /// </summary>
    /// <param name="builder">The string builder to use for the operation.</param>
    /// <returns>A reversed version of the specified string.</returns>
    public static StringBuilder Reverse(
        this StringBuilder builder
        )
    {
        for (int x = 0; x < builder.Length / 2; x++)
        {
            var temp = builder[builder.Length - x - 1];
            builder[builder.Length - x - 1] = builder[x];
            builder[x] = temp;
        }
        return builder;
    }

    // *******************************************************************

    /// <summary>
    /// This method calculates an SHA256 hash for the given string.
    /// </summary>
    /// <param name="value">The value to be hashed.</param>
    /// <returns>The SHA256 hash for the <paramref name="value"/> parameter.</returns>
    public static string ToSha256(
        this string value
        )
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        var bytes = Encoding.UTF8.GetBytes(value);
        var hash = SHA256.HashData(bytes);

        var base64 = Convert.ToBase64String(hash);
        return base64;
    }

    // *******************************************************************

    /// <summary>
    /// This method calculates an SHA512 hash for the given string.
    /// </summary>
    /// <param name="value">The value to be hashed.</param>
    /// <returns>The SHA512 hash for the <paramref name="value"/> parameter.</returns>
    public static string ToSha512(
        this string value
        )
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        var bytes = Encoding.UTF8.GetBytes(value);
        var hash = SHA512.HashData(bytes);

        var base64 = Convert.ToBase64String(hash);
        return base64;
    }

    #endregion
}
