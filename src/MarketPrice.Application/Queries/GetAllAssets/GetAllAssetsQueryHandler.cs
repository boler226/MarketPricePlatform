using AutoMapper;
using MarketPrice.Application.DTOs.Asset;
using MarketPrice.Domain.Interfaces;
using MediatR;

namespace MarketPrice.Application.Queries.GetAllAssets
{
    public class GetAllAssetsQueryHandler(
        IAssetRepository repository,
        IMapper mapper
        ) : IRequestHandler<GetAllAssetsQuery, List<AssetDto>> {
        public async Task<List<AssetDto>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken) {
            var assets = await repository.GetAllAsync(cancellationToken);
            return mapper.Map<List<AssetDto>>(assets);
        }
    }
}
