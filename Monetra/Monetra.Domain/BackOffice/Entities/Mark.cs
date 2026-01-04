
using System.Text.Json.Serialization;
using Monetra.Domain.BackOffice.Commum;
using Monetra.Domain.BackOffice.Commum.Abstraction;
using Monetra.Domain.BackOffice.Entities.Abstraction;

namespace Monetra.Domain.BackOffice.Entities;

public class Mark : Entity
{
    protected Mark()
    {
        
    }
    private Mark(string title, decimal value, Guid customerId, DateTime deadline)
    {
        Title = title;
        TargetAmount = value;
        CustomerId = customerId;
        Deadline = deadline;
        CreateAt = DateTime.UtcNow;
        Id = Guid.NewGuid();
    }
    public string Title { get;private set; }
    public ushort Percentage { get;private set; }
    public decimal TargetAmount { get;private set; }
    public DateTime Deadline { get;private set; }
    public DateTime CreateAt { get; init; }
    public Guid CustomerId { get; init; }
    [JsonIgnore]
    public Customer Customer { get; init; }

    public Result AlterPercentage(ushort value)
    {
        if(value > 100)
            return Result.Failure(new Error("Value.Invalid","Value is invalid"));
        if (value < 0)
            value = 0;
        Percentage = value;
        return Result.Success();
    }

    public static class Factories
    {
        public static Result<Mark> Create(string title, decimal value,Guid customerId,DateTime deadline)
        {
            if(!CheckParameters(title,value,customerId, deadline))
                return Result<Mark>.Failure(new("Mark.Parameters","Parameters is invalid!"));
            var mark = new Mark(title, value, customerId, deadline);
            return Result<Mark>.Success(mark);
        }

        private static bool CheckParameters(string title, decimal value,Guid customerId,DateTime deadline)
        {
            if (value <= 0)
                return false;
            if (deadline <= DateTime.UtcNow)
                return false;
            if (string.IsNullOrWhiteSpace(title) || title.Length <= 2)
                return false;
            if (customerId == Guid.Empty)
                return false;
            return true;
        }
    }
    
}