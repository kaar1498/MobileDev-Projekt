namespace MobileDev.FunctionApp.Core
{
  public static class Routes
  {
    private const string Version = "v1";

    public static class Authentication
    {
      private const string BaseUrl = Version + "/auth";
      public const string Login = BaseUrl + "/login";
      public const string Register = BaseUrl + "/register";
    }

    public static class Exercise
    {
      private const string BaseUrl = Version + "/exercises";
      public const string Get = BaseUrl + "/{id:Guid}";
      public const string GetAll = BaseUrl;
      public const string Create = BaseUrl;
      public const string Update = BaseUrl + "/{id:Guid}";
      public const string Delete = BaseUrl + "/{id:Guid}";
    }
    
    public static class Program
    {
      private const string BaseUrl = Version + "/programs";
      public const string Get = BaseUrl + "/{id:Guid}";
      public const string GetAll = BaseUrl;
      public const string Create = BaseUrl;
      public const string Update = BaseUrl + "/{id:Guid}";
      public const string Delete = BaseUrl + "/{id:Guid}";
    }
  }
}