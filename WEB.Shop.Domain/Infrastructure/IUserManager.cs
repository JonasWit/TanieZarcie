using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface IUserManager
    {
        Task CreateManagerUser(string userName, string password);
    }
}
