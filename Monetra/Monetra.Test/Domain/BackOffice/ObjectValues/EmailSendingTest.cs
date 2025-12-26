using Monetra.Domain.BackOffice.Exception;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Test.Domain.BackOffice.ObjectValues;

public class EmailSendingTest
{
    private const string Name_Valid = "dfasdte";
    private const string Body_Valid = "fdaksdfkjalsfjd";
    private const string Address_Valid = "teteset@gmail.com";
    private const string Title_Valid = "Testando o sistema";
    

    private const string Title_Invalid = "te";
    [Fact]
    public void Should_return_EmailSedingException_If_titleIsInValid()
    {
        Assert.Throws<EmailSedingException>(()
            => new EmailSending(Title_Invalid,Address_Valid,Name_Valid,Body_Valid));
    }

    private const string Name_Invalid = "a";
    [Fact]
    public void Should_return_EmailSedingException_If_nameInvalid()
    {
        Assert.Throws<EmailSedingException>(()
            => new EmailSending(Title_Valid,Address_Valid,Name_Invalid,Body_Valid));
    }
    
    private const string Body_Invalid = "afd";
    [Fact]
    public void Should_return_EmailSedingException_If_BodyInvalid()
    {
        Assert.Throws<EmailSedingException>(()
            => new EmailSending(Title_Valid,Address_Valid,Name_Valid,Body_Invalid));
    }
    private const string Address_Invalid = "d";
    [Fact]
    public void Should_return_EmailSedingException_If_AddressInvalid()
    {
        Assert.Throws<EmailSedingException>(()
            => new EmailSending(Title_Valid,Address_Invalid,Name_Valid,Body_Invalid));
    }
    [Fact]
    public void Should_return_NotNull_If_EmailSeding_IsOk()
    {
        var model = new EmailSending(Title_Valid,Address_Valid,Name_Valid,Body_Valid);
        Assert.NotNull(model);
    }
}