using DevAssignment.UI.Constants;
using DevAssignment.UI.Models.ExchangeClient;
using DevAssignment.UI.Models.User;
using eKYC.Common.Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net;
using System.Text;

namespace DevAssignment.UI.Manager.ExchangeClient;

public class ExchangeClientService
    : IExchangeClientService
{
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackBar;
    private readonly IConfiguration _configuration;
    private readonly NavigationManager _navigationManager;

    public ExchangeClientService(
        IHttpClientFactory httpClientFactory,
        ISnackbar snackBar,
        NavigationManager navigationManager,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _snackBar = snackBar;
        _navigationManager = navigationManager;
        _httpClient = httpClientFactory.CreateClient(nameof(ExchangeClientService));
        _httpClient.BaseAddress = new Uri(configuration.GetValue<string>(AppSettings.BASE_URL_LOCAL));
    }


    private async Task<HttpResponseMessage> PatchAsync(string url, StringContent content)
    {
        return await Sendsync(HttpMethod.Patch, url, content).ConfigureAwait(false);
    }

    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        return await Sendsync(HttpMethod.Get, url).ConfigureAwait(false);
    }

    public async Task<HttpResponseMessage> PostAsync(string url, ByteArrayContent content = null)
    {
        return await Sendsync(HttpMethod.Post, url, content).ConfigureAwait(false);
    }

    private async Task<HttpResponseMessage> Sendsync(HttpMethod method, string requestPath, ByteArrayContent content = null)
    {
        try
        {
            var request = new HttpRequestMessage(method, requestPath);
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            return response;
        }
        catch
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("Something went wrong!")
            };
        }
    }

    public async Task<T> ParseResponseAsync<T>(HttpResponseMessage httpResponseMessage)
        where T : new()
    {
        var responseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonHelper.FromJSONString<T>(responseContent);
    }

    public async Task<ExchangeClientResponse<ResponseModel>> RegisterUserAsync(RegisterUserModel   registerUser)
    {
        var content = new StringContent(JsonHelper.ToJSONString(registerUser), Encoding.UTF8, "application/json");
        var responseMessage = await PostAsync(Routes.UserEndpoints.Post, content).ConfigureAwait(false);
        var response = await ParseResponseAsync<ResponseModel>(responseMessage).ConfigureAwait(false);

        return responseMessage.IsSuccessStatusCode
           ? new ExchangeClientResponse<ResponseModel> { IsSucceeded = true, Data = response }
           : new ExchangeClientResponse<ResponseModel> { IsSucceeded = false }; ;
    }

    public async Task<bool> IsEmailExistAsync(string email)
    {     
        var responseMessage = await GetAsync(string.Format(Routes.UserEndpoints.IsEmailExist, email)).ConfigureAwait(false);  

         return bool.Parse(await responseMessage.Content.ReadAsStringAsync());
    }

}
