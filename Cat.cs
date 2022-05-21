using System.Net;
using System.Text;

namespace webserver
{
    public class Cat
    {
        public static async Task Response(HttpListenerResponse response, string catsDir, int statusCode)
        {
            byte[] data = await System.IO.File.ReadAllBytesAsync($"{catsDir}{statusCode.ToString()}.jpeg");

            response.StatusCode = statusCode;
            response.ContentType = "image/jpeg";
            response.ContentLength64 = data.LongLength;
            response.ContentEncoding = Encoding.UTF8;

            await response.OutputStream.WriteAsync(data, 0, data.Length);
        }
    }
}