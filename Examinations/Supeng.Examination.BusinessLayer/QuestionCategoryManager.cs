using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Supeng.Examination.BusinessLayer.Interfaces;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface IQuestionCategoryManager : ICrud<QuestionCategory>, IDisposable
    {
    }

    public class QuestionCategoryManager : IQuestionCategoryManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        public IList<QuestionCategory> SelectList()
        {
            return _context.QuestionCategories.ToList();
        }

        public QuestionCategory SelectById(int id)
        {
            return _context.QuestionCategories.FirstOrDefault(f => f.QuestionCategoryId == id);
        }

        public string Create(QuestionCategory data)
        {
            try
            {
                _context.QuestionCategories.Add(data);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Modify(QuestionCategory data)
        {
            try
            {
                _context.Entry(data).State = EntityState.Modified;
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
                var item = SelectById(id);
                _context.QuestionCategories.Remove(item);
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
    }
}