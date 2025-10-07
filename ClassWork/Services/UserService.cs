using ClassWork.Abstractions;
using ClassWork.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassWork.Services
{
    public class UserService : IUserService
    {
        private readonly IAppDbContext _context;

        public UserService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user)
        {
            var createdUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return createdUser.Entity;
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.EmailAddress == email);
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChangesAsync();
        }
    }
}
