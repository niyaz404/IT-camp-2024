using AutoMapper;
using WebApi.BLL.Models.Implementation.Auth;
using WebApi.BLL.Models.Implementation.Commit;
using WebApi.BLL.Models.Implementation.Magnetogram;
using WebApi.BLL.Models.Implementation.Report;
using WebApi.DAL.Models.Implementation.Commit;
using WebApi.DAL.Models.Implementation.Magnetogram;
using WebApi.DAL.Models.Implementation.Report;

namespace WebApi.BLL.Mappings;

/// <summary>
/// Профиль для маппинга типов слоев Бизнес-логики и Провайдеров\Репозиториев
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Маппинг слоев Бизнес-логики и Провайдеров\Репозиториев
        CreateMap<UserCredentials, UserCredentialsModel>();
        CreateMap<UserCredentialsModel, UserCredentials>();
        
        CreateMap<DefectEntity, DefectModel>();
        CreateMap<DefectModel, DefectEntity>();
        
        CreateMap<StructuralElementEntity, StructuralElementModel>();
        CreateMap<StructuralElementModel, StructuralElementEntity>();
        
        CreateMap<CommitEntity, CommitModel>();
        CreateMap<CommitModel, CommitEntity>();
        
        CreateMap<MagnetogramEntity, MagnetogramModel>();
        CreateMap<MagnetogramModel, MagnetogramEntity>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.CreatedBy));
        
        CreateMap<ReportEntity, ReportModel>();
        CreateMap<ReportModel, ReportEntity>();
    }
}