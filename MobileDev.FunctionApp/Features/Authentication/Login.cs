using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MobileDev.FunctionApp.Core;
using MobileDev.FunctionApp.Core.Helpers;
using User = MobileDev.FunctionApp.Core.Entities.User;

namespace MobileDev.FunctionApp.Features.Authentication
{
  public static class Login
  {
    private static readonly string EndpointUri = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosEndPointUri");
    private static readonly string PrimaryKey = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosPrimaryKey");

    private static CosmosClient? _cosmosClient;
    private static Container? _container;
    private const string DatabaseId = "MobileDev";
    private const string ContainerId = "Users";

    [FunctionName("Login")]
    public static async Task<IActionResult> RunAsync(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = Routes.Authentication.Login)]
      LoginRequest loginRequest,
      HttpRequest req, ILogger log)
    {
      _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions());
      _container = _cosmosClient.GetContainer(DatabaseId, ContainerId);

      if (!await UserExistsAsync(loginRequest.Username, loginRequest.Password))
      {
        return new UnauthorizedResult();
      }

      var token = TokenIssuer.IssueTokenForUser(loginRequest.Username);
      var hostKey = EnvironmentVariableHelper.GetEnvironmentVariable("HOST_KEY");
      
      return new OkObjectResult(new LoginResponse
      {
        Jwl = token,
        HostKey = hostKey
      });
    }

    private static async Task<bool> UserExistsAsync(string username, string password)
    {
      var sqlQueryText = $"SELECT * FROM c WHERE c.Username = '{username}'";
      var queryDefinition = new QueryDefinition(sqlQueryText);
      var queryResultSetIterator = _container.GetItemQueryIterator<User>(queryDefinition);

      var users = new List<User>();

      while (queryResultSetIterator.HasMoreResults)
      {
        var currentResultSet = await queryResultSetIterator.ReadNextAsync();
        users.AddRange(currentResultSet);
      }

      return users.Any() && PasswordHelper.VerifyPassword(password, users.FirstOrDefault()?.PasswordHash);
    }

    public class LoginRequest
    {
      public string Username { get; set; }

      public string Password { get; set; }
    }

    private class LoginResponse
    {
      public string Jwl { get; set; }
      public string HostKey { get; set; }
    }
  }
}