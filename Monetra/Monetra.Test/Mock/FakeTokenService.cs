using System.Security.Claims;
using Monetra.Domain.BackOffice.Interfaces.Services;

namespace Monetra.Test.Mock;

public class FakeTokenService : ITokenService
{
    public string GenerateToken(IEnumerable<Claim> claims)
    {
        return "kslkfjasljflk asjfl jslçjfjasdfasjfljaslfk";
    }
}