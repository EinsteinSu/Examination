using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;
using Supeng.Office;

namespace Supeng.Examination.Utilities
{
    public class Program
    {
        private static readonly ExaDataContext Context = new ExaDataContext();

        private static void Main(string[] args)
        {
            //ImportQuestions("D:\\import\\xjgl.xlsx");
            //return;
            if (args != null && args.Length > 0)
            {
                var key = args[0].ToLower();
                switch (key)
                {
                    case "-q":
                        InsertFromDocToExcel(int.Parse(args[1]), args[2], args[3]);
                        break;
                    case "-u":
                        ImportUser(args[1]);
                        break;
                    case "-i":
                        ImportQuestions(args[1]);
                        break;
                    case "-s":
                        ImportSite(args[1]);
                        break;
                    case "-t":
                        Test();
                        break;
                }
            }
        }

        static void Test()
        {
            foreach (var userProfile in Context.UserProfiles)
            {
                Console.WriteLine(userProfile.Name);
            }
        }

        #region import from doc
        public static void InsertFromDocToExcel(int questionType, string fileName, string targetFileName)
        {
            if (File.Exists(targetFileName))
            {
                File.Delete(targetFileName);
            }
            using (var excel = new ExcelPackage(new FileInfo(targetFileName)))
            {
                var ws = excel.Workbook.Worksheets.Add("Import Questions");
                using (var doc = new DocXWordOperation(fileName))
                {
                    int row = 0;
                    int optionCount = 0;
                    int answerType = 0;
                    foreach (var paragraph in doc.Doc.Paragraphs)
                    {
                        var line = paragraph.Text;
                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        if (IsMultiplyAnswers(line) < 2)
                        {
                            answerType = IsMultiplyAnswers(line);
                        }

                        if (IsQuestionContent(line))
                        {
                            row++;
                            ws.Cells[row, 1].Value = answerType;
                            ws.Cells[row, 2].Value = questionType;
                            ws.Cells[row, 3].Value = GetQuestionContent(line);
                            ws.Cells[row, 4].Value = 2;
                        }
                        else if (IsOptionalAnswer(line))
                        {
                            row++;
                            optionCount++;
                            ws.Cells[row, 2].Value = GetOptionalAnswerNumber(line);
                            ws.Cells[row, 3].Value = GetOptionalContent(line);
                        }
                        else if (IsAnswers(line))
                        {
                            row -= optionCount;
                            ws.Cells[row, 5].Value = GetAnswer(line);
                            row += optionCount;
                            optionCount = 0;
                        }
                    }
                    excel.Save();
                }
            }
        }

        #region question conditions

        static int IsMultiplyAnswers(string text)
        {
            if (text.Contains("多选"))
            {
                return 1;
            }
            if (text.Contains("单选"))
            {
                return 0;
            }
            return 2;
        }

        static bool IsQuestionContent(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && text.Length > 1)
            {
                return IsNumeric(text.Substring(0, 1));
            }
            return false;
        }

        static string GetQuestionContent(string text)
        {
            int i = 0;
            foreach (var c in text)
            {
                if (IsNumeric(c.ToString()) || c.Equals('.'))
                {
                    i++;
                }
            }
            return text.Substring(i, text.Length - i);
        }
        #endregion

        #region Optional Answer Conditions

        static bool IsOptionalAnswer(string text)
        {
            return OptionalAnswers.Contains(text.Substring(0, 1));
        }

        static string GetOptionalAnswerNumber(string text)
        {
            return text.Substring(0, 1);
        }

        static string GetOptionalContent(string text)
        {
            try
            {
                return text.Substring(2, text.Length - 2).Trim();
            }
            catch (Exception)
            {
                Console.WriteLine("Can not analyze text content");
            }
            return string.Empty;
        }
        #endregion

        #region Answers

        static bool IsAnswers(string text)
        {
            return text.Contains("标准答案");
        }

        static string GetAnswer(string text)
        {
            var answer = text.Replace("标准答案", "").Trim();
            if (answer.Length > 1)
            {
                string.Join(",", answer.ToList());
            }
            return answer.Replace("：", "").Replace(".", "");
        }
        #endregion

        static bool IsNumeric(string str)
        {
            int num;
            bool isNum = int.TryParse(str, out num);
            if (isNum)
            {
                return true;
            }
            return false;
        }




        static string[] OptionalAnswers = new string[] { "A", "B", "C", "D", "E" };
        #endregion

        #region import questions
        public static void ImportQuestions(string fileName)
        {
            if (!Context.QuestionCategories.Any())
            {
                GenerateQuestionCategory();
            }
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Could not found the file");
                return;
            }
            using (ExcelPackage excel = new ExcelPackage(new FileInfo(fileName)))
            {
                var ws = excel.Workbook.Worksheets[1];
                if (ws != null)
                {
                    int doubleBlank = 0;
                    int row = 1;
                    while (true)
                    {
                        if (doubleBlank > 1)
                        {
                            break;
                        }
                        var value = ws.Cells[row, 1].Value;
                        //this is parent row
                        if (value != null)
                        {
                            var q = new Question
                            {
                                Type = (QuestionType)int.Parse(value.ToString()),
                                Content = ws.Cells[row, 3].Value.ToString(),
                                Score = int.Parse(ws.Cells[row, 4].Value.ToString()),
                                CorrectAnswer = ws.Cells[row, 5].Value.ToString(),
                                OptionalAnswers = new List<OptionalAnswer>()
                            };
                            q.CategoryId = FindCategoryId(int.Parse(ws.Cells[row, 2].Value.ToString()));
                            row++;
                            while (true)
                            {
                                value = ws.Cells[row, 1].Value;
                                //this is parent row break
                                if (value != null)
                                {
                                    break;
                                }
                                //process child row
                                if (ws.Cells[row, 2].Value != null)
                                {
                                    OptionalAnswer answer = new OptionalAnswer
                                    {
                                        OrderNumber = ws.Cells[row, 2].Value.ToString(),
                                        Content = ws.Cells[row, 3].Value.ToString()
                                    };
                                    q.OptionalAnswers.Add(answer);
                                    row++;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            Context.Questions.Add(q);
                        }
                        else
                        {
                            doubleBlank++;
                        }
                    }
                }
            }
            Context.SaveChanges();
        }

        private static int FindCategoryId(int id)
        {
            switch (id)
            {
                case 1:
                    return Context.QuestionCategories.FirstOrDefault(f => f.Name.Equals("反洗钱")).QuestionCategoryId;
                case 2:
                    return Context.QuestionCategories.FirstOrDefault(f => f.Name.Equals("柜面业务")).QuestionCategoryId;
                case 3:
                    return Context.QuestionCategories.FirstOrDefault(f => f.Name.Equals("安全保卫")).QuestionCategoryId;
                case 4:
                    return Context.QuestionCategories.FirstOrDefault(f => f.Name.Equals("会计达标")).QuestionCategoryId;
                case 5:
                    return Context.QuestionCategories.FirstOrDefault(f => f.Name.Equals("中间业务")).QuestionCategoryId;
                case 6:
                    return Context.QuestionCategories.FirstOrDefault(f => f.Name.Equals("支付结算业务")).QuestionCategoryId;
                default:
                    return 7;
            }
        }

        private static void GenerateQuestionCategory()
        {
            var categories = "反洗钱,柜面业务,安全保卫,会计达标,中间业务,支付结算业务";
            foreach (var s in categories.Split(','))
            {
                var c = new QuestionCategory { Name = s };
                Context.QuestionCategories.Add(c);
            }
            Context.SaveChanges();
        }

        private static void GenerateQuestions(int count)
        {
            var categories = "反洗钱,柜面业务,安全保卫,会计达标".Split(',');
            GenerateQuestionCategory();
            for (int j = 0; j < 4; j++)
            {
                for (var i = 0; i < count; i++)
                {
                    Context.Questions.Add(new Question
                    {
                        Content =
                            string.Format(
                                "Question {0}: Hello This is question {0}, question category is {1}. \r\nPlease choose an answer and click ok submit your answer. \r\n for multiple answer please choose one or more answers and submit your answer complete your test.",
                                i + 1, categories[j]),
                        Type = i % 2 == 0 ? QuestionType.单选 : QuestionType.多选,
                        Score = 2,
                        CategoryId = FindCategoryId(j + 1),
                        CorrectAnswer = i % 2 == 0 ? "A" : "A,C,D",
                        OptionalAnswers = new List<OptionalAnswer>
                    {
                        new OptionalAnswer {OrderNumber = "A", Content = "Answer A"},
                        new OptionalAnswer {OrderNumber = "B", Content = "Answer B"},
                        new OptionalAnswer {OrderNumber = "C", Content = "Answer C"},
                        new OptionalAnswer {OrderNumber = "D", Content = "Answer D"}
                    }
                    });
                }
            }

            Context.SaveChanges();
        }
        #endregion

        static void ImportSite(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Could not found the file");
                return;
            }
            using (ExcelPackage excel = new ExcelPackage(new FileInfo(fileName)))
            {
                var ws = excel.Workbook.Worksheets[1];
                if (ws != null)
                {
                    int row = 0;
                    while (true)
                    {
                        row++;
                        if (ws.Cells[row, 1].Value == null)
                        {
                            break;
                        }
                        var site = new Site
                        {
                            SiteCode = ws.Cells[row, 2].Value.ToString(),
                            Name = ws.Cells[row, 3].Value.ToString()
                        };
                        Context.Sites.Add(site);
                    }
                    Context.SaveChanges();
                }
            }
        }

        static void ImportUser(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Could not found the file");
                return;
            }
            using (ExcelPackage excel = new ExcelPackage(new FileInfo(fileName)))
            {
                var ws = excel.Workbook.Worksheets[1];
                if (ws != null)
                {
                    int row = 0;
                    while (true)
                    {
                        row++;
                        if (ws.Cells[row, 1].Value == null)
                        {
                            break;
                        }
                        var user = new UserProfile
                        {
                            UserCode = ws.Cells[row, 1].Value.ToString(),
                            Name = ws.Cells[row, 2].Value.ToString(),
                            Mobile = ws.Cells[row, 4].Value.ToString(),
                            SiteId = int.Parse(ws.Cells[row, 5].Value.ToString())
                        };
                        user.Gender = ws.Cells[row, 3].Value.ToString().Equals("男") ?
                            Gender.男 : Gender.女;
                        user.Password = "123";
                        user.EncryptPassword();
                        Context.UserProfiles.Add(user);
                    }
                    Context.SaveChanges();
                }
            }
        }
    }
}