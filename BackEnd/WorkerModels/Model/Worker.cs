namespace WorkerModels.Model;

public class Worker
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CorporateEmail { get; set; }
    public string? WorkerNumber { get; set; }
    public int? LeaderName { get; set; }
    public string? PasswordHash { get; set; }
}
