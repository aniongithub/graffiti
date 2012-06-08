#region License and Copyright Notice
// Copyright (c) 2010 Ananth Balasubramaniam
// All rights reserved.
// 
// The contents of this file are made available under the terms of the
// Eclipse Public License v1.0 (the "License") which accompanies this
// distribution, and is available at the following URL:
// http://www.opensource.org/licenses/eclipse-1.0.php
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either expressed or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// By using this software in any fashion, you are agreeing to be bound by the
// terms of the License.
#endregion

using System.Collections.Generic;

namespace Graffiti.Core.Extensions
{
    internal static class ConversionExtensions
    {
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            var result = new Dictionary<TKey, TValue>();
            foreach (var kvp in items)
                result.Add(kvp.Key, kvp.Value);

            return result;
        }

        public static IDictionary<TKey, IList<TValue>> ToMultiValueDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            var result = new Dictionary<TKey, IList<TValue>>();
            foreach (var kvp in items)
            {
                if (!result.ContainsKey(kvp.Key))
                    result[kvp.Key] = new List<TValue>();
                result[kvp.Key].Add(kvp.Value);
            }

            return result;
        }
    }
}