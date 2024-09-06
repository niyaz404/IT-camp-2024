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
        CreateMap<CommitModel, CommitEntity>()
            .ForMember(dest => dest.ProcessedImage, opt => opt.MapFrom(src => Convert2(src.ProcessedImage)
            ));;
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
    
    public byte[] Convert2(string s)
    {
        // Преобразование строки Base64 в массив байтов
        if (string.IsNullOrEmpty(s))
        {
            return null;
        }
        try
        {
            return Convert.FromBase64String(s);
        }
        catch (FormatException)
        {
            // Обработка ошибки в случае некорректной строки Base64
            return null;
        }
    }
}