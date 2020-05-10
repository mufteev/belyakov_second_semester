using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace lab_7
{
    public static class MySQLUtils
    {
        private static MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder
        {
            Server = "pgsha.ru",
            Port = 35006,
            UserID = "soft0001",
            Password = "FHA8t3tl",
            Database = "soft0001"
        };
        private static MySqlConnection msc = new MySqlConnection(mscsb.ConnectionString);

        public static bool CheckWebConnection()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var buffer = new byte[32];
                    var option = new PingOptions();
                    var reply = ping.Send(mscsb.Server, 500, buffer, option);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        private static StatusResponse GetDataTable()
        {
            msc.OpenAsync();

            var cmd = msc.CreateCommand();
            cmd.CommandText = "SELECT id, name, time, power FROM Smartphones";
            var dt = new DataTable();

            try
            {
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception e)
            {
                return new StatusResponse
                {
                    Code = 1,
                    Message = e.Message
                };
            }

            return new StatusResponse
            {
                Result = dt
            };
        }

        public static async Task<StatusResponse> GetDataTableAsync()
        {
            return await Task.Run(GetDataTable);
        }



    }
}
