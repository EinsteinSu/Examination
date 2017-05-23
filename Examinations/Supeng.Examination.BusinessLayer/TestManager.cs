using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Supeng.Examination.BusinessLayer.Interfaces;
using Supeng.Examination.BusinessLayer.Models;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface ITestManager : ICrud<Test>, IDisposable
    {
        TestDetailViewModel SelecTestDetailViewModel(int testId);

        IList<UserTest> SelectUserTestListByTestId(int testId);

        string SaveSelectedTestUsers(int testId, int siteId, IList<int> users);
    }

    public class TestManager : ITestManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        #region Test Crud
        public IList<Test> SelectList()
        {
            return _context.Tests.OrderByDescending(o=>o.StartTime).ToList();
        }

        public Test SelectById(int id)
        {
            return _context.Tests.FirstOrDefault(f => f.TestId == id);
        }

        public string Create(Test data)
        {
            try
            {
                _context.Tests.Add(data);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Modify(Test data)
        {
            if (DateTime.Now >= data.StartTime)
            {
                return "考试已经开始,或过期,不能修改该数据.";
            }
            try
            {
                _context.Entry(data).State = EntityState.Modified;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(int id)
        {
            try
            {
                var item = SelectById(id);
                if (item == null)
                {
                    throw new KeyNotFoundException("Not Found");
                }
                if (DateTime.Now >= item.StartTime)
                {
                    return "考试已经开始,或过期,不能修改该数据.";
                }
                _context.Tests.Remove(item);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }

        public TestDetailViewModel SelecTestDetailViewModel(int testId)
        {

            var vm = new TestDetailViewModel
            {
                TestId = testId,
                Generated = _context.UserTests.Any(w => w.TestId == testId)
            };
            var item = _context.Tests.FirstOrDefault(f => f.TestId == testId);
            if (item != null)
            {
                vm.CanEdit = !item.TestDone;
            }
            else
            {
                vm.CanEdit = true;
            }
            return vm;
        }

        //todo: to be test
        public IList<UserTest> SelectUserTestListByTestId(int testId)
        {
            return _context.UserTests.Where(w => w.TestId == testId).ToList();
        }

        /// <summary>
        /// Save selected testers
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="siteId">Can be 0, 0 means clear all of users</param>
        /// <param name="users"></param>
        /// <returns></returns>
        public string SaveSelectedTestUsers(int testId, int siteId, IList<int> users)
        {
            try
            {
                _context.Database.ExecuteSqlCommand("exec dbo.TestUserSave @p0, @p1, @p2", testId, siteId,
                    string.Join(",", users));
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}