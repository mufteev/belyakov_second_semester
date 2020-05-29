using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace lab_8
{
    public partial class Form1 : MetroForm
    {
        #region MySQL - definition var
        private MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder
        {
            Server = "pgsha.ru",
            Port = 35006,
            UserID = "soft0001",
            Password = "FHA8t3tl",
            Database = "soft0001_lab_8_transport"
        };

        private MySqlConnection msc;
        private MySqlCommand msCommand;
        #endregion

        #region MySQL - definition SQL
        async Task<DataTable> GetData()
        {
            var dt = new DataTable();

            try
            {
                msc.Open();
                var query = @"SELECT cities.name_city   AS 'City',
                                     prices.name_shop   AS 'Shop',
                                     prices.price       AS 'Price',
                                     cities.way         AS 'Way'
                              FROM 
                                 (cities INNER JOIN merge ON cities.id_city = merge.id_city)
                                 INNER JOIN prices ON prices.id_shop = merge.id_shop";
                var cmd = msc.CreateCommand();
                cmd.CommandText = query;

                var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных:\n{ex.Message}", "Transport");
            }
            finally
            {
                msc.Close();
            }

            return dt;
        }

        async Task<DataTable> GetDataOptimization(int wayConstraint)
        {
            var dt = new DataTable();

            try
            {
                msc.Open();
                var query = @"SELECT cities.name_city          AS 'City',
                                     prices.name_shop          AS 'Shop',
                                     prices.price              AS 'Price',
                                     cities.way                AS 'Way',
                                     prices.price / cities.way AS 'Relative'
                              FROM (cities INNER JOIN merge ON cities.id_city = merge.id_city)
                                       INNER JOIN prices ON prices.id_shop = merge.id_shop
                              ORDER BY Relative DESC";
                var cmd = msc.CreateCommand();
                cmd.CommandText = query;

                var reader = await cmd.ExecuteReaderAsync();

                dt.Columns.Add("City");
                dt.Columns.Add("Shop");
                dt.Columns.Add("Price");
                dt.Columns.Add("Way");
                dt.Columns.Add("Relative");

                var totalWay = 0;
                var totalPrice = 0M;

                foreach (IDataRecord item in reader)
                {
                    if (wayConstraint > 0 && totalWay + item.GetInt32(3) > wayConstraint) break;
                    totalWay += item.GetInt32(3);
                    totalPrice += item.GetDecimal(2);
                    dt.Rows.Add(item.GetValue(0),
                        item.GetValue(1),
                        item.GetValue(2),
                        item.GetValue(3),
                        item.GetValue(4));
                }

                MessageBox.Show($"Sum Price: {totalPrice:C}\nFor sum Way: {totalWay} km", "Transport");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных:\n{ex.Message}", "Transport");
            }
            finally
            {
                msc.Close();
            }

            return dt;
        }
        #endregion

        public Form1()
        {
            InitializeComponent();

            msc = new MySqlConnection(mscsb.ConnectionString);
        }

        private async void MBtnGet_Click(object sender, EventArgs e)
        {
            DGView.DataSource = await GetData();
        }

        private void ItemCheckConnection_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Соединение успешно произведено!", "Transport", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось подключится к удалённому ресурсу.\n\n{ex.Message}", "Transport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void metroButton2_Click(object sender, EventArgs e)
        {
            if (metroRadioButton1.Checked == true)
            {
                var way = int.TryParse(txtConstrainsWay.Text, out int cnstr) ? cnstr : 0;
                DGView.DataSource = await GetDataOptimization(way);
            }
            else
            {
                MessageBox.Show("Выбранный алгоритм не реализован", "Transport");
            }
        }
    }
}
