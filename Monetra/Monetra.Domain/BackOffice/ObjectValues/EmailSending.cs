namespace Monetra.Domain.BackOffice.ObjectValues;

public class EmailSending
{
    public EmailSending(string menssage, string toAddress, string toName, string body)
    {
        Menssage = menssage;
        ToAddress = toAddress;
        ToName = toName;
        Body = body;
    }

    public EmailSending()
    {
        
    }

    public string Menssage { get; set; }
    public string ToAddress { get; set; }
    public string ToName { get; set; }
    public string Body { get; set; }
}