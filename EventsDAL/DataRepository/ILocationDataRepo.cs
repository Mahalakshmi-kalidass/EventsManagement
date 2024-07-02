using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public interface ILocationDataRepo<T>
    {
       bool AddLocation(T location);
        bool DeleteLocation(Guid locationId);
        bool UpdateLocation(T location);
        T GetLocationById(Guid locationId);
        IEnumerable<T> GetAllLocations();

    }
}
