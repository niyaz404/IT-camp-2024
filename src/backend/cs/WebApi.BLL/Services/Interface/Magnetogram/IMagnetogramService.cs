namespace WebApi.BLL.Services.Interface.Magnetogram;

public interface IMagnetogramService
{
    Task SaveMagnetogram(Models.Implementation.Magnetogram.MagnetogramModel magnetogramModel);
}