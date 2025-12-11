using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<User> CreateUser(UserViewModel vm);
        Task<User?> UpdateUser(int id, UserViewModel vm);
        Task DeleteUser(int id);
    }
}
