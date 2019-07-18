using System.Text.RegularExpressions;

namespace Gst.Services
{
    public class EmailCleaner : IEmailCleaner
    {
        public string Clean(string text)
        {
            return ReplaceEmailHeader(text);
        }

        private static string ReplaceEmailHeader(string text)
        {
            return Regex.Replace(text, @"(From:.*)|(To.*)", "");
        }
    }
}
