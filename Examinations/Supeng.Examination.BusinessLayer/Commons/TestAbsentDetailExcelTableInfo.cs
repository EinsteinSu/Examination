using System.Collections.Generic;
using System.Drawing;
using OfficeOpenXml;
using Supeng.Examination.BusinessLayer.Models.TestStatistics;
using Supeng.Office;

namespace Supeng.Examination.BusinessLayer.Commons
{
    public class TestAbsentDetailExcelTableInfo : IExcelTableInfo<TestAbsentDetailViewModel>
    {
        public TestAbsentDetailExcelTableInfo(IList<TestAbsentDetailViewModel> data)
        {
            Data = data;
        }

        public IList<ExcelTableHead> HeadList
        {
            get
            {
                return new List<ExcelTableHead>
                {
                    new ExcelTableHead {HeadText = "姓名", Width = 50, Color = Color.Chocolate},
                    new ExcelTableHead {HeadText = "网点", Width = 50, Color = Color.Chocolate}
                };
            }
        }

        public IList<TestAbsentDetailViewModel> Data { get; }

        public int FillRow(ExcelWorksheet worksheet, int startRow, TestAbsentDetailViewModel data)
        {
            worksheet.Cells[startRow, 1].Value = data.Name;
            worksheet.Cells[startRow, 2].Value = data.SiteName;
            return startRow;
        }
    }
}