using WorkerModels.Model;

namespace WorkerModels.Requests;

public class WorkerAddRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CorporateEmail { get; set; }
    public string WorkerNumber { get; set; }
    public string PasswordHash { get; set; }
    public string[]? Phones{ get; set; } = null;
    public int? LeaderName { get; set; }

    public static explicit operator Worker(WorkerAddRequest workerRequest)
    {
        return new Worker(){
            FirstName = workerRequest.FirstName,
            LastName = workerRequest.LastName,
            CorporateEmail = workerRequest.CorporateEmail,
            PasswordHash = workerRequest.PasswordHash,
            WorkerNumber = workerRequest.WorkerNumber,
            LeaderName = workerRequest.LeaderName,  
        };
    }
}
