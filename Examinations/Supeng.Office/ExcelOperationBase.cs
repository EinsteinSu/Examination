using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Supeng.Office
{
    public class ExcelOperationBase : IDisposable
    {
        private readonly ExcelPackage _excel;
        public ExcelOperationBase()
        {
            _excel = new ExcelPackage();
        }

        public ExcelOperationBase(string fileName)
        {
            var reuslt = new MemoryStream();
            _excel = new ExcelPackage(reuslt, File.OpenRead(fileName));
        }

        public ExcelWorksheets Worksheets
        {
            get { return _excel.Workbook.Worksheets; }
        }

        public ExcelWorksheet CreateSheet(string name, int fontSize = 11, string fontFamily = "Calibri")
        {
            var sheet = _excel.Workbook.Worksheets.Add(name);
            sheet.Cells.Style.Font.Size = fontSize;
            sheet.Cells.Style.Font.Name = fontFamily;
            return sheet;
        }

        public void CreateTable<T>(ExcelWorksheet worksheet, IExcelTableInfo<T> excelTableInfo, int startRow = 1, bool writeHead = true)
        {
            if (writeHead)
            {
                CreateHead(worksheet, excelTableInfo.HeadList, startRow);
                startRow++;
            }
            foreach (T data in excelTableInfo.Data)
            {
                startRow = excelTableInfo.FillRow(worksheet, startRow, data) + 1;
            }
        }

        public void Save(string fileName)
        {
            var buffer = _excel.GetAsByteArray();
            File.WriteAllBytes(fileName, buffer);
        }

        public byte[] GetFileBuffer()
        {
            return _excel.GetAsByteArray();
        }

        private void CreateHead(ExcelWorksheet worksheet, IList<ExcelTableHead> tableHead, int startRow = 1)
        {
            for (int i = 0; i < tableHead.Count; i++)
            {
                var range = worksheet.Cells[startRow, i + 1];
                worksheet.Column(i + 1).Width = tableHead[i].Width;
                range.FillBackgroudColor(tableHead[i].Color);
                range.FillCellBorder(ExcelBorderStyle.Thin);
                range.Value = tableHead[i].HeadText;
            }
        }

        public void Dispose()
        {
            _excel.Dispose();
        }
    }

    public static class ExcelOperationExtensions
    {
        public static void FillBackgroudColor(this ExcelRange range, Color color)
        {
            var fill = range.Style.Fill;
            //todo: custom defein styles
            fill.PatternType = ExcelFillStyle.Solid;
            fill.BackgroundColor.SetColor(color);
        }

        public static void FillCellBorder(this ExcelRange range, ExcelBorderStyle style)
        {
            var boder = range.Style.Border;
            boder.Bottom.Style = boder.Top.Style = boder.Left.Style = boder.Right.Style = style;
        }

        public static void AddHyperLinkText(this ExcelRange range, string hyperLink, string displayText)
        {
            range.Hyperlink = new ExcelHyperLink(hyperLink);
            range.Value = displayText;
            range.Style.Font.UnderLine = true;
            range.Style.Font.Color.SetColor(Color.Blue);
        }

        public static void AddHyperLinkStyle(this ExcelWorkbook wb)
        {
            if (!wb.Styles.NamedStyles.Any(x => x.Name == "Hyperlink"))
            {
                var s = wb.Styles.CreateNamedStyle("Hyperlink");
                s.Style.Font.UnderLine = true;
                s.Style.Font.Color.SetColor(Color.Blue);
            }
        }
    }


}
