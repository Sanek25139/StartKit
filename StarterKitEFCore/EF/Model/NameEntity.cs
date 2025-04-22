using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.EF.Model
{
    public class NameEntity : BaseEntity
    {
        public string Name { get; set; } = null!;

        public override string ToString()
        {
            return Name;
        }
    }
}
