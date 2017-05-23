using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Supeng.Common;
using Supeng.Examination.BusinessLayer.Models;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface IUserUserTestManager : IDisposable
    {
        IList<UserTest> SelectUserTestByUserId(int userId);

        IList<UserTestQuestionViewModel> SelectQuestionListByUserTestId(int userTestId);

        UserTestQuestionViewModel SelecTestQuestionViewModelByQuestionId(int userTestId, int questionId);

        Test SelectTestByUserTestId(int userTestId);

        string AnswerQuestion(int userTestId, int questionId, string answer);
    }

    public class UserTestManager : IUserUserTestManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        public IList<UserTest> SelectUserTestByUserId(int userId)
        {
            //_context.Database.ExecuteSqlCommand("UserTestStatusUpdate @p0", userId);
            var list = _context.UserTests.Include(i => i.UserAnswers).Where(
                w => w.UserId == userId && 
                DateTime.Now >= w.Test.StartTime).ToList();
            return list;
        }

        public IList<UserTestQuestionViewModel> SelectQuestionListByUserTestId(int userTestId)
        {
            var result = from q in _context.UserAnswers
                         where q.UserTest.UserTestId == userTestId
                         select new UserTestQuestionViewModel
                         {
                             QuestionId = q.QuestionId,
                             Answer = q.Answer,
                             Content = q.Question.Content,
                             //for security aviod somebody catch the data stream
                             //CorrectAnswer = q.Question.CorrectAnswer,
                             UserTestId = q.UserTestId,
                             //OptionalAnswers = q.Question.OptionalAnswers,
                             Score = q.Question.Score,
                             Type = q.Question.Type
                         };

            return result.ToList();
        }

        public UserTestQuestionViewModel SelecTestQuestionViewModelByQuestionId(int userTestId, int questionId)
        {
            var result = from q in _context.UserAnswers
                         where q.UserTest.UserTestId == userTestId && q.QuestionId == questionId
                         select new UserTestQuestionViewModel
                         {
                             QuestionId = q.QuestionId,
                             Answer = q.Answer,
                             UserTestId = q.UserTestId,
                             Content = q.Question.Content,
                             //CorrectAnswer = q.Question.CorrectAnswer,
                             OptionalAnswers = q.Question.OptionalAnswers,
                             Score = q.Question.Score,
                             Type = q.Question.Type
                         };
            return result.FirstOrDefault();
        }

        public Test SelectTestByUserTestId(int userTestId)
        {
            var firstOrDefault = _context.UserTests.FirstOrDefault(f => f.UserTestId == userTestId);
            return firstOrDefault.Test;
        }

        //todo: to be test
        public string AnswerQuestion(int userTestId, int questionId, string answer)
        {
            try
            {
                _context.Database.ExecuteSqlCommand("UserAnswerUpdate @p0, @p1, @p2",
                    userTestId, questionId, answer);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}