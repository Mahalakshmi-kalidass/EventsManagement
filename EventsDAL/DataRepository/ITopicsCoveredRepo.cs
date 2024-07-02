using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public interface ITopicsCoveredRepo<T>
    {
        bool AddTopic(T topic);
        bool DeleteTopic(Guid topicId);
        bool UpdateTopic(T topic);
        T GetTopicById(Guid topicId);
        IEnumerable<T> GetAllTopics();
        IEnumerable<T> GetTopicsByStaff(Guid StaffId);

        IEnumerable<T> GetTopicsByStaffsOnLocation(Guid StaffId,Guid LocationId);
        IEnumerable<T> GetTopicsByStaffonLocationForEvent(Guid StaffId,Guid LocationId,Guid eventId);

    }
}
