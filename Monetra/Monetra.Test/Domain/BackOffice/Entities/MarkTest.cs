using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Test.Domain.BackOffice.Entities;

public class MarkTest
{
    [Fact]
    public void Should_Return_False_If_ParametersINCreate_IsInvalid()
    {
        var resultCreate = Mark.Factories.Create
            (string.Empty, -10, Guid.NewGuid(),DateTime.Now.AddDays(-10));
        Assert.False(resultCreate.IsSuccess);
    }
    
    [Fact]
    public void Should_Return_True_If_ParametersINCreate_IsValid()
    {
        var resultCreate = Mark.Factories.Create
            ("teste", 10, Guid.NewGuid(),DateTime.Now.AddDays(10));
        Assert.True(resultCreate.IsSuccess);
    }
}