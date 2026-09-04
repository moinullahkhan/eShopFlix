using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MapsterMapper;
using BC = BCrypt.Net.BCrypt;

namespace AuthService.Application.Services
{
    public class UserAppService : IUserAppService 
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserAppService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDTO LoginUser(LoginDTO loginDTO)
        {
            User user = _userRepository.GetUserByEmail(loginDTO.Email);

            if (user != null)
            {
                bool isPasswordValid = BC.Verify(loginDTO.Password, user.Password);
                if (isPasswordValid)
                {
                    UserDTO model = _mapper.Map<UserDTO>(user);
                   
                    return model;
                }
            }
            return null;
        }

        public bool SignUpUser(SignUpDTO signUpDTO, string Role)
        {
            User existingUser = _userRepository.GetUserByEmail(signUpDTO.Email);
            if (existingUser != null)
            {
                return false; // User with the same email already exists
            }
            // Hash the password before storing
            string hashedPassword = BC.HashPassword(signUpDTO.Password);
            signUpDTO.Password = hashedPassword;
            User newUser = _mapper.Map<User>(signUpDTO);
            bool isCreated = _userRepository.RegisterUser(newUser, Role);
            return isCreated;
        }
    }
}
