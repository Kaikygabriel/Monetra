using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Services;

namespace Monetra.Application.UseCases.Custumer;

public class HandlerAuthBase:  HandlerBase
{
    private readonly ITokenService _tokenService;
    public HandlerAuthBase(IUnitOfWork unitOfWork, ITokenService tokenService) : base(unitOfWork)
    {
        _tokenService = tokenService;
    }

    protected string GenerateJwtTokenOfCustomer(Customer customer)
    {
        return _tokenService.GenerateToken(GenerateClaimsOfCustomer(customer));
    }
    protected IEnumerable<Claim> GenerateClaimsOfCustomer(Customer customer)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, customer.User.Email.Address),
            new Claim(ClaimTypes.Name, customer.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
        };
        return claims;
    }
}