using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.DataBase
{
    public class UsersManager : IUsersManager
    {
        private ApplicationDbContext _context;

        public UsersManager(ApplicationDbContext context) => _context = context;





    }
}
