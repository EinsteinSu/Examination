using System.Collections.Generic;
using System.Drawing;
using OfficeOpenXml;

namespace Supeng.Office
{
    public interface IExcelTableInfo<T>
    {
        IList<ExcelTableHead> HeadList { get; }

        IList<T> Data { get; }

        int FillRow(ExcelWorksheet worksheet, int startRow, T data);
    }

    public class ExcelTableHead
    {
        public ExcelTableHead()
        {
            Color = Color.Gainsboro;
        }

        public string HeadText { get; set; }

        public int Width { get; set; }

        public Color Color { get; set; }

        //todo: add more properties that provide create excel head
    }
}