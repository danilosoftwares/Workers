namespace WorkerModels.Requests;

public class UserLoginRequest
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}
