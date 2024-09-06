using AutoMapper;
using WebApi.BLL.Models.Implementation.Magnetogram;
using WebApi.BLL.Services.Interface.Magnetogram;
using WebApi.DAL.Models.Implementation.Magnetogram;
using WebApi.DAL.Providers.Interface;

namespace WebApi.BLL.Services.Implementation.Magnetogram;

/// <summary>
/// Сервис для работы с магнитограммами
/// </summary>
public class MagnetogramService : IMagnetogramService
{
    private readonly IProcessingProvider _processingProvider;
    private readonly IMapper _mapper;
    public MagnetogramService(IProcessingProvider processingProvider, IMapper mapper)
    {
        _processingProvider = processingProvider;
        _mapper = mapper;
    }
    public async Task<string> SaveMagnetogram(MagnetogramModel magnetogram)
    {
        return await _processingProvider.SaveMagnetogram(_mapper.Map<MagnetogramEntity>(magnetogram));
    }
}