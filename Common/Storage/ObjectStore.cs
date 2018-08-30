/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Text;
using Newtonsoft.Json;
using QuantConnect.Interfaces;

namespace QuantConnect.Storage
{
    /// <summary>
    /// Helper class for easier access to <see cref="IObjectStore"/> methods
    /// </summary>
    public static class ObjectStore
    {
        /// <summary>
        /// Returns the object data as a string for the specified key
        /// </summary>
        public static string ReadAsString(this IObjectStore store, string key, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;

            return encoding.GetString(store.Read(key));
        }

        /// <summary>
        /// Returns the JSON deserialized object data for the specified key
        /// </summary>
        public static T ReadAsJson<T>(this IObjectStore store, string key)
        {
            var json = store.ReadAsString(key);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Returns the XML deserialized object data for the specified key
        /// </summary>
        public static T ReadAsXml<T>(this IObjectStore store, string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the object data in JSON format for the specified key
        /// </summary>
        public static bool SaveJson<T>(this IObjectStore store, string key, T obj, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;

            var json = JsonConvert.SerializeObject(obj);
            return store.Save(key, encoding.GetBytes(json));
        }

        /// <summary>
        /// Saves the object data in XML format for the specified key
        /// </summary>
        public static bool SaveXml<T>(this IObjectStore store, string key, T obj)
        {
            throw new NotImplementedException();
        }
    }
}
