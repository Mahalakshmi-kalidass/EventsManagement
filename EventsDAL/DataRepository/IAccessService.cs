using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public  interface IAccessService<T>
    {
        T Create(T entity);
        IEnumerable<T> GetAccessByUserId(Guid userId);
        IEnumerable<T> GetAllAccessInfo();

        bool Delete(Guid UserId, Guid EventId);
    }
}
