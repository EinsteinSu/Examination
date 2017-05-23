using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class SiteMangerTest : TestBase
    {
        private readonly SiteManager _manager = new SiteManager();

        [TestMethod]
        public void Create()
        {
            var item = Add();
            Assert.IsTrue(item.SiteId > 0);
            Assert.IsTrue(Context.Sites.Any());
        }

        [TestMethod]
        public void Modify()
        {
            const string name = "test1";
            var item = Add();
            item.Name = name;
            var error = _manager.Modify(item);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            item = Context.Sites.FirstOrDefault(f => f.SiteId == item.SiteId);
            Assert.IsNotNull(item);
            Assert.AreEqual(item.Name, name);
        }

        [TestMethod]
        public void Delete()
        {
            var item = Add();
            var error = _manager.Delete(item.SiteId);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            item = Context.Sites.FirstOrDefault(f => f.SiteId == item.SiteId);
            Assert.IsNull(item);
        }

        private Site Add()
        {
            var site = new Site();
            site.SiteCode = "000";
            site.Name = "Test";
            var error = _manager.Create(site);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            return site;
        }
    }
}