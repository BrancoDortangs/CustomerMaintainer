using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CustomerMaintainer
{
    internal class CustomerService
    {
        private static readonly string FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Data.xml";

        public static int ReadAmountOfCustomersAtDate(DateTime date)
        {
            bool isSameYear = false;
            bool isSameMonth = false;

            string[] lines = File.ReadAllLines(FileName);

            foreach (string line in lines)
            {
                if (line.IsCorrectYearOpenTag(date.Year))
                    isSameYear = true;
                else if (line.IsCorrectMonthOpenTag(date.Month))
                    isSameMonth = true;
                else if (line.IsCorrectMonthCloseTag(date.Month))
                    isSameMonth = false;
                else if (line.IsCorrectYearCloseTag(date.Year))
                    isSameYear = false;
                else if (isSameYear && isSameMonth && line.IsCorrectDayTag(date.Day))
                    return line.ReadValueFromDay();
            }
            return 0;
        }

        public static void SaveAmountOfCustomersAtDate(DateTime date, int? amountOfCustomers)
        {
            int customersAmount = Convert.ToInt32(amountOfCustomers);

            bool isSameYear = false;
            bool isSameMonth = false;
            bool shouldCreateNewDayTag = true;
            int indexToWriteDayTagTo = -1;

            CreateYearTagsIfNeeded(date);
            CreateMonthTagsIfNeeded(date);

            List<string> lines = File.ReadAllLines(FileName).ToList();

            for (int i = 1; i < lines.Count; i++)
            {
                if (lines[i].IsCorrectYearOpenTag(date.Year))
                    isSameYear = true;
                else if (isSameYear && lines[i].IsCorrectMonthOpenTag(date.Month))
                    isSameMonth = true;
                else if (isSameYear && lines[i].IsCorrectMonthCloseTag(date.Month))
                {
                    indexToWriteDayTagTo = i;
                    break;
                }
                else if (isSameYear && isSameMonth)
                {
                    if (lines[i].IsCorrectDayTag(date.Day))
                    {
                        indexToWriteDayTagTo = i;
                        shouldCreateNewDayTag = false;
                        break;
                    }
                    if (lines[i].IsLaterDayTag(date.Day))
                    {
                        indexToWriteDayTagTo = i;
                        break;
                    }
                }
            }

            if (indexToWriteDayTagTo != -1)
            {
                if (shouldCreateNewDayTag)
                    lines.Insert(indexToWriteDayTagTo, TagService.CreateNewDayTag(date.Day, customersAmount));
                else
                    lines[indexToWriteDayTagTo] = TagService.CreateNewDayTag(date.Day, customersAmount);
                File.WriteAllLines(FileName, lines);
            }
        }

        private static void CreateYearTagsIfNeeded(DateTime date)
        {
            string fileContent = File.ReadAllText(FileName);
            if (!fileContent.HasYearTags(date.Year))
                WriteYearTags(date.Year);
        }

        private static void CreateMonthTagsIfNeeded(DateTime date)
        {
            string fileContent = File.ReadAllText(FileName);
            if (!fileContent.HasMonthTagsInYearTags(date))
                WriteMonthTags(date);
        }

        public static int[] GetAmountOfCustomersInEachMonthFromYear(int year)
        {
            bool isSameYear = false;

            int currentMonth = 1;
            int[] customersEachMonth = new int[12];

            string[] lines = File.ReadAllLines(FileName);

            foreach (string line in lines)
            {
                if (line.IsCorrectYearOpenTag(year))
                    isSameYear = true;
                else if (line.IsCorrectYearCloseTag(year))
                    isSameYear = false;
                else if (line.IsMonthOpenTag())
                    currentMonth = line.GetMonth();
                else if (line.IsCorrectYearCloseTag(year))
                    isSameYear = false;
                else if (isSameYear && line.IsDayTag())
                    customersEachMonth[currentMonth - 1] += line.ReadValueFromDay();
            }
            return customersEachMonth;
        }

        public static int GetTotalDifferenceComparedToDate(int[] years)
        {
            bool isThisYear = false;
            bool isLastYear = false;

            int thisYear = years[0];
            int lastYear = years[1];

            int currentMonth = DateTime.Now.Month;
            int currentDay = DateTime.Now.Day;

            int month = 1;

            int customersLastYearUntilToday = 0;
            int customersThisYearUntilToday = 0;

            string[] lines = File.ReadAllLines(FileName);

            foreach (string line in lines)
            {
                if (line.IsCorrectYearOpenTag(lastYear))
                    isLastYear = true;
                else if (line.IsCorrectYearCloseTag(lastYear))
                {
                    isLastYear = false;
                    month = 1;
                }
                else if (line.IsCorrectYearOpenTag(thisYear))
                    isThisYear = true;
                else if (line.IsCorrectYearCloseTag(thisYear))
                    isThisYear = false;
                else if (line.IsMonthOpenTag())
                    month = line.GetMonth();
                else if (!line.IsMonthCloseTag() && (isLastYear || isThisYear) && line.IsDayTag())
                    if (month == currentMonth)
                    {
                        if (line.GetDay() > currentDay)
                        {
                            if (isThisYear)
                                break;
                        }
                        else if (isThisYear)
                            customersThisYearUntilToday += line.ReadValueFromDay();
                        else if (isLastYear)
                            customersLastYearUntilToday += line.ReadValueFromDay();
                    }
                    else if (month > currentMonth)
                    {
                        if (isThisYear)
                            break;
                    }
                    else if (isThisYear)
                        customersThisYearUntilToday += line.ReadValueFromDay();
                    else if (isLastYear)
                        customersLastYearUntilToday += line.ReadValueFromDay();
            }
            return customersThisYearUntilToday - customersLastYearUntilToday;
        }

        #region WriteTags
        private static void WriteYearTags(int year)
        {
            string[] lines = File.ReadAllLines(FileName);
            foreach (string line in lines)
            {
                if (line.IsLaterYearOpenTag(year) || line.Contains("</data>"))
                {
                    InsertTagsToFile(lines, line, year, false);
                    break;
                }
            }
        }

        private static void WriteMonthTags(DateTime date)
        {
            string[] lines = File.ReadAllLines(FileName);
            bool isSameYear = false;

            foreach (string line in lines)
            {
                if (line.IsCorrectYearOpenTag(date.Year))
                    isSameYear = true;
                else if (line.IsCorrectYearCloseTag(date.Year))
                {
                    InsertTagsToFile(lines, line, date.Month);
                    break;
                }
                else if (isSameYear)
                {
                    if (line.IsLaterMonthOpenTag(date.Month))
                    {
                        InsertTagsToFile(lines, line, date.Month);
                        break;
                    }
                }
            }
        }

        private static void InsertTagsToFile(string[] lines, string line, int amount, bool monthTag = true)
        {
            int lineIndex = Array.IndexOf(lines, line);
            List<string> list = lines.ToList();
            if (monthTag)
            {
                list.Insert(lineIndex, TagService.CreateMonthOpenTag(amount));
                list.Insert(lineIndex + 1, TagService.CreateMonthCloseTag(amount));
            }
            else
            {
                list.Insert(lineIndex, TagService.CreateYearOpenTag(amount));
                list.Insert(lineIndex + 1, TagService.CreateYearCloseTag(amount));
            }
            File.WriteAllLines(FileName, list);
        }
        #endregion

        public static double GetAverageToGoPerDayThisMonth(int[] years)
        {
            int customersLastYearThisMonth = GetAmountOfCustomersInMonthForDate(years[1], DateTime.Now.Month);
            int customersThisYearThisMonth = GetAmountOfCustomersInMonthForDate(years[0], DateTime.Now.Month);
            double difference = customersLastYearThisMonth - customersThisYearThisMonth;
            int amountOfWorkdaysLeftInThisMonth = GetAmountOfWorkingDaysLeftInMonth();
            double averagePerDay = difference / amountOfWorkdaysLeftInThisMonth;
            return averagePerDay;
        }

        private static bool IsWorkDay(DateTime day)
        {
            return day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday;
        }

        private static int GetAmountOfWorkingDaysLeftInMonth()
        {
            DateTime today = DateTime.Now;
            var remainingDates = Enumerable.Range(today.Day, DateTime.DaysInMonth(today.Year, today.Month) - today.Day + 1)
                                .Select(day => new DateTime(today.Year, today.Month, day));
            return remainingDates.Count(IsWorkDay);
        }

        public static int GetAmountOfCustomersInMonthForDate(int year, int month)
        {
            bool isSameYear = false;
            bool isSameMonth = false;

            int customersThisMonth = 0;

            string[] lines = File.ReadAllLines(FileName);

            foreach (string line in lines)
            {
                if (line.IsCorrectYearOpenTag(year))
                    isSameYear = true;
                else if (line.IsCorrectMonthOpenTag(month))
                    isSameMonth = true;
                else if (line.IsCorrectMonthCloseTag(month))
                    isSameMonth = false;
                else if (line.IsCorrectYearCloseTag(year))
                    isSameYear = false;
                else if (isSameYear && isSameMonth && line.IsDayTag())
                    customersThisMonth += line.ReadValueFromDay();
            }
            return customersThisMonth;
        }

        #region Unused    
        private static int GetAmountOfCustomersInYear(int year)
        {
            bool isSameYear = false;

            int customersThisYear = 0;

            string[] lines = File.ReadAllLines(FileName);

            foreach (string line in lines)
            {
                if (line.IsCorrectYearOpenTag(year))
                    isSameYear = true;
                else if (line.IsCorrectYearCloseTag(year))
                    isSameYear = false;
                else if (isSameYear && line.IsDayTag())
                    customersThisYear += line.ReadValueFromDay();
            }
            return customersThisYear;
        }
        #endregion
    }
}