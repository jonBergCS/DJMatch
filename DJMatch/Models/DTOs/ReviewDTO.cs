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
    public class ReviewDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int ID { get; set; }
        public int DJ_ID { get; set; }
        public int UserID { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public System.DateTime Date { get; set; }
    }

    public class ReviewMapper : MapperBase<Review, ReviewDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<Review, ReviewDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<Review, ReviewDTO>>)(p => new ReviewDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    ID = p.ID,
                    DJ_ID = p.DJ_ID,
                    UserID = p.UserID,
                    Text = p.Text,
                    Score = p.Score,
                    Date = p.Date
                }));
            }
        }

        public override void MapToModel(ReviewDTO dto, Review model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.ID = dto.ID;
            model.DJ_ID = dto.DJ_ID;
            model.UserID = dto.UserID;
            model.Text = dto.Text;
            model.Score = dto.Score;
            model.Date = dto.Date;

        }
    }
}
