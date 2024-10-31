namespace WorkerModels.Model;

public class WorkerItemFromAllResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CorporateEmail { get; set; }
    public string WorkerNumber { get; set; }
    public int? LeaderName { get; set; }

    public static explicit operator WorkerItemFromAllResponse(Worker worker)
    {
        return new WorkerItemFromAllResponse(){
            Id = worker.Id,
            FirstName = worker.FirstName,
            LastName = worker.LastName,
            CorporateEmail = worker.CorporateEmail,
            WorkerNumber = worker.WorkerNumber,
            LeaderName = worker.LeaderName,
        };
    }
}
