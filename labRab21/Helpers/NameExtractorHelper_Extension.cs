using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace labRab21.Helpers
{
    internal static class NameExtractorHelper_Extension
    {
        public static string GetShortName(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return string.Empty;
            }
            string[] results = inputString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            switch (results.Length)
            {
                case 1:
                    return results[0];
                case 2:
                    return string.Format("{0} {1}.", results[0], results[1].FirstOrDefault());
                default:
                    return string.Format("{0} {1}. {2}", results[0], results[1].FirstOrDefault(), results[2].FirstOrDefault());
            }
        }
    }

}
