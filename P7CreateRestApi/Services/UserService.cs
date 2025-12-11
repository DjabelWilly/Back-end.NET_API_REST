using AutoMapper;
using P7CreateRestApi.Application.ViewModels;
using P7CreateRestApi.Data.Repositories;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _repository.GetAll();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<User> CreateUser(UserViewModel vm)
        {
            var entity = _mapper.Map<User>(vm);
            return await _repository.Add(entity);
        }

        public async Task<User?> UpdateUser(int id, UserViewModel vm)
        {
            var existing = await _repository.GetById(id);
            if (existing == null)
                return null;

            // vm → entity existante
            _mapper.Map(vm, existing);
            return await _repository.Update(existing);
        }

        public async Task DeleteUser(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null)
                throw new KeyNotFoundException($"User {id} introuvable.");

            await _repository.Delete(entity);
        }
    }
}