using System.Data.Entity;
using Supeng.Examination.Model;

namespace Supeng.Examination.DataAccess
{
    public class ExaDataContext : DbContext
    {
        public ExaDataContext()
            : base("name=ExaDataContext")
        {

        }

        public IDbSet<Question> Questions { get; set; }

        public IDbSet<QuestionCategory> QuestionCategories { get; set; }

        public IDbSet<TestPaper> TestPapers { get; set; }

        public IDbSet<TestPaperFormula> TestPaperFormulas { get; set; }

        public IDbSet<UserProfile> UserProfiles { get; set; }

        public IDbSet<Site> Sites { get; set; }

        public IDbSet<TestPaperQuestion> TestPaperQuestions { get; set; }

        public DbSet<OptionalAnswer> OpitionalAnswers { get; set; }

        public IDbSet<UserTest> UserTests { get; set; }

        public IDbSet<UserAnswer> UserAnswers { get; set; }

        public IDbSet<Test> Tests { get; set; }

        public IDbSet<TechReport> TechReports { get; set; }
        public IDbSet<SecurityAction> SecurityActions { get; set; }

        public IDbSet<SecurityRole> SecurityRoles { get; set; }

        public IDbSet<SecurityRoleAction> SecurityRoleActions { get; set; }


    }
}