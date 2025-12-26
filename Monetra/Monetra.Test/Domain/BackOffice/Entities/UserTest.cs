using Monetra.Domain.BackOffice.Entities;
using Monetra.Domain.BackOffice.Exception;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Test.Domain.BackOffice.Entities;

public class UserTest
{
    private readonly Email Email_Valid = new Email("teste@gmail.com");
    
    [Fact]
    public void Should_Return_NotNull_If_PasswordIsValid()
    {
        var passwordValid = "fdjalsfdjal@";
        var result = new User(passwordValid,Email_Valid);
        Assert.NotNull(result);
    }
    
    [Fact]
    public void Should_Return_Exception_If_PasswordIsInvalid()
    {
        var passwordValid = "ad";
        Assert.Throws<UserException>(()=>new User(passwordValid,Email_Valid));
    }
    
    [Fact]
    public void Should_UpdatePassword_If_PasswordIsValid()
    {
        var passwordValid = "teste";
        var result = new User(passwordValid,Email_Valid);
        var newPassword = "fdjalsfdjal";
        result.UpdatePassword(newPassword);
        Assert.Equal(newPassword, result.Password);
    }
}