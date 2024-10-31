using WorkerModels.Model;
using WorkerModels.Requests;

namespace WorkerModels.Interface.Repository
{
    public interface IWorkerRepository
    {
        Task<List<Worker>> GetAll();
        Task<Worker> Get(int id);
        Task<Worker> Add(WorkerAddRequest work);
        Task<Worker> Edit(Worker work);
        Task<Worker> Delete(Worker work);
        Task<List<Worker>> GetByEmailWorkNumber(string email, string workNumber);
    }
}
