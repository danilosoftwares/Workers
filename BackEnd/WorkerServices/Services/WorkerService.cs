namespace WorkerServices.Services;

using System.Collections.Generic;
using WorkerModels.Interface.Repository;
using WorkerModels.Interface.Services;
using WorkerModels.Model;
using WorkerModels.Requests;
using WorkerModels.Response;

public class WorkerService : IWorkerService
{
    private readonly IWorkerRepository _repository;
    private readonly IPhonesRepository _phonesRepository;

    public WorkerService(IWorkerRepository repository, IPhonesRepository phonesRepository)
    {
        _repository = repository;
        _phonesRepository = phonesRepository;
    }

    public async Task<List<WorkerItemFromAllResponse>> GetAllWorkers()
    {
        var all = await _repository.GetAll();
        return all.Select(x => (WorkerItemFromAllResponse)x).ToList();
    }

    public async Task<WorkerItemResponse?> GetWorkerById(int id)
    {
        var item = await _repository.Get(id);
        if (item.PasswordHash != null && item.PasswordHash != "")
        {
            try
            {
                item.PasswordHash = Cryptography.DecryptString(item.PasswordHash);
            }
            catch (Exception e)
            {

            }
        }
        var result = (WorkerItemResponse)item;
        var all = await _phonesRepository.GetAll(id);
        result.Phones = all.Select(s=>s.PhoneNumber).ToArray();
        return item != null ? result : null;
    }

    public async Task<WorkerEditResponse?> AddWorker(WorkerAddRequest worker)
    {
        var exist = await _repository.GetByEmailWorkNumber(worker.CorporateEmail, worker.WorkerNumber);
        if (exist != null && exist.Count() > 0)
        {
            return new WorkerEditResponse()
            {
                Status = false,
                Message = "já existe funcionario com mesmo email ou numero de chapa",
            };
        }
        if (worker.LeaderName != null)
        {
            var lider = await _repository.Get((int)worker.LeaderName);
            if (lider == null)
            {
                return new WorkerEditResponse()
                {
                    Status = false,
                    Message = "lider informado nao existe na base de dados",
                };
            }
        }

        worker.PasswordHash = Cryptography.EncryptString(worker.PasswordHash);
        var result = await _repository.Add(worker);
        result.PasswordHash = Cryptography.DecryptString(result.PasswordHash);

        if (worker.Phones != null)
        {
            var phones = worker.Phones.Select(s => new Phones()
            {
                PhoneNumber = s,
                IdWorker = result.Id,
            });
            await _phonesRepository.Add(phones.ToArray());
        }

        return new WorkerEditResponse()
        {
            Status = true,
            Content = result,
        };
    }

    public async Task<WorkerEditResponse> EditWorker(int id, WorkerEditRequest worker)
    {
        var existingWorker = await _repository.Get(id);
        if (existingWorker == null)
        {
            return new WorkerEditResponse()
            {
                Status = false,
                Message = "id nao encontrado"
            };
        }
        if (worker.LeaderName != null)
        {
            var exist = await _repository.Get((int)worker.LeaderName);
            if (exist == null)
            {
                return new WorkerEditResponse()
                {
                    Status = false,
                    Message = "lider informado nao existe na base de dados",
                };
            }
        }

        var editWorker = (Worker)worker;
        editWorker.Id = id;
        editWorker.PasswordHash = Cryptography.EncryptString(worker.PasswordHash);
        await _repository.Edit(editWorker);
        editWorker.PasswordHash = Cryptography.DecryptString(editWorker.PasswordHash);

        if (worker.Phones != null)
        {
            await _phonesRepository.Delete(id);
            var phones = worker.Phones.Select(s => new Phones()
            {
                PhoneNumber = s,
                IdWorker = id,
            });
            await _phonesRepository.Add(phones.ToArray());
        }

        return new WorkerEditResponse()
        {
            Status = true,
            Content = editWorker,
        };
    }

    public async Task<WorkerRemoveResponse> RemoveWorker(int id)
    {
        var existingWorker = await _repository.Get(id);
        if (existingWorker == null)
        {
            return new WorkerRemoveResponse()
            {
                Status = false,
                Message = "id nao encontrado"
            };
        }

        await _repository.Delete(existingWorker);
        return new WorkerRemoveResponse()
        {
            Status = true,
            Message = "registro removido com sucesso"
        };
    }
}
