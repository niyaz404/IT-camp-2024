using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.Implementation.Report;

/// <summary>
/// Dto отчета
/// </summary>
public class ReportDto
{
    /// <summary>
    /// Файл отчета
    /// </summary>
    public FileContentResult File { get; set; }
}