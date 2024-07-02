using EventsDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public class EventDataRepo : ICRUDDataRepo<Event>
    {
        public Event Add(Event EventData)
        {
            try
            {
                using(EventContext context = new EventContext())
                {
                    EventData.EventId = Guid.NewGuid();
                    string eventDate = EventData.EventDate.ToString("yyyy-MM-dd HH:mm");
                    // context.Events.Add(EventData);
                    string query = $@"EXEC SP_CreateEvent
                                     @EventId='{EventData.EventId}',
                                      @EventName='{EventData.EventName}',
                                       @EventDescription='{EventData.EventDescription}',
                                        @EventDate='{eventDate}'
                                    ";
                    context.Database.ExecuteSqlRaw(query);
                    context.SaveChanges();
                    return EventData;
                }
            }
            catch (Exception ex)
            {
                return new Event();
            }
        }

        public bool Delete(Guid EventId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.Events.Where(e => e.EventId.Equals(EventId)).FirstOrDefault();
                    if(existing != null)
                    {
                        context.Events.Remove(existing);
                        context.SaveChanges();
                        return true;
                    }
                  return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Event GetById(Guid EventId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.Events.Where(e => e.EventId.Equals(EventId)).FirstOrDefault();
                    if (existing != null)
                    {
                        return existing;
                    }
                    return new Event();
                }
            }
            catch (Exception ex)
            {
                return new Event();
            }
        }

        public IEnumerable<Event> GetAll()
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    return context.Events.ToList();
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Event>(); ;
            }
        }

        public bool Update(Event eventData)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.Events.Where(e => e.EventId.Equals(eventData.EventId)).FirstOrDefault();
                    if (existing != null)
                    {
                        //existing.EventName = eventData.EventName;
                        //existing.EventDescription = eventData.EventDescription;
                        //existing.EventDate = eventData.EventDate;
                        string eventDate = eventData.EventDate.ToString("yyyy-MM-dd HH:mm");

                        string query = $@"EXEC SP_UpdateEvent
                                        @EventId='{eventData.EventId}',
                                      @EventName='{eventData.EventName}',
                                       @EventDescription='{eventData.EventDescription}',
                                        @EventDate='{eventDate}'
                
                            ";
                        context.Database.ExecuteSqlRaw(query);
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
