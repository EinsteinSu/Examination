using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Supeng.Examination.BusinessLayer.Commons;
using Supeng.Examination.BusinessLayer.Models.TestStatistics;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;
using Supeng.Office;

namespace Supeng.Examination.BusinessLayer
{
    public interface IStatisticsManager : IDisposable
    {
        IList<TestResultViewModel> TestResultSelect();

        TestResultViewModel TestResultSelect(int testId);

        IList<TestResultDetailViewModel> TestResultDetailSelect(int testId, int userTestId = 0);

        IList<UserTestResultViewModel> UserTestResultSelect(int userTestId);

        byte[] ExportTestResultDetails(int testId);

        IList<TestAbsentViewModel> TestAbsentSelect();

        IList<TestAbsentDetailViewModel> TestAbsentDetailSelect(int testId);

        byte[] ExportTestAbsentDetails(int testId);

        byte[] ExportUserTestDetals(int userTestId);

        byte[] ExportAllUserTest(int testId);
    }

    public class StatisticsManager : IStatisticsManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        public void Dispose()
        {
            _context.Dispose();
        }

        public IList<TestResultViewModel> TestResultSelect()
        {
            return _context.Database.SqlQuery<TestResultViewModel>("exec TestResultSelect @p0", 0).ToList();
        }

        public TestResultViewModel TestResultSelect(int testId)
        {
            return _context.Database.SqlQuery<TestResultViewModel>("exec TestResultSelect @p0", testId).FirstOrDefault();
        }

        public IList<TestResultDetailViewModel> TestResultDetailSelect(int testId, int userTestId = 0)
        {
            return _context.Database.SqlQuery<TestResultDetailViewModel>
                ("exec TestResultDetailsSelect @p0, @p1",
                    testId, userTestId).ToList();
        }

        public IList<UserTestResultViewModel> UserTestResultSelect(int userTestId)
        {
            return
                _context.Database.SqlQuery<UserTestResultViewModel>
                    ("exec UserTestDetailsSelect @p0", userTestId)
                    .ToList();
        }

        public byte[] ExportTestResultDetails(int testId)
        {
            var test = TestResultSelect(testId);
            if (test == null)
                return new byte[0];
            var list = TestResultDetailSelect(testId);
            using (var excel = new ExcelOperationBase())
            {
                var workSheet = excel.CreateSheet(test.TestName);
                excel.CreateTable(workSheet, new TestResultDetailExcelTableInfo(list));
                return excel.GetFileBuffer();
            }
        }

        public IList<TestAbsentViewModel> TestAbsentSelect()
        {
            return _context.Database.SqlQuery<TestAbsentViewModel>("exec TestAbsentSelect").ToList();
        }

        //todo: to be test
        public IList<TestAbsentDetailViewModel> TestAbsentDetailSelect(int testId)
        {
            return
                _context.Database.SqlQuery<TestAbsentDetailViewModel>
                ("exec TestAbsentDetailsSelect @p0", testId)
                    .ToList();
        }

        public byte[] ExportTestAbsentDetails(int testId)
        {
            var test = TestResultSelect(testId);
            if (test == null)
                return new byte[0];
            var list = TestAbsentDetailSelect(testId);
            using (var excel = new ExcelOperationBase())
            {
                var workSheet = excel.CreateSheet(test.TestName + "缺考考生");
                excel.CreateTable(workSheet, new TestAbsentDetailExcelTableInfo(list));
                return excel.GetFileBuffer();
            }
        }

        public byte[] ExportUserTestDetals(int userTestId)
        {
            var list = UserTestResultSelect(userTestId);
            var userTest = _context.UserTests.Include(i => i.Test).Include(i => i.User).FirstOrDefault(w => w.UserTestId == userTestId);
            if (userTest == null)
            {
                return new byte[0];
            }
            foreach (var result in list)
            {
                result.OptionalAnswers = new List<OptionalAnswer>();
                foreach (var item in _context.OpitionalAnswers.Where(w => w.QuestionId == result.QuestionId))
                {
                    result.OptionalAnswers.Add(item);
                }
            }
            using (var excel = new ExcelOperationBase())
            {
                var workSheet = excel.CreateSheet(string.Format("{0} {1} 考试结果", userTest.User.Name, userTest.Test.Name));
                excel.CreateTable(workSheet, new UserTestResultTableInfo(list));
                return excel.GetFileBuffer();
            }
        }

        public byte[] ExportAllUserTest(int testId)
        {
            var test = TestResultSelect(testId);
            if (test == null)
                return new byte[0];
            var list = TestResultDetailSelect(testId);
            using (var excel = new ExcelOperationBase())
            {
                foreach (var t in list)
                {
                    var userTestList = UserTestResultSelect(t.UserTestId);
                    var userTest =
                        _context.UserTests.Include(i => i.Test)
                            .Include(i => i.User)
                            .FirstOrDefault(w => w.UserTestId == t.UserTestId);
                    if (userTest == null)
                    {
                        return new byte[0];
                    }
                    foreach (var result in userTestList)
                    {
                        result.OptionalAnswers = new List<OptionalAnswer>();
                        foreach (var item in _context.OpitionalAnswers.Where(w => w.QuestionId == result.QuestionId))
                        {
                            result.OptionalAnswers.Add(item);
                        }
                    }
                    var workSheet = excel.CreateSheet(string.Format("{0} {1} 考试结果", userTest.User.Name, userTest.Test.Name));
                    excel.CreateTable(workSheet, new UserTestResultTableInfo(userTestList));
                }
                return excel.GetFileBuffer();
            }
        }
    }
}