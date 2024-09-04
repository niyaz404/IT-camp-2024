using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using ReportService.BLL.Models;
using ReportService.BLL.Report.Interface;
using SixLabors.ImageSharp;

namespace ReportService.BLL.Helpers;

public class PdfReportGenerator : IReportGenerator
{
    
    public byte[] GenerateCommitReport(CommitModel commit)
    {
        // Создаем новый PDF-документ
        PdfDocument document = new PdfDocument();
        PdfPage page = document.AddPage();
        XGraphics gfx = XGraphics.FromPdfPage(page);

        // Задаем шрифт
        XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
        
        double tableWidth = page.Width * 0.8; // 80% ширины страницы
        double xOffset = (page.Width - tableWidth) / 2; // Центрирование таблицы

        DrawHeaderTable(commit, gfx, new XPoint(xOffset, 60), tableWidth, font);
        
        gfx.DrawString("Таблица 1 – Конструктивные элементы", font, XBrushes.Black,
            new XRect(xOffset, 180, page.Width, 25), XStringFormats.TopLeft);

        var startPoint = new XPoint(xOffset, 205);

        DrawStructuralElementsTable(commit.StructuralElements, gfx, ref startPoint, font);
        
        startPoint = new XPoint(startPoint.X, startPoint.Y + 25);
        
        gfx.DrawString("Таблица 2 – Дефекты", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, page.Width, 25), XStringFormats.TopLeft);

        startPoint = new XPoint(startPoint.X, startPoint.Y + 25);

        DrawDefectsTable(commit, gfx, ref startPoint, font);
        
        gfx.DrawString($"Общее количество дефектов: {commit.Defects.Count}", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, page.Width, 25), XStringFormats.TopLeft);

        document.Save("f.pdf");
        // Сохранение PDF в поток
        using (MemoryStream stream = new MemoryStream())
        {
            document.Save(stream, false);
            return stream.ToArray();
        }
    }

    private static void DrawHeaderTable(CommitModel commit, XGraphics gfx, XPoint startPoint, double tableWidth, XFont font)
    {
        double tableHeight = 100; // Высота таблицы
        double rowHeight = tableHeight / 4; // Высота строки
        double cellWidth = tableWidth / 2; // Ширина ячейки для строк с двумя ячейками
        var background = new XSolidBrush(XColors.LightSkyBlue);

        // Рисуем первую строку (одна ячейка на всю ширину)
        gfx.DrawRectangle(XPens.Black, startPoint.X, startPoint.Y, tableWidth, rowHeight);
        gfx.DrawString("Проект", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, tableWidth, rowHeight),
            XStringFormats.Center);

        double y = startPoint.Y + rowHeight;
        // Первая ячейка
        gfx.DrawRectangle(background, startPoint.X, y, cellWidth, rowHeight);
        gfx.DrawString($"Название отчета", font, XBrushes.Black,
            new XRect(startPoint.X, y, cellWidth, rowHeight),
            XStringFormats.Center);

        // Вторая ячейка
        gfx.DrawRectangle(XPens.Black, startPoint.X + cellWidth, y, cellWidth, rowHeight);
        gfx.DrawString($"{commit.Name}", font, XBrushes.Black,
            new XRect(startPoint.X + cellWidth, y, cellWidth, rowHeight),
            XStringFormats.Center);
        
        y = startPoint.Y + 2 * rowHeight;
        // Первая ячейка
        gfx.DrawRectangle(background, startPoint.X, y, cellWidth, rowHeight);
        gfx.DrawString($"Эксперт", font, XBrushes.Black,
            new XRect(startPoint.X, y, cellWidth, rowHeight),
            XStringFormats.Center);

        // Вторая ячейка
        gfx.DrawRectangle(XPens.Black, startPoint.X + cellWidth, y, cellWidth, rowHeight);
        gfx.DrawString($"{commit.CreatedBy}", font, XBrushes.Black,
            new XRect(startPoint.X + cellWidth, y, cellWidth, rowHeight),
            XStringFormats.Center);
        
        y = startPoint.Y + 3 * rowHeight;
        // Первая ячейка
        gfx.DrawRectangle(background, startPoint.X, y, cellWidth, rowHeight);
        gfx.DrawString($"Дата", font, XBrushes.Black,
            new XRect(startPoint.X, y, cellWidth, rowHeight),
            XStringFormats.Center);

        // Вторая ячейка
        gfx.DrawRectangle(XPens.Black, startPoint.X + cellWidth, y, cellWidth, rowHeight);
        gfx.DrawString($"{commit.CreatedAt:yy.MM.yyyy hh:mm}", font, XBrushes.Black,
            new XRect(startPoint.X + cellWidth, y, cellWidth, rowHeight),
            XStringFormats.Center);
    }

    private static void DrawStructuralElementsTable(IEnumerable<StructuralElementModel> elements, XGraphics gfx, ref XPoint startPoint,
        XFont font)
    {
        var rowHeight = 25;
        var numberWidth = 30;
        var typeWidth = 150;
        var coordinateWidth = 180;
        // Рисуем заголов таблицы 
        gfx.DrawRectangle(XPens.Black, startPoint.X, startPoint.Y, numberWidth, rowHeight);
        gfx.DrawString("№", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
            XStringFormats.Center);
        
        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth, startPoint.Y, typeWidth, rowHeight);
        gfx.DrawString("Тип", font, XBrushes.Black,
            new XRect(startPoint.X + numberWidth, startPoint.Y, typeWidth, rowHeight),
            XStringFormats.Center);

        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + typeWidth, startPoint.Y, coordinateWidth, rowHeight);
        gfx.DrawString("Положение на магнитограмме", font, XBrushes.Black,
            new XRect(startPoint.X + numberWidth + typeWidth, startPoint.Y, coordinateWidth, rowHeight),
            XStringFormats.Center);
        
        var groupedElements = elements.GroupBy(e => e.StructuralElementTypeId);
        // Рисуем структурные элементы
        foreach (var group in groupedElements)
        {
            var groupArray = group.ToArray();
            for(int i = 1; i <= groupArray.Length; i++)
            {
                gfx.DrawRectangle(XPens.Black, startPoint.X, startPoint.Y + 50 * i, numberWidth, rowHeight);
                gfx.DrawString($"{i}", font, XBrushes.Black,
                    new XRect(startPoint.X, startPoint.Y + 50 * i, numberWidth, rowHeight),
                    XStringFormats.Center);
        
                gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth, startPoint.Y + 50 * i, typeWidth, rowHeight);
                gfx.DrawString($"{groupArray[i - 1].StructuralElementTypeId}", font, XBrushes.Black,
                    new XRect(startPoint.X + numberWidth, startPoint.Y + 50 * i, typeWidth, rowHeight),
                    XStringFormats.Center);

                gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + typeWidth, startPoint.Y + 50 * i, coordinateWidth, rowHeight);
                gfx.DrawString($"{groupArray[i - 1].StartXCoordinate}", font, XBrushes.Black,
                    new XRect(startPoint.X + numberWidth + typeWidth, startPoint.Y + 50 * i, coordinateWidth, rowHeight),
                    XStringFormats.Center);
            }
        }

        startPoint = new XPoint(startPoint.X, startPoint.Y + rowHeight * (elements.Count() + 1));
    }
    
    private static void DrawDefectsTable(CommitModel commit, XGraphics gfx, ref XPoint startPoint, XFont font)
    {
        var rowHeight = 25;
        var numberWidth = 80;
        var cellWidth = 150;
        // Рисуем заголов таблицы 
        gfx.DrawRectangle(XPens.Black, startPoint.X, startPoint.Y, numberWidth, rowHeight);
        gfx.DrawString("№", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
            XStringFormats.Center);
        
        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth, startPoint.Y, cellWidth, rowHeight);
        gfx.DrawString("Всего конструктивных элементов СЛЕВА", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
            XStringFormats.Center);

        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth, startPoint.Y, cellWidth, rowHeight);
        gfx.DrawString("Всего конструктивных элементов СПРАВА", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
            XStringFormats.Center);
        
        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 2, startPoint.Y, cellWidth, rowHeight);
        gfx.DrawString("Конструктивный элемент СЛЕВА от дефекта", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
            XStringFormats.Center);

        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 3, startPoint.Y, cellWidth, rowHeight);
        gfx.DrawString("Конструктивный элемент СПРАВА от дефекта", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
            XStringFormats.Center);

        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 4, startPoint.Y, cellWidth, rowHeight);
        gfx.DrawString("Положение на магнитограмме относительно констр. элемента СЛЕВА", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
            XStringFormats.Center);

        var deltaHeight = rowHeight;
        /// Рисуем дефекты
        for(int i = 0; i < commit.Defects.Count; i++)
        {
            // Начальные координаты ячеек таблицы в ПДФ
            // Не связаны с координатами дефектов и элементов
            var x = startPoint.X;
            var y = startPoint.Y + deltaHeight;
            
            // Смотрим левые координаты текущего дефекта и структурного элемента
            var elements = commit.StructuralElements
                .Select(d => new { X = d.StartXCoordinate, d.StructuralElementTypeId }).ToList();
            var currentCoordinate = commit.Defects[i].StartXCoordinate;
            
            gfx.DrawRectangle(XPens.Black, startPoint.X, startPoint.Y + deltaHeight, numberWidth, rowHeight);
            gfx.DrawString($"{i}", font, XBrushes.Black,
                new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
                XStringFormats.Center);

            // По левым координатам ищем количество структурных элементов слева
            var leftCount = elements.Count(e => e.X < currentCoordinate);
        
            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth, startPoint.Y, cellWidth, rowHeight);
            gfx.DrawString($"{leftCount}", font, XBrushes.Black,
                new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
                XStringFormats.Center);

            // По левым координатам ищем количество структурных элементов справа
            var rightCount = elements.Count(e => e.X > currentCoordinate);

            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth, startPoint.Y, cellWidth, rowHeight);
            gfx.DrawString($"{rightCount}", font, XBrushes.Black,
                new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
                XStringFormats.Center);

            // По левым координатам ищем тип структурного элемента слева
            var leftType = elements.FirstOrDefault(e => e.X < currentCoordinate)?.StructuralElementTypeId;
        
            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 2, startPoint.Y, cellWidth, rowHeight);
            gfx.DrawString($"{leftType}", font, XBrushes.Black,
                new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
                XStringFormats.Center);
            
            // По левым координатам ищем тип структурного элемента справа
            var rightType = elements.FirstOrDefault(e => e.X > currentCoordinate)?.StructuralElementTypeId;

            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 3, startPoint.Y, cellWidth, rowHeight);
            gfx.DrawString($"{rightCount}", font, XBrushes.Black,
                new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
                XStringFormats.Center);

            // По левым координате находим расстояние до констр. элемента слева
            var delta = currentCoordinate - elements.FirstOrDefault(e => e.X < currentCoordinate)?.X ?? 0;
            
            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 4, startPoint.Y, cellWidth, rowHeight);
            gfx.DrawString($"{delta}", font, XBrushes.Black,
                new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight),
                XStringFormats.Center);

            deltaHeight += rowHeight * (i + 1);
        }

        startPoint = new XPoint(startPoint.X, startPoint.Y + deltaHeight);
    }
}