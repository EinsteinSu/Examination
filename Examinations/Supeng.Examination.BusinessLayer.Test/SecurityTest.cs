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
    public class SecurityTest : TestBase
    {
        ISecurityActionManager manager = new SecurityActionManager();
        ISecurityRoleManager roleManager = new SecurityRoleManager();
        [TestMethod]
        public void AddSecurityAction()
        {
            var action = AddAction();
            Assert.IsTrue(action.SecurityActionId > 0);
            Assert.IsTrue(manager.SelectList().Any());
        }

        [TestMethod]
        public void ModifySecurityAction()
        {
            const string newName = "Test1";
            var action = AddAction();
            action.Name = newName;
            var error = manager.Modify(action);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            action = Context.SecurityActions.FirstOrDefault(f => f.SecurityActionId == action.SecurityActionId);
            Assert.IsNotNull(action);
            Assert.AreEqual(newName, action.Name);
        }

        [TestMethod]
        public void DeleteSecurityAction()
        {

        }

        private SecurityAction AddAction()
        {
            var action = new SecurityAction();
            action.Name = "Test";
            var error = manager.Create(action);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            return action;
        }
    }
}
