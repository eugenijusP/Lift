using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;

namespace Lift.Domain.Helpers
{
    public static class Utils
    {


        public static int ExceptionResult(Exception ex)
        {
            var type = ex.GetType().ToString();
            return type switch
            {
                "DVieta.Domain.Helpers.DomainException" => 460,
                "DVieta.Infrastucture.Helpers.SqlInfraException" => 470,
                _ => 500,
            };
        }

        public static string GetRequestBody(HttpRequest Request)
        {
            using var receiveStream = Request.Body;
            using var readStream = new StreamReader(receiveStream, Encoding.UTF8);
            return readStream.ReadToEnd();
        }

        public static void LogEventToFile(string fileName, string txt)
        {
            var fullFileName = string.Format("{0}\\Log\\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
            try
            {
                using var file = new StreamWriter(fullFileName, true);
                file.WriteLine(txt);
            }
            catch
            {
                //If something happend
            }
        }

        public static void LogRequestToFile(string txt, string filename = "")
        {
            var fullFileName = FullFileName(filename);
            try
            {
                txt = $"{DateTime.Now} : {txt}";
                using var file = new StreamWriter(fullFileName, true);
                file.WriteLine(txt);
                file.Close();
            }
            catch (Exception ex)
            {
                LogEventToFile("errror.txt", ex.Message);
            }
        }

        public static string ExtractFromRequestPath(string requestPath)
        {
            requestPath = requestPath.Replace("/dvieta/", "");
            requestPath = requestPath.Replace("/pm/", "");
            requestPath = requestPath.Replace("/benco/", "");
            var i = requestPath.IndexOf("/", StringComparison.CurrentCulture);
            requestPath = requestPath.Substring(0, i);
            return requestPath;
        }

        private static string FullFileName(string filename)
        {
            var dir = string.Format("{0}Log", AppDomain.CurrentDomain.BaseDirectory);
            Directory.CreateDirectory(dir);
            dir = string.Format("{0}\\{1}", dir, DateTime.Now.ToString("yyyyMMdd"));
            Directory.CreateDirectory(dir);
            if (string.IsNullOrEmpty(filename))
            {
                return string.Format("{0}\\{1}", dir, DateTime.Now.ToString("yyyy-MM-dd HH.txt"));
            }
            else
            {
                return string.Format("{0}\\{1}", dir, filename);
            }
        }

        /// <summary>
        /// Ping host and returns true if ping goes through
        /// </summary>
        public static bool PingHost(string host)
        {
            try
            {
                using var pingSender = new Ping();
                var timeout = 120;
                host = HostForPing(host);
                var reply = pingSender.Send(host, timeout);
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Trims host for ping
        /// </summary>
        private static string HostForPing(string host)
        {
            host = host.Replace("http://", "");
            host = host.Replace("https://", "");
            host = host.Replace("https://", "");
            host = host[0..^1];
            return host;
        }
    }
}
