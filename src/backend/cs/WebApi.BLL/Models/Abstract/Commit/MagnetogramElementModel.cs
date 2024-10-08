﻿using WebApi.BLL.Enums;

namespace WebApi.BLL.Models.Abstract.Commit;

/// <summary>
/// Элемент на магнитограмме
/// </summary>
public abstract class MagnetogramElementModel
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Тип элемента на магнитограмме
    /// </summary>
    public ElementType Type { get; protected set; }
    
    /// <summary>
    /// Х-координата начала области
    /// </summary>
    public int StartXCoordinate { get; set; }
    
    /// <summary>
    /// Х-координата конца области
    /// </summary>
    public int EndXCoordinate { get; set; }
}