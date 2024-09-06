using WebApi.DAL.Models.Implementation.Magnetogram;

namespace WebApi.DAL.Providers.Interface;

/// <summary>
/// Интерфейс провайдера для работы с сервисом обработки
/// </summary>
public interface IProcessingProvider
{
    public Task<string> SaveMagnetogram(MagnetogramEntity magnetogram);
}