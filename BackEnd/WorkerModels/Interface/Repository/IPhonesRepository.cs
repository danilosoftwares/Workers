using WorkerModels.Model;
using WorkerModels.Requests;

namespace WorkerModels.Interface.Repository
{
    public interface IPhonesRepository
    {
        Task<List<Phones>> GetAll(int idWorker);
        Task Add(Phones[] phones);
        Task Delete(int idWorker);
    }
}
