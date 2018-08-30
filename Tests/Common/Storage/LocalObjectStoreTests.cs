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

using NUnit.Framework;
using QuantConnect.Storage;

namespace QuantConnect.Tests.Common.Storage
{
    [TestFixture]
    public class LocalObjectStoreTests
    {
        private readonly LocalObjectStore _store = new LocalObjectStore();

        [TestCase("my_key", "./storage/9ed6e46a3ff88783ff75296a4ba523f9.dat")]
        [TestCase("test/123", "./storage/0a2557f6be73a1b8a6abe84104899591.dat")]
        public void GetFilePathReturnsFileName(string key, string expectedPath)
        {
            Assert.AreEqual(expectedPath, _store.GetFilePath(key).Replace("\\", "/"));
        }

        [Test]
        public void SavesAndLoadsJson()
        {
            var obj = new TestSettings { EmaFastPeriod = 12, EmaSlowPeriod = 26 };

            Assert.IsTrue(_store.SaveJson("my_settings_json", obj));

            var obj2 = _store.ReadAsJson<TestSettings>("my_settings_json");

            Assert.AreEqual(obj.EmaFastPeriod, obj2.EmaFastPeriod);
            Assert.AreEqual(obj.EmaSlowPeriod, obj2.EmaSlowPeriod);
        }

        private class TestSettings
        {
            public int EmaFastPeriod { get; set; }
            public int EmaSlowPeriod { get; set; }
        }
    }
}
