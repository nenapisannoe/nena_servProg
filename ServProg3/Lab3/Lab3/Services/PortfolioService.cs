using System.Collections;
using System.Text.Json;
using Lab3.Models;
using Microsoft.AspNetCore.Hosting;

namespace Lab3.Services
{
    public class PortfolioService
    {
        private readonly string _filePath;
        private static readonly JsonSerializerOptions JsonSerializerOptions = new();

        public IWebHostEnvironment WebHostEnvironment { get; }

        public PortfolioService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string DataFilePath
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "Data", "Portfolio.json"); }
        }
        public IEnumerable<PortfolioItem>? GetPortfolioItems()
        {
            IEnumerable<PortfolioItem>? data = null;
           using var r = File.OpenText(DataFilePath);
            var e = File.Exists(DataFilePath);
            Console.Write(e);
           data = JsonSerializer.Deserialize<PortfolioItem[]>(r.ReadToEnd(), JsonSerializerOptions);
            Console.Write(data);
            return data;
        }
    }
}

