using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Ninito.UsualSuspects.Tests.SerializedDictionary
{
    public class SerializedDictionaryTestSuite
    {
        private SerializedDictionary<string, int> _serializedDictionary;

        [SetUp]
        public void Setup_SerializedDictionary()
        {
            _serializedDictionary = new SerializedDictionary<string, int>();
        }

        [Test]
        public void Count_OfDictionaryWithOneEntry_ShouldBeOne()
        {
            _serializedDictionary.Add("key", 1);
            
            _serializedDictionary.Count.Should().Be(1);
        }

        [Test]
        public void Add_NewKeyToDictionary_ShouldAddKey()
        {
            _serializedDictionary.Add("key", 0);
            
            _serializedDictionary.Should().Contain("key", 0);
        }
        
        [Test]
        public void Remove_ExistingKeyFromDictionary_ShouldRemoveKey()
        {
            _serializedDictionary.Add("key", 0);
            _serializedDictionary.Remove("key");
            
            _serializedDictionary.Should().NotContain("key", 0);
        }
        
        [Test]
        public void Contains_AnExistingKey_ShouldReturnTrue()
        {
            _serializedDictionary.Add("key", 0);
            
            _serializedDictionary.ContainsKey("key").Should().BeTrue();
        }

        [Test]
        public void TryGetValue_OfExistingKey_ShouldOutputValue()
        {
            _serializedDictionary.Add("key", 1);
            
            _serializedDictionary.TryGetValue("key", out int number);
            
            number.Should().Be(1);
        }
        
        [Test]
        public void GetValue_OfExistingKey_ShouldOutputValue()
        {
            _serializedDictionary.Add("key", 0);
            
            _serializedDictionary["key"].Should().Be(0);
        }
        
        [Test]
        public void GetValue_OfNonExistentKey_ShouldThrowException()
        {
            Action invalidRetrieval = () => { int value = _serializedDictionary["key"]; };

            invalidRetrieval.Should().Throw<KeyNotFoundException>();
        }
        
        [Test]
        public void Clear_DictionaryWithItems_ShouldRemoveAllItems()
        {
            _serializedDictionary.Add("key", 1);
            
            _serializedDictionary.Clear();

            _serializedDictionary.Count.Should().Be(0);
        }

        [Test]
        public void CopyTo_Array_ShouldGenerateArrayWithAllItems()
        {
            var array = new KeyValuePair<string, int>[1];

            _serializedDictionary.Add("key", 1);
            
            _serializedDictionary.CopyTo(array, 0);

            array.Should().NotBeEmpty();
        }
    }
}