using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.Models.DTO
{
    public class FavDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Art Art { get; set; }
        public Author Author { get; set; }
    }
}

        //public static string SqlQuery;
        //public static int art_id;
        //public static int fav_id;
        //public int total_art = 0;

        //// Fetch [Favourite] rows that belongs to the logged in user in the current session
        //// [Favourite]s wil empty [FavArt] rows will not be fetched
        //// Ordered by [Favourite].id because the COUNT query is ordered by [FavArt].id
        //// Keeping the same order will ease frontend parsing so please do not change this
        //public static void FetchCurrentUser(int id)
        //{
        //    SqlQuery = $@"
        //        SELECT 
        //               [Favourite].id, [Favourite].name,
        //               [Art].id, [Art].style, [Art].description, [Art].price, [Art].stock,  
        //               [Art].likes, [Art].url,
        //               [Author].id, [Author].description, [Author].verified 
        //        FROM [Art], [Author], [User], [Favourite], [FavArt]
        //        WHERE [Favourite].user_id = [User].Id
        //        AND [FavArt].art_id = [Art].id
        //        AND [FavArt].fav_id = [Favourite].id
        //        AND [Art].author_id = [Author].id
        //        AND [User].Id = {id} 
        //        ORDER BY [Favourite].id ASC;";
        //}

        //// Fetch all [Favourite]s of the user of the current session, regardless of whether it has rows in [FavArt]
        //// [Favourite]s with empty [FavArt] rows will be displayed
        //// Useful for finding out all [Favourite]s that belong to a particular user
        //public static void FetchAllUserFavourites(int id)
        //{
        //    SqlQuery = $@"
        //        SELECT 
        //                [Favourite].id, [Favourite].name
        //        FROM [Favourite]
        //        WHERE [Favourite].user_id = {id}
        //        ORDER BY [Favourite].id ASC;
        //    ";
        //}
        
        //// Insert into [FavArt] table
        //// {fav_id} and {art_id} are static variables
        //public static void InsertFavArt()
        //{
        //    SqlQuery = $@"INSERT INTO [FavArt] VALUES ('{fav_id}','{art_id}')";
        //}

        //// Delete from [FavArt] table
        //// {fav_id} and {art_id} are static variables
        //public static void RemoveFromFavArt()
        //{
        //    SqlQuery = $@"DELETE FROM [FavArt] WHERE fav_id = {fav_id} AND art_id = {art_id};";
        //}

        //// Count the number of rows in [FavArt] for each [Favourite]
        //// The [Favourite]s belong to the user of the current session
        //public static void CountArtInFavourites(int id)
        //{
        //    SqlQuery = $@"SELECT COUNT([FavArt].art_id) AS total_art, [FavArt].fav_id
        //                    FROM [FavArt], [Favourite], [User]
        //                    WHERE [FavArt].fav_id = [Favourite].id
        //                    AND [User].Id = [Favourite].user_id
        //                    AND [User].Id = {id}
        //                    GROUP BY [FavArt].fav_id
        //                    order by [favart].fav_id asc;";
        //}

        //// These fields orrespond to the constructors that also correspond to the value in SqlQuery
        //// Create a FavQuery object for the SqlQuery
        //public override ISqlParser ParseFromSqlReader(SqlDataReader reader)
        //{
        //    if(reader.FieldCount > 2)
        //    {
        //        return new FavQuery(
        //        reader.GetInt32(0),
        //        reader.GetStringOrNull(1),
        //        new Art(
        //            reader.GetInt32(2),
        //            reader.GetStringOrNull(3),
        //            reader.GetStringOrNull(4),
        //            reader.GetDecimal(5),
        //            reader.GetInt32(6),
        //            reader.GetInt32(7),
        //            reader.GetStringOrNull(8)
        //            ),
        //        new Author(
        //            reader.GetInt32(9),
        //            reader.GetStringOrNull(10),
        //            reader.GetBoolean(11)
        //            )
        //        );
        //    }
        //    try
        //    {
        //        return new FavQuery(
        //        reader.GetInt32(0),
        //        reader.GetInt32(1)
        //        );
        //    }
        //    catch
        //    {
        //        return new FavQuery(
        //            reader.GetInt32(0),
        //            reader.GetStringOrNull(1)
        //            );
        //    }
        //}