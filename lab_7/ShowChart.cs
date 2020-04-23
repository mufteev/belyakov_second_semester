using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CsvHelper;
using CsvHelper.Configuration;

namespace lab_7
{
    public partial class ShowChart : Form
    {
        private List<CSVtype> smarts;
        private List<CheckBox> checkBoxes;

        private List<CSVtype> nParetos = new List<CSVtype>();
        private List<CSVtype> paretos = new List<CSVtype>();

        Dictionary<string, Func<CSVtype, CSVtype, bool>> dcCondition = new Dictionary<string, Func<CSVtype, CSVtype, bool>>
        {
            {"1", (l, r) => l.Time >= r.Time && l.Power >= r.Power },
            {"2", (l, r) => l.Time <= r.Time && l.Power >= r.Power },
            {"3", (l, r) => l.Time <= r.Time && l.Power <= r.Power },
            {"4", (l, r) => l.Time >= r.Time && l.Power <= r.Power }
        };

        public ShowChart(List<CSVtype> list)
        {
            InitializeComponent();
            smarts = list;

            checkBoxes = new List<CheckBox>
            {
                checkBox1, checkBox2, checkBox3, checkBox4
            };


            for (int i = 0; i < smarts.Count; i++)
            {
                chart.Series[0].Points.AddXY(smarts[i].Time, smarts[i].Power);
            }

            chart.ChartAreas[0].AxisX.Interval = smarts.Count / 10;
            chart.ChartAreas[0].AxisY.Interval = smarts.Count / 10;
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            foreach (var ch in checkBoxes.Where(x => x != sender))
            {
                ch.Checked = false;
            }

            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            paretos.Clear();
            nParetos.Clear();

            var tag = (sender as CheckBox).Tag.ToString();

            for (int i = 0; i < smarts.Count; i++)
            {
                var b = true;
                for (int j = 0; j < smarts.Count && b; j++)
                    if (i != j && dcCondition[tag](smarts[j], smarts[i]))
                        b = false;
                if (b)
                {
                    paretos.Add(smarts[i]);
                    chart.Series[1].Points.AddXY(smarts[i].Time, smarts[i].Power);
                }
                else
                {
                    nParetos.Add(smarts[i]);
                    chart.Series[0].Points.AddXY(smarts[i].Time, smarts[i].Power);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sfdPNG.ShowDialog() == DialogResult.OK)
            {
                chart.SaveImage(sfdPNG.FileName, ChartImageFormat.Png);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sfdCSV.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new StreamWriter(sfdCSV.FileName))
                using (var cw = new CsvWriter(sw, new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ";"
                }))
                {
                    cw.WriteRecords(paretos);
                }
            }
        }
    }
}
