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
    public class SongsToPlaylistDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int SongID { get; set; }
        public int PlaylistID { get; set; }
        public string Tags { get; set; }
    }

    public class SongsToPlaylistMapper : MapperBase<SongsToPlaylist, SongsToPlaylistDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<SongsToPlaylist, SongsToPlaylistDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<SongsToPlaylist, SongsToPlaylistDTO>>)(p => new SongsToPlaylistDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    SongID = p.SongID,
                    PlaylistID = p.PlaylistID,
                    Tags = p.Tags
                }));
            }
        }

        public override void MapToModel(SongsToPlaylistDTO dto, SongsToPlaylist model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.SongID = dto.SongID;
            model.PlaylistID = dto.PlaylistID;
            model.Tags = dto.Tags;

        }
    }
}
