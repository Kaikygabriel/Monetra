namespace Monetra.Domain.BackOffice.Exception;

public class EmailException : ApplicationException
{
    private const string MenssageDefault = "Error in email!";

    public EmailException(string menssage = MenssageDefault) : base(menssage)
    {
        
    }
}