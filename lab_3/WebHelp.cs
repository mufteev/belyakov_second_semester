using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        private string _current;

        public WebDownloadFilesOnDir(List<string> links, string pathDir)
        {
            _queue = links;
            _count = links.Count;
            _dir = pathDir;
        }

        private void _webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var current = e.BytesReceived;
            var total = e.TotalBytesToReceive;
            var percent = (double)current / total * 5;
            Console.Write($"\r({_current}) {_currentFile} {new string('.', (int)percent)}");
        }

        private Task WaitForDownloadComplete(string link, string path)
        {
            using (var _webClient = new WebClient())
            {                
                _webClient.DownloadProgressChanged += _webClient_DownloadProgressChanged;
                var task = _webClient.DownloadFileTaskAsync(link, path);
                task.ContinueWith(_ =>
                {
                    _webClient.DownloadProgressChanged -= _webClient_DownloadProgressChanged;
                }, TaskContinuationOptions.ExecuteSynchronously);
                return task; 
            }
        }

        public async void Download()
        {
            for (int i = 0; i < _count; i++)
            {
                _current = $"{i + 1}/{_count}";
                _currentFile = Path.GetFileName(_queue[i]);
                var fullPath = Path.Combine(_dir, _currentFile);
                await WaitForDownloadComplete(_queue[i], fullPath);
                // Это треш, асинхронный метод скачивания работает как попало...
                Thread.Sleep(10);
                Console.WriteLine($"\r({_current}) {_currentFile}      ");
            }
        }
    }
}
