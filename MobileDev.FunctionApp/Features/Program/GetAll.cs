using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MobileDev.FunctionApp.Core;
using MobileDev.FunctionApp.Core.FunctionInvocationFilters;
using MobileDev.FunctionApp.Core.Helpers;

namespace MobileDev.FunctionApp.Features.Program
{
  public class GetAll : AuthenticationFilter
  {
    private static readonly string EndpointUri = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosEndPointUri");
    private static readonly string PrimaryKey = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosPrimaryKey");

    private static CosmosClient? _cosmosClient;
    private static Container? _container;
    private const string DatabaseId = "MobileDev";
    private const string ContainerId = "Programs";
    
    [FunctionName("Program_GetAll")]
    public static async Task<IActionResult> RunAsync(
      [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.Program.GetAll)]
      HttpRequest req, ILogger log)
    {
      _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions());
      _container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
      
      if (!Auth.IsValid)
      {
        return new UnauthorizedResult();
      }
      
      var result =await GeAllAsync();
      
      return new OkObjectResult(result.Adapt<List<GetAllProgramResponse>>());
    }
    
    private static async Task<List<Core.Entities.Program>> GeAllAsync()
    {
      var sqlQueryText = $"SELECT * FROM c WHERE c.partitionKey = '{Auth.Username}'";
      var queryDefinition = new QueryDefinition(sqlQueryText);
      var queryResultSetIterator = _container?.GetItemQueryIterator<Core.Entities.Program>(queryDefinition);

      var users = new List<Core.Entities.Program>();

      while (queryResultSetIterator is {HasMoreResults: true})
      {
        var currentResultSet = await queryResultSetIterator.ReadNextAsync();
        users.AddRange(currentResultSet);
      }

      return users;
    }
    
    public class GetAllProgramResponse
    {
      public Guid Id { get; set; }
      public string? Name { get; set; }
      public List<GetAllExerciseResponse>? Exercises { get; set; }
      public class GetAllExerciseResponse
      {
        public string? Name { get; set; }
        public List<GetAllImageResponse>? Images { get; set; }
        public int? Duration { get; set; }
        public int? RestFrequency { get; set; }
        public int? RestDuration { get; set; }
        public int? Repetitions { get; set; }
        public class GetAllImageResponse
        {
          public byte[]? Bytes { get; set; }
          public string? Description { get; set; }
        }
      }
    }
  }
}