using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerModels.Interface.Repository;
using WorkerModels.Interface.Services;
using WorkerModels.Model;
using WorkerModels.Requests;
using WorkerModels.Response;
using WorkerServices.Services;

public class WorkerServiceTests
{
    private readonly Mock<IWorkerRepository> _repositoryMock;
    private readonly Mock<IPhonesRepository> _phonesRepositoryMock;
    private readonly Mock<ICryptography> _cryptographyMock; 
    private readonly IWorkerService _workerService;

    public WorkerServiceTests()
    {
        _repositoryMock = new Mock<IWorkerRepository>();
        _phonesRepositoryMock = new Mock<IPhonesRepository>();
        _cryptographyMock = new Mock<ICryptography>(); 
        _workerService = new WorkerService(_repositoryMock.Object, _phonesRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllWorkers_ShouldReturnWorkers_WhenWorkersExist()
    {
        var workers = new List<Worker>
        {
            new Worker { Id = 1, FirstName = "John", LastName = "Doe" },
            new Worker { Id = 2, FirstName = "Jane", LastName = "Doe" }
        };
        _repositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(workers);

        var result = await _workerService.GetAllWorkers();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].FirstName);
        Assert.Equal("Jane", result[1].FirstName);
    }

    [Fact]
    public async Task GetWorkerById_ShouldReturnWorker_WhenWorkerExists()
    {
        var worker = new Worker { Id = 1, FirstName = "John", LastName = "Doe", PasswordHash = "encryptedPassword" };
        _repositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(worker);
        
        _cryptographyMock.Setup(c => c.DecryptString("encryptedPassword")).Returns("password");
        _phonesRepositoryMock.Setup(repo => repo.GetAll(1)).ReturnsAsync(new List<Phones>());

        var result = await _workerService.GetWorkerById(1);

        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
    }

    [Fact]
    public async Task AddWorker_ShouldReturnWorkerEditResponse_WhenWorkerIsAddedSuccessfully()
    {
        var newWorkerRequest = new WorkerAddRequest { FirstName = "John", LastName = "Doe", CorporateEmail = "john@example.com", WorkerNumber = "12345", PasswordHash = "password" };
        var addedWorker = new Worker { Id = 1, FirstName = "John", LastName = "Doe", CorporateEmail = "john@example.com", WorkerNumber = "12345" };

        _repositoryMock.Setup(repo => repo.GetByEmailWorkNumber(newWorkerRequest.CorporateEmail, newWorkerRequest.WorkerNumber)).ReturnsAsync(new List<Worker>());
        _repositoryMock.Setup(repo => repo.Add(newWorkerRequest)).ReturnsAsync(addedWorker);
        
        _cryptographyMock.Setup(c => c.EncryptString("password")).Returns("encryptedPassword");
        _cryptographyMock.Setup(c => c.DecryptString("encryptedPassword")).Returns("password");
        _phonesRepositoryMock.Setup(repo => repo.Add(It.IsAny<Phones[]>())).Returns(Task.CompletedTask);

        var result = await _workerService.AddWorker(newWorkerRequest);

        Assert.NotNull(result);
        Assert.True(result.Status);
        Assert.Equal(addedWorker.FirstName, result.Content.FirstName);
    }

    [Fact]
    public async Task EditWorker_ShouldReturnWorkerEditResponse_WhenWorkerIsEditedSuccessfully()
    {
        var existingWorker = new Worker { Id = 1, FirstName = "John", LastName = "Doe" };
        var editWorkerRequest = new WorkerEditRequest { FirstName = "Jane", LastName = "Doe", PasswordHash = "newPassword" };

        _repositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(existingWorker);
        _repositoryMock.Setup(repo => repo.Edit(It.IsAny<Worker>())).ReturnsAsync(existingWorker);
        
        _cryptographyMock.Setup(c => c.EncryptString("newPassword")).Returns("encryptedNewPassword");
        _cryptographyMock.Setup(c => c.DecryptString("encryptedNewPassword")).Returns("newPassword");
        _phonesRepositoryMock.Setup(repo => repo.Delete(1)).Returns(Task.CompletedTask);
        _phonesRepositoryMock.Setup(repo => repo.Add(It.IsAny<Phones[]>())).Returns(Task.CompletedTask);

        var result = await _workerService.EditWorker(1, editWorkerRequest);

        Assert.NotNull(result);
        Assert.True(result.Status);
        Assert.Equal("Jane", result.Content.FirstName);
    }

    [Fact]
    public async Task RemoveWorker_ShouldReturnWorkerRemoveResponse_WhenWorkerIsRemovedSuccessfully()
    {
        var existingWorker = new Worker { Id = 1, FirstName = "John", LastName = "Doe" };
        _repositoryMock.Setup(repo => repo.Get(1)).ReturnsAsync(existingWorker);
        _repositoryMock.Setup(repo => repo.Delete(existingWorker)).ReturnsAsync(existingWorker);

        var result = await _workerService.RemoveWorker(1);

        Assert.True(result.Status);
        Assert.Equal("registro removido com sucesso", result.Message);
    }
}
