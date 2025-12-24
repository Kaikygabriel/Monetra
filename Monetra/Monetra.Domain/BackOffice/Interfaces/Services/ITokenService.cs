using System.Security.Claims;

namespace Monetra.Domain.BackOffice.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(IEnumerable<Claim>claims);
}