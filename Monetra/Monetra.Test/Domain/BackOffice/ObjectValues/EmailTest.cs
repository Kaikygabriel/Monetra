using Monetra.Domain.BackOffice.Exception;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Test.Domain.BackOffice.ObjectValues;

public class EmailTest
{
    private const string AddressValid = "teste@gmail.com";

    [Fact]
    public void Should_Return_NotNull_If_Email_Is_Valid()
    {
        var email = new Email(AddressValid);
        Assert.NotNull(email);
    }
    
    private const string AddressInvalid = "teste.com";
    [Fact]
    public void Should_Return_EmailException_If_Email_Is_Invalid()
    {
       Assert.Throws<EmailException>
           (() => new Email(AddressInvalid));
    }
    
    private const string AddressSmall = "t";
    [Fact]
    public void Should_Return_EmailException_If_Email_Is_Small()
    {
        Assert.Throws<EmailException>
            (() => new Email(AddressSmall));
    }
}