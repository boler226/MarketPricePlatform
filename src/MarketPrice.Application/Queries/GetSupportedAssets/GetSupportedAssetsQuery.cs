using MarketPrice.Application.DTOs.Asset;
using MediatR;

namespace MarketPrice.Application.Queries.GetSupportedAssets
{
    public record GetSupportedAssetsQuery : IRequest<List<AssetDto>>;
}
