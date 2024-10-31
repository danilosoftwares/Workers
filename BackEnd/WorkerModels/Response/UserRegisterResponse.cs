using WorkerModels.Model;

namespace WorkerModels.Response;

public class UserRegisterResponse
{
    public bool Status { get; set; }
    public string? Message { get; set; }
    public UserResponse? Content { get; set; }
}