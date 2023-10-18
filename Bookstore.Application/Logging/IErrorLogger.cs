using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Application.ErrorLogger
{
    public interface IErrorLogger
    {
        void Log(AppError error);
    }

    public class AppError
    {
        public string Username { get; set; }
        public Guid ErrorId { get; set; }
        public Exception Error { get; set; }
    }
}
