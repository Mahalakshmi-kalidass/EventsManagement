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
    public class StaffDataRepo : IStaffDataRepo<Staff>
    {
        public bool AddStaff(Staff staff)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    staff.StaffId = Guid.NewGuid();
                    context.Staffs.Add(staff);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
        }

        public bool DeleteStaff(Guid staffId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.Staffs.Where(s => s.StaffId.Equals(staffId)).FirstOrDefault();
                    if (existing != null)
                    {
                        context.Staffs.Remove(existing);
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

        public IEnumerable<Staff> GetAllStaff()
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    return context.Staffs.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Staff GetStaffById(Guid staffId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.Staffs.Where(s => s.StaffId.Equals(staffId)).FirstOrDefault();
                    if (existing != null)
                    {
                        return existing;
                    }
                    return new Staff();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IEnumerable<Staff> GetStaffOnLocationForEvent(Guid eventId, Guid locationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Staff> GetStaffsByLocationId(Guid locationId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.Staffs.Where(s => s.LocationId.Equals(locationId)).ToList();
                    if (existing != null)
                    {
                        return existing;
                    }
                    return Enumerable.Empty<Staff>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
        }

        public bool UpdateStaff(Staff staff)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.Staffs.Where(s => s.StaffId.Equals(staff.StaffId)).FirstOrDefault();
                    if (existing != null)
                    {
                        existing.StaffName = staff.StaffName;
                        existing.Email = staff.Email;
                        existing.LocationId = staff.LocationId;
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
    }
}
