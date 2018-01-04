//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DJMatch.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int DJId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int PlaylistId { get; set; }
        public string EventType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual DJ DJ { get; set; }
        public virtual Playlist Playlist { get; set; }
        public virtual User User { get; set; }
    }
}
