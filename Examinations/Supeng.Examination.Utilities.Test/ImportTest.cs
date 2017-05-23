using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.DataAccess;

namespace Supeng.Examination.Utilities.Test
{
    [TestClass]
    public class ImportTest
    {
        protected readonly ExaDataContext Context = new ExaDataContext();

        [TestCleanup]
        public void Clean()
        {
            Context.Database.ExecuteSqlCommand("Delete from TechReports");
            Context.Database.ExecuteSqlCommand("Delete from SecurityRoleActions");
            Context.Database.ExecuteSqlCommand("Delete from SecurityRoles");
            Context.Database.ExecuteSqlCommand("Delete from SecurityActions");
            Context.Database.ExecuteSqlCommand("Delete from TestPaperQuestions");
            Context.Database.ExecuteSqlCommand("Delete from UserAnswers");
            Context.Database.ExecuteSqlCommand("Delete from UserTests");
            Context.Database.ExecuteSqlCommand("Delete from TestPapers");
            Context.Database.ExecuteSqlCommand("Delete from OptionalAnswers");
            Context.Database.ExecuteSqlCommand("Delete from questions");
            Context.Database.ExecuteSqlCommand("Delete from QuestionCategories");
            Context.Database.ExecuteSqlCommand("Delete from Userprofiles");
            Context.Database.ExecuteSqlCommand("Delete from Sites");
            Context.Database.ExecuteSqlCommand("Delete from tests");
        }

        [TestMethod]
        public void Import()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "Model.xlsx");
            Program.ImportQuestions(file);
        }
    }
}