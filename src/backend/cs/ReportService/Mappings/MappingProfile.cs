using AutoMapper;
using DAL.Models.Implementation.Commit;
using DAL.Models.Implementation.Report;
using ReportService.BLL.Models;

namespace ReportService.Mappings;

/// <summary>
/// Профиль маппинга слоев Контроллеров и Бизнес-логики
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CommitModel, CommitEntity>();
        CreateMap<CommitEntity, CommitModel>()
            .ForMember(dest => dest.Defects, opt => opt.MapFrom(src => new List<DefectModel>()))
            .ForMember(dest => dest.StructuralElements,
                opt => opt.MapFrom((src, trg) => new List<StructuralElementModel>()));
        ;
        
        CreateMap<DefectModel, DefectEntity>();
        CreateMap<DefectEntity, DefectModel>();
        
        CreateMap<StructuralElementModel, StructuralElementEntity>();
        CreateMap<StructuralElementEntity, StructuralElementModel>();
        
        CreateMap<ReportModel, ReportEntity>();
        CreateMap<ReportEntity, ReportModel>();
    }
}