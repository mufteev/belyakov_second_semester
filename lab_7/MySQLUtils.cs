using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;

namespace lab_7
{
    public static class MySQLUtils
    {
        private static MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder
        {
            Server     = "pgsha.ru",
            Port       = 35006,
            UserID     = "soft0001",
            Password   = "FHA8t3tl",
            Database   = "soft0001"
        };
        private static MySqlConnection msc = new MySqlConnection(mscsb.ConnectionString);

        public static StatusResponse CheckWebConnection()
        {
            try
            {
                using (var tcp = new TcpClient
                {
                    ReceiveTimeout = 200,
                    SendTimeout = 200
                })
                {
                    tcp.Connect(mscsb.Server, (int)mscsb.Port);
                    return new StatusResponse();
                }
            }
            catch (Exception e)
            {
                return new StatusResponse
                {
                    Code = 1,
                    Message = e.Message
                };
            }
        }

        public static async Task<StatusResponse> GetDataTableAsync()
        {
            if (CheckWebConnection() is StatusResponse sr && sr.Code != 0)
            {
                return sr;
            }
            return await Task.Run(GetDataTable);
        }
        public static async Task<StatusResponse> InsertRecordsAsync(List<CSVtype> records)
        {
            if (CheckWebConnection() is StatusResponse sr && sr.Code != 0)
            {
                return sr;
            }
            return await Task.Run(() => InsertRecords(records));
        }
        public static async Task<StatusResponse> RefreshTableAsync()
        {
            if (CheckWebConnection() is StatusResponse sr && sr.Code != 0)
            {
                return sr;
            }
            if (CheckTableExist())
            {
                return await Task.Run(ResetTable);
            }

            return await Task.Run(CreateTable);
        }


        private static StatusResponse GetDataTable()
        {
            try
            {
                msc.Open();
            }
            catch (Exception e)
            {
                return new StatusResponse
                {
                    Code = 2,
                    Message = e.Message
                };
            }

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
                    Code = 3,
                    Message = e.Message
                };
            }
            finally
            {
                msc.Close();
            }

            return new StatusResponse
            {
                Result = dt
            };
        }

        private static StatusResponse CreateTable()
        {
            try
            {
                msc.Open();
                var cmd = msc.CreateCommand();
                cmd.CommandText = @"create table Smartphones
                                    (
                                        id    int                      not null
                                            primary key,
                                        name  varchar(15) charset utf8 not null,
                                        time  int default 0            not null,
                                        power int default 0            not null
                                    );";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return new StatusResponse
                {
                    Code = 4,
                    Message = e.Message
                };
            }
            finally
            {
                msc.Close();
            }

            return new StatusResponse();
        }

        private static StatusResponse ResetTable()
        {
            try
            {
                msc.Open();
                var cmd = msc.CreateCommand();
                cmd.CommandText = @"DELETE FROM Smartphones;";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return new StatusResponse
                {
                    Code = 5,
                    Message = e.Message
                };
            }

            finally
            {
                msc.Close();
            }
            return new StatusResponse();
        }

        private static bool CheckTableExist()
        {
            try
            {
                msc.Open();
                var cmd = msc.CreateCommand();
                cmd.CommandText = @"SELECT 1 FROM Smartphones LIMIT 1;";
                var res = cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                msc.Close();
            }

            return true;
        }

        private static StatusResponse InsertRecords(List<CSVtype> records)
        {
            try
            {
                msc.Open();
                var cmd = msc.CreateCommand();
                cmd.CommandText = "INSERT INTO Smartphones(name, time, power) VALUES "
                                  + string.Join(",\n", records.Select(x => $"('{x.Name}', {x.Time}, {x.Power})"));
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return new StatusResponse
                {
                    Code = 6,
                    Message = e.Message
                };
            }
            finally
            {
                msc.Close();
            }

            return new StatusResponse();
        }

    }
}
