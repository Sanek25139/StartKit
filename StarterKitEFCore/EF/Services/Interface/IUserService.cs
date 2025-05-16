using StarterKit.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.EF.Services.Interface
{
    public interface IUserService<T> where T : BaseEntity, IUser
    {
        event Action<T?> OnChangeUser;
        T? GetUser();

        Task<bool> SignIn(string login, string password);
        void SignOut();
    }
}
