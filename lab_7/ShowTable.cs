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
        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataReader reader;
        DataTable dt;

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

                    SQLiteIUtils.RefreshTable();
                    SQLiteIUtils.InsertRecords(smartphones);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new SQLiteConnection("Data Source=Pareto.db");
            string query = "Select id, name, time, power from Smartphones";
            cmd = new SQLiteCommand(query, conn);

            dt = new DataTable();
            dataGridViewSmart.DataSource = dt;

            conn.Open();
            reader = cmd.ExecuteReader();
            dt.Clear();
            dt.Load(reader);

            smartphones = dt
                .AsEnumerable()
                .Select(x => new CSVtype
                {
                    ID = x.Field<long>("id"),
                    Name = x.Field<string>("name"),
                    Power = x.Field<int>("power"),
                    Time = x.Field<int>("time")
                })
                .ToList();

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var f = new ShowChart(smartphones))
            {
                f.ShowDialog();
            }
        }
    }
}
