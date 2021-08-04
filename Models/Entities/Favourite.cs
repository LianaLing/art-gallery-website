using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models.Entities
{
    public class Favourite : ISqlParser
    {
        public int id;
        public string name;
        public int user_id;

        public Favourite()
        {
        }

        public Favourite(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Favourite(int id, string name, int user_id)
        {
            this.id = id;
            this.name = name;
            this.user_id = user_id;
        }

        public virtual ISqlParser ParseFromSqlReader(SqlDataReader reader)
        {
            if(reader.FieldCount == 2)
            {
                return new Favourite(reader.GetInt32(0),
                reader.GetString(1)
                );
            }
            return new Favourite(reader.GetInt32(0), 
                reader.GetString(1), 
                reader.GetInt32(2)
                );
        }
    }
}