using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using MobileDev.FunctionApp.Core.Entities;

namespace MobileDev.FunctionApp.Core.FunctionInvocationFilters
{
  /// <summary>
  ///     Base class for authenticated service which checks the incoming JWT token.
  /// </summary>
  public class AuthenticationFilter : IFunctionInvocationFilter
  {
    private const string AuthenticationHeaderName = "Authorization";
    protected static AuthenticationInfo Auth { get; set; }

    /// <summary>
    ///     Pre-execution filter.
    /// </summary>
    /// <remarks>
    ///     This mechanism can be used to extract the authentication information. 
    /// </remarks>
    public Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
    {
      try
      {
        Auth = new AuthenticationInfo(executingContext.Arguments.First(f => f.Value is HttpRequest).Value as HttpRequest);
      }
      catch
      {
        // ignored
      }

      return Task.CompletedTask;
    }

    public Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}