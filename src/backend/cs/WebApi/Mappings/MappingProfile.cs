using AutoMapper;
using WebApi.BLL.Models.Implementation.Auth;
using WebApi.BLL.Models.Implementation.Commit;
using WebApi.BLL.Models.Implementation.Magnetogram;
using WebApi.BLL.Models.Implementation.Report;
using WebApi.Models.Implementation.Auth;
using WebApi.Models.Implementation.Commit;
using WebApi.Models.Implementation.Magnerogram;
using WebApi.Models.Implementation.Report;

namespace WebApi.Mappings;

/// <summary>
/// Профиль для маппинга типов слоев Контроллеров и Бизнес-логики
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Маппинг слоев Контроллеров и Бизнес-логики
        CreateMap<UserCredentialsDto, UserCredentialsModel>();
        CreateMap<UserCredentialsModel, UserCredentialsDto>();
        
        CreateMap<DefectDto, DefectModel>();
        CreateMap<DefectModel, DefectDto>();
        
        CreateMap<StructuralElementDto, StructuralElementModel>();
        CreateMap<StructuralElementModel, StructuralElementDto>();
        
        CreateMap<CommitDto, CommitModel>();
        CreateMap<CommitModel, CommitDto>();
        
        CreateMap<MagnetogramDto, MagnetogramModel>();
        CreateMap<MagnetogramModel, MagnetogramDto>();
        
        CreateMap<ReportDto, ReportModel>();
        CreateMap<ReportModel, ReportDto>();
    }
}