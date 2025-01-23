using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary_Perf
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// This is the method to add the value to the dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TValue? GetOrAdd<TKey, TValue>(
            this Dictionary<TKey, TValue> dict, TKey key, TValue? value)
            where TKey: notnull
        {
            ref var val = ref CollectionsMarshal.GetValueRefOrAddDefault(dict, key, out var exists);
            if (exists)
            {
                return val;
            }
            val = value;
            return val;
        }

        /// <summary>
        /// Updating a value in the dictionary using Key
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryUpdate<TKey, TValue>(
            this Dictionary<TKey, TValue> dict, TKey key, TValue value)
            where TKey: notnull
        {
            ref var val = ref CollectionsMarshal.GetValueRefOrNullRef(dict, key);
            if (Unsafe.IsNullRef(ref val))
            {
                return false;
            }

            val = value;
            return true;
        }
    }
}
