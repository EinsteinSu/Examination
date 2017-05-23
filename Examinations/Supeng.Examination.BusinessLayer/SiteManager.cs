using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Supeng.Examination.BusinessLayer.Interfaces;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface ISiteManager : ICrud<Site>, IDisposable
    {
        IList<Site> SelectSitesIncludeEmpty();
    }

    public class SiteManager : ISiteManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();

        public IList<Site> SelectList()
        {
            return _context.Sites.ToList();
        }

        public Site SelectById(int id)
        {
            return _context.Sites.FirstOrDefault(f => f.SiteId == id);
        }

        public string Create(Site data)
        {
            try
            {
                _context.Sites.Add(data);
                _context.SaveChanges();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Modify(Site data)
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
                _context.Sites.Remove(item);
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

        public IList<Site> SelectSitesIncludeEmpty()
        {
            var list = new List<Site>();
            list.Add(new Site { SiteId = 0, Name = "全部" });
            list.AddRange(_context.Sites);
            return list;
        }
    }
}