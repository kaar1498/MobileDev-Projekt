using System;
using Newtonsoft.Json;

namespace MobileDev.FunctionApp.Core.Entities
{
  public class User : BaseEntity
  {
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Address { get; set; }
    
    public int PhoneNumber { get; set; }
  }
}