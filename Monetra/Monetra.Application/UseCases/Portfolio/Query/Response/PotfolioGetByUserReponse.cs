using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Application.UseCases.Portfolio.Query.Response;

public class PotfolioGetByUserReponse
{
    public PotfolioGetByUserReponse(Guid id, bool visible, string title, DateTime createDate, Investment fixedIncome, Investment variableIncome)
    {
        Id = id;
        Visible = visible;
        Title = title;
        CreateDate = createDate;
        FixedIncome = fixedIncome;
        VariableIncome = variableIncome;
    }

    public PotfolioGetByUserReponse()
    {
        
    }
    public Guid Id { get; set; }
    public bool Visible { get;private set; }
    public string Title { get;private set; }
    public DateTime CreateDate { get;private set; }
    public Investment FixedIncome  { get;private set; }
    public Investment VariableIncome  { get;private set; }

    public static implicit operator PotfolioGetByUserReponse(Domain.BackOffice.Entities.Portfolio port)
        => new(port.Id,port.Visible,port.Title,port.CreateDate,port.FixedIncome,port.VariableIncome);

    public static IEnumerable<PotfolioGetByUserReponse> ConvertListInResponse(
        IEnumerable<Domain.BackOffice.Entities.Portfolio> list)
    {
        var listnew = new List<PotfolioGetByUserReponse>();
        foreach(var item in list)
            listnew.Add(item);
        return listnew;
    }
}