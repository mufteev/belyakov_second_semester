using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab_3
{
    class Program
    {
        static Dictionary<string, string> assocSection = new Dictionary<string, string>
        {
            { "0", "(pdf|download|help|gost|ref)" },
            { "1", "pdf" },
            { "2", "download" },
            { "3", "help" },
            { "4", "gost" },
            { "5", "ref" }
        };


        static void Main(string[] args)
        {
            Console.WriteLine("Выберите раздел, из которого следует скачать файлы");
            Console.Write("0 - Все разделы" +
                        "\n1 - pdf" +
                        "\n2 - download" +
                        "\n3 - help" +
                        "\n4 - gost" +
                        "\n5 - ref" +
                        "\n> ");
            var section = Console.ReadLine();

            var data = WebHelp.GetHtmlData("https://pcoding.ru/darkNet.php");
            var links = WebHelp.GetSection(data, assocSection[section]);

            Console.Write("Укажите директорию в которую выгрузить файлы (. - текущая директория)\n(Можно указать несуществующую директорию)\n> ");
            var path = Console.ReadLine();
            path = Path.GetFullPath(path);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            WebHelp.SaveFilesWithStats(links, path);

            Console.ReadKey();
        }
    }
}
