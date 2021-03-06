using System;
using System.Collections.Generic;
using System.Linq;
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
using Container = Microsoft.Azure.Cosmos.Container;

namespace MobileDev.FunctionApp.Features.Exercise
{
  public class Get : AuthenticationFilter
  {
    private static readonly string EndpointUri = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosEndPointUri");
    private static readonly string PrimaryKey = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosPrimaryKey");

    private static CosmosClient? _cosmosClient;
    private static Container? _container;
    private const string DatabaseId = "MobileDev";
    private const string ContainerId = "Exersices";
    
    [FunctionName("Exercise_Get")]
    public static async Task<IActionResult> RunAsync(
      [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.Exercise.Get)]
      HttpRequest req,
      Guid id,
      ILogger log)
    {
      _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions());
      _container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
      
      if (!Auth.IsValid)
      {
        return new UnauthorizedResult();
      }

      var result = await GetAsync(id.ToString());
      
      return new OkObjectResult(result?.Adapt<GetExerciseResponse>());
    }
    
    private static async Task<Core.Entities.Exercise?> GetAsync(string id)
    {
      var sqlQueryText = $"SELECT * FROM c WHERE c.id = '{id}' AND c.partitionKey = '{Auth.Username}'";
      var queryDefinition = new QueryDefinition(sqlQueryText);
      var queryResultSetIterator = _container?.GetItemQueryIterator<Core.Entities.Exercise>(queryDefinition);

      var users = new List<Core.Entities.Exercise>();

      while (queryResultSetIterator is {HasMoreResults: true})
      {
        var currentResultSet = await queryResultSetIterator.ReadNextAsync();
        users.AddRange(currentResultSet);
      }

      return users.FirstOrDefault();
    }
    
    public class GetExerciseResponse
    {
      public Guid Id { get; set; }
      public string? Name { get; set; }
      public List<GetImageResponse>? Images { get; set; }
      public int? Duration { get; set; }
      public int? RestFrequency { get; set; }
      public int? RestDuration { get; set; }
      public int? Repetitions { get; set; }

      public class GetImageResponse
      {
        public byte[]? Bytes { get; set; }
        public string? Description { get; set; }
      }
    }
    
  }
}