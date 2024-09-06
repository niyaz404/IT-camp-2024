using AutoMapper;
using DAL.Models.Implementation.Commit;
using DAL.Models.Implementation.Report;
using ReportService.BLL.Models;

namespace ReportService.BLL.Mappings;

/// <summary>
/// Профиль маппинга слоев Бизнес-логики и Репозиториев
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
        CreateMap<StructuralElementEntity, StructuralElementModel>()
            .ForMember(dest => dest.TypeDto,
                opt => opt.MapFrom(src => new StructuralElementTypeDto { Id = src.TypeId, Name = src.TypeName }));

        
        CreateMap<ReportModel, ReportEntity>();
        CreateMap<ReportEntity, ReportModel>();
    }
}