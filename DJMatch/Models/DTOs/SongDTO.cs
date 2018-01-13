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
    public class SongDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
    }

    public class SongMapper : MapperBase<Song, SongDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<Song, SongDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<Song, SongDTO>>)(p => new SongDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    ID = p.ID,
                    Name = p.Name,
                    Artist = p.Artist,
                    Genre = p.Genre
                }));
            }
        }

        public override void MapToModel(SongDTO dto, Song model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.ID = dto.ID;
            model.Name = dto.Name;
            model.Artist = dto.Artist;
            model.Genre = dto.Genre;

        }
    }
}
