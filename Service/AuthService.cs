﻿using EstoqueApi.Model;
using EstoqueApi.Model.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EstoqueApi.Interface.Repository;
using EstoqueApi.Interface.Service;

namespace EstoqueApi.Service {
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) {
            _userRepository = userRepository;

            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<List<ApplicationUser>> ListUsers() {
            List<ApplicationUser> listUsers = await _userRepository.ListUsers();

            return listUsers;
        }

        public async Task<ApplicationUser> GetUserById(string userId) {
            ApplicationUser user = await _userRepository.GetUser(userId);

            if (user == null)
                throw new ArgumentException("Usuário não existe!");

            return user;
        }

        public async Task<int> UpdateUser(ApplicationUser user) {
            ApplicationUser findUser = await _userRepository.GetUser(user.Id);
            if (findUser == null)
                throw new ArgumentException("Usuário não encontrado");

            findUser.Email = user.Email;
            findUser.UserName = user.UserName;

            return await _userRepository.UpdateUser(findUser);
        }

        public async Task<bool> DeleteUser(string userId) {
            ApplicationUser findUser = await _userRepository.GetUser(userId);
            if (findUser == null)
                throw new ArgumentException("Usuário não encontrado");

            await _userRepository.DeleteUser(userId);

            return true;
        }

        public async Task<bool> SignUp(SignUpDTO signUpDTO) {
            ApplicationUser? userExists = await _userManager.FindByNameAsync(signUpDTO.Username);
            if (userExists != null)
                throw new ArgumentException("Username already exists!");

            userExists = await _userManager.FindByEmailAsync(signUpDTO.Email);
            if (userExists != null)
                throw new ArgumentException("Email already exists!");

            ApplicationUser user;

            user = new ApplicationUser() {
                Email = signUpDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = signUpDTO.Username
            };

            IdentityResult? result = await _userManager.CreateAsync(user, signUpDTO.Password);

            if(result.Errors.All(x => 
            x.Code == "PasswordRequiresNonAlphanumeric" || 
            x.Code == "PasswordRequiresDigit" || 
            x.Code == "PasswordRequiresUpper")) throw new ArgumentException("Senha deve conter pelo menos um numero, umaletra e um caracter especial.");

            if (!result.Succeeded)
                throw new ArgumentException("Cadastro do usuário falhou.");

            return true;
        }

        public async Task<SsoDTO> SignIn(SignInDTO signInDTO) {
            var user = await _userManager.FindByNameAsync(signInDTO.Username);
            if (user == null)
                throw new ArgumentException("Usuário não encontrado.");

            if (!await _userManager.CheckPasswordAsync(user, signInDTO.Password))
                throw new ArgumentException("Senha inválida.");

            IList<string>? userRoles = await _userManager.GetRolesAsync(user);

            List<Claim> authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles) {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new SsoDTO(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }

        public async Task<ApplicationUser> GetCurrentUser() {
            string? userId = _userManager.GetUserId(_httpContextAccessor.HttpContext!.User); // Get user id:

            ApplicationUser user = await _userRepository.GetUser(userId);

            return user;
        }
    }
}