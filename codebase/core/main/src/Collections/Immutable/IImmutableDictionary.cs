using System.Collections.Generic;

namespace Axle.Collections.Immutable
{
    /// <summary>
    /// Represents an immutable collection of key/value pairs. 
    /// </summary>
    /// <typeparam name="TKey">
    /// The type of the keys in the dictionary.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// The type of the values in the dictionary.
    /// </typeparam>
    #if NETSTANDARD || UNITY_2018_1_OR_NEWER
    public interface IImmutableDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    #else
    public interface IImmutableDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    #endif
    {
        /// <summary>Retrieves an empty dictionary that has the same ordering and key/value comparison rules as this dictionary instance.</summary>
        /// <returns>An empty dictionary with equivalent ordering and key/value comparison rules.</returns>
        IImmutableDictionary<TKey, TValue> Clear();

        /// <summary>Adds an element with the specified key and value to the dictionary.</summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <exception cref="T:System.ArgumentException">The given key already exists in the dictionary but has a different value.</exception>
        /// <returns>A new immutable dictionary that contains the additional key/value pair.</returns>
        IImmutableDictionary<TKey, TValue> Add(TKey key, TValue value);

        /// <summary>Adds the specified key/value pairs to the dictionary.</summary>
        /// <param name="pairs">The key/value pairs to add.</param>
        /// <exception cref="T:System.ArgumentException">One of the given keys already exists in the dictionary but has a different value.</exception>
        /// <returns>A new immutable dictionary that contains the additional key/value pairs.</returns>
        IImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);

        /// <summary>Sets the specified key and value in the immutable dictionary, possibly overwriting an existing value for the key.</summary>
        /// <param name="key">The key of the entry to add.</param>
        /// <param name="value">The key value to set.</param>
        /// <returns>A new immutable dictionary that contains the specified key/value pair.</returns>
        IImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value);

        /// <summary>Sets the specified key/value pairs in the immutable dictionary, possibly overwriting existing values for the keys.</summary>
        /// <param name="items">The key/value pairs to set in the dictionary. If any of the keys already exist in the dictionary, this method will overwrite their previous values.</param>
        /// <returns>A new immutable dictionary that contains the specified key/value pairs.</returns>
        IImmutableDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);

        /// <summary>Removes the elements with the specified keys from the immutable dictionary.</summary>
        /// <param name="keys">The keys of the elements to remove.</param>
        /// <returns>A new immutable dictionary with the specified keys removed; or this instance if the specified keys cannot be found in the dictionary.</returns>
        IImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

        /// <summary>Removes the element with the specified key from the immutable dictionary.</summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>A new immutable dictionary with the specified element removed; or this instance if the specified key cannot be found in the dictionary.</returns>
        IImmutableDictionary<TKey, TValue> Remove(TKey key);

        #if NETSTANDARD || (UNITY_2018_1_OR_NEWER && !UNITY_WEBGL)
        /// <summary>Determines whether the immutable dictionary contains the specified key/value pair.</summary>
        /// <param name="pair">The key/value pair to locate.</param>
        /// <returns>
        /// <see langword="true" /> if the specified key/value pair is found in the dictionary; otherwise, <see langword="false" />.</returns>
        bool Contains(KeyValuePair<TKey, TValue> pair);

        /// <summary>Determines whether this dictionary contains a specified key.</summary>
        /// <param name="equalKey">The key to search for.</param>
        /// <param name="actualKey">The matching key located in the dictionary if found, or <c>equalkey</c> if no match is found.</param>
        /// <returns>
        /// <see langword="true" /> if a match for <paramref name="equalKey" /> is found; otherwise, <see langword="false" />.</returns>
        bool TryGetKey(TKey equalKey, out TKey actualKey);
        #endif
            
        /// <summary>
        /// Gets the key comparer for the immutable dictionary.
        /// </summary>
        IEqualityComparer<TKey> KeyComparer { get; }
    }
}