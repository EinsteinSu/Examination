using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using Supeng.Examination.BusinessLayer.Models.TestStatistics;
using Supeng.Office;

namespace Supeng.Examination.BusinessLayer.Commons
{
    public class TestResultDetailExcelTableInfo : IExcelTableInfo<TestResultDetailViewModel>
    {
        public TestResultDetailExcelTableInfo(IList<TestResultDetailViewModel> data)
        {
            Data = data;
        }

        public IList<ExcelTableHead> HeadList
        {
            get
            {
                return new List<ExcelTableHead>
                {
                    new ExcelTableHead {HeadText = "姓名",Width = 50},
                    new ExcelTableHead {HeadText = "网点",Width = 50},
                    new ExcelTableHead {HeadText = "分数",Width = 50},
                };
            }
        }

        public IList<TestResultDetailViewModel> Data { get; }

        public int FillRow(ExcelWorksheet worksheet, int startRow, TestResultDetailViewModel data)
        {
            worksheet.Cells[startRow, 1].Value = data.Name;
            worksheet.Cells[startRow, 2].Value = data.SiteName;
            worksheet.Cells[startRow, 3].Value = data.Score;
            if (data.Score < 60)
            {
                worksheet.Cells[startRow, 3].FillBackgroudColor(Color.Red);
            }
            return startRow;
        }
    }
}
