using Monetra.Domain.BackOffice.Exception;

namespace Monetra.Domain.BackOffice.ObjectValues;

public struct Email
{
    public Email()
    {
        
    }
    public Email(string address)
    {
        if (IsValid(address))
            throw new EmailException("Address in email is invalid !");
        Address = address;
    }
    
    public string Address { get;private set; }

    private bool IsValid(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || address.Length <= 3 || !address.Contains('@'))
            return false;
        return true;
    }
}