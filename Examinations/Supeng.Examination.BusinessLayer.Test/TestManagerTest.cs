using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class TestManagerTest : TestBase
    {
        readonly TestManager _manager = new TestManager();

        [TestMethod]
        public void Create()
        {
            CreateTest("test");
        }

        [TestMethod]
        public void Modify()
        {
            const string name = "Name1";
            var test = CreateTest("test");
            test.Name = name;
            var error = _manager.Modify(test);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            test = Context.Tests.FirstOrDefault(w => w.TestId == test.TestId);
            Assert.IsNotNull(test);
            Assert.AreEqual(name, test.Name);
        }

        /// <summary>
        /// The Test has been started, so we couldn't edit this test
        /// </summary>
        [TestMethod]
        public void Modify_With_Error()
        {
            var test = CreateTest("test", true);
            var error = _manager.Modify(test);
            Assert.IsTrue(!string.IsNullOrEmpty(error));
        }

        [TestMethod]
        public void Delete()
        {
            var test = CreateTest("test");
            var error = _manager.Delete(test.TestId);
            Assert.IsTrue(string.IsNullOrEmpty(error));
        }

        [TestMethod]
        public void SaveSelectedTestUsers()
        {
            const int testerCount = 20;
            var test = GenerateTestWithTester("test", 10);
            var error = _manager.SaveSelectedTestUsers(test.TestId, 0, Context.UserProfiles.Select(s => s.UserId).ToList());
            Assert.IsTrue(string.IsNullOrEmpty(error));
            var count = Context.UserTests.Count(c => c.TestId == test.TestId);
            Assert.IsTrue(count == testerCount);
        }

        [TestMethod]
        public void SelectUserTestListByTestId()
        {
            const int testerCount = 20;
            var test = GenerateTestWithTester("test", 10);
            GenerateUsers(testerCount);
            var error = _manager.SaveSelectedTestUsers(test.TestId, 0, Context.UserProfiles.Select(s => s.UserId).Take(20).ToList());
            Assert.IsTrue(string.IsNullOrEmpty(error));
            var result = _manager.SelectUserTestListByTestId(test.TestId);
            Assert.IsTrue(result.Count == testerCount);
        }

        [TestMethod]
        public void SelectTestDetailViewModel()
        {
            var test = GenerateTestWithTester("test", 10);
            _manager.SaveSelectedTestUsers(test.TestId, 0, Context.UserProfiles.Select(s => s.UserId).ToList());
            var result = _manager.SelecTestDetailViewModel(test.TestId);
            Assert.IsTrue(result.Generated);
        }

        [TestMethod]
        public void SelecTestDetailViewModel_WithOut_Tester()
        {
            var test = CreateTest("test");
            var result = _manager.SelecTestDetailViewModel(test.TestId);
            Assert.IsFalse(result.Generated);
        }
    }
}
