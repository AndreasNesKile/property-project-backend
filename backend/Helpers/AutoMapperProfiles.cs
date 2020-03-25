using AutoMapper;
using backend.DTO;
using backend.Models;
using System;
using System.Linq;

namespace backend.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PropertyImage, PropertyImageDTO>();
            CreateMap<Renovation, RenovationDTO>();
            CreateMap<Valuation, ValuationDTO>();
            CreateMap<Owner, OwnerDTO>();
            CreateMap<Owner, OwnerToPropertyDetailDTO>();
            CreateMap<OwnershipLog, OwnershipLogDTO>()
                .ForMember(
                    ow => ow.Owner,
                    opt => opt.MapFrom(src => src.Owner)
                );
            CreateMap<Property, PropertyToListDTO>()
                .ForMember(
                    p => p.PropertyImage,
                    opt => opt.MapFrom(src => src.PropertyImages.FirstOrDefault()))
                .ForMember(
                    p => p.PropertyStatus,
                    opt => opt.MapFrom(src => src.PropertyStatus.Name)
                )
                .ForMember(
                    p => p.PropertyType,
                    opt => opt.MapFrom(src => src.PropertyType.Name)
                );
            CreateMap<Property, PropertyDetailsToGuestDTO>()
                .ForMember(
                    p => p.PropertyImages,
                    opt => opt.MapFrom(src => src.PropertyImages))
                .ForMember(
                    p => p.PropertyStatus,
                    opt => opt.MapFrom(src => src.PropertyStatus.Name)
                )
                .ForMember(
                    p => p.PropertyType,
                    opt => opt.MapFrom(src => src.PropertyType.Name)
                );
            CreateMap<Property, PropertyDetailsToBuyerDTO>()
                .ForMember(
                    p => p.PropertyImages,
                    opt => opt.MapFrom(src => src.PropertyImages))
                .ForMember(
                    p => p.PropertyStatus,
                    opt => opt.MapFrom(src => src.PropertyStatus.Name)
                )
                .ForMember(
                    p => p.PropertyType,
                    opt => opt.MapFrom(src => src.PropertyType.Name)
                )
                .ForMember(
                    p => p.LastRenovated,
                    opt => opt.MapFrom(src =>
                        /*
                         * Checks if the property has any renovation.
                         * If true, it maps the LastRenovated date to the date of the last renovation
                         * If false, it maps the date to the date the property was built
                         */
                        src.Renovations.Any() ?
                        src.Renovations.Aggregate((R1, R2) => R1.DateTo > R2.DateTo ? R1 : R2).DateTo : src.CreatedAt)
                );
            CreateMap<OwnerType, OwnerTypeDTO>();
            CreateMap<Property, PropertyDetailsToAgentDTO>()
               .ForMember(
                    p => p.PropertyImages,
                    opt => opt.MapFrom(src => src.PropertyImages)
                )
               .ForMember(
                    p => p.PropertyStatus,
                    opt => opt.MapFrom(src => src.PropertyStatus.Name)
                )
                .ForMember(
                    p => p.PropertyType,
                    opt => opt.MapFrom(src => src.PropertyType.Name)
                )
                .ForMember(
                    p => p.LastRenovated,
                    opt => opt.MapFrom(src =>
                        src.Renovations.Any() ?
                        src.Renovations.Aggregate((R1, R2) => R1.DateTo > R2.DateTo ? R1 : R2).DateTo : src.CreatedAt)
                )
                .ForMember(
                    p => p.CurrentOwner,
                    opt => opt.MapFrom(src =>  src.OwnershipLogs.Any() ? 
                    src.OwnershipLogs.FirstOrDefault(o => !o.DateSold.HasValue).Owner : null)
                );
            CreateMap<Account, AccountDTO>()
                .ForMember(
                    t => t.AccountType,
                    opt => opt.MapFrom(src => src.AccountType.Name)
                );
            CreateMap<AccountForUpdateDTO, Account>();
        }
    }
}
