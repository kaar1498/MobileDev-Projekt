using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MobileDev_Projekt.Entities;
using Polly;
using RestSharp;
using RestSharp.Authenticators;

namespace MobileDev_Projekt.Services
{
  public class FastCastleApi
  {
    private const string BaseUrl = "https://fast-castle-50377.herokuapp.com";
    readonly IRestClient _client;

    public FastCastleApi()
    {
      _client = new RestClient(BaseUrl);
    }

    public async Task<bool> Login(string email, string password)
    {
      var request = new RestRequest("/app-users");
      request.AddParameter("email", email.ToLower());
      var response = await Execute<IEnumerable<AppUser>>(request);
      var appUsers = response as AppUser[] ?? response.ToArray();
      App.AppUser = appUsers.FirstOrDefault();
      return appUsers.Any();
    }

    public async Task<bool> Register(string name, string username, string password,
      string email, string address, int phoneNumber)
    {
      await Task.Delay(3000);
      return true;
    }

    public async Task<IEnumerable<ExercisePlan>> GetExercisePlans(int appUserId)
    {
      var request = new RestRequest("/Exercise-Plans");
      request.AddParameter("app_users.id", appUserId);
      return await Execute<IEnumerable<ExercisePlan>>(request);
    }

    private async Task<string> Authenticate()
    {
      const string json = @"{""identifier"": ""student"",""password"": ""Student1234"" }";

      var request = new RestRequest("/auth/local");
      request.AddJsonBody(json);

      var response = await _client.PostAsync<Auth>(request);
      return response.jwt;
    }

    private async Task<T> Execute<T>(IRestRequest request)
    {
      var policy = Policy.HandleResult<IRestResponse<T>>(r => r.StatusCode == HttpStatusCode.Forbidden)
        .RetryAsync(async (_, _) => { _client.Authenticator = new JwtAuthenticator(await Authenticate()); });

      var response = await policy.ExecuteAsync(async () => await _client.ExecuteAsync<T>(request));

      return response.Data;
    }
  }
}