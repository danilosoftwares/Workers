namespace WorkerModels.Model;

public class WorkerItemResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CorporateEmail { get; set; }
    public string WorkerNumber { get; set; }
    public int? LeaderName { get; set; }
    public string PasswordHash { get; set; }
    public string[]? Phones { get; set; }

    public static explicit operator WorkerItemResponse(Worker worker)
    {
        return new WorkerItemResponse(){
            Id = worker.Id,
            FirstName = worker.FirstName,
            LastName = worker.LastName,
            CorporateEmail = worker.CorporateEmail,
            WorkerNumber = worker.WorkerNumber,
            LeaderName = worker.LeaderName,
            PasswordHash = worker.PasswordHash,
        };
    }
}
