using Monetra.Domain.BackOffice.Entities.Abstraction;
using Monetra.Domain.BackOffice.ObjectValues;

namespace Monetra.Domain.BackOffice.Entities;

public class Portfolio : Entity
{
    public Portfolio(string title)
    {
        Title = title;
    }

    public Portfolio( Investment fixedIncome, Investment variableIncome, string title,bool visible)
    {
        Visible = false;
        CreateDate = DateTime.Now;
        Title = title;
        FixedIncome = fixedIncome;
        VariableIncome = variableIncome;
    }

    public bool Visible { get;private set; }
    public string Title { get;private set; }
    public DateTime CreateDate { get;private set; }
    public Investment FixedIncome  { get;private set; }
    public Investment VariableIncome  { get;private set; }

    public decimal TotalPrice()
        => FixedIncome.Value + VariableIncome.Value;

    public void ConvertToNoVisible()
        => Visible = false;
    public void ConvertToVisible()
        => Visible = true;
}