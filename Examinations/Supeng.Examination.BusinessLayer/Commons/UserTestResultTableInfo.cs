using System.Collections.Generic;
using System.Drawing;
using OfficeOpenXml;
using Supeng.Examination.BusinessLayer.Models.TestStatistics;
using Supeng.Examination.Model;
using Supeng.Office;

namespace Supeng.Examination.BusinessLayer.Commons
{
    public class UserTestResultTableInfo : IExcelTableInfo<UserTestResultViewModel>
    {
        public UserTestResultTableInfo(IList<UserTestResultViewModel> data)
        {
            Data = data;
        }

        public IList<ExcelTableHead> HeadList
        {
            get
            {
                return new List<ExcelTableHead>
                {
                    new ExcelTableHead {HeadText = "序号", Width = 10},
                    new ExcelTableHead {HeadText = "试题", Width = 100},
                    new ExcelTableHead {HeadText = "正确答案", Width = 30},
                    new ExcelTableHead {HeadText = "考生答案", Width = 30}
                };
            }
        }

        public IList<UserTestResultViewModel> Data { get; }

        public int FillRow(ExcelWorksheet worksheet, int startRow, UserTestResultViewModel data)
        {
            worksheet.Cells[startRow, 1].Value = data.Sequence;
            worksheet.Cells[startRow, 2].Value = data.QuestionContent;
            worksheet.Cells[startRow, 2].Style.WrapText = true;
            worksheet.Cells[startRow, 3].Value = data.CorrectAnswer;
            worksheet.Cells[startRow, 4].Value = data.Answer;
            if (!string.IsNullOrEmpty(data.Answer) && UserAnswerHelper.CanGetScore(data.Answer, data.CorrectAnswer))
            {
                worksheet.Cells[startRow, 4].FillBackgroudColor(Color.Green);
            }
            else
            {
                worksheet.Cells[startRow, 4].FillBackgroudColor(Color.Red);
            }
            foreach (var answer in data.OptionalAnswers)
            {
                startRow++;
                worksheet.Cells[startRow, 2].Value = string.Format("{0}. {1}", answer.OrderNumber,
                    answer.Content);
            }
            return startRow;
        }
    }
}