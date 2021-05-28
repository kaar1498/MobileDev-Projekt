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

namespace MobileDev.FunctionApp.Features.Exercise
{
  public class Update : AuthenticationFilter
  {
    private static readonly string EndpointUri = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosEndPointUri");
    private static readonly string PrimaryKey = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosPrimaryKey");

    private static CosmosClient? _cosmosClient;
    private static Container? _container;
    private const string DatabaseId = "MobileDev";
    private const string ContainerId = "Exersices";
    
    [FunctionName("Exercise_Update")]
    public static async Task<IActionResult> RunAsync(
      [HttpTrigger(AuthorizationLevel.Admin, "put", Route = Routes.Exercise.Update)]
      [FromBody] UpdateExerciseRequest programRequest,
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
      
      try
      {
        var newExersice = programRequest.Adapt<Core.Entities.Exercise>();
        newExersice.Id = id;
        newExersice.PartitionKey = Auth.Username;
        var result = await _container.UpsertItemAsync(newExersice, new PartitionKey(newExersice.PartitionKey));
        return new OkObjectResult(result.Resource.Adapt<UpdateExerciseResponse>());
      }
      catch (Exception e)
      {
        return new ConflictObjectResult(e.Message);
      }
    }
    
    public class UpdateExerciseRequest
    {
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
    
    public class UpdateExerciseResponse
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