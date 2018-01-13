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
    public class QuestionDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int ID { get; set; }
        public string Text { get; set; }
    }

    public class QuestionMapper : MapperBase<Question, QuestionDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<Question, QuestionDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<Question, QuestionDTO>>)(p => new QuestionDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    ID = p.ID,
                    Text = p.Text
                }));
            }
        }

        public override void MapToModel(QuestionDTO dto, Question model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.ID = dto.ID;
            model.Text = dto.Text;

        }
    }
}
