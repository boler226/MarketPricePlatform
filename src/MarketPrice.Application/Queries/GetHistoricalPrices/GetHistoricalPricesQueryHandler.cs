using AutoMapper;
using MarketPrice.Application.DTOs.Bar;
using MarketPrice.Infrastructure.Services.Interfaces;
using MediatR;

namespace MarketPrice.Application.Queries.GetHistoricalPrices {
    public class GetHistoricalPricesQueryHandler(
        IFintachartsService service,
        IMapper mapper
        ) : IRequestHandler<GetHistoricalPricesQuery, List<BarDto>> {
        public async Task<List<BarDto>> Handle(GetHistoricalPricesQuery request, CancellationToken cancellationToken) {
            var bars = await service.GetHistoricalPricesAsync(request.instrumentId, request.startDate, request.provider);
            return mapper.Map<List<BarDto>>(bars);
        }
    }
}
