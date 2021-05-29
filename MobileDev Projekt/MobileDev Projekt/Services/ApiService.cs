using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using RestSharp;
using RestSharp.Authenticators;
using Xamarin.Forms;

namespace MobileDev_Projekt.Services
{
  public class ApiService
  {
    //private const string BaseUrl = "http://10.0.2.2:7071/api/v1";
    private const string BaseUrl = "https://mobiledevfunctionapp.azurewebsites.net/api/v1";
    private readonly IRestClient _client;
    
    public ApiService()
    {
      _client = new RestClient(BaseUrl);
    }

    public void AuthenticateClient(string jwt, string hostKey)
    {
      _client.Authenticator = new JwtAuthenticator(jwt);
      _client.AddDefaultHeader("x-functions-key", hostKey);
    }

    public async Task<T> ExecuteAsync<T>(IRestRequest request)
    {
      var policy = Policy.Handle<HttpRequestException>()
        .OrResult<IRestResponse<T>>(restResponse => restResponse.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout)
        .RetryAsync(3);

      try
      {
        var response = await policy.ExecuteAsync(() => _client.ExecuteAsync<T>(request));
        return response.Data;
      }
      catch (Exception e)
      {
        DependencyService.Get<IMessage>().LongAlert(e.Message);
        return default;
      }
    } 
  }
}