using System;
using System.Collections.Generic;

namespace MobileDev_Projekt.Entities
{
  public class Exersice
  {
    public int id { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public DateTime published_at { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public List<Image> Image { get; set; }
  }
}