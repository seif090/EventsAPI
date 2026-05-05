using System.Security.Claims;
using EventsAPI.Domain.Entities;

namespace EventsAPI.Application.Authentication.Contracts;

public interface IJwtTokenGenerator
{
    (string Token, DateTime ExpiresAt) GenerateToken(User user, IEnumerable<Claim> additionalClaims);
}
