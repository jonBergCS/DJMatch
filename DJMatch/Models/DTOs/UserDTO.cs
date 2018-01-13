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
    public class UserDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }
    }

    public class UserMapper : MapperBase<User, UserDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<User, UserDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<User, UserDTO>>)(p => new UserDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    ID = p.ID,
                    Name = p.Name,
                    Email = p.Email,
                    PhoneNum = p.PhoneNum,
                    Password = p.Password
                }));
            }
        }

        public override void MapToModel(UserDTO dto, User model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.ID = dto.ID;
            model.Name = dto.Name;
            model.Email = dto.Email;
            model.PhoneNum = dto.PhoneNum;
            model.Password = dto.Password;

        }
    }
}
