namespace Monetra.Domain.BackOffice.ObjectValues;

public class Investment
{
    public Investment()
    { 
    }

    public Investment(decimal value)
    {
        Value = value;
    }
    public decimal Value { get;private set; }

    public decimal CalculateValueByPercentageInYear(int valuePercentage)
    {
        decimal final = (decimal)valuePercentage / 100;
        return (final * Value) + Value;
    }

    public void AddValue(decimal value)
        => Value += value;
    
    public void RemoveValue(decimal price)
    {
        if(PriceRemoveIsValid(Value,price)) return;
        Value -= price;
    }

    private bool PriceRemoveIsValid(decimal value, decimal valueRemove)
        => value < valueRemove;
}