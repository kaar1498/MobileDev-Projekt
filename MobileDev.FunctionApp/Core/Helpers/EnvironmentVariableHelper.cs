using System;

namespace MobileDev.FunctionApp.Core.Helpers
{
  public static class EnvironmentVariableHelper
  {
    public static string GetEnvironmentVariable(string name)
    {
      return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
    }
  }
}