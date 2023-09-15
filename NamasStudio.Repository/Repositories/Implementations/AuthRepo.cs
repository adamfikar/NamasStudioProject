using Microsoft.EntityFrameworkCore;
using NamasStudio.DataAccess.Models;
using NamasStudio.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Repository.Repositories.Implementations
{
    internal class AuthRepo : IAuthRepo
    {
        private NamasStudioContext _dbContext;

        public AuthRepo(NamasStudioContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User CreateUser(User user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public User FindUserById(string username)
        {
            var entity = _dbContext.Users
                .Include(c => c.Role)
                .FirstOrDefault(c => c.Username == username)
            ?? throw new ArgumentException("User not found!!");
            return entity!;
        }
    }
}
