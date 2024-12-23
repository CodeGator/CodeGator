
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Collections.Generic;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class contains extension methods related to the <see cref="IEnumerable{T}"/>
/// type.
/// </summary>
public static partial class EnumerableExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method creates a <see cref="Dictionary{TKey, TValue}"/> from 
    /// an <see cref="IEnumerable{T}"/> according to the specified key 
    /// selector and element selector functions.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <param name="enumerable">The enumerable to use for the operation.</param>
    /// <param name="syncKeySelector">The key selector.</param>
    /// <param name="asyncValueSelector">The value selector.</param>
    /// <returns>
    /// A task to perform the operation that returns a <see cref="Dictionary{TKey, TValue}"/>
    /// </returns>
    public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TInput, TKey, TValue>(
        this IEnumerable<TInput> enumerable,
        Func<TInput, TKey> syncKeySelector,
        Func<TInput, Task<TValue>> asyncValueSelector
        ) where TKey : notnull
    {
        var dictionary = new Dictionary<TKey, TValue>();
        foreach (var item in enumerable)
        {
            var key = syncKeySelector(item);
            var value = await asyncValueSelector(item);
            dictionary.Add(key, value);
        }
        return dictionary;
    }

    // *******************************************************************

    /// <summary>
    /// This method returns elements from a sequence that are distinct, on 
    /// the specified property.
    /// </summary>
    /// <typeparam name="T">The type associated with the sequences.</typeparam>
    /// <typeparam name="K">The type associated with the property.</typeparam>
    /// <param name="lhs">The left-hand queryable sequence.</param>
    /// <param name="rhs">The right-hand LINQ expression to select a property.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> sequence.</returns>
    public static IEnumerable<T> DistinctOn<T, K>(
        this IEnumerable<T> lhs,
        Func<T, K> rhs
        )
    {
        Guard.Instance().ThrowIfNull(lhs, nameof(lhs))
            .ThrowIfNull(rhs, nameof(rhs));

        return lhs.GroupBy(rhs)
            .Select(x => x.First())
            .AsEnumerable();
    }

    // *******************************************************************

    /// <summary>
    /// This method filters out elements based on the contents of a comma
    /// separated black list. 
    /// </summary>
    /// <typeparam name="T">The type associated with the sequence.</typeparam>
    /// <param name="sequence">The sequence to use for the operation.</param>
    /// <param name="selector">The selector to apply for the operation.</param>
    /// <param name="blackList">The comma separated black list to use for 
    /// the operation.</param>
    /// <returns>An enumerable sequence of <typeparamref name="T"/> items.</returns>
    /// <remarks>
    /// <para>The intent, with this method, is to quickly filter an enumerable
    /// sequence by applying a black list to a specific element. So, anything 
    /// in the sequence that matches the corresponding black list is dropped
    /// from the collection.</para>
    /// </remarks>
    public static IEnumerable<T> ApplyBlackList<T>(
        this IEnumerable<T> sequence,
        Func<T, string> selector,
        string blackList
        )
    {
        Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
            .ThrowIfNull(selector, nameof(selector));

        var blackParts = blackList.Split(',');

        var result = sequence.Where(
            x => !blackParts.Any(y => selector(x).IsMatch(y.Trim()))
            ).DistinctOn(z => z?.ToString() ?? "")
            .ToList();

        return result;
    }

    // *******************************************************************

    /// <summary>
    /// This method filters out elements based on the contents of a comma
    /// separated white list. 
    /// </summary>
    /// <typeparam name="T">The type associated with the sequence.</typeparam>
    /// <param name="sequence">The sequence to use for the operation.</param>
    /// <param name="selector">The selector to apply for the operation.</param>
    /// <param name="whiteList">The comma separated white list to use for 
    /// the operation.</param>
    /// <returns>An enumerable sequence of <typeparamref name="T"/> items.</returns>
    /// <remarks>
    /// <para>The intent, with this method, is to quickly filter an enumerable
    /// sequence by applying a white list to a specific element. So, anything 
    /// in the sequence that doesn't match the white list is dropped from the 
    /// collection.</para>
    /// </remarks>
    public static IEnumerable<T> ApplyWhiteList<T>(
        this IEnumerable<T> sequence,
        Func<T, string> selector,
        string whiteList
        )
    {
        Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
            .ThrowIfNull(selector, nameof(selector));

        var whiteParts = whiteList.Split(',');

        var results = sequence.Where(
            x => whiteParts.Any(y => selector(x).IsMatch(y.Trim()))
            ).DistinctOn(z => z?.ToString())
             .ToList();

        return results;
    }

    // *******************************************************************

    /// <summary>
    /// This method recursively iterates through an enumerable sequence
    /// applying the specified delegate action to each item.
    /// </summary>
    /// <typeparam name="T">The type of object in the sequence.</typeparam>
    /// <param name="sequence">The enumerable sequence to use for the operation.</param>
    /// <param name="selector">The selector for finding child sequences.</param>
    /// <param name="action">The delegate to apply to each item in the sequence.</param>
    /// <remarks>
    /// <para>The intent, with this method, is to quickly enumerate through
    /// the items in an enumerable sequence, without setting up the standard
    /// .NET foreach loop. Any errors are collected and thrown as a single
    /// <see cref="AggregateException"/> object.</para>
    /// <para>This approach is not good for all looping scenarios - such as 
    /// breaking from a loop early, or returning values from a loop, etc. So
    /// use your best judgment when deciding to loop this way.</para>
    /// </remarks>
    /// <example>
    /// This example shows how to call the <see cref="EnumerableExtensions.ForEach{T}(IEnumerable{T}, Func{T, IEnumerable{T}}, Action{T})"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     class TestType
    ///     {
    ///         public int Id { get; set; }
    ///         public TestType[] C {get; set; }
    ///     }
    ///
    ///     static void Main()
    ///     {
    ///         var list = new TestType[]
    ///         {
    ///             new TestType()
    ///             {
    ///                 Id = 0, 
    ///                 C = new TestType[]
    ///                 {
    ///                     new TestType
    ///                     {
    ///                         Id = 1,
    ///                         C = new TestType[0]
    ///                     }
    ///                 }
    ///            },
    ///            new TestType()
    ///            {
    ///                Id = 2,
    ///                C = new TestType[]
    ///                {
    ///                    new TestType
    ///                    {
    ///                        Id = 3,
    ///                        C = new TestType[0]
    ///                    }
    ///                }
    ///            }
    ///        };
    ///        
    ///        list.ForEach(x => x.C, y => 
    ///        { 
    ///            Console.Write($"{Id}");
    ///        });
    ///    }
    /// }
    /// </code>
    /// Prints: 0123
    /// </example>
    public static void ForEach<T>(
        this IEnumerable<T> sequence,
        Func<T, IEnumerable<T>> selector,
        Action<T> action
        )
    {
        Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
            .ThrowIfNull(selector, nameof(selector))
            .ThrowIfNull(action, nameof(action));

        var errors = new List<Exception>();

        foreach (var item in sequence)
        {
            try
            {
                selector(item).ForEach(
                    selector,
                    action
                    );

                action(item);
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }
        }

        if (errors.Count == 0)
        {
            throw new AggregateException(
                "Error while iterating over an enumerable sequence! " +
                "See any inner exception(s) for more details.",
                innerExceptions: errors
                );
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method iterates through an enumerable sequence applying the 
    /// specified delegate action to each item.
    /// </summary>
    /// <typeparam name="T">The type of object in the sequence.</typeparam>
    /// <param name="sequence">The enumerable sequence to use for the operation.</param>
    /// <param name="action">The delegate to apply to each item in the sequence.</param>
    /// <remarks>
    /// <para>The intent, with this method, is to quickly enumerate through
    /// the items in an enumerable sequence, without setting up the standard
    /// .NET foreach loop. Any errors are collected and thrown as a single
    /// <see cref="AggregateException"/> object.</para>
    /// <para>This approach is not good for all looping scenarios - such as 
    /// breaking from a loop early, or returning values from a loop, etc. So
    /// use your best judgment when deciding to loop this way.</para>
    /// </remarks>
    /// <example>
    /// This example shows how to call the <see cref="EnumerableExtensions.ForEach{T}(IEnumerable{T}, Action{T})"/>
    /// method.
    /// <code>
    /// class TestClass 
    /// {
    ///     static void Main()
    ///     {
    ///         var list = new int[]
    ///         {
    ///             0, 1, 2, 3
    ///         };
    ///         
    ///         list.ForEach(x => 
    ///         {
    ///             Console.Write(x);
    ///         });
    ///     }
    /// }
    /// </code>
    /// Prints: 0123
    /// </example>
    public static void ForEach<T>(
        this IEnumerable<T> sequence,
        Action<T> action
        )
    {
        Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
            .ThrowIfNull(action, nameof(action));

        var errors = new List<Exception>();

        foreach (var item in sequence)
        {
            try
            {
                action(item);
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }
        }

        if (errors.Count != 0)
        {
            throw new AggregateException(
                "Error while iterating over an enumerable sequence! " +
                "See any inner exception(s) for more details.",
                innerExceptions: errors
                );
        }
    }

    #endregion
}
