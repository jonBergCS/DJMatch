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
    
    public partial class UserAnswer
    {
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        public string Text { get; set; }
    
        public virtual Answer Answer { get; set; }
        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
    }
}
