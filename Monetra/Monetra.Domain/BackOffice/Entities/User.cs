using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Entities.Abstraction;
using Monetra.Domain.BackOffice.Exception;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Domain.BackOffice.Entities;

public class User : Entity
{
    protected User()
    {
        
    }
    public User(string password, Email email)
    {
        if (PasswordIsInValid(password))
            throw new UserException("Password in user is invalid!");
        Password = password;
        Email = email;
        Id = Guid.NewGuid();
    }

    public Email Email { get;private set; }
    public string Password { get;private set; }
    

    public Result UpdatePassword(string password)
    {
        if (PasswordIsInValid(password))
            return Result.Failure(new Error("Password.Invalid","Password invalid!"));
        Password = password;
        return Result.Success();
    }
    
    private bool PasswordIsInValid(string password)
        => string.IsNullOrWhiteSpace(password) || password.Length <= 4;

}