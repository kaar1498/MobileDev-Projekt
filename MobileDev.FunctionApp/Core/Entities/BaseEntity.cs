using System;
using Newtonsoft.Json;

namespace MobileDev.FunctionApp.Core.Entities
{
  public abstract class BaseEntity
  {
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    
    [JsonProperty(PropertyName = "partitionKey")]
    public string PartitionKey { get; set; }
  }
}