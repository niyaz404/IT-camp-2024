using AuthService.BLL.Models;
using AuthService.Models;
using AutoMapper;

namespace AuthService.Mappings;

/// <summary>
/// Профиль для маппинга типов слоев Контроллеров и Бизнес-логики
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Маппинг слоев Контроллеров и Бизнес-логики
        CreateMap<User, UserModel>();
        CreateMap<UserModel, User>();
        
        CreateMap<UserCredentials, UserCredentialsModel>();
        CreateMap<UserCredentialsModel, UserCredentials>();
        
        CreateMap<Role, RoleModel>();
        CreateMap<RoleModel, Role>();
    }
}