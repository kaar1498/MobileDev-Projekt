using System;
using System.Threading.Tasks;
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
  public class Delete : AuthenticationFilter
  {
    
    private static readonly string EndpointUri = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosEndPointUri");
    private static readonly string PrimaryKey = EnvironmentVariableHelper.GetEnvironmentVariable("CosmosPrimaryKey");

    private static CosmosClient? _cosmosClient;
    private static Container? _container;
    private const string DatabaseId = "MobileDev";
    private const string ContainerId = "Programs";
    
    [FunctionName("Program_Delete")]
    public static async Task<IActionResult> RunAsync(
      [HttpTrigger(AuthorizationLevel.Admin, "delete", Route = Routes.Program.Delete)]
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
        await _container.DeleteItemAsync<Core.Entities.Program>(id.ToString(), new PartitionKey(Auth.Username));
        return new NoContentResult();
      }
      catch (Exception e)
      {
        return new ConflictObjectResult(e.Message);
      }
    }
  }
}