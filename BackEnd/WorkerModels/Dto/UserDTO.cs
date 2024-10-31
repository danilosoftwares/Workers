namespace WorkerModels.Model;

public class UserDTO
{
    public int Id { get; set; }
    public string Email { get; set; }

    public static explicit operator UserDTO(User workerRequest)
    {
        return new UserDTO(){
            Id = workerRequest.Id,
            Email = workerRequest.Email,
        };
    }
}
