using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public interface IEventAllocationDataRepo<T>
    {
        bool AddEventAllocation(T eventAllocation);
        bool DeleteEventAllocation(Guid eventAllocationId);
        
        bool DeleteEventAllocationByEventId(Guid eventId, Guid locationId);
        bool UpdateEventAllocation(T eventAllocation);
        T GetEventAllocationById(Guid eventAllocationId);
        IEnumerable<T> GetAllEventsAllocation();
        IEnumerable<T> GetStaffAllocationByEvent(Guid eventId, Guid locationId);
        IEnumerable<T> GetEventAllocatedByLocationId(Guid locationId);

        IEnumerable<T>GetAllocationsByEventId(Guid eventId);

        
    }
}
