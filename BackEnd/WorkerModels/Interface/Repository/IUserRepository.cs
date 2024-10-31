using WorkerModels.Model;
using WorkerModels.Requests;

namespace WorkerModels.Interface.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByEmail(string email);
        Task<User> Add(UserRegisterRequest user);
    }
}
