using ClassWork.Entities;

namespace ClassWork.Services
{
    public interface IUserService
    {
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task<User> Add(User user);
        void Remove(User user);
    }
}
