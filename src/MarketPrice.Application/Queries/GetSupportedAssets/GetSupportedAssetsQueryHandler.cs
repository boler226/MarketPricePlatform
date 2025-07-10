using AutoMapper;
using MarketPrice.Application.DTOs.Asset;
using MarketPrice.Domain.Entities;
using MarketPrice.Domain.Interfaces;
using MarketPrice.Infrastructure.Services.Interfaces;
using MediatR;

namespace MarketPrice.Application.Queries.GetSupportedAssets
{
    public class GetSupportedAssetsQueryHandler(
        IFintachartsService service,
        IMapper mapper
        ) : IRequestHandler<GetSupportedAssetsQuery, List<AssetDto>> {
        public async Task<List<AssetDto>> Handle(GetSupportedAssetsQuery request, CancellationToken cancellationToken) {
            var assetDtos = await service.GetSupportedAssetsAsync();
            return mapper.Map<List<AssetDto>>(assetDtos);
        }
    }
}
