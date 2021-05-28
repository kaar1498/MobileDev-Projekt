using System.Collections.Generic;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace MobileDev.FunctionApp.Core.Helpers
{
  /// <summary>
  ///     Wrapper class for encapsulating the token issuance logic.
  /// </summary>
  public static class TokenIssuer
  {
    private static readonly IJwtEncoder JwtEncoder = new JwtEncoder(new HMACSHA256Algorithm(), new JsonNetSerializer(), new JwtBase64UrlEncoder());
    
    /// <summary>
    ///     This method is intended to be the main entry point for generation of the JWT.
    /// </summary>
    /// <param name="username">The user that the token is being issued for.</param>
    /// <returns>A JWT token which can be returned to the user.</returns>
    public static string IssueTokenForUser(string username)
    {
      var claims = new Dictionary<string, object>
      {
        {"username", username},

        {"role", "admin"}
      };

      return JwtEncoder.Encode(claims, EnvironmentVariableHelper.GetEnvironmentVariable("JWT_Private_Key"));
    }
  }
}