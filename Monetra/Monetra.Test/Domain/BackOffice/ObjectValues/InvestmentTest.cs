using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Test.Domain.BackOffice.ObjectValues;

public class InvestmentTest
{
    private const int VALUE = 100;
    [Fact]
    public void Should_Return_Value_Insert()
    {
        var model = new Investment(VALUE );
        Assert.Equal(VALUE,model.Value );
    }
    private const int VALUE_REMOVE = 10;
    [Fact]
    public void Should_Remove_Value_Of_ValueTotal()
    {
        var resultExpered = VALUE - VALUE_REMOVE;
        var model = new Investment(VALUE );
        model.RemoveValue(VALUE_REMOVE);
        
        Assert.Equal(resultExpered,model.Value);
    }
    private const int VALUE_REMOVEINVALID = 110;
    [Fact]
    public void Should_NoRemove_Value_if_ValueRemove_Is_Invalid()
    {
        var model = new Investment(VALUE );
        model.RemoveValue(VALUE_REMOVEINVALID);
        
        Assert.Equal(VALUE,model.Value);
    }
    private const int PORCENTAGE = 10;
    private const int PORCENTAGE_EXPERED = 110;
    [Fact]
    public void Should_Return_PorcentageCorrect_Of_Value()
    {
        var model = new Investment(VALUE );
        var result = model.CalculateValueByPercentageInYear(PORCENTAGE);
        
        Assert.Equal(PORCENTAGE_EXPERED,result);
    }
}