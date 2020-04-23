using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_7
{
    class SQLiteIUtils
    {
        public static string NameFileDB { set; get; } = "Pareto.db";
        public static string connectionString = "Data Source=" + NameFileDB + ";";
        public static string QueryCreate = @"CREATE TABLE Smartphones (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                time INT  NOT NULL DEFAULT(0),
                power INT NOT NULL DEFAULT(0)
            );"; // так можно переносить строки в коде
        public static string QueryReset = "DELETE FROM Smartphones";
        public static string QueryInsert = "INSERT INTO Smartphones(power, price) VALUES(10.0, 15000)";
        public static string QuerySelect = "SELECT count(*) FROM Smartphones";

        public static void RefreshTable()
        {
            if (!File.Exists(NameFileDB))
            {
                CreateTable();
            }
            else
            {
                ResetTable();
            }
        }

        public static void InsertRecords(List<CSVtype> records)
        { 
            using (var con = new SQLiteConnection(connectionString))
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = "INSERT INTO Smartphones(name, time, power) VALUES "
                                  + string.Join(",\n", records.Select(x => $"('{x.Name}', {x.Time}, {x.Power})"));
                cmd.ExecuteNonQuery();
            }
        }

        public static void CreateTable()
        {
            SQLiteConnection.CreateFile(NameFileDB);
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand comm = new SQLiteCommand(QueryCreate, conn);
                comm.ExecuteNonQuery();
            };
        }

        public static void ResetTable()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand comm = new SQLiteCommand(QueryReset, conn);
                comm.ExecuteNonQuery();
            };
        }

        public static void InsertRecord()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand comm = new SQLiteCommand(QueryInsert, conn);
                comm.ExecuteNonQuery();
            }
        }

        public static int GetCountRecord()
        {
            int count = 0;
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand comm = new SQLiteCommand(QuerySelect, conn);
                count = Convert.ToInt32(comm.ExecuteScalar());
            }
            return count;
        }
    }
}
