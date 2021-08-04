using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models.Queries
{
    public class UserQuery : ISqlParser
    {
        public static string SqlQuery = @"
            SELECT [User].id, [User].username, [User].name, [User].ic, [User].dob, 
                   [User].contact_no, [User].email, [User].avatar_url,
                   [Favourite].id, [Favourite].name,
                   [Art].id, [Art].style, [Art].description, [Art].price, [Art].stock,  
                   [Art].likes, [Art].url,
                   [Author].id, [Author].description, [Author].verified,
            FROM [Art], [Author], [User], [Favourite]
            WHERE [Favourite].user_id = [User].id
            AND [Favourite].art_id = [Art].id
            AND [Art].author_id = [Author].id
            AND [User].id = '1',
            ORDER BY [Favourite].name ASC;
        ";

        public int id;
        public string username;
        public string name;
        public string ic;
        public Nullable<DateTime> dob;
        public string contactNo;
        public string email;
        public string avatarUrl;

        public UserQuery(int id, string username, string name, string ic, DateTime? dob, string contactNo, string email, string avatarUrl)
        {
            this.id = id;
            this.username = username;
            this.name = name;
            this.ic = ic;
            this.dob = dob;
            this.contactNo = contactNo;
            this.email = email;
            this.avatarUrl = avatarUrl;
        }

        public ISqlParser ParseFromSqlReader(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}