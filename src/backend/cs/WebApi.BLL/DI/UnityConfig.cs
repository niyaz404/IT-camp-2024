using AutoMapper;
using Unity;
using WebApi.BLL.Services.Implementation.Commit;
using WebApi.BLL.Services.Implementation.Magnetogram;
using WebApi.BLL.Services.Implementation.Report;
using WebApi.BLL.Services.Interface.Commit;
using WebApi.BLL.Services.Interface.Magnetogram;
using WebApi.BLL.Services.Interface.Report;
using WebApi.DAL.Providers.Implementation;
using WebApi.DAL.Providers.Interface;

namespace WebApi.BLL.DI;

/// <summary>
/// Настройка внедрения зависимостей
/// </summary>
public static class UnityConfig
{
    static IUnityContainer Configure(IUnityContainer container)
    {
        container.RegisterType<IMapper, Mapper>();
        
        container.RegisterType<IAuthProvider, AuthProvider>();
        container.RegisterType<IReportProvider, ReportProvider>();
        container.RegisterType<IProcessingProvider, ProcessingProvider>();
        
        container.RegisterType<ICommitService, CommitService>();
        container.RegisterType<IMagnetogramService, MagnetogramService>();
        container.RegisterType<IDefectService, DefectService>();
        container.RegisterType<IStructuralElementService, StructuralElementService>();
        container.RegisterType<IReportService, ReportService>();

        return container;
    }
}