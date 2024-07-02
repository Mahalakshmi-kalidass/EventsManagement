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
    public class TopicDataRepo : ITopicsCoveredRepo<TopicCovered>
    {
        public bool AddTopic(TopicCovered topic)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    topic.TopicId = Guid.NewGuid();
                    //context.TopicsCovered.Add(topic);
                    string query = $@"EXEC SP_CreateTopicOnEvent
                                            @topicId = '{topic.TopicId}',@topicName = '{topic.TopicName}',@staffId = '{topic.StaffId}',@EventId = '{topic.EventId}',@LocationId = '{topic.LocationId}'
                        ";
                    context.Database.ExecuteSqlRaw(query);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);

            }
        }

        public bool DeleteTopic(Guid topicId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.TopicsCovered.Where(t => t.TopicId.Equals(topicId)).FirstOrDefault();
                    if (existing != null)
                    {
                        context.TopicsCovered.Remove(existing);
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

        public IEnumerable<TopicCovered> GetAllTopics()
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    return context.TopicsCovered.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            } 
        }

        public TopicCovered GetTopicById(Guid topicId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.TopicsCovered.Where(t => t.TopicId.Equals(topicId)).FirstOrDefault();
                    if (existing != null)
                    {
                        return existing;
                    }
                    return new TopicCovered();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IEnumerable<TopicCovered> GetTopicsByStaff(Guid StaffId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.TopicsCovered.Where(t => t.StaffId.Equals(StaffId)).ToList();
                    if (existing != null)
                    {
                        return existing;
                    }
                    return Enumerable.Empty<TopicCovered>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IEnumerable<TopicCovered> GetTopicsByStaffonLocationForEvent(Guid StaffId, Guid LocationId, Guid eventId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.TopicsCovered.Where(t => 
                    t.StaffId.Equals(StaffId) && t.LocationId.Equals(LocationId) && t.EventId.Equals(eventId)
                    ).ToList();
                    if (existing != null)
                    {
                        return existing;
                    }
                    return Enumerable.Empty<TopicCovered>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IEnumerable<TopicCovered> GetTopicsByStaffsOnLocation(Guid StaffId, Guid LocationId)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.TopicsCovered.Where(t =>
                    t.StaffId.Equals(StaffId) && t.LocationId.Equals(LocationId) 
                    ).ToList();
                    if (existing != null)
                    {
                        return existing;
                    }
                    return Enumerable.Empty<TopicCovered>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool UpdateTopic(TopicCovered topic)
        {
            try
            {
                using (EventContext context = new EventContext())
                {
                    var existing = context.TopicsCovered.Where(e => e.TopicId.Equals(topic.TopicId)).FirstOrDefault();
                    if (existing != null)
                    {
                        //existing.TopicName = topic.TopicName;
                        //existing.EventId = topic.EventId;
                        //existing.LocationId = topic.LocationId;
                        //existing.StaffId = topic.StaffId;
                        string query = $@"EXEC SP_UpdateTopicOnEvent @topicId = '{topic.TopicId}',
                                        @topicName ='{topic.TopicName}',
                                        @staffId = '{topic.StaffId}',
                                        @EventId = '{topic.EventId}',
                                        @LocationId ='{topic.LocationId}'";
                        context.Database.ExecuteSqlRaw(query);
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
