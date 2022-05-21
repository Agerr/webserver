using System.Net;

namespace webserver
{
    public class Log
    {
        public static int logCount = 0;
        public static void NewLog(HttpListenerRequest request, HttpListener listener)
        {
            logCount++;
            UpdateConsole(listener);
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
        
    }
}