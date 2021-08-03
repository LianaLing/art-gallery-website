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

        // Select rows from DB and parse them to type provided
        public static List<T> Select<T>(string SqlQuery, Func<SqlDataReader, T> parser = null) where T : ISqlParser, new()
        {
            // @see https://stackoverflow.com/questions/41102713/why-must-i-use-dispose/41102750
            // 'using' will ensure resources are disposed correctly.
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(SqlQuery, conn))
                {
                    // Open DB Connection
                    conn.Open();

                    // Execute sql query
                    SqlDataReader dataReader = command.ExecuteReader();

                    // Create an empty list
                    List<T> results = new List<T>();

                    // Read the query results
                    while(dataReader.Read())
                    {
                        // If no rows are fetch return an empty list
                        if (!dataReader.HasRows) return new List<T>();

                        // Parse data from the reader
                        T data;

                        // Use the parser provided if given
                        if (parser != null)
                            data = (T) parser(dataReader);
                        else
                            data = (T) new T().ParseFromSqlReader(dataReader);
                        
                        // Add parsed data into the list
                        results.Add(data);
                    }

                    // Return the list of data
                    return results;
                }
            }
        }

        // Insert rows into the DB
        public static int Insert(string SqlQuery)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(SqlQuery, conn))
                {
                    // Open DB connection
                    conn.Open();

                    // Construct SQL command from query
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.InsertCommand = command;

                    // Execute a transaction with the command
                    int rowsAffected = adapter.InsertCommand.ExecuteNonQuery();

                    // Return the rows affected
                    return rowsAffected;
                }
            }
        }

        // Update rows in the DB
        public static int Update(string SqlQuery)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(SqlQuery, conn))
                {
                    // Open DB connection
                    conn.Open();

                    // Construct SQL command from query
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = command;

                    // Execute a transaction with the command
                    int rowsAffected = adapter.UpdateCommand.ExecuteNonQuery();

                    // Return the rows affected
                    return rowsAffected;
                }
            }
        }

        // Delete rows from the DB
        public static int Delete(string SqlQuery)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(SqlQuery, conn))
                {
                    // Open DB connection
                    conn.Open();

                    // Construct SQL command from query
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.DeleteCommand = command;

                    // Execute a transaction with the command
                    int rowsAffected = adapter.DeleteCommand.ExecuteNonQuery();

                    // Return the rows affected
                    return rowsAffected;
                }
            }
        }
    }
}