using Microsoft.EntityFrameworkCore;
using WorkerModels.Interface.Repository;
using WorkerModels.Model;
using WorkerModels.Requests;
using WorkerRepositories.Data;

namespace WorkerRepositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<User?> GetByEmail(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(w => w.Email == email);
            return result;
        }

        public async Task<User> Add(UserRegisterRequest user)
        {
            var userAdded = await _context.Users.AddAsync((User)user);
            await _context.SaveChangesAsync();
            return userAdded.Entity;
        }
    }
}
