using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public static class TestHelper
    {
        public static string Get4CharacterRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            var output = Regex.Replace(path, @"[\d-]", string.Empty);
            return output.Substring(0, 4);  // Return 8 character string
        }


    }
}
