using System.Collections.Generic;

namespace Supeng.Examination.BusinessLayer.Interfaces
{
    public interface ICrud<T> where T : class
    {
        IList<T> SelectList();

        T SelectById(int id);

        string Create(T data);

        string Modify(T data);

        string Delete(int id);
    }
}