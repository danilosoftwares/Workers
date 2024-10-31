using WorkerModels.Model;

namespace WorkerModels.Response;

public class WorkerEditResponse
{
    public bool Status { get; set; }
    public string? Message { get; set; }
    public Worker? Content { get; set; }
}