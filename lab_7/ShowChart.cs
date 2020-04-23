using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab_7
{
    public partial class ShowChart : Form
    {
        private List<CSVtype> smarts;
        public ShowChart(List<CSVtype> list)
        {
            InitializeComponent();
            smarts = list;

            //chartRandom.ChartAreas[0].Axes[0].Minimum = smarts.Min(x => x.Time);
            //chartRandom.ChartAreas[0].Axes[0].Maximum = smarts.Max(x => x.Time);
            //chartRandom.ChartAreas[0].Axes[1].Minimum = smarts.Min(x => x.Power);
            //chartRandom.ChartAreas[0].Axes[1].Maximum = smarts.Max(x => x.Power);



            for (int i = 0; i < smarts.Count; i++)
            {
                chartRandom.Series[0].Points.AddXY(smarts[i].Time, smarts[i].Power);
                //chartRandom.Series[0].Points.AddXY(i, i);
            }

        }
    }
}
