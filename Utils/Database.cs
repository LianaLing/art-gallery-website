using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ArtGalleryWebsite.Models;

namespace ArtGalleryWebsite.Utils
{
    public static class Database
    {
        // SQL connection config
        private static string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marcus\Projects\art-gallery-website\App_Data\ArtGallery.mdf;Integrated Security=True";
        private static SqlConnection conn = new SqlConnection(connString);

        public static void OpenDbConnection()
        {
            conn.Open();
        }

        public static void CloseDbConnection()
        {
            conn.Close();
        }

        public static List<T> Query<T>(string SqlQuery, Func<SqlDataReader, T> parser = null) where T : ISqlParser, new()
        {
            // Sql command to execute
            SqlCommand command = new SqlCommand(SqlQuery, conn);
            SqlDataReader dataReader = command.ExecuteReader();

            // Create an empty list
            List<T> results = new List<T>();

            // Read the query results
            while(dataReader.Read())
            {
                // If no rows are fetch return an empty list
                if (!dataReader.HasRows)
                {
                    conn.Close();
                    return new List<T>();
                }

                // Parse data from the reader
                T data;

                // Use the parser provided if given
                if (parser != null)
                {
                    data = (T) parser(dataReader);
                }
                else
                {
                    data = (T) new T().ParseFromSqlReader(dataReader);
                }
                
                // Add parsed data into the list
                results.Add(data);
            }

            // Return the list of data
            return results;
        }

    }
}