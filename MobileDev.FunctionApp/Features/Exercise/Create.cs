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
  public class Create : AuthenticationFilter
  {
    private static readonly string EndpointUri = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosEndPointUri");
    private static readonly string PrimaryKey = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosPrimaryKey");

    private static CosmosClient? _cosmosClient;
    private static Container? _container;
    private const string DatabaseId = "MobileDev";
    private const string ContainerId = "Exersices";

    [FunctionName("Exercise_Create")]
    public static async Task<IActionResult> RunAsync(
      [HttpTrigger(AuthorizationLevel.Admin, "post", Route = Routes.Exercise.Create)]
      [FromBody] CreateExerciseRequest request,
      HttpRequest req,
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
        var newExercise = request.Adapt<Core.Entities.Exercise>();
        newExercise.Id = Guid.NewGuid();
        newExercise.PartitionKey = Auth.Username;
        var result = await _container.CreateItemAsync(newExercise, new PartitionKey(newExercise.PartitionKey));
        return new OkObjectResult(result.Resource);
      }
      catch (Exception e)
      {
        return new ConflictObjectResult(e.Message);
      }
    }

    public class CreateExerciseRequest
    {
      public string? Name { get; set; }
      public List<CreateImageRequest>? Images { get; set; }
      public int? Duration { get; set; }
      public int? RestFrequency { get; set; }
      public int? RestDuration { get; set; }
      public int? Repetitions { get; set; }

      public class CreateImageRequest
      {
        public byte[]? Bytes { get; set; }
        public string? Description { get; set; }
      }
    }
  }
}