using EventsDAL.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.DataRepository
{
    public class LocationDataRepo : ICRUDDataRepo<Location>
    {
        public Location Add(Location locationData)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    locationData.LocationId = Guid.NewGuid();
                    context.Locations.Add(locationData);
                    context.SaveChanges();
                    return locationData;
                }
            }
            catch (Exception ex)
            {
                return new Location();
            }
        }

        public bool Delete(Guid LocationId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.Locations.Where(e => e.LocationId.Equals(LocationId)).FirstOrDefault();
                    if (existing != null)
                    {
                        context.Locations.Remove(existing);
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

        public Location GetById(Guid locationId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.Locations.Where(e => e.LocationId.Equals(locationId)).FirstOrDefault();
                    if (existing != null)
                    {
                        return existing;
                    }
                    return new Location();
                }
            }
            catch (Exception ex)
            {
                return new Location();
            }
        }

        public IEnumerable<Location> GetAll()
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    return context.Locations.ToList();
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Location>(); ;
            }
        }

        public bool Update(Location location)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.Locations.Where(e => e.LocationId.Equals(location.LocationId)).FirstOrDefault();
                    if (existing != null)
                    {
                        existing.LocationName = location.LocationName;
                        
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
