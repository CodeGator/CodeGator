
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace System.Collections.Generic;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// This class contains extension methods related to the <see cref="IList{T}"/>
/// type.
/// </summary>
public static partial class ListExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method removes a range of elements from a <see cref="IList{T}"/> list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list to use for the operation.</param>
    /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
    /// <param name="count">The number of elements to remove.</param>
    /// <remarks>
    /// <para>The intent, with this method, is to quickly remove a range
    /// of elements from a list.</para>
    /// </remarks>
    public static void RemoveRange<T>(
        this IList<T> list,
        int index,
        int count
        )
    {
        Guard.Instance().ThrowIfNull(list, nameof(list));

        (list as List<T>)?.RemoveRange(index, count);
    }

    // *******************************************************************

    /// <summary>
    ///  This method adds the elements of the specified collection to the 
    ///  end of the <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list to use for the operation.</param>
    /// <param name="collection">The collection whose elements should be 
    /// added to the end of the <see cref="IEnumerable{T}"/>
    /// The collection itself cannot be null, but it can contain elements 
    /// that are null, if type T is a reference type.</param>
    /// <remarks>
    /// <para>The intent, with this method, is to quickly add a range
    /// of elements from one list, into another.</para>
    /// </remarks>
    public static void AddRange<T>(
        this IList<T> list,
        IEnumerable<T> collection
        )
    {
        Guard.Instance().ThrowIfNull(list, nameof(list))
            .ThrowIfNull(collection, nameof(collection));

        (list as List<T>)?.AddRange(collection);
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
    /// <para>The intent, with this method, is to recursively enumerate through
    /// the items in a list, without setting up the standard .NET foreach loop. 
    /// Any errors are collected and thrown as a single <see cref="AggregateException"/> 
    /// object.</para>
    /// <para>This approach is not good for all looping scenarios - such as 
    /// breaking from a loop early, or returning values from a loop, etc. So
    /// use your best judgment when deciding to loop this way.</para>
    /// </remarks>
    /// <example>
    /// This example shows how to call the <see cref="ListExtensions.ForEach{T}(IList{T}, Func{T, IEnumerable{T}}, Action{T})"/>
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
        this IList<T> sequence,
        Func<T, IEnumerable<T>> selector,
        Action<T> action
        )
    {
        Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
            .ThrowIfNull(action, nameof(action));

        sequence.AsEnumerable().ForEach(selector, action);
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
    /// the items in a list, without setting up the standard .NET foreach loop. 
    /// Any errors are collected and thrown as a single <see cref="AggregateException"/> 
    /// object.</para>
    /// <para>This approach is not good for all looping scenarios - such as 
    /// breaking from a loop early, or returning values from a loop, etc. So
    /// use your best judgment when deciding to loop this way.</para>
    /// </remarks>
    /// <example>
    /// This example shows how to call the <see cref="ListExtensions.ForEach{T}(IList{T}, Action{T})"/>
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
        this IList<T> sequence,
        Action<T> action
        )
    {
        Guard.Instance().ThrowIfNull(sequence, nameof(sequence))
            .ThrowIfNull(action, nameof(action));

        sequence.AsEnumerable().ForEach(action);
    }

    // *******************************************************************

    /// <summary>
    /// This method randomly shuffles the contents of the given list.
    /// </summary>
    /// <typeparam name="T">The type of associated element.</typeparam>
    /// <param name="list">The list to be shuffled.</param>
    /// <returns>The shuffled list.</returns>
    public static List<T> Shuffle<T>(
        this List<T> list
        )
    {
        Guard.Instance().ThrowIfNull(list, nameof(list));

        using var rnd = RandomNumberGenerator.Create();

        return list.Shuffle(rnd);
    }

    // *******************************************************************

    /// <summary>
    /// This method randomly shuffles the contents of the given list.
    /// </summary>
    /// <typeparam name="T">The type of associated element.</typeparam>
    /// <param name="list">The list to be shuffled.</param>
    /// <param name="rnd">A random number generator.</param>
    /// <returns>The shuffled list.</returns>
    public static List<T> Shuffle<T>(
        this List<T> list,
        RandomNumberGenerator rnd
        )
    {
        Guard.Instance().ThrowIfNull(list, nameof(list))
            .ThrowIfNull(rnd, nameof(rnd));

        var count = list.Count;

        if (count == 0)
        {
            return list;
        }

        for (var x = 0; x < count; x++)
        {
            var index = rnd.Next(0, count - 1);
            var item = list[x];
            
            list[x] = list[index];
            list[index] = item;
        }

        return list;
    }

    #endregion
}
