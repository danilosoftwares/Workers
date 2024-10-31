namespace WorkerApi.Routes;

using Microsoft.AspNetCore.Builder;
using WorkerModels.Interface.Services;
using WorkerModels.Interfaces.Security;
using WorkerModels.Requests;
using WorkerModels.Response;

public static class UsersRoutes
{
    public static void MapUsersRoutes(this WebApplication app)
    {
        var workers = app.MapGroup("/user").WithTags("Users");

        workers.MapPost("/register", async (IUserService service, UserRegisterRequest user) =>
        {
            return await service.AddUser(user);
        })
        .WithName("RegisterUser")
        .Produces<UserRegisterResponse>(StatusCodes.Status200OK)
        .WithDescription("Insere usuario");

        workers.MapPost("/login", async (IUserService service, ITokenService jwtGenerate, UserLoginRequest user) =>
        {
            var exists = await service.Login(user);
            if (exists.Status)
            {
                var result = jwtGenerate.CreateJwtToken(exists.User);
                exists.Token = result;
                return Results.Ok(exists);
            }
            return Results.BadRequest(exists);
        })
        .WithName("LoginUser")
        .Produces<UserLoginResponse>(StatusCodes.Status200OK)
        .WithDescription("Valida usuario e senha");

    }
}