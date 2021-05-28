using System.Collections.Generic;

namespace MobileDev.FunctionApp.Core.Entities
{
  public class Program : BaseEntity
  {
    public string Name { get; set; }
    public List<Exercise> Exercises { get; set; }
  }
}