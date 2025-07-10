using MarketPrice.Application.DTOs.Asset;
using MediatR;

namespace MarketPrice.Application.Queries.GetAllAssets
{
    public record GetAllAssetsQuery : IRequest<List<AssetDto>>;
}
