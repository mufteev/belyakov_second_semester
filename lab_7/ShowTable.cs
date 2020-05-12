using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration;

namespace lab_7
{
    public partial class ShowTable : Form
    {
        public ShowTable()
        {
            InitializeComponent();
        }

        private List<CSVtype> smartphones;

        private async void btn_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (var sr = new StreamReader(ofd.OpenFile()))
                using (var cr = new CsvReader(sr, new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    Delimiter = ";",
                    HasHeaderRecord = true
                }))
                {
                    smartphones = cr.GetRecords<CSVtype>().ToList();

                    var refreshResult = await MySQLUtils.RefreshTableAsync();
                    if (refreshResult.Code != 0)
                    {
                        MessageBox.Show($"Error:\n{refreshResult.Message}");
                        return;
                    }

                    var insertResult = await MySQLUtils.InsertRecordsAsync(smartphones);
                    if (insertResult.Code != 0)
                    {
                        MessageBox.Show($"Error:\n{insertResult.Message}");
                        return;
                    }

                    MessageBox.Show("Импорт данных прошёл успешно", "Парето");

                    //SQLiteIUtils.RefreshTable();
                    //SQLiteIUtils.InsertRecords(smartphones);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var dt = SQLiteIUtils.GetTable();
            var response = MySQLUtils.GetDataTableAsync();

            response.ContinueWith(task =>
            {
                var result = task.Result;
                if (result.Code == 0 && result.Result is DataTable dt)
                {
                    dataGridViewSmart.BeginInvoke((Action) (() =>
                    {
                        dataGridViewSmart.SuspendLayout();
                        dataGridViewSmart.DataSource = dt;
                        dataGridViewSmart.ResumeLayout();
                    }));
                    smartphones = dt
                        .AsEnumerable()
                        .Select(x => new CSVtype
                        {
                            ID = x.Field<int>("id"),
                            Name = x.Field<string>("name"),
                            Power = x.Field<int>("power"),
                            Time = x.Field<int>("time")
                        })
                        .ToList();
                }
                else
                {
                    MessageBox.Show($"Error:\n{result.Message}", "Парето");
                }
            });

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (smartphones is null)
            {
                MessageBox.Show("Сначала подгрузите данные");
                return;
            }

            if (smartphones.Count == 0)
            {
                MessageBox.Show("Данные отсутствуют");
                return;
            }

            using (var f = new ShowChart(smartphones))
            {
                f.ShowDialog();
            }
        }
    }
}
