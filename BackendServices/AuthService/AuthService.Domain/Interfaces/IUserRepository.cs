using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces
{
    public interface IUserRepository
    {
        bool RegisterUser(User user, string role);
        User GetUserByEmail(string email);
    }
}
