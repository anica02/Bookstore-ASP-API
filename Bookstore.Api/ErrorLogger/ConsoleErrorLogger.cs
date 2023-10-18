using Bookstore.Application.ErrorLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.API.ErrorLogger
{
    public class ConsoleErrorLogger : IErrorLogger
    {
        public void Log(AppError error)
        {
            StringBuilder builder = new StringBuilder();
            var date = DateTime.UtcNow.ToLongDateString();
            builder.AppendLine("Date " + date);
            builder.AppendLine("Username: " + error.Username != null ? error.Username : "admin");
            builder.AppendLine("Error id: " + error.ErrorId.ToString());
            builder.AppendLine("Error message: " + error.Error.Message);
            builder.AppendLine("Stack trace: " + error.Error.StackTrace);

            Console.WriteLine(builder.ToString());
            Console.WriteLine(error.Error.InnerException);
        }
    }
}
