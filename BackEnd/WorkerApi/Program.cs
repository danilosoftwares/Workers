using WorkerApi.Routes;
using WorkerRepositories.Extensions;
using WorkerApi.Configurations;
using WorkerModels.Security;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRepositoryServices()
                .AddRepositories()
                .AddServices()
                .AddSwaggerService()
                .AddCrossOrigin()
                .AddJWTAuthentication(builder.Configuration);


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapWorkersRoutes();
app.MapUsersRoutes();   


app.Run();