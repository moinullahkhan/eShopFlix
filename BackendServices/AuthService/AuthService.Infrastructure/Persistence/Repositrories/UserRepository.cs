using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositrories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthServiceContext _db;
        public UserRepository(AuthServiceContext db)
        {
            _db = db;
        }
        public User GetUserByEmail(string email)
        {
            return _db.Users.Include(u => u.Roles).Where(u => u.Email == email).FirstOrDefault();
        }

        public bool RegisterUser(User user, string role)
        {
            Role existingRole = _db.Roles.Where(r => r.Name == role).FirstOrDefault();
            if (existingRole == null)
            {
                return false;
            }

            user.Roles.Add(existingRole);
            _db.Users.Add(user);
            _db.SaveChanges();
            return true;
        }
    }
}
