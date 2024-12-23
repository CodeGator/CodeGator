
// I found this idea here: https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings

#pragma warning disable IDE0130
namespace System.Security.Cryptography;
#pragma warning restore IDE0130

/// <summary>
/// This class contains extension methods related to the <see cref="RandomNumberGenerator"/>
/// type.
/// </summary>
public static partial class RandomNumberGeneratorExtensions
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains mixed case alphanumeric characters for generating strings.
    /// </summary>
    private static readonly char[] _allChars =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    /// <summary>
    /// This field contains lower case alpha characters for generating strings.
    /// </summary>
    private static readonly char[] _lowerChars =
        "abcdefghijklmnopqrstuvwxyz".ToCharArray();

    /// <summary>
    /// This field contains upper case alpha characters for generating strings.
    /// </summary>
    private static readonly char[] _upperChars =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    /// <summary>
    /// This field contains numeric characters for generating strings.
    /// </summary>
    private static readonly char[] _numChars = "1234567890".ToCharArray();

    /// <summary>
    /// This field contains symbol characters for generating strings.
    /// </summary>
    private static readonly char[] _symbChars = 
        "~!@#$%^&*()[];:<>,.-=_+".ToCharArray();

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method generates a string of random alphanumeric characters.
    /// </summary>
    /// <param name="random">The random number generator to use for the 
    /// operation.</param>
    /// <param name="size">The number of characters to include in the 
    /// string.</param>
    /// <returns>A random alphanumeric string.</returns>
    /// <example>
    /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.NextString(RandomNumberGenerator, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         var random = RandomNumberGenerator.Create();
    ///         var str = random.NextString(10);
    /// 
    ///         // str contains a 10 character string.
    ///     }
    /// }
    /// </code>
    /// </example>
    public static string NextString(
        this RandomNumberGenerator random,
        int size
        )
    {
        byte[] data = new byte[sizeof(int) * size];
        random.GetBytes(data);

        var sb = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * sizeof(int));
            var idx = rnd % _allChars.Length;

            sb.Append(_allChars[idx]);
        }

        var result = sb.ToString();
        return result;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns a random integer who's value has been constrained
    /// to be within the range specified by the <paramref name="min"/> and
    /// <paramref name="max"/> parameters.
    /// </summary>
    /// <param name="random">The random number generator to use for the 
    /// operation.</param>
    /// <param name="min">The lower value for the range.</param>
    /// <param name="max">The upper value for the range.</param>
    /// <returns>A randon integer who's value has been constrained
    /// to be within the range specified by the <paramref name="min"/> and
    /// <paramref name="max"/> parameters.</returns>
    /// <example>
    /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.Next(RandomNumberGenerator, int, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         var random = RandomNumberGenerator.Create();
    ///         var num = random.Next(1, 10);
    /// 
    ///         // num contains a number between 1 and 10.
    ///     }
    /// }
    /// </code>
    /// </example>
    public static int Next(
        this RandomNumberGenerator random,
        int min,
        int max
        )
    {
        max--;

        var bytes = new byte[sizeof(int)];
        random.GetNonZeroBytes(bytes);

        var val = BitConverter.ToInt32(bytes);

        var result = ((val - min) % (max - min + 1) + (max - min + 1)) %
            (max - min + 1) + min;

        return result;

    }

    // *******************************************************************

    /// <summary>
    /// This method returns a random integer value.
    /// </summary>
    /// <param name="random">The random number generator to use for the 
    /// operation.</param>
    /// <returns>A random integer value</returns>
    /// <example>
    /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.Next(RandomNumberGenerator)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         var random = RandomNumberGenerator.Create();
    ///         var num = random.Next();
    /// 
    ///         // num contains a number.
    ///     }
    /// }
    /// </code>
    /// </example>
    public static int Next(
        this RandomNumberGenerator random
        )
    {
        var bytes = new byte[sizeof(int)];
        random.GetNonZeroBytes(bytes);

        var result = BitConverter.ToInt32(bytes);

        return result;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns a random numeric value.
    /// </summary>
    /// <param name="random">The random number generator to use for the 
    /// operation.</param>
    /// <param name="size">The number of characters to include in the 
    /// string.</param>
    /// <returns>A random numeric string.</returns>
    /// <example>
    /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.NextDigits(RandomNumberGenerator, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         var random = RandomNumberGenerator.Create();
    ///         var num = random.NextDigits(3);
    /// 
    ///         // num contains random numbers.
    ///     }
    /// }
    /// </code>
    /// </example>
    public static string NextDigits(
        this RandomNumberGenerator random,
        int size
        )
    {
        byte[] data = new byte[sizeof(int) * size];
        random.GetBytes(data);

        var sb = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * sizeof(int));
            var idx = rnd % _numChars.Length;

            sb.Append(_numChars[idx]);
        }

        var result = sb.ToString();
        return result;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns a string with random symbols.
    /// </summary>
    /// <param name="random">The random number generator to use for the 
    /// operation.</param>
    /// <param name="size">The number of symbols to include in the string.</param>
    /// <returns>A random symbolic string.</returns>
    /// <example>
    /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.NextSymbols(RandomNumberGenerator, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         var random = RandomNumberGenerator.Create();
    ///         var num = random.NextSymbols(3);
    /// 
    ///         // num contains random symbols.
    ///     }
    /// }
    /// </code>
    /// </example>
    public static string NextSymbols(
        this RandomNumberGenerator random,
        int size
        )
    {
        byte[] data = new byte[sizeof(int) * size];
        random.GetBytes(data);

        var sb = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * sizeof(int));
            var idx = rnd % _symbChars.Length;

            sb.Append(_symbChars[idx]);
        }

        var result = sb.ToString();
        return result;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns a string with upper case alpha characters.
    /// </summary>
    /// <param name="random">The random number generator to use for the 
    /// operation.</param>
    /// <param name="size">The number of characters to include in the string.</param>
    /// <returns>A random alpha string.</returns>
    /// <example>
    /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.NextUpper(RandomNumberGenerator, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         var random = RandomNumberGenerator.Create();
    ///         var num = random.NextUpper(3);
    /// 
    ///         // num contains random upper case alpha characters.
    ///     }
    /// }
    /// </code>
    /// </example>
    public static string NextUpper(
        this RandomNumberGenerator random,
        int size
        )
    {
        byte[] data = new byte[sizeof(int) * size];
        random.GetBytes(data);

        var sb = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * sizeof(int));
            var idx = rnd % _upperChars.Length;

            sb.Append(_upperChars[idx]);
        }

        var result = sb.ToString();
        return result;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns a string with lower case alpha characters.
    /// </summary>
    /// <param name="random">The random number generator to use for the 
    /// operation.</param>
    /// <param name="size">The number of characters to include in the string.</param>
    /// <returns>A random alpha string.</returns>
    /// <example>
    /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.NextLower(RandomNumberGenerator, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         var random = RandomNumberGenerator.Create();
    ///         var num = random.NextLower(3);
    /// 
    ///         // num contains random lower case alpha characters.
    ///     }
    /// }
    /// </code>
    /// </example>
    public static string NextLower(
        this RandomNumberGenerator random,
        int size
        )
    {
        byte[] data = new byte[sizeof(int) * size];
        random.GetBytes(data);

        var sb = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * sizeof(int));
            var idx = rnd % _lowerChars.Length;

            sb.Append(_lowerChars[idx]);
        }

        var result = sb.ToString();
        return result;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns a string with randomly shuffled characters.
    /// </summary>
    /// <param name="random">The random number generator to use for the 
    /// operation.</param>
    /// <param name="source">The string to be shuffled.</param>
    /// <returns>A shuffled string.</returns>
    /// <example>
    /// This example shows how to call the <see cref="RandomNumberGeneratorExtensions.Shuffle(RandomNumberGenerator, string)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         var random = RandomNumberGenerator.Create();
    ///         var str = random.Shuffle("this is a test");
    /// 
    ///         // str contains randomly shuffled characters.
    ///     }
    /// }
    /// </code>
    /// </example>
    public static string Shuffle(
        this RandomNumberGenerator random,
        string source
        )
    {
        // Copied from: https://rosettacode.org/wiki/Knuth_shuffle#C.23
        // Any flaws are probably mine.

        var sb = new StringBuilder(source);
        for (int i = 0; i < sb.Length; i++)
        {
            int j = random.Next(i, sb.Length);
            var temp = sb[i]; sb[i] = sb[j]; sb[j] = temp;
        }

        var result = sb.ToString();
        return result;
    }

    #endregion
}
