namespace Monetra.Application.Commum;

public static class Errors
{
    public static readonly Error CustumerNoExisting =
        new("Custumer.NoExisting", "Custumer no Existing");
    public static readonly Error CustumerAlreadyExisting =
        new("Custumer.AlreadyExisting", "Custumer Already Existing");
    public static readonly Error UserCreatedFalid =
        new("User.CreatedFalid", "User Created Falid");
    public static readonly Error InvalidEmail =
        new("Email.Invalid", "Invalid email address");
    public static readonly Error PasswordInvalid =
        new("Password.Invalid", "Password invalid");
}