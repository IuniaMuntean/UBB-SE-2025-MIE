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
        private static string connectionString = "Host=localhost;" +
                                   "Port=5432;" +
                                   "Database=postgres;" +
                                   "Username=postgres;" +
                                   "Password=postgres;";
        public static void syncDB()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();

            //var baseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            //var filePath = Path.Combine(baseDirectoryPath, "Scripts.sql");
            string sqlScript = File.ReadAllText("D:\\FACULTA\\SEM VI\\UBB-SE-2025-MIE\\App1\\SQL Scripts\\Scripts.sql");

            string[] createTableQueries = sqlScript.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var query in createTableQueries)
            {
                string tableName = GetTableNameFromQuery(query);

                if (!DoesTableExist(connection, tableName))
                {
                    // Execute the CREATE TABLE query
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"Table '{tableName}' created successfully.");
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
            // Naive extraction of table name (works if "CREATE TABLE table_name" is consistent)
            query = query.Trim();
            if (query.StartsWith("CREATE TABLE", StringComparison.OrdinalIgnoreCase))
            {
                var parts = query.Split(new[] { ' ', '(', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                return parts[2]; // Table name
            }
            throw new InvalidOperationException("Unable to extract table name from query.");
        }
    }
}

