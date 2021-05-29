using System.Collections.Generic;

namespace MobileDev_Projekt.Entities
{
  public class Exercise : BaseEntity
  {
    public string Name { get; set; }
    public List<Image> Images { get; set; }
    public int Duration { get; set; }
    public int RestFrequency { get; set; }
    public int RestDuration { get; set; }
    public int Repetitions { get; set; }
  }
}