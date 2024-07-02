using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public interface IStaffDataRepo<T>
    {
        bool AddStaff(T staff);
        bool DeleteStaff(Guid staffId);

        bool UpdateStaff(T staff);

        T GetStaffById(Guid staffId);

        IEnumerable<T> GetAllStaff();

        IEnumerable<T> GetStaffsByLocationId(Guid locationId);

        IEnumerable<T> GetStaffOnLocationForEvent(Guid eventId,Guid locationId);

    }

}
