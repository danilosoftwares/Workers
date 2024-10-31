using WorkerModels.Model;

namespace WorkerModels.Requests;

public class UserRegisterRequest
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public static explicit operator User(UserRegisterRequest workerRequest)
    {
        return new User(){
            Email = workerRequest.Email,
            PasswordHash = workerRequest.PasswordHash,
        };
    }
}
