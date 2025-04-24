using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.EF.Model
{
    public interface IUser
    {
        string Login { get; set; }
        string Password {  get; set; }
    }
}
