using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DJMatch.Models.Infrastructure;
using DJMatch.Models;

namespace DJMatch.Models
{
    public class EventDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int ID { get; set; }
        public int UserId { get; set; }
        public int DJId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int PlaylistId { get; set; }
        public string EventType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class EventMapper : MapperBase<Event, EventDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<Event, EventDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<Event, EventDTO>>)(p => new EventDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    ID = p.ID,
                    UserId = p.UserId,
                    DJId = p.DJId,
                    Date = p.Date,
                    PlaylistId = p.PlaylistId,
                    EventType = p.EventType,
                    Name = p.Name,
                    Description = p.Description
                }));
            }
        }

        public override void MapToModel(EventDTO dto, Event model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.ID = dto.ID;
            model.UserId = dto.UserId;
            model.DJId = dto.DJId;
            model.Date = dto.Date;
            model.PlaylistId = dto.PlaylistId;
            model.EventType = dto.EventType;
            model.Name = dto.Name;
            model.Description = dto.Description;

        }
    }
}
