using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Data.Repositories
{
    public interface IUserRepository
    {
        User FindByUserName(string username);
        Task<List<User>> FindAll();
        void Add(User user);
        User FindById(int id);
        void Update(User user);
        void Delete(User user);
    }
}
