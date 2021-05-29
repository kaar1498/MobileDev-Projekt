using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;
using MobileDev.FunctionApp.Core.FunctionInvocationFilters;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(MobileDev.FunctionApp.Startup))]
namespace MobileDev.FunctionApp
{
  public class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      builder.Services.AddSingleton<IFunctionFilter, AuthenticationFilter>();
    }
  }
}