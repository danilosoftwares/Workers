namespace WorkerModels.Interface.Services;
using WorkerModels.Requests;
using WorkerModels.Response;

public interface IUserService
{
    Task<UserRegisterResponse?> AddUser(UserRegisterRequest user);
    Task<UserLoginResponse?> Login(UserLoginRequest user);
}
