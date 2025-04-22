using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.EF.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LinkedEntityAttribute : Attribute
    {
        public Type? TargetType { get; } // Опционально: можно явно указать тип

        public LinkedEntityAttribute(Type? targetType = null)
        {
            TargetType = targetType;
        }
    }
}
