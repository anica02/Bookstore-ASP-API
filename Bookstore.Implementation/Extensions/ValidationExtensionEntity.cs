using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.Extensions
{
    public class ValidationExtensionEntity
    {

        public static IEnumerable<string> AllowedBookCovers => new List<string>
        {
           "Softcover", "Hardcover"
        };

        public static IEnumerable<string> AllowedBookWriting => new List<string>
        {
           "Cyrillic",  "Latin Alphabet"
        };

        public static IEnumerable<string> AllowedBookFormats => new List<string>
        {
          "12x13", "14x20", "13x20"
        };


        public static IEnumerable<string> AllowedFileExtensions => new List<string>
        {
          "jpg", "jpeg", "mp4", "gif", "png"
        };

        public static IEnumerable<string> AllowedDeliveryMethods => new List<string>
        {
           "post office", "express"
        };

        public static IEnumerable<string> AllowedPaymentMethods => new List<string>
        {
           "cash","credit card"
        };
    }
}
