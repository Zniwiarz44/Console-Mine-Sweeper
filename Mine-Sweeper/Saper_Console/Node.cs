using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper_Console
{
    class Node
    {
        public int Value { get; set; }
        public bool Revealed { get; set; }

        public override bool Equals(object obj)
        {
            var val = obj as Node;
            if (object.ReferenceEquals(val, null))
                return false;
            return Value == val.Value; // OR this.Value == ((Node_.obj).Value
        }
        public override string ToString()
        {
            return string.Format("{0}", Value);
        }
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
