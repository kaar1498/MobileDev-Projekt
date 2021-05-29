using System.Collections.Generic;
using Mapster;

namespace MobileDev_Projekt.Entities
{
  public class Program : BaseEntity
  {
    public string Name { get; set; }
    public List<Exercise> Exercises { get; set; }
  }
}