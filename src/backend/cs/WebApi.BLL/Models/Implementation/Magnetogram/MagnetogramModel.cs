﻿namespace WebApi.BLL.Models.Implementation.Magnetogram;

/// <summary>
/// Модель магнитограммы
/// </summary>
public class MagnetogramModel
{
    /// <summary>
    /// Название магнитограммы
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// ФИО загрузившего
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Время сохранения магнитограммы
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Файл магнитограммы в формате .pkl
    /// </summary>
    public string File { get; set; }
}