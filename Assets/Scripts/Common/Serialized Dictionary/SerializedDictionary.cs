using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ninito.UsualSuspects
{
    /// <summary>
    ///     A dictionary serializable by Unity
    /// </summary>
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        #region Private Fields

        [SerializeField]
        private List<KeyValuePair> keyValueList = new List<KeyValuePair>();

        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        #pragma warning disable 0414

        [SerializeField, HideInInspector]
        private bool keyCollision;

        #pragma warning restore 0414

        #endregion

        #region IDictionary Implementation

        public int Count => dictionary.Count;

        public bool IsReadOnly { get; set; }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        /// <summary>
        ///     Adds a key value pair to the dictionary
        /// </summary>
        /// <param name="item">The key value pair to add to the dictionary</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        ///     Clears the dictionary and other internal collections
        /// </summary>
        public void Clear()
        {
            // Clears the internal dictionary, index key dictionary and serialized list
            dictionary.Clear();
            keyValueList.Clear();
        }

        /// <summary>
        ///     Checks if the dictionary contains a specific value
        /// </summary>
        /// <param name="item">The value to search for</param>
        /// <returns>Whether the value exists</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            // If the value exists
            return dictionary.TryGetValue(item.Key, out TValue value) &&
                   EqualityComparer<TValue>.Default.Equals(value, item.Value);
        }


        /// <summary>
        ///     Copies the dictionary to an array
        /// </summary>
        /// <param name="array">The array to copy to</param>
        /// <param name="arrayIndex">The index to begin copying into</param>
        /// <exception cref="ArgumentException">
        ///     The array cannot be null and must have an equal
        ///     number of elements
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">The starting index cannot be negative</exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentException("The array cannot be null");
            }

            if (array.Length - arrayIndex < dictionary.Count)
            {
                throw new ArgumentException("The destination array has fewer elements than" +
                                            " the collection.");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex),
                    "The starting array index " +
                    "cannot be negative.");
            }

            foreach (var keyValuePair in dictionary)
            {
                array[arrayIndex] = keyValuePair;
                arrayIndex++;
            }
        }

        /// <summary>
        ///     Removes a KeyValuePair from the dictionary and other internal collections
        /// </summary>
        /// <param name="item">The KeyValuePair to remove</param>
        /// <returns>Whether it successfully removed the KeyValuePair</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!dictionary.TryGetValue(item.Key, out TValue value)) return false;

            bool valueMatch = EqualityComparer<TValue>.Default.Equals(value, item.Value);

            return valueMatch && Remove(item.Key);
        }

        /// <summary>
        ///     Adds a value to the dictionary
        /// </summary>
        /// <param name="key">The key to add the value to</param>
        /// <param name="value">The value to add</param>
        public void Add(TKey key, TValue value)
        {
            // Adds value to dictionary
            dictionary.Add(key, value);

            // Adds value to serialized list
            keyValueList.Add(new KeyValuePair(key, value));
        }

        /// <summary>
        ///     Whether the dictionary contains a given key
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <returns>Whether the key exists in the Dictionary</returns>
        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return dictionary.Remove(key);
        }

        /// <summary>
        ///     Attempts to get a value from the dictionary
        /// </summary>
        /// <param name="key">The key to get the value from</param>
        /// <param name="value">The variable to assign the value to, if it is found</param>
        /// <returns>Whether a value was found</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        ///     Sets and gets values
        /// </summary>
        /// <param name="key">The key to set to a given value</param>
        public TValue this[TKey key]
        {
            get => dictionary[key];
            set
            {
                // Sets value
                dictionary[key] = value;

                keyValueList.Add(new KeyValuePair(key, value));
            }
        }

        public ICollection<TKey> Keys => dictionary.Keys;
        public ICollection<TValue> Values => dictionary.Values;

        #endregion

        #region ISerializationCallbackReceiver Implementation

        /// <summary>
        ///     <para>Implement this method to receive a callback before Unity serializes your object.</para>
        /// </summary>
        public void OnBeforeSerialize()
        {
        }

        /// <summary>
        ///     <para>Implement this method to receive a callback after Unity deserializes your object.</para>
        /// </summary>
        public void OnAfterDeserialize()
        {
            dictionary.Clear();
            keyCollision = false;

            for (int index = 0, upper = keyValueList.Count; index < upper; index++)
            {
                // Adds key and value if no key collision is detected
                if (keyValueList[index].key != null && !ContainsKey(keyValueList[index].key))
                {
                    dictionary.Add(keyValueList[index].key, keyValueList[index].value);
                }
                else
                {
                    keyCollision = true;
                }
            }
        }

        #endregion

        #region KeyValuePair Struct

        /// <summary>
        ///     A non-generic Key-Value pair struct serializable by Unity
        /// </summary>
        [Serializable]
        private struct KeyValuePair
        {
            #region Public Fields

            [SerializeField]
            public TKey key;

            [SerializeField]
            public TValue value;

            #endregion

            #region Constructor

            /// <summary>
            ///     Creates a new serialized Key Value Pair
            /// </summary>
            /// <param name="key">The key of the pair</param>
            /// <param name="value">The key of the value</param>
            public KeyValuePair(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }

            #endregion
        }

        #endregion
    }
}