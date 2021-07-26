using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryWebsite.Models
{
    public interface ISqlParser
    {
        ISqlParser ParseFromSqlReader(SqlDataReader reader);
    }
}
