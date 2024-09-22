using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using ReportService.BLL.Models;
using ReportService.BLL.Report.Interface;

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
        XFont font = new XFont("Verdana", 8, XFontStyle.Regular);
        
        double tableWidth = page.Width * 0.8; // 80% ширины страницы
        double xOffset = (page.Width - tableWidth) / 2; // Центрирование таблицы
        var rowHeight = 25;

        DrawHeaderTable(commit, gfx, new XPoint(xOffset, 60), tableWidth, font);
        
        gfx.DrawString("Таблица 1 – Конструктивные элементы", font, XBrushes.Black,
            new XRect(xOffset, 180, page.Width, rowHeight), XStringFormats.TopLeft);

        var startPoint = new XPoint(xOffset, 205);

        DrawStructuralElementsTable(commit.StructuralElements, gfx, ref startPoint, font);
        
        startPoint = new XPoint(startPoint.X, startPoint.Y + rowHeight * 2);
        
        gfx.DrawString("Таблица 2 – Дефекты", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, page.Width, rowHeight), XStringFormats.CenterLeft);

        startPoint = new XPoint(startPoint.X, startPoint.Y + rowHeight);

        DrawDefectsTable(commit, gfx, ref startPoint);
        
        gfx.DrawString($"Общее количество дефектов: {commit.Defects.Count}", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y + rowHeight, page.Width, rowHeight), XStringFormats.CenterLeft);

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
        gfx.DrawRectangle(XPens.Black, startPoint.X, y, cellWidth, rowHeight);
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
        gfx.DrawRectangle(XPens.Black, startPoint.X, y, cellWidth, rowHeight);
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
        gfx.DrawRectangle(XPens.Black, startPoint.X, y, cellWidth, rowHeight);
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
        var numberWidth = 15;
        var typeWidth = 150;
        var coordinateWidth = 200;
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
        
        var groupedElements = elements.GroupBy(e => e.Type.Id);
        // Рисуем структурные элементы
        foreach (var group in groupedElements)
        {
            var groupArray = group.ToArray();
            for(int i = 1; i <= groupArray.Length; i++)
            {
                gfx.DrawRectangle(XPens.Black, startPoint.X, startPoint.Y + rowHeight * i, numberWidth, rowHeight);
                gfx.DrawString($"{i}", font, XBrushes.Black,
                    new XRect(startPoint.X, startPoint.Y + rowHeight * i, numberWidth, rowHeight),
                    XStringFormats.Center);
        
                gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth, startPoint.Y + rowHeight * i, typeWidth, rowHeight);
                gfx.DrawString($"{groupArray[i - 1].Type.Name}", font, XBrushes.Black,
                    new XRect(startPoint.X + numberWidth, startPoint.Y + rowHeight * i, typeWidth, rowHeight),
                    XStringFormats.Center);

                gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + typeWidth, startPoint.Y + rowHeight * i, coordinateWidth, rowHeight);
                gfx.DrawString($"{groupArray[i - 1].StartXCoordinate}", font, XBrushes.Black,
                    new XRect(startPoint.X + numberWidth + typeWidth, startPoint.Y + rowHeight * i, coordinateWidth, rowHeight),
                    XStringFormats.Center);
            }

            startPoint = new XPoint(startPoint.X, startPoint.Y + rowHeight * group.Count());
        }
    }
    
    private static void DrawDefectsTable(CommitModel commit, XGraphics gfx, ref XPoint startPoint)
    {
        var rowHeight = 26;
        var numberWidth = 15;
        var cellWidth = 100;
        var font = new XFont("Verdana", 8, XFontStyle.Regular);
        // Рисуем заголов таблицы 
        gfx.DrawRectangle(XPens.Black, startPoint.X, startPoint.Y, numberWidth, rowHeight * 2);
        DrawTextInCell(gfx, "№", font, XBrushes.Black,
            new XRect(startPoint.X, startPoint.Y, numberWidth, rowHeight * 2),
            XStringFormats.BottomCenter);
        
        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth, startPoint.Y, cellWidth, rowHeight * 2);
        DrawTextInCell(gfx, "Всего конструктивных элементов СЛЕВА", font, XBrushes.Black,
            new XRect(startPoint.X + numberWidth, startPoint.Y, cellWidth, rowHeight * 2),
            XStringFormats.Center);

        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth, startPoint.Y, cellWidth, rowHeight * 2);
        DrawTextInCell(gfx, "Всего конструктивных элементов СПРАВА", font, XBrushes.Black,
            new XRect(startPoint.X + numberWidth + cellWidth, startPoint.Y, cellWidth, rowHeight * 2),
            XStringFormats.TopCenter);
        
        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 2, startPoint.Y, cellWidth, rowHeight * 2);
        DrawTextInCell(gfx, "Конструктивный элемент СЛЕВА от дефекта", font, XBrushes.Black,
            new XRect(startPoint.X + numberWidth + cellWidth * 2, startPoint.Y, cellWidth, rowHeight * 2),
            XStringFormats.Center);

        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 3, startPoint.Y, cellWidth, rowHeight * 2);
        DrawTextInCell(gfx, "Конструктивный элемент СПРАВА от дефекта", font, XBrushes.Black,
            new XRect(startPoint.X + numberWidth + cellWidth * 3, startPoint.Y, cellWidth, rowHeight * 2),
            XStringFormats.Center);

        gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 4, startPoint.Y, cellWidth, rowHeight * 2);
        DrawTextInCell(gfx, "Положение на магнитограмме относительно констр. элемента СЛЕВА", font, XBrushes.Black,
            new XRect(startPoint.X + numberWidth + cellWidth * 4, startPoint.Y, cellWidth, rowHeight * 2),
            XStringFormats.Center);

        var deltaHeight = rowHeight * 2;
        /// Рисуем дефекты
        for(int i = 0; i < commit.Defects.Count; i++)
        {
            // Начальные координаты ячеек таблицы в ПДФ
            // Не связаны с координатами дефектов и элементов
            var x = startPoint.X;
            var y = startPoint.Y + deltaHeight;
            
            // Смотрим левые координаты текущего дефекта и структурного элемента
            var elements = commit.StructuralElements
                .Select(d => new { X = d.StartXCoordinate, TypeId = d.Type.Id }).ToList();
            var currentCoordinate = commit.Defects[i].StartXCoordinate;
            
            gfx.DrawRectangle(XPens.Black, startPoint.X, startPoint.Y + deltaHeight, numberWidth, rowHeight * 2);
            gfx.DrawString($"{i + 1}", font, XBrushes.Black,
                new XRect(startPoint.X, startPoint.Y + deltaHeight, numberWidth, rowHeight * 2),
                XStringFormats.Center);

            // По левым координатам ищем количество структурных элементов слева
            var leftElements = elements.Where(e => e.X < currentCoordinate).ToList();
        
            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth, startPoint.Y + deltaHeight, cellWidth, rowHeight * 2);
            gfx.DrawString($"{leftElements.Count()}", font, XBrushes.Black,
                new XRect(startPoint.X + numberWidth, startPoint.Y + deltaHeight, cellWidth, rowHeight * 2),
                XStringFormats.Center);

            // По левым координатам ищем количество структурных элементов справа
            var rightElements = elements.Where(e => e.X > currentCoordinate).ToList();

            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth, startPoint.Y + deltaHeight, cellWidth, rowHeight * 2);
            gfx.DrawString($"{rightElements.Count()}", font, XBrushes.Black,
                new XRect(startPoint.X + numberWidth + cellWidth, startPoint.Y + deltaHeight, cellWidth, rowHeight * 2),
                XStringFormats.Center);

            // По левым координатам ищем тип структурного элемента слева
            var leftWeldseamCount = leftElements.Count(e => e.TypeId == 1);
            var leftBendCount = leftElements.Count(e => e.TypeId == 2);
            var leftBranchingCount = leftElements.Count(e => e.TypeId == 3);
            var leftPatchCount = leftElements.Count(e => e.TypeId == 4);
            var halfRowHeight = rowHeight / 2;
        
            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 2, startPoint.Y + deltaHeight, cellWidth, halfRowHeight);
            gfx.DrawString($"Сварной шов-{leftWeldseamCount}", font, XBrushes.Black,
                new XRect(startPoint.X + numberWidth + cellWidth * 2, startPoint.Y + deltaHeight, cellWidth, halfRowHeight),
                XStringFormats.Center);
            
            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 2, startPoint.Y + deltaHeight + halfRowHeight, cellWidth, halfRowHeight);
            gfx.DrawString($"Изгиб-{leftBendCount}", font, XBrushes.Black,
                new XRect(startPoint.X + numberWidth + cellWidth * 2, startPoint.Y + deltaHeight + halfRowHeight, cellWidth, halfRowHeight),
                XStringFormats.Center);

            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 2, startPoint.Y + deltaHeight +
                rowHeight, cellWidth, halfRowHeight);
            gfx.DrawString($"Ветвление-{leftBranchingCount}", font, XBrushes.Black,
                new XRect(startPoint.X + numberWidth + cellWidth * 2, startPoint.Y + deltaHeight + rowHeight,
                    cellWidth, halfRowHeight),
                XStringFormats.Center);
            
            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 2, startPoint.Y + deltaHeight +
                3 * halfRowHeight, cellWidth, halfRowHeight);
            gfx.DrawString($"Заплатка-{leftPatchCount}", font, XBrushes.Black,
                new XRect(startPoint.X + numberWidth + cellWidth * 2, startPoint.Y + deltaHeight +
                                                                      3 * halfRowHeight, cellWidth, halfRowHeight),
                XStringFormats.Center);
            
            // По левым координатам ищем тип структурного элемента справа
            var rightWeldseamCount = rightElements.Count(e => e.TypeId == 1);
            var rightBendCount = rightElements.Count(e => e.TypeId == 2);
            var rightBranchingCount = rightElements.Count(e => e.TypeId == 3);
            var rightPatchCount = rightElements.Count(e => e.TypeId == 4);
        
            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 3, startPoint.Y + deltaHeight, cellWidth, halfRowHeight);
            gfx.DrawString($"Сварной шов-{rightWeldseamCount}", font, XBrushes.Black,
                new XRect(startPoint.X + numberWidth + cellWidth * 3, startPoint.Y + deltaHeight, cellWidth, halfRowHeight),
                XStringFormats.Center);
            
            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 3, startPoint.Y + deltaHeight + halfRowHeight, cellWidth, halfRowHeight);
            gfx.DrawString($"Изгиб-{rightBendCount}", font, XBrushes.Black,
                new XRect(startPoint.X + numberWidth + cellWidth * 3, startPoint.Y + deltaHeight + halfRowHeight, cellWidth, halfRowHeight),
                XStringFormats.Center);

            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 3, startPoint.Y + deltaHeight +
                rowHeight, cellWidth, halfRowHeight);
            gfx.DrawString($"Ветвление-{rightBranchingCount}", font, XBrushes.Black,
                new XRect(startPoint.X + numberWidth + cellWidth * 3, startPoint.Y + deltaHeight + rowHeight,
                    cellWidth, halfRowHeight),
                XStringFormats.Center);
            
            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 3, startPoint.Y + deltaHeight +
                3 * halfRowHeight, cellWidth, halfRowHeight);
            gfx.DrawString($"Заплатка-{rightPatchCount}", font, XBrushes.Black,
                new XRect(startPoint.X + numberWidth + cellWidth * 3, startPoint.Y + deltaHeight +
                                                                      3 * halfRowHeight, cellWidth, halfRowHeight),
                XStringFormats.Center);

            // По левым координате находим расстояние до констр. элемента слева
            var delta = currentCoordinate - elements.FirstOrDefault(e => e.X < currentCoordinate)?.X ?? 0;
            
            gfx.DrawRectangle(XPens.Black, startPoint.X + numberWidth + cellWidth * 4, startPoint.Y + deltaHeight, cellWidth, rowHeight * 2);
            gfx.DrawString($"{delta}", font, XBrushes.Black,
                new XRect(startPoint.X + numberWidth + cellWidth * 4, startPoint.Y + deltaHeight, cellWidth, rowHeight * 2),
                XStringFormats.Center);

            deltaHeight += rowHeight * 2;
        }

        startPoint = new XPoint(startPoint.X, startPoint.Y + deltaHeight);
    }
    
    private static void DrawTextInCell(XGraphics gfx, string text, XFont font, XBrush brush, XRect rect, XStringFormat format)
    {
        var lines = SplitTextToFitWidth(gfx, text, font, rect.Width);
        double lineHeight = gfx.MeasureString("A", font).Height;
        double totalTextHeight = lineHeight * lines.Count;
    
        // Вычисляем отступ сверху для центрирования текста в ячейке
        double yOffset = rect.Y + (rect.Height - totalTextHeight) / 2;

        foreach (var line in lines)
        {
            gfx.DrawString(line, font, brush, new XRect(rect.X, yOffset, rect.Width, lineHeight), format);
            yOffset += lineHeight;
        }
    }
    
    /// Метод для разбиения текста на строки, чтобы они помещались в ширину ячейки
    private static List<string> SplitTextToFitWidth(XGraphics gfx, string text, XFont font, double maxWidth)
    {
        var words = text.Split(' ');
        var lines = new List<string>();
        var currentLine = "";

        foreach (var word in words)
        {
            var testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
            var size = gfx.MeasureString(testLine, font);

            if (size.Width > maxWidth)
            {
                if (!string.IsNullOrEmpty(currentLine))
                {
                    lines.Add(currentLine);
                }
                currentLine = word;
            }
            else
            {
                currentLine = testLine;
            }
        }

        if (!string.IsNullOrEmpty(currentLine))
        {
            lines.Add(currentLine);
        }

        return lines;
    }
}