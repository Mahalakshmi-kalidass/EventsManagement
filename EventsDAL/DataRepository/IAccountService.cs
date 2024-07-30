using EventsDAL.Models;
using EventsDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public interface IAccountService
    {
        bool RegisterUser(Register user);
        User AuthenticateUser(Login user);

        IEnumerable<User> GetAllUsers();

        bool SetUserRole(Guid userId, Role role);
    }
}
