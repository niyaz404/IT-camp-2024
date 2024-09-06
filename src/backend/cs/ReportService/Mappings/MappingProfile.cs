using AutoMapper;
using ReportService.BLL.Models;
using ReportService.Models;
using StructuralElementTypeDto = ReportService.BLL.Models.StructuralElementTypeDto;

namespace ReportService.Mappings;

/// <summary>
/// Профиль маппинга слоев Контроллеров и Бизнес-логики
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CommitModel, CommitDto>();
        CreateMap<CommitDto, CommitModel>();
        
        CreateMap<DefectModel, DefectDto>();
        CreateMap<DefectDto, DefectModel>();
        
        CreateMap<StructuralElementModel, StructuralElementDto>();
        CreateMap<StructuralElementDto, StructuralElementModel>();
        
        CreateMap<ReportModel, ReportDto>();
        CreateMap<ReportDto, ReportModel>();
    }
}