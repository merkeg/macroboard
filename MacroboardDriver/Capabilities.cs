using System;
using System.Collections.Generic;

namespace MacroboardDriver
{
    public class Capabilities<TKey> : Dictionary<TKey, object> where TKey : Enum
    {
        public T GetCapability<T>(TKey capability)
        {
            return (T) this[capability];
        }

        public void SetCapability<T>(TKey capability, T value)
        {
            this[capability] = value;
        }
    }
}