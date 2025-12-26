using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Test.Domain.BackOffice.Entities;

public class PortfolioTeste
{
    private const string Title_Invalid = "fd";
    private const string Title_Valid = "Portfolio";
    
    private readonly Investment InvestmentValid = new (100);
    [Fact]
    public void Should_Return_NotNull_If_Title_Are_Valid()
    {
        var result = new Portfolio(InvestmentValid,InvestmentValid,Title_Valid);
        Assert.NotNull(result);
    }

    [Fact]
    public void Should_Return_NotNull_If_Title_Are_Invalid()
    {
        Assert.Throws<Exception>(() 
        => new Portfolio
            (InvestmentValid, InvestmentValid, Title_Invalid));
    }

    [Fact]
    public void Should_Return_True_When_Visible_Is_ConvertToVisible()
    {
        var result = new Portfolio(InvestmentValid,InvestmentValid,Title_Valid);
        result.ConvertToVisible();
        Assert.True(result.Visible);        

    }
    [Fact]
    public void Should_Return_True_When_Visible_Is_ConvertToNoVisible()
    {
        var result = new Portfolio(InvestmentValid,InvestmentValid,Title_Valid);
        result.ConvertToNoVisible();
        Assert.False(result.Visible);        

    }
}