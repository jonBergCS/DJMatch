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
    public class DJDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<int> ExperienceYears { get; set; }
        public Nullable<int> PriceMin { get; set; }
        public Nullable<int> PriceMax { get; set; }
        public string Genres { get; set; }
        public string Address { get; set; }
        public string PhoneNum { get; set; }
        public string WebSite { get; set; }
        public string FacebookPage { get; set; }
        public string IGProfile { get; set; }
    }

    public class DJMapper : MapperBase<DJ, DJDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<DJ, DJDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<DJ, DJDTO>>)(p => new DJDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    ID = p.ID,
                    Name = p.Name,
                    BirthDate = p.BirthDate,
                    ExperienceYears = p.ExperienceYears,
                    PriceMin = p.PriceMin,
                    PriceMax = p.PriceMax,
                    Genres = p.Genres,
                    Address = p.Address,
                    PhoneNum = p.PhoneNum,
                    WebSite = p.WebSite,
                    FacebookPage = p.FacebookPage,
                    IGProfile = p.IGProfile
                }));
            }
        }

        public override void MapToModel(DJDTO dto, DJ model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.ID = dto.ID;
            model.Name = dto.Name;
            model.BirthDate = dto.BirthDate;
            model.ExperienceYears = dto.ExperienceYears;
            model.PriceMin = dto.PriceMin;
            model.PriceMax = dto.PriceMax;
            model.Genres = dto.Genres;
            model.Address = dto.Address;
            model.PhoneNum = dto.PhoneNum;
            model.WebSite = dto.WebSite;
            model.FacebookPage = dto.FacebookPage;
            model.IGProfile = dto.IGProfile;

        }
    }
}
