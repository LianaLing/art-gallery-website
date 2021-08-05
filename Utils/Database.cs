using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using ArtGalleryWebsite.Models;
using System.Data;
using System.Threading;

// TODO: Use SqlCommand parameters instead of string interpolation to avoid SQLInjection

namespace ArtGalleryWebsite.Utils
{
    /// <summary>
    /// A helper class that is available globally for querying the database
    /// </summary>
    public static class Database
    {
        // SQL connection config
        private static string connString = ConfigurationManager.ConnectionStrings["ArtGalleryDB"].ConnectionString;

        /// <summary>
        ///  Open the db connection only if it is closed,
        ///  the function will try to re-open if it fails
        /// </summary>
        /// <param name="conn">The SQL connection</param>
        private static void EnsureConnectionOpen(SqlConnection conn)
        {
            int retries = 3;
            if (conn.State == ConnectionState.Open)
            {
                return;
            }
            else
            {
                while (retries >= 0 && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    retries--;
                    Thread.Sleep(30);
                }
            }
        }

        /// <summary>
        /// Close connection only if it is opened
        /// </summary>
        /// <param name="conn">The SQL connection</param>
        private static void EnsureConnectionClose(SqlConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Executes a SQL query that returns a single scalar value as the result
        /// </summary>
        /// <param name="SqlQuery">The SQL query to execute</param>
        /// <returns>The single scalar value object without type</returns>
        public static T QueryValue<T>(string SqlQuery)
        {
            object result = null;

            if (String.IsNullOrEmpty(SqlQuery))
            {
                throw new ArgumentException("Sql query cannot be null or empty");
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(SqlQuery, conn))
                {
                    EnsureConnectionOpen(conn);
                    result = command.ExecuteScalar();
                }
            }

            if (result is T)
            {
                return (T)result;
            }

            try
            {
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (InvalidCastException)
            {
                throw;
            }
        }

        /// <summary>
        /// Select rows from DB and parse them to type provided
        /// </summary>
        /// <typeparam name="T">The desired type to be parsed (must implement ISqlParser)</typeparam>
        /// <param name="SqlQuery">The SQL query to execute</param>
        /// <param name="parser">An optional customer parser, this will override the default sql parser from the type</param>
        /// <returns>List of parsed objects from query results</returns>
        public static List<T> Select<T>(string SqlQuery, Func<SqlDataReader, T> parser = null)
            where T : ISqlParser, new()
        {
            // @see https://stackoverflow.com/questions/41102713/why-must-i-use-dispose/41102750
            // 'using' will ensure resources are disposed correctly.
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(SqlQuery, conn))
                {
                    // Open DB Connection
                    EnsureConnectionOpen(conn);

                    // Execute sql query
                    SqlDataReader dataReader = command.ExecuteReader();

                    // Create an empty list
                    List<T> results = new List<T>();

                    // Read the query results
                    while (dataReader.Read())
                    {
                        // If no rows are fetch return an empty list
                        if (!dataReader.HasRows) return new List<T>();

                        // Parse data from the reader
                        T data;

                        // Use the parser provided if given
                        if (parser != null)
                            data = (T)parser(dataReader);
                        else
                            data = (T)new T().ParseFromSqlReader(dataReader);

                        // Add parsed data into the list
                        results.Add(data);
                    }

                    // Return the list of data
                    return results;
                }
            }
        }

        /// <summary>
        /// Insert rows into the DB
        /// </summary>
        /// <param name="SqlQuery">the SQL query to be executed</param>
        /// <returns>Number of rows affected</returns>
        public static int Insert(string SqlQuery)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(SqlQuery, conn))
                {
                    // Open DB connection
                    EnsureConnectionOpen(conn);

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

        /// <summary>
        /// Update rows in the DB
        /// </summary>
        /// <param name="SqlQuery">The SQL query to execute</param>
        /// <returns>Number of rows affected</returns>
        public static int Update(string SqlQuery)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(SqlQuery, conn))
                {
                    // Open DB connection
                    EnsureConnectionOpen(conn);

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

        /// <summary>
        /// Delete rows from the DB
        /// </summary>
        /// <param name="SqlQuery">the SQL query to be executed</param>
        /// <returns>Number of rows affected</returns>
        public static int Delete(string SqlQuery)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(SqlQuery, conn))
                {
                    // Open DB connection
                    EnsureConnectionOpen(conn);

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

        public static string GetStringOrNull(this IDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }


        public static DateTime? GetDateTimeOrNull(this IDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : (DateTime?)reader.GetDateTime(ordinal);
        }
    }
}