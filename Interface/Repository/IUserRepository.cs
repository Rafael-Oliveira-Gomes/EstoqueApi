using EstoqueApi.Model;

namespace EstoqueApi.Interface.Repository;

public interface IUserRepository
{
    Task<List<ApplicationUser>> ListUsers();
    Task<ApplicationUser> GetUser(string userId);
    Task<ApplicationUser> CreateUser(ApplicationUser user);
    Task<int> UpdateUser(ApplicationUser user);
    Task<bool> DeleteUser(string userId);
}