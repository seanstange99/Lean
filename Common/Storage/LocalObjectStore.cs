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
using System.IO;
using QuantConnect.Interfaces;
using QuantConnect.Logging;
using QuantConnect.Packets;

namespace QuantConnect.Storage
{
    /// <summary>
    /// A local disk implementation of <see cref="IObjectStore"/>.
    /// </summary>
    public class LocalObjectStore : IObjectStore
    {
        private const string RootPath = "./storage";

        /// <summary>
        /// Static constructor
        /// </summary>
        static LocalObjectStore()
        {
            Directory.CreateDirectory(RootPath);
        }

        /// <summary>
        /// Initializes the object store
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="projectId">The project id</param>
        /// <param name="userToken">The user token</param>
        /// <param name="controls">The job controls instance</param>
        public void Initialize(int userId, int projectId, string userToken, Controls controls)
        {
        }

        /// <summary>
        /// Determines whether the store contains data for the specified key
        /// </summary>
        /// <param name="key">The object key</param>
        /// <returns>True if the key was found</returns>
        public bool ContainsKey(string key)
        {
            var fileName = GetFilePath(key);

            return File.Exists(fileName);
        }

        /// <summary>
        /// Returns the object data for the specified key
        /// </summary>
        /// <param name="key">The object key</param>
        /// <returns>A byte array containing the data</returns>
        public byte[] Read(string key)
        {
            var fileName = GetFilePath(key);

            try
            {
                return File.ReadAllBytes(fileName);
            }
            catch (Exception exception)
            {
                Log.Trace($"Error reading file: [{key}] - {exception}");
                return null;
            }
        }

        /// <summary>
        /// Saves the object data for the specified key
        /// </summary>
        /// <param name="key">The object key</param>
        /// <param name="contents">The object data</param>
        /// <returns>True if the save operation was successful</returns>
        public bool Save(string key, byte[] contents)
        {
            var fileName = GetFilePath(key);

            try
            {
                File.WriteAllBytes(fileName, contents);
            }
            catch (Exception exception)
            {
                Log.Trace($"Error saving file: [{key}] - {exception}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes the object data for the specified key
        /// </summary>
        /// <param name="key">The object key</param>
        /// <returns>True if the delete operation was successful</returns>
        public bool Delete(string key)
        {
            var fileName = GetFilePath(key);

            try
            {
                File.Delete(fileName);
            }
            catch (Exception exception)
            {
                Log.Trace($"Error deleting file: [{key}] - {exception}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns the file path for the specified key
        /// </summary>
        /// <param name="key">The object key</param>
        /// <returns>The path for the file</returns>
        public string GetFilePath(string key)
        {
            return Path.Combine(RootPath, $"{key.ToMD5()}.dat");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
