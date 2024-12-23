﻿
#pragma warning disable IDE0130
namespace System.Text;
#pragma warning restore IDE0130

/// <summary>
/// This class contains extension methods related to the <see cref="StringBuilder"/>
/// type.
/// </summary>
public static partial class StringBuilderExtensions
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
        this StringBuilder builder
        )
    {
        Guard.Instance().ThrowIfNull(builder, nameof(builder));

        using var rnd = RandomNumberGenerator.Create();

        return builder.Shuffle(rnd);
    }

    // *******************************************************************

    /// <summary>
    /// This method randomly shuffles the characters in the given string.
    /// </summary>
    /// <param name="builder">The string builder to use for the operation.</param>
    /// <param name="rnd">The random number generator to use for the operation.</param>
    /// <returns>A randomly shuffled string.</returns>
    public static StringBuilder Shuffle(
        this StringBuilder builder,
        RandomNumberGenerator rnd
        )
    {
        Guard.Instance().ThrowIfNull(builder, nameof(builder));

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
    /// This method reverses the characters in a string.
    /// </summary>
    /// <param name="builder">The string builder to use for the operation.</param>
    /// <returns>A reversed version of the specified string.</returns>
    public static StringBuilder Reverse(
        this StringBuilder builder
        )
    {
        Guard.Instance().ThrowIfNull(builder, nameof(builder));

        for (int x = 0; x < builder.Length / 2; x++)
        {
            var temp = builder[builder.Length - x - 1];
            builder[builder.Length - x - 1] = builder[x];
            builder[x] = temp;
        }

        return builder;
    }

    #endregion
}