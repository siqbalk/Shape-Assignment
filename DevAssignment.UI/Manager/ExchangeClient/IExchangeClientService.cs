using DevAssignment.UI.Models.ExchangeClient;
using DevAssignment.UI.Models.User;

namespace DevAssignment.UI.Manager.ExchangeClient;

public interface IExchangeClientService
{
    Task<HttpResponseMessage> GetAsync(string url);
    Task<HttpResponseMessage> PostAsync(string url, ByteArrayContent content = null);
    Task<T> ParseResponseAsync<T>(HttpResponseMessage httpResponseMessage) where T : new();
    Task<ExchangeClientResponse<ResponseModel>> RegisterUserAsync(RegisterUserModel registerUser);
    Task<ExchangeClientResponse<ResponseModel>> IsEmailExistAsync(string email);

}
