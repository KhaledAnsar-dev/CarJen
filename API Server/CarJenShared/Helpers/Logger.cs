using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenShared.Helpers
{
    public static class Logger
    {
        private static readonly string _logDirectory = Path.Combine(AppContext.BaseDirectory, "logs");
        private static readonly string _errorLogPath = Path.Combine(_logDirectory, "errors.log");

        public static void LogError(Exception ex, string context = "")
        {
            try
            {
                if (!Directory.Exists(_logDirectory))
                    Directory.CreateDirectory(_logDirectory);

                string message = $"[{DateTime.Now}] [{context}] {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
                File.AppendAllText(_errorLogPath, message);
            }
            catch
            {
                #if DEBUG
                    Console.WriteLine("Logging failed.");
                #endif
            }
        }
    }
}
