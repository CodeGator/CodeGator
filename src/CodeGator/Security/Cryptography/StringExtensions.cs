
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
    /// This method randomly shuffles the characters in the given string.
    /// </summary>
    /// <param name="builder">The string builder to use for the operation.</param>
    /// <returns>A randomly shuffled string.</returns>
    public static StringBuilder Shuffle(
        [NotNull] this StringBuilder builder
        )
    {
        Guard.Instance().ThrowIfNull(builder, nameof(builder));

        using var rnd = RandomNumberGenerator.Create();

        return builder.Shuffle(rnd);
    }

    // *******************************************************************

    /// <summary>
    /// This method randomly shuffles the characters in the given
    /// string builder.
    /// </summary>
    /// <param name="builder">The string builder to use for the operation.</param>
    /// <param name="rnd">The random number generator to use for the operation.</param>
    /// <returns>A randomly shuffled string.</returns>
    public static StringBuilder Shuffle(
        [NotNull] this StringBuilder builder,
        [NotNull] RandomNumberGenerator rnd
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
    /// This method calculates an SHA256 hash for the given string.
    /// </summary>
    /// <param name="value">The value to be hashed.</param>
    /// <returns>The SHA256 hash for the <paramref name="value"/> parameter.</returns>
    public static string ToSha256(
        [NotNull] this string value
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
        [NotNull] this string value
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
