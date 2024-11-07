using Core;
using Core.Interfaces;
using BCrypt.Net;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> CreateUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return await _userRepository.CreateUser(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user is null)
            {
                return false;
            }

            return await _userRepository.DeleteUser(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<bool> UpdateUser(User user)
        {
            if (await _userRepository.GetUserById(user.Id) == null)
            {
                return false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return await _userRepository.UpdateUser(user);
        }

        public async Task<String> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return GenerateJwtToken(user);
            }

            return null;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var cards = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30), // no se si sera bueno cerrar cada media hora
                signingCredentials: cards
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}