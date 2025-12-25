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
        if (PasswordIsValid(password))
            throw new UserException("Password in user is invalid!");
        Password = password;
        Email = email;
    }

    public Email Email { get;private set; }
    public string Password { get;private set; }


    public void UpdatePassword(string password)
    {
        if (PasswordIsValid(password))
            throw new UserException("Password in user is invalid!");
        Password = password;
    }
    
    private bool PasswordIsValid(string password)
        => string.IsNullOrWhiteSpace(password) || password.Length <= 4;

}