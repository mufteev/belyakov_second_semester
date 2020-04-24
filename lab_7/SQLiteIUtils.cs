using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            );";
        public static string QueryReset = "DELETE FROM Smartphones";

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
                try
                {
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("no such table"))
                    {
                        conn.Close();
                        File.Delete(NameFileDB);
                        CreateTable();
                    }
                }
            }
        }

        public static DataTable GetTable()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT id, name, time, power FROM Smartphones";
                var dt = new DataTable();
                try
                {
                    dt.Load(cmd.ExecuteReader());
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("no such table"))
                        MessageBox.Show("Сначала экспортируйте данные из CSV");
                    else
                        throw ex;
                }

                return dt;
            }
        }
    }
}
