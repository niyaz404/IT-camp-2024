using AuthService.BLL.Helpers;
using AuthService.BLL.Models;
using AuthService.DAL.Models;
using AutoMapper;

namespace AuthService.BLL.Mappings;

/// <summary>
/// Профиль для маппинга типов слоев Бизнес-логики и Репозиториев
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserModel, UserEntity>()
            .ForMember(dest => dest.PasswordSalt, opt => opt.MapFrom(src => PasswordHelper.GenerateSalt()))
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom((src, trg) => PasswordHelper.HashPassword(src.Password, trg.PasswordSalt)));
    }
}