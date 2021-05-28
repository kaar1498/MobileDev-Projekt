using System.Collections.Generic;

namespace MobileDev.FunctionApp.Core.Entities
{
  public class Exercise : BaseEntity
  {
    public string Name { get; set; }
    public List<Image> ImageModels { get; set; }
    public int Duration { get; set; }
    public int RestFrequency { get; set; }
    public int RestDuration { get; set; }
    public int Repetitions { get; set; }
  }
}