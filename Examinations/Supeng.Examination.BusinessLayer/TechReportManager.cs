using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supeng.Examination.DataAccess;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer
{
    public interface ITechReportManager : IDisposable
    {
        string Create(TechReport tech);
    }

    public class TechReportManager : ITechReportManager
    {
        private readonly ExaDataContext _context = new ExaDataContext();
        public string Create(TechReport tech)
        {
            try
            {
                _context.TechReports.Add(tech);
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
