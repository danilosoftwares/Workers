namespace WorkerApi.Routes;

using Microsoft.AspNetCore.Builder;
using WorkerModels.Interface.Services;
using WorkerModels.Model;
using WorkerModels.Requests;
using WorkerModels.Response;

public static class WorkersRoutes
{
    public static void MapWorkersRoutes(this WebApplication app)
    {
        var workers = app.MapGroup("/workers").WithTags("Workers");
        
        workers.MapGet("/all", async (IWorkerService service) =>
        {
            var result = await service.GetAllWorkers();
            return Results.Ok(result);
        })
        .RequireAuthorization()
        .WithName("GetAllWorkers")
        .Produces<List<WorkerItemFromAllResponse>>(StatusCodes.Status200OK)
        .WithDescription("Busca todos os registros de trabalhadores do banco de dados");

        workers.MapGet("/{id:int}", async (IWorkerService service, int id) =>
        {
            var employee = await service.GetWorkerById(id);
            return employee is not null ? Results.Ok(employee) : Results.NoContent();
        })
        .RequireAuthorization()
        .WithName("GetWorkerById")
        .Produces<WorkerItemResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent)
        .WithDescription("Busca trabalhador especifico no banco de dados");

        workers.MapPost("/", async (IWorkerService service, WorkerAddRequest work) =>
        {
            var result = await service.AddWorker(work);
            return result.Status ? Results.Ok(result) : Results.BadRequest(result);
        })
        .RequireAuthorization()
        .WithName("PostWorker")
        .Produces<WorkerEditResponse>(StatusCodes.Status200OK)
        .Produces<WorkerEditResponse>(StatusCodes.Status400BadRequest)
        .WithDescription("Insere trabalhador especifico no banco de dados");

        workers.MapPut("/{id:int}", async (IWorkerService service, int? id, WorkerEditRequest worker) =>
        {
            if (id==null) return Results.BadRequest("Id do trabalhador obrigatorio");
            var result = await service.EditWorker((int)id, worker);
            return result.Status ? Results.Ok(result) : Results.BadRequest(result);
        })
        .RequireAuthorization()
        .WithName("PutWorker")
        .Produces<WorkerEditResponse>(StatusCodes.Status200OK)
        .Produces<WorkerEditResponse>(StatusCodes.Status400BadRequest)
        .WithDescription("Insere trabalhador especifico no banco de dados");

        workers.MapDelete("/{id:int}", async (IWorkerService service, int id) =>
        {
            var result = await service.RemoveWorker(id);
            return result.Status ? Results.Ok(result) : Results.BadRequest(result);
        })
        .RequireAuthorization()
        .WithName("DeleteWorker")
        .Produces<WorkerRemoveResponse>(StatusCodes.Status200OK)
        .Produces<WorkerRemoveResponse>(StatusCodes.Status400BadRequest)
        .WithDescription("Remove trabalhador especifico no banco de dados");
    }
}