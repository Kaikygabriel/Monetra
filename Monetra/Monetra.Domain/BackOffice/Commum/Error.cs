namespace Monetra.Domain.BackOffice.Commum;

public class Error(string code, string menssage)
{
    public string Code { get;} = code;
    public string Menssage { get;} = menssage;

    public override string ToString()
    {
        return $"{Code} \n  {Menssage}";
    }
};