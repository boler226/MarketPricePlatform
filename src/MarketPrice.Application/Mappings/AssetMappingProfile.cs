using AutoMapper;
using MarketPrice.Application.DTOs.Asset;
using MarketPrice.Application.DTOs.Bar;
using MarketPrice.Domain.Entities.Asset;
using MarketPrice.Domain.Entities.Bar;

namespace MarketPrice.Application.Mappings
{
    public class AssetMappingProfile : Profile {
        public AssetMappingProfile() {
            CreateMap<AssetEntity, AssetDto>()
                .ForMember(dest => dest.Mappings, opt => opt.MapFrom(src =>
                    src.Mappings.ToDictionary(
                        m => m.Provider,
                        m => new MappingDto {
                            Symbol = m.Symbol,
                            Exchange = m.Exchange,
                            DefaultOrderSize = m.DefaultOrderSize,
                            MaxOrderSize = m.MaxOrderSize,
                            TradingHours = new TradingHoursDto {
                                RegularStart = m.RegularStart,
                                RegularEnd = m.RegularEnd,
                                ElectronicStart = m.ElectronicStart,
                                ElectronicEnd = m.ElectronicEnd
                            }
                        })))
                .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => src.Profile));

            CreateMap<AssetDto, AssetEntity>()
               .ForMember(dest => dest.Mappings, opt => opt.Ignore())
               .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => src.Profile));

            CreateMap<ProfileEntity, ProfileDto>();
            CreateMap<ProfileDto, ProfileEntity>();

            CreateMap<MappingEntity, MappingDto>();
            CreateMap<MappingDto, MappingEntity>();

            CreateMap<BarEntity, BarDto>();
            CreateMap<BarDto, BarEntity>();
        }
    }
}
