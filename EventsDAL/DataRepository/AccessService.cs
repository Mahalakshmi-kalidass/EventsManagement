using EventsDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public class AccessService : IAccessService<EventAccess>
    {
        public EventAccess Create(EventAccess entity)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    entity.Id = Guid.NewGuid();
                    string query = $@"Insert Into [dbo].[EventAccesses] (Id,UserId,EventId) Values ('{entity.Id}','{entity.UserId}','{entity.EventId}')";
                    context.Database.ExecuteSqlRaw(query);
                    context.SaveChanges();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
        }

        public bool Delete(Guid UserId, Guid EventId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var Access = context.EventAccesses.Where(e => e.UserId.Equals(UserId) && e.EventId.Equals(EventId)).FirstOrDefault();
                    if(Access != null)
                    {
                        context.EventAccesses.Remove(Access);
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
        }

        public IEnumerable<EventAccess> GetAccessByUserId(Guid userId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var allAccess = context.EventAccesses.Where(e => e.UserId.Equals(userId)).ToList();
                    return allAccess;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
        }

        public IEnumerable<EventAccess> GetAllAccessInfo()
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var allAccess = context.EventAccesses.ToList();
                    return allAccess;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
        }
    }
}
