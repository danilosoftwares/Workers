using WorkerModels.Model;
using WorkerModels.Security;

namespace WorkerModels.Response;

public class UserLoginResponse
{
    public bool Status { get; set; }
    public string? Message { get; set; }
    public UserDTO? User { get; set; }
    public ResultToken? Token { get; set; }
}