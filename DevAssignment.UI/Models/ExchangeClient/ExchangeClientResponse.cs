namespace DevAssignment.UI.Models.ExchangeClient;

public class ExchangeClientResponse<T>
{
    public T Data { get; set; }
    public bool IsSucceeded { get; set; }
    public string Message { get; set; }
}
