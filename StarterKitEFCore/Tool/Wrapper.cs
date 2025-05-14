using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.Tool
{
    public class Wrapper<T>
    {
        public T? Value { get; set; }
        public bool IsNull => Value == null;
        public bool IsNotNull => !IsNull;

        public override bool Equals(object? obj)
        {
            if(Value == null) return false;
            if(obj == null) return false;

            if(obj is Wrapper<T> w)
                return Value.Equals(w.Value);
            if(obj is T v)
                return Value.Equals(v);
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
