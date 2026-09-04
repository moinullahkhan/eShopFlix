using AuthService.Application.DTOs;
using AuthService.Domain.Entities;
using Mapster;

namespace AuthService.Application.Mappings
{
    public class AuthRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserDTO>()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.Roles, src => src.Roles.Select(r => r.Name).ToArray());

            config.NewConfig<SignUpDTO, User>().TwoWays();
        }
    }
}
