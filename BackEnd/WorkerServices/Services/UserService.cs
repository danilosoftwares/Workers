namespace WorkerServices.Services;

using WorkerModels.Interface.Repository;
using WorkerModels.Interface.Services;
using WorkerModels.Model;
using WorkerModels.Requests;
using WorkerModels.Response;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<UserRegisterResponse?> AddUser(UserRegisterRequest user)
    {
        var exists = await _repository.GetByEmail(user.Email);
        if (exists != null)
        {
            return new UserRegisterResponse()
            {
                Status = false,
                Message = "email já existe",
            };
        }
        var result = await _repository.Add(user);
        return new UserRegisterResponse()
        {
            Status = true,
            Content = (UserResponse)result,
        };
    }

    public async Task<UserLoginResponse?> Login(UserLoginRequest user)
    {
        var exists = await _repository.GetByEmail(user.Email);
        if ((exists == null) || (user.PasswordHash != exists.PasswordHash))
        {
            return new UserLoginResponse()
            {
                Status = false,
                Message = "email ou senha inválido",
            };
        }
        return new UserLoginResponse()
        {
            Status = true,
            User = (UserDTO)exists,
        };
    }

}
