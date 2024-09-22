using AutoMapper;
using WebApi.BLL.Enums;
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

        CreateMap<StructuralElementEntity, StructuralElementModel>()
            .ForMember(dest => dest.Type, opt => opt.Ignore())
            .ForMember(dest => dest.StructuralElementType, opt => opt.MapFrom(src => (StructuralElementType)src.Type.Id));

        CreateMap<StructuralElementModel, StructuralElementEntity>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => new StructuralElementTypeEntity { Id = (int)src.StructuralElementType }));

        CreateMap<CommitEntity, CommitModel>();
        CreateMap<CommitModel, CommitEntity>()
            .ForMember(dest => dest.ProcessedImage, opt => opt.MapFrom(src => Convert2(src.ProcessedImage)
            ));
        ;
        
        CreateMap<MagnetogramEntity, MagnetogramModel>();
        CreateMap<MagnetogramModel, MagnetogramEntity>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.CreatedBy));
        
        CreateMap<ReportEntity, ReportModel>();
        CreateMap<ReportModel, ReportEntity>();
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

    public string Get(StructuralElementModel src)
    {
        return src.StructuralElementType.ToString();
    }
}