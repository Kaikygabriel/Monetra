namespace Monetra.Domain.BackOffice.Commum;

public static class Errors
{
    #region Customer
        public static readonly Error CustumerNoExisting =
            new("Custumer.NoExisting", "Custumer no Existing");
        public static readonly Error CustumerAlreadyExisting =
            new("Custumer.AlreadyExisting", "Custumer Already Existing");
    #endregion

    #region User
        public static readonly Error UserCreatedFalid =
            new("User.CreatedFalid", "User Created Falid");
        public static readonly Error InvalidEmail =
            new("Email.Invalid", "Invalid email address");
        public static readonly Error PasswordInvalid =
            new("Password.Invalid", "Password invalid");
    
    #endregion

    #region Potfolio
        public static readonly Error PortfolioNoExisting =
            new("Portfolio.NoExisting", "Portfolio no existing");
        public static readonly Error CustomerIdIsNotEqualPortfolioCustomerId 
            = new("UserId.IsNotEqual", "UserId of request Is Not Equal user id from portfolio");


    #endregion


}