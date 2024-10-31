namespace WorkerModels.Interfaces.Security;
using WorkerModels.Model;
using WorkerModels.Security;

public interface ITokenService
{
    ResultToken CreateJwtToken(UserDTO user);
}