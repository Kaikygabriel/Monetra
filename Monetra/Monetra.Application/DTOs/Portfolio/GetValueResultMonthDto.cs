using Monetra.Domain.BackOffice.Entities;

namespace Monetra.Application.DTOs.Portfolio;

public record GetValueResultMonthDto(string Phrase,IEnumerable<Transaction> Transactions);