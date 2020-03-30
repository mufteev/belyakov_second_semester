using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Web;
//using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace lab_3
{
    public static class WebHelp
    {
        public static string GetHtmlData(string link)
        {
            string data;

            using (var wc = new WebClient())
            {
                data = Encoding.UTF8.GetString(wc.DownloadData(link));
            }
            return data;
        }

        public static List<string> GetSection(string data, string section)
            => new Regex($@"href=(?<link>https:\/\/pcoding\.ru\/{section}\/[^\s]*)", RegexOptions.IgnoreCase)
            .Matches(data)
            .Cast<Match>()
            .Select(x => x.Groups["link"].Value)
            .ToList();

        public static void SaveFilesWithStats(List<string> links, string pathDir)
        {
            Console.WriteLine();
            var downloader = new WebDownloadFilesOnDir(links, pathDir);
            downloader.Download();
        }
    }

    internal class WebDownloadFilesOnDir
    {
        private List<string> _queue;
        private string _dir;
        private int _count;
        private string _currentFile;
        private HttpClientDownloadWithProgress _httpClient;

        private string _current;

        public WebDownloadFilesOnDir(List<string> links, string pathDir)
        {
            _queue = links;
            _count = links.Count;
            _dir = pathDir;

            _httpClient = new HttpClientDownloadWithProgress();
            _httpClient.ProgressChanged += _httpClient_ProgressChanged;
        }

        private void _httpClient_ProgressChanged(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage)
        {
            Console.Write($"\r({_current}) {_currentFile} {new string('.', (int)(progressPercentage * 5))}");
        }

        public async void Download()
        {
            for (int i = 0; i < _count; i++)
            {
                _current = $"{i + 1}/{_count}";
                _currentFile = Path.GetFileName(_queue[i]);
                var fullPath = Path.Combine(_dir, _currentFile);

                _httpClient.DownloadUrl = _queue[i];
                _httpClient.DestinationFilePath = fullPath;
                await _httpClient.StartDownload();

                Console.WriteLine($"\r({_current}) {_currentFile}      ");
            }
        }

        ~WebDownloadFilesOnDir()
        {
            _httpClient?.Dispose();
        }
    }
}
