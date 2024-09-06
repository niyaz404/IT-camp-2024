using AutoMapper;
using WebApi.BLL.Models.Implementation.Report;
using WebApi.BLL.Services.Interface.Report;
using WebApi.DAL.Providers.Interface;

namespace WebApi.BLL.Services.Implementation.Report;

public class ReportService : IReportService
{
    private readonly IReportProvider _reportProvider;
    private readonly IMapper _mapper;
    public ReportService(IReportProvider reportProvider, IMapper mapper)
    {
        _reportProvider = reportProvider;
        _mapper = mapper;
    }
    public async Task<ReportModel> Get(string commitId)
    {
        var reportEntity = await _reportProvider.GetReport(commitId);
        return _mapper.Map<ReportModel>(reportEntity);
    }
}