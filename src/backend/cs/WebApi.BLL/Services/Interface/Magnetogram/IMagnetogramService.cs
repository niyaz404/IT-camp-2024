namespace WebApi.BLL.Services.Interface.Magnetogram;

/// <summary>
/// Интерфейс
/// </summary>
public interface IMagnetogramService
{
    public Task<string> SaveMagnetogram(Models.Implementation.Magnetogram.MagnetogramModel magnetogramModel);
}