using System;
using System.Text.RegularExpressions;

namespace CustomerMaintainer
{
    public static class TagService
    {
        public static int ReadValueFromDay(this string line)
        {
            Match match = Regex.Match(line, @"<day-(\d+)>(\d+)</day-(\d+)>");
            return Convert.ToInt32(match.Groups[2].Value);
        }

        public static int GetYear(this string line)
        {
            Match match = Regex.Match(line, @"<[/]?year-(\d+)>");
            return Convert.ToInt32(match.Groups[1].Value);
        }

        public static int GetMonth(this string line)
        {
            Match match = Regex.Match(line, @"<[/]?month-(\d+)>");
            return Convert.ToInt32(match.Groups[1].Value);
        }

        public static int GetDay(this string line)
        {
            Match match = Regex.Match(line, @"<day-(\d+)>(\d+)</day-(\d+)>");
            if (match.Groups[1].Value == match.Groups[3].Value)
                return Convert.ToInt32(match.Groups[1].Value);
            return 0;
        }

        public static string CreateNewDayTag(int day, int amount)
        {
            return Indentation.Day + "<day-" + day + ">" + amount + "</day-" + day + ">";
        }

        #region IsKindOfTag
        public static bool IsYearOpenTag(this string line)
        {
            MatchCollection matches = Regex.Matches(line, @"<year-(\d+)>");
            if (matches.Count > 0)
                return true;
            return false;
        }

        public static bool IsYearCloseTag(this string line)
        {
            MatchCollection matches = Regex.Matches(line, @"</year-(\d+)>");
            if (matches.Count > 0)
                return true;
            return false;
        }

        public static bool IsMonthOpenTag(this string line)
        {
            MatchCollection matches = Regex.Matches(line, @"<month-(\d+)>");
            if (matches.Count > 0)
                return true;
            return false;
        }

        public static bool IsMonthCloseTag(this string line)
        {
            MatchCollection matches = Regex.Matches(line, @"</month-(\d+)>");
            if (matches.Count > 0)
                return true;
            return false;
        }

        public static bool IsDayTag(this string line)
        {
            MatchCollection matches = Regex.Matches(line, @"<day-(\d+)>(\d+)</day-(\d+)>");
            if (matches.Count > 0)
                return true;
            return false;
        }

        public static bool IsLaterYearOpenTag(this string line, int year)
        {
            return line.IsYearOpenTag() && line.GetYear() > year;
        }

        public static bool IsLaterMonthOpenTag(this string line, int month)
        {
            return line.IsMonthOpenTag() && line.GetMonth() > month;
        }

        public static bool IsLaterDayTag(this string line, int day)
        {
            return line.IsDayTag() && line.GetDay() > day;
        }
        #endregion

        public static bool HasYearTags(this string fileContent, int year)
        {
            string regex = CreateYearOpenTag(year) + "(?s).*" + CreateYearCloseTag(year);
            MatchCollection matches = Regex.Matches(fileContent, regex);
            if (matches.Count > 0)
                return true;
            return false;
        }

        public static bool HasMonthTagsInYearTags(this string fileContent, DateTime date)
        {
            string regex = CreateYearOpenTag(date.Year) + "(?s).*" + CreateMonthOpenTag(date.Month) + "(?s).*" +
                           CreateMonthCloseTag(date.Month) + "(?s).*" + CreateYearCloseTag(date.Year);
            MatchCollection matches = Regex.Matches(fileContent, regex);
            if (matches.Count > 0)
                return true;
            return false;
        }

        #region CreateTag
        public static string CreateYearOpenTag(int year)
        {
            return Indentation.Year + "<year-" + year + ">";
        }

        public static string CreateYearCloseTag(int year)
        {
            return Indentation.Year + "</year-" + year + ">";
        }

        public static string CreateMonthOpenTag(int month)
        {
            return Indentation.Month + "<month-" + month + ">";
        }

        public static string CreateMonthCloseTag(int month)
        {
            return Indentation.Month + "</month-" + month + ">";
        }
        #endregion

        #region IsCorrectTag
        public static bool IsCorrectYearOpenTag(this string line, int year)
        {
            return line.IsYearOpenTag() && line.GetYear() == year;
        }

        public static bool IsCorrectYearCloseTag(this string line, int year)
        {
            return line.IsYearCloseTag() && line.GetYear() == year;
        }

        public static bool IsCorrectMonthOpenTag(this string line, int month)
        {
            return line.IsMonthOpenTag() && line.GetMonth() == month;
        }

        public static bool IsCorrectMonthCloseTag(this string line, int month)
        {
            return line.IsMonthCloseTag() && line.GetMonth() == month;
        }

        public static bool IsCorrectDayTag(this string line, int day)
        {
            return line.IsDayTag() && line.GetDay() == day;
        }
        #endregion
    }
}
