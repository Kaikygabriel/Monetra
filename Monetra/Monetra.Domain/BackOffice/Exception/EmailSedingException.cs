namespace Monetra.Domain.BackOffice.Exception;

public class EmailSedingException : ApplicationException
{
    private const string MenssageDefault = "Error sending email!";

    public EmailSedingException(string menssage = MenssageDefault) : base(menssage)
    {
        
    }
}