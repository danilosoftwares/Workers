using WorkerModels.Model;

namespace WorkerModels.Response;

public class UserResponse
{
    public int Id { get; set; }
    public string Email { get; set; }

    public static explicit operator UserResponse(User user)
    {
        return new UserResponse(){
            Id = user.Id,
            Email = user.Email,
        };
    }
}