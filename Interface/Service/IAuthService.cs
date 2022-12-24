using EstoqueApi.Model;
using EstoqueApi.Model.Dto;

namespace EstoqueApi.Interface.Service;

public interface IAuthService
{
    Task<List<ApplicationUser>> ListUsers();
    Task<ApplicationUser> GetUserById(string userId);
    Task<int> UpdateUser(ApplicationUser user);
    Task<bool> DeleteUser(string userId);
    Task<bool> SignUp(SignUpDTO signUpDTO);
    Task<SsoDTO> SignIn(SignInDTO signInDTO);
    Task<ApplicationUser> GetCurrentUser();
}