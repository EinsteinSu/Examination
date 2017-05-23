using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Supeng.Examination.Model.Test
{
    [TestClass]
    public class UserProfileTest
    {
        const string Password = "123$$$";

        [TestMethod]
        public void EncryptPassword()
        {
            var user = new UserProfile { Password = Password };
            user.EncryptPassword();
            Assert.IsTrue(user.Password.Length > 0);
            Console.WriteLine(user.Password);
        }
        [TestMethod]
        public void ComparePassword_Equals()
        {
            var user = new UserProfile { Password = Password };
            user.EncryptPassword();
            Assert.IsTrue(user.Password.Length > 0);
            Assert.IsTrue(user.ComparePassword(Password));
        }

        [TestMethod]
        public void ComparePassword_Not_Equals()
        {
            var user = new UserProfile { Password = Password };
            user.EncryptPassword();
            Assert.IsTrue(user.Password.Length > 0);
            Assert.IsFalse(user.ComparePassword(Password + "$"));
        }
    }
}
