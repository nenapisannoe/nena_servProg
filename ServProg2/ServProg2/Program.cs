using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ServProg1
{
    class SimpleHttpServer
    {
        private readonly string _rootDirectory;
        private HttpListener listener;

        public SimpleHttpServer(string rootDirectory, string[] prefixes)
        {
            listener = new HttpListener();
            _rootDirectory = rootDirectory;
            foreach (string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Ожидание запросов...");

            listener.Start();

            while (listener.IsListening)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                Task.Run(() => ProcessAsync(context));
            }
            listener.Close();
        }

        async Task ProcessAsync(HttpListenerContext context)
        {
            string requestPath = context.Request.Url.LocalPath;
            string filePath = Path.Combine(_rootDirectory, requestPath.TrimStart('/'));

            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (File.Exists(filePath))
            {
                byte[] buffer = File.ReadAllBytes(filePath);
                response.StatusCode = (int)HttpStatusCode.OK;
                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("404 - Кофейник не найден");
                await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            }

            LogRequest(context);
            context.Response.Close();
        }

        void LogRequest(HttpListenerContext context)
        {
            string logMessage = $"Дата обращения: {DateTime.Now}, IP клиента: {context.Request.RemoteEndPoint.Address}, путь обращения: {context.Request.Url.LocalPath}, код ответа: {context.Response.StatusCode}";
            File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            string rootDirectory = @"C:\Users\nena\source\repos\ServProg2\ServProg2\content";
            string[] prefixes = { "http://localhost:8080/" };

            SimpleHttpServer server = new SimpleHttpServer(rootDirectory, prefixes);
            await server.RunAsync();
        }
    }
}
