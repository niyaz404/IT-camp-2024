﻿using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.Implementation.Report;

/// <summary>
/// Dto отчета
/// </summary>
public class ReportDto
{
    /// <summary>
    /// Идентификатор отчета
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Идентификатор данных о магнитограмме
    /// </summary>
    public string CommitId { get; set; }
    
    /// <summary>
    /// Дата создания отчета
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Файл отчета
    /// </summary>
    public byte[] File { get; set; }
}