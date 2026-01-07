using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Test.Domain.BackOffice.Entities;

public class PortfolioTeste
{
    private const string Title_Invalid = "fd";
    private const string Title_Valid = "Portfolio";
    
    private readonly Investment InvestmentValid = new (100);
    [Fact]
    public void Should_Return_True_if_Parameters_Are_Valids()
    {
        var result = Portfolio.Factories.Create(InvestmentValid,InvestmentValid,InvestmentValid,Title_Valid);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Should_Return_NotNull_If_Title_Are_Invalid()
    {
        var result = Portfolio.Factories.Create(InvestmentValid, InvestmentValid, InvestmentValid,Title_Invalid);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Should_Return_True_When_Visible_Is_ConvertToVisible()
    {
        var result = Portfolio.Factories.Create(InvestmentValid,InvestmentValid,InvestmentValid,Title_Valid);
        result.Value.ConvertToVisible();
        Assert.True(result.Value.Visible);        

    }
    [Fact]
    public void Should_Return_True_When_Visible_Is_ConvertToNoVisible()
    {
        var result = Portfolio.Factories.Create(InvestmentValid,InvestmentValid,InvestmentValid,Title_Valid);
        result.Value.ConvertToNoVisible();
        Assert.False(result.Value.Visible);        
    }
}