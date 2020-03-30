using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace lab_3
{
    public class HttpClientDownloadWithProgress : IDisposable
    {
        // Ссылка
        public string DownloadUrl { get; set; }
        // Файл
        public string DestinationFilePath { get; set; }

        private HttpClient _httpClient;

        // Коллбэк
        public delegate void ProgressChangedHandler(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage);
        public event ProgressChangedHandler ProgressChanged;

        public HttpClientDownloadWithProgress(string downloadUrl = "", string destinationFilePath = "")
        {
            DownloadUrl = downloadUrl;
            DestinationFilePath = destinationFilePath;

            _httpClient = new HttpClient { Timeout = TimeSpan.FromHours(1) };
        }

        // Запуск асинхронного скачивания контента
        public async Task StartDownload()
        {
            using (var response = await _httpClient.GetAsync(DownloadUrl, HttpCompletionOption.ResponseHeadersRead))
                await DownloadFileFromHttpResponseMessage(response);
        }

        // Подготовка 
        private async Task DownloadFileFromHttpResponseMessage(HttpResponseMessage response)
        {
            // Бросить исключение, если ответ от сервера вне диапазона 200-299 (Success)
            response.EnsureSuccessStatusCode();

            // Получение объёма контента заранее
            var totalBytes = response.Content.Headers.ContentLength;

            // Асинхронный поток для асинхронного чтения ответа сервера
            using (var contentStream = await response.Content.ReadAsStreamAsync())
                await ProcessContentStream(totalBytes, contentStream);
        }

        private async Task ProcessContentStream(long? totalDownloadSize, Stream contentStream)
        {
            var totalBytesRead = 0L;
            var readCount = 0L;
            var buffer = new byte[8192];
            var isMoreToRead = true;

            // Файловый поток с заданным буфером
            using (var fileStream = new FileStream(DestinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
            {
                do
                {
                    // Чтение потока пошагово, шаг - длина буфера
                    var bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        isMoreToRead = false;
                        TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                        continue;
                    }

                    await fileStream.WriteAsync(buffer, 0, bytesRead);

                    totalBytesRead += bytesRead;
                    readCount += 1;

                    // Каждый мегабайт (чуть меньше) вызываем коллбэк
                    if (readCount % 100 == 0)
                        TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                }
                while (isMoreToRead);
            }
        }

        private void TriggerProgressChanged(long? totalDownloadSize, long totalBytesRead)
        {
            // Подписка на событие отсутствует
            if (ProgressChanged == null)
                return;

            double? progressPercentage = null;
            if (totalDownloadSize.HasValue)
                progressPercentage = (double)totalBytesRead / totalDownloadSize.Value;

            ProgressChanged(totalDownloadSize, totalBytesRead, progressPercentage);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
