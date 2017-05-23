using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class TechReportTest : TestBase
    {
        private readonly TechReportManager manager = new TechReportManager();
        [TestMethod]
        public void Create()
        {
            GenerateUsers(10);
            var tech = new TechReport();
            tech.Subject = "test";
            tech.Description = "hello";
            var firstOrDefault = Context.UserProfiles.FirstOrDefault();
            if (firstOrDefault != null)
                tech.UserId = firstOrDefault.UserId;
            var error = manager.Create(tech);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            Assert.IsTrue(Context.TechReports.Any());
        }
    }
}
