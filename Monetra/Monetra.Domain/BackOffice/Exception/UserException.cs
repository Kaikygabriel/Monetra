namespace Monetra.Domain.BackOffice.Exception;

public class UserException : ApplicationException
{
    private const string MenssageDefault = "Error in user";
    public UserException(string menssage = MenssageDefault)  :base(menssage)
    {
        
    }
}