using MarketPrice.Application.DTOs.Bar;
using MediatR;

namespace MarketPrice.Application.Queries.GetHistoricalPrices {
    public record GetHistoricalPricesQuery(string instrumentId, DateTime startDate, string provider = "oanda") : IRequest<List<BarDto>>;
}
