using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Mayfly.Extensions
{
    public static class StringExtensions
    {
        public static string[] GetLongWords(this string text, int minLength)
        {
            string[] words = text.Split(' ');
            List<string> result = new List<string>();
            foreach (string word in words)
            {
                if (word.Length >= minLength)
                {
                    result.Add(word);
                }
            }
            return result.ToArray();
        }

        public static string InsertBreaks(this string comments, int rowLength)
        {
            if (comments.Length > rowLength)
            {
                string start = comments.Substring(0, rowLength);
                int breakIndex = start.LastIndexOf(' ');
                start = comments.Substring(0, breakIndex);
                string end = comments.Substring(breakIndex);
                comments = start.Trim() + Constants.Break + InsertBreaks(end.Trim(), rowLength);
            }
            return comments;
        }

        public static string[] Split(this string text, int firstlinelength, int alllineslength)
        {
            List<string> result = new List<string>();

            if (text.Length > firstlinelength)
            {
                string start = text.Substring(0, firstlinelength);
                int breakIndex = start.LastIndexOfAny(new char[] { ' ', '-' }) + 1;

                if (breakIndex == -1)
                {
                    result.Add(string.Empty);
                    result.AddRange(text.Split(alllineslength));
                }
                else
                {
                    start = text.Substring(0, breakIndex);
                    result.Add(start.Trim());
                    string end = text.Substring(breakIndex);
                    result.AddRange(end.Trim().Split(alllineslength));
                }

                return result.ToArray();
            }

            return new string[] { text };
        }

        public static string[] Split(this string text, int linelength)
        {
            return text.Split(linelength, linelength);
        }

        public static bool Contains(this string value, char c)
        {
            return new List<char>(value.ToCharArray()).Contains(c);
        }

        public static char GetInitial(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return ' ';
            if ((Constants.Forbidden + "\"").Contains(value[0])) return ((value.Length > 1) ? (GetInitial(value.Substring(1))) : ' ');
            else return value[0];
        }

        public static string GetHashSha256(this string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public static string SanitizePath(this string path, char replaceChar)
        {
            string dir = Path.GetDirectoryName(path);
            foreach (char c in Path.GetInvalidPathChars())
                dir = dir.Replace(c, replaceChar);

            string name = Path.GetFileName(path);
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, replaceChar);

            return dir + name;
        }

        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string StripNumbers(this string input)
        {
            return Regex.Replace(input, @"[\d-]", string.Empty);
        }

        public static string GetLocalizedValue(this string value)
        {
            return GetLocalizedValue(value, CultureInfo.CurrentCulture);
        }

        public static string GetLocalizedValue(this string value, CultureInfo ci)
        {
            string result = string.Empty;

            CultureInfo currentci = CultureInfo.InvariantCulture;

            foreach (string line in value.Split(new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries))
            {
                string line1 = line;
                if (line.Length > 3 && line[2] == ':')
                {
                    try
                    {
                        currentci = new CultureInfo(line.Substring(0, 2));
                        line1 = line.Substring(3).Trim();
                    }
                    catch { }
                }

                if (currentci.Equals(ci))
                    result += line1;
            }

            if (string.IsNullOrWhiteSpace(result) && !ci.Equals(CultureInfo.InvariantCulture)) return GetLocalizedValue(value, CultureInfo.InvariantCulture);
            else return result;
        }
    }
}
