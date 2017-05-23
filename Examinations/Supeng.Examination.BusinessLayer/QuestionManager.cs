using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Supeng.Common;
using Supeng.Examination.BusinessLayer.Interfaces;
using Supeng.Examination.BusinessLayer.Models;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface IQuestionManager : ICrud<Question>, IPaging, IDisposable
    {

        IList<OptionalAnswer> SelectOptionalAnswers(int questionId);

        string CreateOptionalAnswer(int questionId, OptionalAnswer answer);

        string ModifyOptionalAnswer(OptionalAnswer answer);

        string DeleteOptionalAnswer(int answerId);
    }

    public class QuestionManager : IQuestionManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        #region Question Crud
        public IList<Question> SelectList()
        {
            return _context.Questions.Where(w => !w.Deleted)
                .OrderByDescending(o=>o.QuestionId).ToList();
        }

        public Question SelectById(int id)
        {
            return _context.Questions.FirstOrDefault(f => f.QuestionId == id);
        }

        public string Create(Question question)
        {
            try
            {
                _context.Questions.Add(question);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Modify(Question question)
        {
            try
            {
                _context.Entry(question).State = EntityState.Modified;
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
                var original = SelectById(id);
                if (original == null)
                {
                    throw new KeyNotFoundException("Not found");
                }
                original.Deleted = true;
                _context.Entry(original).Property(p => p.Deleted).IsModified = true;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion


        public IList<OptionalAnswer> SelectOptionalAnswers(int questionId)
        {
            return _context.OpitionalAnswers.Where(w => w.QuestionId == questionId).ToList();
        }

        public string CreateOptionalAnswer(int questionId, OptionalAnswer answer)
        {
            try
            {
                answer.QuestionId = questionId;
                _context.OpitionalAnswers.Add(answer);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ModifyOptionalAnswer(OptionalAnswer answer)
        {
            try
            {
                _context.Entry(answer).State = EntityState.Modified;
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteOptionalAnswer(int answerId)
        {
            try
            {
                var answer = _context.OpitionalAnswers.FirstOrDefault(f => f.AnswerId == answerId);
                if (answer == null)
                {
                    throw new KeyNotFoundException("Optional answer could not found.");
                }
                _context.OpitionalAnswers.Remove(answer);
                _context.SaveChanges();
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

        public IQueryable Model
        {
            get { return _context.Questions; }
        }
    }
}