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
    public class PlaylistDTO
    {

        ////BCC/ BEGIN CUSTOM CODE SECTION 
        public List<SongDTO> Songs { get; set; }

        ////ECC/ END CUSTOM CODE SECTION 
        public int ID { get; set; }
        public int DJ_ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Name { get; set; }
    }

    public class PlaylistMapper : MapperBase<Playlist, PlaylistDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<Playlist, PlaylistDTO>> SelectorExpression
        {
            get
            {
                var result = (Expression<Func<Playlist, PlaylistDTO>>)(p => new PlaylistDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    Songs = p.SongsToPlaylists.Select(s => s.Song).Select(new SongMapper().SelectorExpression.Compile()).ToList(),
                    ////ECC/ END CUSTOM CODE SECTION 
                    ID = p.ID,
                    DJ_ID = p.DJ_ID,
                    UserID = p.UserID,
                    Name = p.Name
                });

                

                return (result);
            }
        }

        public override void MapToModel(PlaylistDTO dto, Playlist model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.ID = dto.ID;
            model.DJ_ID = dto.DJ_ID;
            model.UserID = dto.UserID;
            model.Name = dto.Name;

        }
    }
}
