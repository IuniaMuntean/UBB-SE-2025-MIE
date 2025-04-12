using Npgsql;
using static LinqToDB.Sql;
using System;
using System.Data;
using Npgsql;
using System.IO;
using Org.BouncyCastle.Crypto.Generators;

namespace App1.SQL_Scripts
{
    internal class DBSync
    {
        /*
         * Class for updating the database with new tables - MIT
         * Instructions:
         *      In Scripts.sql, add the create table querie(s) for the new table(s). 
         *      MUST HAVE ; AFTER EACH QUERIE
         *      On line 32, add the full path for Scripts.sql (where it's saved on your device) 
         *  The syncDB() function will run on app start up (from App.xaml.cs, line 19)
        */

        private static string connectionStringRazvan = "Host=localhost;" +
                                   "Port=5432;" +
                                   "Database=postgres;" +
                                   "Username=razvan-admin;" +
                                   "Password=Cj159550285/;";

        private static string connectionStringIunia = "Host=localhost;" +
                                   "Port=5432;" +
                                   "Database=truck-company;" +
                                   "Username=postgres;" +
                                   "Password=postgres;";

        private static string connectionString = connectionStringIunia;

        public static void syncDB()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString ?? "");
            connection.Open();

            string sqlScript = File.ReadAllText("C:\\Users\\razva\\Desktop\\Github repos\\UBB-SE-2025-MIE\\App1\\SQL Scripts\\Scripts.sql");
            // D:\\FACULTA\\SEM VI\\UBB-SE-2025-MIE\\App1\\SQL Scripts\\Scripts.sql

            string[] createTableQueries = sqlScript.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var query in createTableQueries)
            {
                //if(query == "")
                //{
                //    continue;
                //}
                string tableName = GetTableNameFromQuery(query);

                if (!DoesTableExist(connection, tableName))
                {
                    // Execute the CREATE TABLE query
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        try {
                            command.ExecuteNonQuery();
                            Console.WriteLine($"Table '{tableName}' created successfully.");
                        }
                        catch (Npgsql.PostgresException ex) {
                            Console.WriteLine($"Error creating table '{tableName}': {ex.Message}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Table '{tableName}' already exists.");
                }
            }
        }

        private static bool DoesTableExist(NpgsqlConnection connection, string tableName)
        {
            string query = $@"
            SELECT EXISTS (
                SELECT 1
                FROM information_schema.tables
                WHERE table_name = '{tableName}'
            );";

            using (var command = new NpgsqlCommand(query, connection))
            {
                return (bool)command.ExecuteScalar();
            }
        }

        private static string GetTableNameFromQuery(string query)
        {
            // Extract table name from CREATE TABLE or function name from CREATE FUNCTION
            query = query.Trim();
            if (string.IsNullOrEmpty(query))
            {
                return null;
            }

            if (query.StartsWith("CREATE TABLE", StringComparison.OrdinalIgnoreCase))
            {
                var parts = query.Split(new[] { ' ', '(', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 3)
                {
                    return parts[2];
                }
            }
            else if (query.StartsWith("CREATE OR REPLACE FUNCTION", StringComparison.OrdinalIgnoreCase))
            {
                var parts = query.Split(new[] { ' ', '(', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 5)
                {
                    return parts[4];
                }
            }
            else if (query.StartsWith("CREATE TRIGGER", StringComparison.OrdinalIgnoreCase))
            {
                var parts = query.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 3)
                {
                    return parts[2];
                }
            }

            // For comment lines or other non-CREATE statements, return null
            if (query.TrimStart().StartsWith("--"))
            {
                return null;
            }
            
            // For RETURN NEW statements (used in triggers), return null
            if (query.Trim().Equals("RETURN NEW", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }
            throw new InvalidOperationException($"Unable to extract name from query: {query}");

        }
    }
}

