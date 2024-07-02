using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public interface ICRUDDataRepo<T>
    {
        T Add(T obj);
        bool Delete(Guid objId);
        bool Update(T obj);
        T GetById(Guid objId);
        IEnumerable<T> GetAll();

    }
}
