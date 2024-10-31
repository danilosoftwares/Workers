namespace WorkerModels.Interface.Services;

using System.Collections.Generic;
using WorkerModels.Model;
using WorkerModels.Requests;
using WorkerModels.Response;

public interface IWorkerService
{
    Task<List<WorkerItemFromAllResponse>> GetAllWorkers();
    Task<WorkerItemResponse?> GetWorkerById(int id);
    Task<WorkerEditResponse?> AddWorker(WorkerAddRequest worker);
    Task<WorkerEditResponse> EditWorker(int id, WorkerEditRequest worker);
    Task<WorkerRemoveResponse> RemoveWorker(int id);
}
