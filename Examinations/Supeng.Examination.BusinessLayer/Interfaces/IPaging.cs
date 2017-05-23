using System.Collections.Generic;
using System.Linq;

namespace Supeng.Examination.BusinessLayer.Interfaces
{
    public interface IPaging
    {
        IQueryable Model { get; }
    }
}
