namespace WorkerModels.Security;
public class ResultToken 
{
    public string access_token { get; set; } = "";
    public string token_type { get; set;} = "Bearer";
    public int expires_in { get; set; } = 0;
}