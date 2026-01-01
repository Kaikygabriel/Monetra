using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Monetra.Application.Service;
using Monetra.Application.Service.Abstraction;
using Monetra.Application.UseCases;
using Monetra.Domain.BackOffice.Interfaces.Repostiries;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Customer;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Portifolio;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.Transaction;
using Monetra.Domain.BackOffice.Interfaces.Repostiries.User;
using Monetra.Domain.BackOffice.Interfaces.Services;
using Monetra.Infra.Repositories;
using Monetra.Infra.Repositories.Customer;
using Monetra.Infra.Repositories.Portfolio;
using Monetra.Infra.Repositories.Transaction;
using Monetra.Infra.Repositories.User;

namespace Monetra.Infra.CrossCuting;

public static class Ioc
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services,IConfiguration configuration)
    {
      
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        services.AddScoped<ICustomerRepository , CustomerRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddScoped<IServiceUser,ServiceUser>();
        services.AddScoped<ITokenService,TokenService>();
        services.AddScoped<IServiceEmail,ServiceEmail>();
        services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(HandlerBase).Assembly));
        
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
            };
        });
        services.AddAuthorization();
        
        services.AddMemoryCache(x => x.SizeLimit = 200);
        return services;
    }
}