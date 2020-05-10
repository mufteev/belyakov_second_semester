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

        private void btn_Click(object sender, EventArgs e)
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
                    //SQLiteIUtils.RefreshTable();
                    //SQLiteIUtils.InsertRecords(smartphones);
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //var dt = SQLiteIUtils.GetTable();
            var response = await MySQLUtils.GetDataTableAsync();

            if (response.Code == 0 && response.Result is DataTable dt)
            {
                dataGridViewSmart.DataSource = dt;
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
                MessageBox.Show($"Error:\n{response.Message}", "Парето");
            }
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
