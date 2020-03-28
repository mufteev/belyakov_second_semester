using CsvHelper;
using CsvHelper.Configuration;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab_5_dll
{
    public class Team
    {
        public string Name { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Draw { get; set; }
        public int Defeat { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int TournamentPoints { get; set; }
    }

    public static class WebHelperSports
    {
        private static Regex TableRow = new Regex(@"<a class=""name"".*?>(?<Name>.*?)<\/a>[^\n]*\n<td>(?<Matches>.*?)<\/[^\n]*\n<td>(?<Wins>.*?)<\/[^\n]*\n<td>(?<Draw>.*?)<\/[^\n]*\n<td>(?<Defeat>.*?)<\/[^n]*\n<td>(?<GoalsScored>.*?)<\/[^\n]*\n<td>(?<GoalsConceded>.*?)<\/[^\n]*\n<td>(?<TournamentPoints>.*?)<",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
        private static MatchCollection GetRows(string htmlTable) =>
            TableRow.Matches(htmlTable);


        public static IEnumerable<Team> GetSportTable(string link)
        {
            var web = new HtmlWeb();
            var doc = web.Load(link);
            //var xPath = "//table[contains(@class, 'sortable-table')]/tbody/tr";
            var xPath = "//table[contains(@class, 'sortable-table')]/tbody";

            var tableHtml = doc.DocumentNode.SelectSingleNode(xPath);
            var matchRows = GetRows(tableHtml.InnerHtml);

            foreach (var row in matchRows.Cast<Match>())
            {
                yield return new Team
                {
                    Name                = row.Groups["Name"].Value,
                    Matches             = int.Parse(row.Groups["Matches"].Value),
                    Wins                = int.Parse(row.Groups["Wins"].Value),
                    Draw                = int.Parse(row.Groups["Draw"].Value),
                    Defeat              = int.Parse(row.Groups["Defeat"].Value),
                    GoalsScored         = int.Parse(row.Groups["GoalsScored"].Value),
                    GoalsConceded       = int.Parse(row.Groups["GoalsConceded"].Value),
                    TournamentPoints    = int.Parse(row.Groups["TournamentPoints"].Value),
                };
            }
        }

        public static void FlushToDisk(IEnumerable<Team> teams, string path)
        {
            using (var sw = new StreamWriter(path, false,  Encoding.UTF8))
            using (var cw = new CsvWriter(sw, new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true
            }))
            {
                cw.WriteRecords(teams);
            }
        }
    }
}
