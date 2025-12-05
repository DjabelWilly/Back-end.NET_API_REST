using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LocalDbContext _context;

        public UserRepository(LocalDbContext context)
        {
            _context = context;
        }

        // Gestion de User == null
        public User FindByUserName(string username)
        {
            var user = _context.Users
                                 .FirstOrDefault(u => u.Username == username);

            if (user == null)
                throw new InvalidOperationException("User not found.");

            return user;
        }

        public async Task<List<User>> FindAll()
        {
            return await _context.Users.ToListAsync();
        }

        // Methode implementée
        public void Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        // Gestion de User == null
        public User FindById(int id)
        {
            var user = _context.Users
                               .FirstOrDefault(u => u.Id == id);

            if (user == null)
                throw new InvalidOperationException("User not found.");

            return user;
        }

        // Ajout Modifier user
        public void Update(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // Récupère l'utilisateur existant en base
            var userToUpdate = FindById(user.Id);

            // Mettre à jour
            userToUpdate.Username = user.Username;

            _context.SaveChanges();
        }

        // Ajout Suppression user
        public void Delete(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

           var userToDelete = FindById(user.Id);
            _context.Remove(userToDelete);
            _context.SaveChanges();
        }
    }
}