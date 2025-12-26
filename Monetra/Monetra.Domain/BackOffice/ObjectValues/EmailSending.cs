using Monetra.Domain.BackOffice.Exception;

namespace Monetra.Domain.BackOffice.ObjectValues;

public class EmailSending
{
    protected EmailSending()
    {
        
    }
    public EmailSending(string title, string toAddress, string toName, string body)
    {
        if (!ParametersIsValid(title, toAddress, toName, body))
            throw new EmailSedingException("Error in parameters of EmailSending");
        Title = title;
        ToAddress = toAddress;
        ToName = toName;
        Body = body;
    }
    
    public string Title { get; set; }
    public string ToAddress { get; set; }
    public string ToName { get; set; }
    public string Body { get; set; }

    private bool ParametersIsValid(string title, string toAddress, string toName, string body)
    {
        if (StringIsValid(title, 2)||
            StringIsValid(toAddress,4)||
            StringIsValid(toName,2)||
            StringIsValid(body,4))
            return false;
        
        return true;
    }

    private bool StringIsValid(string text, int lenghtExpered)
        => string.IsNullOrWhiteSpace(text) || text.Length <= lenghtExpered;
}