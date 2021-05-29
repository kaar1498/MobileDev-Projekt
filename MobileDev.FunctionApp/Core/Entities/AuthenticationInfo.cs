using System;
using System.Collections.Generic;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using MobileDev.FunctionApp.Core.Helpers;

namespace MobileDev.FunctionApp.Core.Entities
{
  public class AuthenticationInfo
  {
    public bool IsValid { get; }
    public string Username { get; }
    public string Role { get; }

    public AuthenticationInfo(HttpRequest request)
    {
      // Check if we have a header.
      if (!request.Headers.ContainsKey("Authorization"))
      {
        IsValid = false;

        return;
      }

      string authorizationHeader = request.Headers["Authorization"];

      // Check if the value is empty.
      if (string.IsNullOrEmpty(authorizationHeader))
      {
        IsValid = false;

        return;
      }

      // Check if we can decode the header.
      IDictionary<string, object> claims;

      try
      {
        if (authorizationHeader.StartsWith("Bearer"))
        {
          authorizationHeader = authorizationHeader[7..];
        }

        // Validate the token and decode the claims.
        claims = new JwtBuilder()
          .WithAlgorithm(new HMACSHA256Algorithm())
          .WithSecret(EnvironmentVariableHelper.GetEnvironmentVariable("JWT_Private_Key"))
          .MustVerifySignature()
          .Decode<IDictionary<string, object>>(authorizationHeader);
      }
      catch
      {
        IsValid = false;

        return;
      }

      // Check if we have user claim.
      if (!claims.ContainsKey("username"))
      {
        IsValid = false;

        return;
      }

      IsValid = true;
      Username = Convert.ToString(claims["username"]);
      Role = Convert.ToString(claims["role"]);
    }
  }
}