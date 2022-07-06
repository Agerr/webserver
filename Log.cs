using System.Net;
using System.Text;

namespace webserver
{
    public class Log
    {
        public static int logCount = 0;
        public static void NewLog(HttpListenerRequest request, HttpListener listener)
        {
            logCount++;
            UpdateConsole(listener);
            RequestLog(request);
        }
        public static void UpdateConsole(HttpListener listener)
        {
            Console.Clear();
            Console.WriteLine("Listening for connections on:");
            ListPrefixes(listener);
            Console.WriteLine("Interactions: {0}", logCount);
            Console.Write("Press any key to stop...");
        }
        private static void ListPrefixes(HttpListener listener)
        {
            // List prefixes
            Console.WriteLine(new String('-', listener.Prefixes.OrderByDescending( s => s.Length ).First().Length + 2));
            Console.WriteLine("• {0}", String.Join("\n• ", listener.Prefixes));
            Console.WriteLine(new String('-', listener.Prefixes.OrderByDescending( s => s.Length ).First().Length + 2) + Environment.NewLine);
        }
        private static void RequestLog(HttpListenerRequest request)
        {
            DateTime now = DateTime.Now;
            string logPath = $"{Environment.CurrentDirectory}/Log.txt";

            using (StreamWriter sw = File.AppendText(logPath))
            {
                sw.WriteLine($"{new String('-', 10)} {DateTime.Now.ToString("yyyy/dd/MM HH:mm:ss")} {new String('-', 10)}");
                sw.WriteLine("Type: " + request.HttpMethod);
                sw.WriteLine("Url: " + request.Url);
                sw.WriteLine("IP: " + request.RemoteEndPoint);
                sw.WriteLine("Agent: " + request.UserAgent);
                sw.WriteLine("Endpoint: " + request.LocalEndPoint);
                sw.WriteLine("Languages: " + String.Join('/', request.UserLanguages));
                sw.WriteLine($"{new String('-', 22 + DateTime.Now.ToString("yyyy/dd/MM HH:mm:ss").Length)}");
            }
        }
    }
}