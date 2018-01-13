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
    public class AnswerDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int ID { get; set; }
        public string Text { get; set; }
        public int QuestionID { get; set; }
    }

    public class AnswerMapper : MapperBase<Answer, AnswerDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<Answer, AnswerDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<Answer, AnswerDTO>>)(p => new AnswerDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    ID = p.ID,
                    Text = p.Text,
                    QuestionID = p.QuestionID
                }));
            }
        }

        public override void MapToModel(AnswerDTO dto, Answer model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.ID = dto.ID;
            model.Text = dto.Text;
            model.QuestionID = dto.QuestionID;

        }
    }
}
