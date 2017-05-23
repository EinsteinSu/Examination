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
    public class QuestionCategoryTest : TestBase
    {
        readonly QuestionCategoryManager _manager = new QuestionCategoryManager();

        [TestMethod]
        public void Create()
        {
            var item = Add();
            Assert.IsTrue(item.QuestionCategoryId > 0);
            Assert.IsTrue(Context.QuestionCategories.Any());
        }

        [TestMethod]
        public void Modify()
        {
            const string name = "Hello1";
            var item = Add();
            item.Name = name;
            var error = _manager.Modify(item);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            item = Context.QuestionCategories.FirstOrDefault(f => f.QuestionCategoryId == item.QuestionCategoryId);
            Assert.IsNotNull(item);
            Assert.AreEqual(item.Name, name);
        }

        [TestMethod]
        public void Delete()
        {
            var item = Add();
            var error = _manager.Delete(item.QuestionCategoryId);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            item = Context.QuestionCategories.FirstOrDefault(f => f.QuestionCategoryId == item.QuestionCategoryId);
            Assert.IsNull(item);
        }

        private QuestionCategory Add()
        {
            var item = new QuestionCategory
            {
                Name = "Hello",
                Description = "This is a script"
            };
            var error = _manager.Create(item);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            return item;
        }
    }
}
