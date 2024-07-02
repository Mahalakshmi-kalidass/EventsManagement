using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public interface IEventDataRepo<T>
    {
        bool AddEvent(T eventData);
        bool DeleteEvent(Guid eventId);

        bool updateEvent(T eventData);

        T GetEventById(Guid eventId);

        IEnumerable<T> GetAllEvents();
    }
}
