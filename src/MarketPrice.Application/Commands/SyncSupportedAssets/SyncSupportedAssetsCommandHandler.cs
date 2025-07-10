using AutoMapper;
using MarketPrice.Domain.Entities.Asset;
using MarketPrice.Domain.Interfaces;
using MarketPrice.Infrastructure.Services.Interfaces;
using MediatR;

namespace MarketPrice.Application.Commands.SyncSupportedAssets {
    public class SyncSupportedAssetsCommandHandler(
        IAssetRepository repository,
        IFintachartsService service
        ) : IRequestHandler<SyncSupportedAssetsCommand> {
        public async Task Handle(SyncSupportedAssetsCommand request, CancellationToken cancellationToken) {
            var assetDtos = await service.GetSupportedAssetsAsync();

            var assets = assetDtos.Select(dto => new AssetEntity {
                Id = dto.Id,
                Symbol = dto.Symbol,
                Kind = dto.Kind,
                Description = dto.Description,
                TickSize = dto.TickSize,
                Currency = dto.Currency,
                BaseCurrency = dto.BaseCurrency,
                Profile = new ProfileEntity {
                    Id = Guid.NewGuid(),
                    Name = dto.Profile?.Name ?? string.Empty
                },
                Mappings = dto.Mappings.Select(mapping => new MappingEntity {
                    Id = Guid.NewGuid(),
                    Provider = mapping.Key,
                    Symbol = mapping.Value.Symbol,
                    Exchange = mapping.Value.Exchange,
                    DefaultOrderSize = mapping.Value.DefaultOrderSize,
                    MaxOrderSize = mapping.Value.MaxOrderSize,
                    RegularStart = mapping.Value.TradingHours.RegularStart,
                    RegularEnd = mapping.Value.TradingHours.RegularEnd,
                    ElectronicStart = mapping.Value.TradingHours.ElectronicStart,
                    ElectronicEnd = mapping.Value.TradingHours.ElectronicEnd
                }).ToList()
            }).ToList();

            await repository.AddOrUpdateRangeAsync(assets, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
        }
    }
}
