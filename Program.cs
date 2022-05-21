using System.Net;

namespace webserver
{
    class Webserver
    {
        // Config variables:
        public static string[] prefixes = { "http://*:6969/" };
        public static string rootDir = $"{Environment.CurrentDirectory}/root/";
        public static string catsDir = $"{Environment.CurrentDirectory}/cats/";
        // ----------------
        public static HttpListener listener = new HttpListener();
        public static bool handleConnections = true;
        public static void Main()
        {
            // Create a Http server and start listening for incoming connections
            foreach (string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }
            listener.Start();

            // Update console view
            Log.UpdateConsole(listener);

            // Task for handling all of the connections
            HandleConnections();

            // Wait for user to end session
            Console.ReadKey();
            listener.Close();
        }
        static async Task HandleConnections()
        {
            while (handleConnections)
            {
                // Get request/response
                HttpListenerContext ctx = await listener.GetContextAsync();
                HttpListenerRequest request = ctx.Request;
                HttpListenerResponse response = ctx.Response;

                // Log it
                Log.NewLog(request, listener);

                // Handle different request types
                switch(request.HttpMethod.ToString())
                {
                    case "GET":
                        await TypeGet.main(request, response, rootDir, catsDir);
                        break;
                    default:
                        await Cat.Response(response, catsDir, 405);
                        break;
                }

                // End of response
                response.Close();
            }
        }
    }
}