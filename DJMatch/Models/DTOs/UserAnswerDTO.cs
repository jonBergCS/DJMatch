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
    public class UserAnswerDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        public string Text { get; set; }
    }

    public class UserAnswerMapper : MapperBase<UserAnswer, UserAnswerDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<UserAnswer, UserAnswerDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<UserAnswer, UserAnswerDTO>>)(p => new UserAnswerDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    UserID = p.UserID,
                    QuestionID = p.QuestionID,
                    AnswerID = p.AnswerID,
                    Text = p.Text
                }));
            }
        }

        public override void MapToModel(UserAnswerDTO dto, UserAnswer model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.UserID = dto.UserID;
            model.QuestionID = dto.QuestionID;
            model.AnswerID = dto.AnswerID;
            model.Text = dto.Text;

        }
    }
}
