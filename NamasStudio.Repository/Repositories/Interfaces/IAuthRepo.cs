using NamasStudio.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Repository.Repositories.Interfaces
{
    public interface IAuthRepo
    {
        User CreateUser(User user);
        User FindUserById(string username);
    }
}
