using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Monetra.Application.Service.Abstraction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Services;

namespace Monetra.Application.UseCases.Customer;

public class HandlerCustomerBase:  HandlerBase
{
    protected readonly IServiceUser _serviceUser;
    protected readonly ITokenService _tokenService;
    public HandlerCustomerBase(IUnitOfWork unitOfWork, ITokenService tokenService, IServiceUser serviceUser) : base(unitOfWork)
    {
        _tokenService = tokenService;
        _serviceUser = serviceUser;
    }

    protected string GenerateJwtTokenOfCustomer(Domain.BackOffice.Entities.Customer customer)
    {
        return _tokenService.GenerateToken(GenerateClaimsOfCustomer(customer));
    }
    private IEnumerable<Claim> GenerateClaimsOfCustomer(Domain.BackOffice.Entities.Customer customer)
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