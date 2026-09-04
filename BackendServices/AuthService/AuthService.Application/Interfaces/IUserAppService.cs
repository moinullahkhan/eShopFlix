using AuthService.Application.DTOs;

namespace AuthService.Application.Interfaces
{
    public interface IUserAppService
    {
        UserDTO LoginUser(LoginDTO loginDTO);
        bool SignUpUser(SignUpDTO signUpDTO, string Role);
    }
}
