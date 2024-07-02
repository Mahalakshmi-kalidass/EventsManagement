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
    public class EventAllocationDataRepo : IEventAllocationDataRepo<EventAllocation>
    {
        public bool AddEventAllocation(EventAllocation eventAllocation)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    eventAllocation.EventAllocationId = Guid.NewGuid();
                    // context.EventAllocations.Add(eventAllocation);
                    string query = $@"EXEC SP_CreateEventAllocation 
                                        @EventAllocationId = '{eventAllocation.EventAllocationId}',
                                        @EventId = '{eventAllocation.EventId}',
                                        @LocationId='{eventAllocation.LocationId}',
                                        @staffId = '{eventAllocation.StaffId}'";
                    context.Database.ExecuteSqlRaw(query);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteEventAllocation(Guid eventAllocationId)
        {

            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.EventAllocations.Where(e => e.EventAllocationId.Equals(eventAllocationId)).FirstOrDefault();
                    if (existing != null)
                    {
                        context.EventAllocations.Remove(existing);
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

        public IEnumerable<EventAllocation> GetAllEventsAllocation()
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    return context.EventAllocations.ToList();
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<EventAllocation>(); ;
            }
        }

        public IEnumerable<EventAllocation> GetEventAllocatedByLocationId(Guid locationId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                   var allocations = context.EventAllocations.Where(ea=>ea.LocationId.Equals(locationId)).ToList();
                    if (allocations.Any())
                    {
                        return allocations;
                    }
                    return Enumerable.Empty<EventAllocation>();//empty list

                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<EventAllocation>();
            }
        }

        public EventAllocation GetEventAllocationById(Guid eventAllocationId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.EventAllocations.Where(e => eventAllocationId.Equals(eventAllocationId)).FirstOrDefault();
                    if(existing != null)
                    {
                        return existing;
                    }
                    return new EventAllocation();
                }
            }
            catch (Exception ex)
            {
                    return new EventAllocation();

            }
        }
        //this will give all the details for specified eventid - location, staff
        public IEnumerable<EventAllocation> GetAllocationsByEventId(Guid eventId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var allocations = context.EventAllocations.Where(ea => ea.EventId.Equals(eventId)).ToList();
                    if (allocations.Any())
                    {
                        return allocations;
                    }
                    return Enumerable.Empty<EventAllocation>();//empty list

                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<EventAllocation>();
            }
        }

        public bool UpdateEventAllocation(EventAllocation eventAllocation)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.EventAllocations.Where(e => e.EventAllocationId.Equals(eventAllocation.EventAllocationId)).FirstOrDefault();
                    if (existing != null)
                    {
                        //existing.EventId = eventAllocation.EventId;
                        //existing.LocationId = eventAllocation.LocationId;
                        //existing.StaffId = eventAllocation.StaffId;
                        string query = $@"EXEC SP_UpdateEventAllocation 
                                        @EventAllocationId = '{eventAllocation.EventAllocationId}',
                                        @EventId = '{eventAllocation.EventId}',
                                        @LocationId='{eventAllocation.LocationId}',
                                        @staffId = '{eventAllocation.StaffId}'";
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

        public IEnumerable<EventAllocation> GetStaffAllocationByEvent(Guid eventId, Guid locationId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var allocations = context.EventAllocations.Where(ea => ea.EventId.Equals(eventId)&&ea.LocationId.Equals(locationId)).ToList();
                    if (allocations.Any())
                    {
                        return allocations;
                    }
                    return Enumerable.Empty<EventAllocation>();//empty list

                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<EventAllocation>();
            }
        }

        public bool DeleteEventAllocationByEventId(Guid eventId, Guid locationId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {


                    var allocations = context.EventAllocations.Where(ea => ea.EventId.Equals(eventId) && ea.LocationId.Equals(locationId)).ToList();
                    if (allocations.Any())
                    {
                        foreach (var allocation in allocations)
                        {
                            context.EventAllocations.Remove(allocation);
                            //delete topics handled on that event location 
                            var topicsHandled = context.TopicsCovered.Where(t =>
                    t.StaffId.Equals(allocation.StaffId) && t.LocationId.Equals(allocation.LocationId) && t.EventId.Equals(allocation.EventId)
                    ).ToList();
                            foreach (var item in topicsHandled)
                            {
                                context.TopicsCovered.Remove(item);
                            }
                        }
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
