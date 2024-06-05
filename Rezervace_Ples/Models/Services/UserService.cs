using Microsoft.EntityFrameworkCore;
using Rezervace_Ples.Models.DBConnection;
using Rezervace_Ples.Models.TableObjects;

namespace Rezervace_Ples.Models.Services
{
    public class UserService
    {
        private MyContext context = new MyContext();


        public List<User> getUsers()
        {
            return context.Uzivatele.ToList();
        }

        public User Verify(User user)
        {
            foreach (var item in getUsers())
            {
                if (item.Heslo == user.Heslo && item.Prihlasovaci_Jmeno == user.Prihlasovaci_Jmeno)
                {
                    return item;
                }
            }
            return null;
        }

        public bool isAdmin(User user)
        {
            if (user.isAdmin)
            {
                return true;
            }
            return false;
        }
    }
}
