using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models.OldEntities
{
    public class FavArt : ISqlParser
    {

        public int fav_id;
        public int art_id;

        public FavArt(int fav_id, int art_id)
        {
            this.fav_id = fav_id;
            this.art_id = art_id;
        }

        public virtual ISqlParser ParseFromSqlReader(SqlDataReader reader)
        {
            return new FavArt(
                reader.GetInt32(0),
                reader.GetInt32(1)
                );
        }
    }
}