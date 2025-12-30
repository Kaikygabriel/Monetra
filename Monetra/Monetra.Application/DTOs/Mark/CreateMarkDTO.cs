using System.Runtime.CompilerServices;
using Monetra.Domain.BackOffice.Commum;

namespace Monetra.Application.DTOs.Mark;

public class CreateMarkDTO
{
    public string Title { get; set; }
    public decimal Value { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime DeadLine { get; set; }

    public static implicit operator Result<Domain.BackOffice.Entities.Mark>(CreateMarkDTO model)
        => Domain.BackOffice.Entities.Mark.Factories.Create
            (model.Title, model.Value, model.CustomerId, model.DeadLine);
}