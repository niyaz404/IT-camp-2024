using System.Buffers.Text;
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

        CreateMap<MagnetogramDto, MagnetogramModel>()
            .ForMember(dest => dest.File,
                opt => opt.MapFrom(src => ConvertFormFileToBase64(src.File)));
        CreateMap<MagnetogramModel, MagnetogramDto>();
        
        CreateMap<ReportDto, ReportModel>();
        CreateMap<ReportModel, ReportDto>();
    }
    
    public byte[] ConvertFormFileToByteArray(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
            return null;

        using (var memoryStream = new MemoryStream())
        {
            formFile.CopyTo(memoryStream); // Синхронный вызов
            return memoryStream.ToArray();
        }
    }
    
    public static string ConvertFormFileToBase64(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
        {
            throw new ArgumentException("File is empty or null");
        }

        // Читаем содержимое IFormFile в байтовый массив
        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            formFile.CopyTo(memoryStream);
            fileBytes = memoryStream.ToArray();
        }

        // Преобразуем байты в строку Base64
        return Convert.ToBase64String(fileBytes);
    }

}