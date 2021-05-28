using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using MobileDev.FunctionApp.Core;
using MobileDev.FunctionApp.Core.Helpers;
using User = MobileDev.FunctionApp.Core.Entities.User;

namespace MobileDev.FunctionApp.Authentication
{
  public static class Register
  {
    private static readonly string EndpointUri = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosEndPointUri");
    private static readonly string PrimaryKey = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosPrimaryKey");

    private static CosmosClient _cosmosClient;
    private static Container _container;
    private const string DatabaseId = "MobileDev";
    private const string ContainerId = "Users";

    [FunctionName("Register")]
    public static async Task<IActionResult> RunAsync(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = Routes.Authentication.Register)]
      RegisterRequest request,
      HttpRequest req, 
      ILogger log)
    {
      _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions());
      _container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
      
      if (await UserExistsAsync(request.Username)) return new ConflictObjectResult("User with username already exists");
      
      try
      {
        var newUser = request.Adapt<User>();
        newUser.Id = Guid.NewGuid();
        newUser.PartitionKey = request.Username;
        newUser.PasswordHash = PasswordHelper.GetPasswordHash(request.Password);
        await _container.CreateItemAsync(newUser, new PartitionKey(newUser.PartitionKey));
      }
      catch (Exception e)
      {
        return new ConflictObjectResult(e.Message);
      }
      
      return new OkObjectResult(TokenIssuer.IssueTokenForUser(request.Username));
    }

    private static async Task<bool> UserExistsAsync(string username)
    {
      var sqlQueryText = $"SELECT c.username FROM c WHERE c.username = '{username}'";
      var queryDefinition = new QueryDefinition(sqlQueryText);
      var queryResultSetIterator = _container.GetItemQueryIterator<User>(queryDefinition);

      var users = new List<User>();

      while (queryResultSetIterator.HasMoreResults)
      {
        var currentResultSet = await queryResultSetIterator.ReadNextAsync();
        users.AddRange(currentResultSet);
      }

      return users.Any();
    }
    
    public class RegisterRequest
    {
      public string Name { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }
      public string Email { get; set; }
      public string Address { get; set; }
      public int PhoneNumber { get; set; }
    }
  }
}