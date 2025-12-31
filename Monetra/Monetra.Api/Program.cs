using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Monetra.Api.Extensions;
using Monetra.Api.Logger;
using Monetra.Infra.CrossCuting;
using Monetra.Infra.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddProvider(new MonetraLoggerProvider());
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseSqlServer(builder.Configuration["ConnectionString:DefaultConnection"]
        , b => b.MigrationsAssembly("Monetra.Api")));
builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
