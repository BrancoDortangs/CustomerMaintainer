using System;
using NUnit.Framework;

namespace CustomerMaintainer.Tests
{
    [TestFixture]
    public class TagServiceTests
    {
        private const string Year = "2018";
        private const string Month = "4";
        private const string Day = "21";

        private const string FileContent = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n" +
                                           "<data>\n" +
               Indentation.Year + "<year-" + Year + ">\n" +
                 Indentation.Month + "<month-" + Month + ">\n" +
                      Indentation.Day + "<day-" + Day + ">7</day-" + Day + ">\n" +
                 Indentation.Month + "</month-" + Month + ">\n" +
               Indentation.Year + "</year-" + Year + ">\n" +
                                           "</data>";

        [Test]
        public void ReadValueFromDay_WithValidDay_ReturnsAmount([Range(0, 50, 1)]int amount)
        {
            string dayTag = "<day-7>" + amount + "</day-7>";
            Assert.AreEqual(amount, dayTag.ReadValueFromDay());
        }

        [Test]
        [TestCase(Indentation.Year + "<year-2018>")]
        [TestCase(Indentation.Year + "</year-2018>")]
        public void GetYear_WithLineThatContainsYear_ReturnsYear(string line)
        {
            int year = line.GetYear();
            Assert.AreEqual(year, 2018);
        }

        [Test]
        [TestCase(Indentation.Month + "<month-6>")]
        [TestCase(Indentation.Month + "</month-6>")]
        public void GetMonth_WithLineThatContainsMonth_ReturnsMonth(string line)
        {
            int month = line.GetMonth();
            Assert.AreEqual(month, 6);
        }

        [Test]
        public void GetDay_WithLineThatContainsDay_ReturnsDay([Range(1, 31, 1)] int day)
        {
            string dayTag = "<day-" + day + ">1</day-" + day + ">";
            int result = dayTag.GetDay();
            Assert.AreEqual(result, day);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(31, 50)]
        public void CreateNewDayTag_WithDayAndAmount_CreatesDayTag(int day, int amount)
        {
            string dayTag = TagService.CreateNewDayTag(day, amount);
            Assert.AreEqual(dayTag, Indentation.Day + "<day-" + day + ">" + amount + "</day-" + day + ">");
        }

        [Test]
        public void IsYearOpenTag_WithYearOpenTag_ReturnsTrue()
        {
            const string yearOpenTag = "<year-2018>";
            bool isYearOpenTag = yearOpenTag.IsYearOpenTag();
            Assert.AreEqual(isYearOpenTag, true);
        }

        [Test]
        public void IsYearOpenTag_WithWrongLine_ReturnsFalse()
        {
            const string randomTag = "INVALID LINE";
            bool isYearOpenTag = randomTag.IsYearOpenTag();
            Assert.AreEqual(isYearOpenTag, false);
        }

        [Test]
        public void IsYearCloseTag_WithYearCloseTag_ReturnsTrue()
        {
            const string yearCloseTag = "</year-2018>";
            bool isYearCloseTag = yearCloseTag.IsYearCloseTag();
            Assert.AreEqual(isYearCloseTag, true);
        }

        [Test]
        public void IsYearCloseTag_WithWrongLine_ReturnsFalse()
        {
            const string randomTag = "INVALID LINE";
            bool isYearCloseTag = randomTag.IsYearCloseTag();
            Assert.AreEqual(isYearCloseTag, false);
        }

        [Test]
        public void IsMonthOpenTag_WithMonthOpenTag_ReturnsTrue()
        {
            const string monthOpenTag = "<month-1>";
            bool isMonthOpenTag = monthOpenTag.IsMonthOpenTag();
            Assert.AreEqual(isMonthOpenTag, true);
        }

        [Test]
        public void IsMonthOpenTag_WithWrongLine_ReturnsFalse()
        {
            const string randomTag = "INVALID LINE";
            bool isMonthOpenTag = randomTag.IsMonthOpenTag();
            Assert.AreEqual(isMonthOpenTag, false);
        }

        [Test]
        public void IsMonthCloseTag_WithMonthCloseTag_ReturnsTrue()
        {
            const string monthCloseTag = "</month-1>";
            bool isMonthCloseTag = monthCloseTag.IsMonthCloseTag();
            Assert.AreEqual(isMonthCloseTag, true);
        }

        [Test]
        public void IsMonthCloseTag_WithWrongLine_ReturnsFalse()
        {
            const string randomTag = "INVALID LINE";
            bool isMonthCloseTag = randomTag.IsMonthCloseTag();
            Assert.AreEqual(isMonthCloseTag, false);
        }

        [Test]
        public void IsDayTag_WithDayTag_ReturnsTrue()
        {
            const string dayTag = "<day-1>10</day-1>";
            bool isDayTag = dayTag.IsDayTag();
            Assert.AreEqual(isDayTag, true);
        }

        [Test]
        public void IsLaterYearOpenTag_WithLaterYearOpenTag_ReturnsTrue([Range(2017, 2027, 1)]int year)
        {
            string laterYearOpenTag = "<year-" + (year + 1) + ">";
            bool isLaterYearOpenTag = laterYearOpenTag.IsLaterYearOpenTag(year);
            Assert.AreEqual(isLaterYearOpenTag, true);
        }

        [Test]
        public void IsLaterYearOpenTag_WithSameYearOpenTag_ReturnsFalse([Range(2017, 2027, 1)]int year)
        {
            string sameYearOpenTag = "<year-" + year + ">";
            bool isLaterYearOpenTag = sameYearOpenTag.IsLaterYearOpenTag(year);
            Assert.AreEqual(isLaterYearOpenTag, false);
        }

        [Test]
        public void IsLaterYearOpenTag_WithEarlierYearOpenTag_ReturnsFalse([Range(2017, 2027, 1)]int year)
        {
            string earlierYearOpenTag = "<year-" + (year - 1) + ">";
            bool isLaterYearOpenTag = earlierYearOpenTag.IsLaterYearOpenTag(year);
            Assert.AreEqual(isLaterYearOpenTag, false);
        }

        [Test]
        public void IsLaterYearOpenTag_WithWrongLine_ReturnsFalse([Range(2017, 2027, 1)]int year)
        {
            const string randomTag = "INVALID LINE";
            bool isLaterYearOpenTag = randomTag.IsLaterYearOpenTag(year);
            Assert.AreEqual(isLaterYearOpenTag, false);
        }

        [Test]
        public void IsLaterMonthOpenTag_WithLaterMonthOpenTag_ReturnsTrue([Range(1, 12, 1)]int month)
        {
            string laterMonthOpenTag = "<month-" + (month + 1) + ">";
            bool isLaterMonthOpenTag = laterMonthOpenTag.IsLaterMonthOpenTag(month);
            Assert.AreEqual(isLaterMonthOpenTag, true);
        }

        [Test]
        public void IsLaterMonthOpenTag_WithSameMonthOpenTag_ReturnsFalse([Range(1, 12, 1)]int month)
        {
            string sameMonthOpenTag = "<month-" + month + ">";
            bool isLaterMonthOpenTag = sameMonthOpenTag.IsLaterMonthOpenTag(month);
            Assert.AreEqual(isLaterMonthOpenTag, false);
        }

        [Test]
        public void IsLaterMonthOpenTag_WithEarlierMonthOpenTag_ReturnsFalse([Range(1, 12, 1)]int month)
        {
            string earlierMonthOpenTag = "<month-" + (month - 1) + ">";
            bool isLaterMonthOpenTag = earlierMonthOpenTag.IsLaterMonthOpenTag(month);
            Assert.AreEqual(isLaterMonthOpenTag, false);
        }

        [Test]
        public void IsLaterMonthOpenTag_WithWrongLine_ReturnsFalse([Range(1, 12, 1)]int month)
        {
            const string randomTag = "INVALID LINE";
            bool isLaterMonthOpenTag = randomTag.IsLaterMonthOpenTag(month);
            Assert.AreEqual(isLaterMonthOpenTag, false);
        }

        [Test]
        public void IsLaterDayTag_WithLaterDayTag_ReturnsTrue([Range(1, 31, 1)]int day)
        {
            string laterDayTag = "<day-" + (day + 1) + ">10</day-" + (day + 1) + ">";
            bool isLaterDayTag = laterDayTag.IsLaterDayTag(day);
            Assert.AreEqual(isLaterDayTag, true);
        }

        [Test]
        public void IsLaterDayTag_WithSameDayTag_ReturnsFalse([Range(1, 31, 1)]int day)
        {
            string sameDayTag = "<day-" + day + ">10</day-" + day + ">";
            bool isLaterDayTag = sameDayTag.IsLaterDayTag(day);
            Assert.AreEqual(isLaterDayTag, false);
        }

        [Test]
        public void IsLaterDayTag_WithEarlierDayTag_ReturnsFalse([Range(1, 31, 1)]int day)
        {
            string earlierDayTag = "<day-" + (day - 1) + ">10</day-" + (day - 1) + ">";
            bool isLaterDayTag = earlierDayTag.IsLaterDayTag(day);
            Assert.AreEqual(isLaterDayTag, false);
        }

        [Test]
        public void IsLaterDayTag_WithWrongLine_ReturnsFalse([Range(1, 31, 1)]int day)
        {
            const string randomTag = "INVALID LINE";
            bool isLaterDayTag = randomTag.IsLaterDayTag(day);
            Assert.AreEqual(isLaterDayTag, false);
        }

        [Test]
        public void HasYearTags_WithYearTags_ReturnsTrue()
        {
            bool hasYearTags = FileContent.HasYearTags(2018);
            Assert.AreEqual(hasYearTags, true);
        }

        [Test]
        public void HasMonthTagsInYearTags_WithMonthTagsInYearTags_ReturnsTrue()
        {
            DateTime date = new DateTime(2018, 4, 1);
            bool hasMonthTagsInYearTags = FileContent.HasMonthTagsInYearTags(date);
            Assert.AreEqual(hasMonthTagsInYearTags, true);
        }

        [Test]
        public void CreateYearOpenTag_WithValidYear_CreatesYearOpenTag([Range(2017, 2027, 1)] int year)
        {
            string yearOpenTag = TagService.CreateYearOpenTag(year);

            Assert.AreEqual(yearOpenTag, Indentation.Year + "<year-" + year + ">");
        }

        [Test]
        public void CreateYearCloseTag_WithValidYear_CreatesYearCloseTag([Range(2017, 2027, 1)] int year)
        {
            string yearCloseTag = TagService.CreateYearCloseTag(year);

            Assert.AreEqual(yearCloseTag, Indentation.Year + "</year-" + year + ">");
        }

        [Test]
        public void CreateMonthOpenTag_WithValidMonth_CreatesMonthOpenTag([Range(1, 12, 1)] int month)
        {
            string monthOpenTag = TagService.CreateMonthOpenTag(month);

            Assert.AreEqual(monthOpenTag, Indentation.Month + "<month-" + month + ">");
        }

        [Test]
        public void CreateMonthCloseTag_WithValidMonth_CreatesMonthCloseTag([Range(1, 12, 1)] int month)
        {
            string monthCloseTag = TagService.CreateMonthCloseTag(month);

            Assert.AreEqual(monthCloseTag, Indentation.Month + "</month-" + month + ">");
        }

        [Test]
        public void IsCorrectYearOpenTag_WithCorrectYearOpenTag_ReturnsTrue([Range(2017, 2027, 1)] int year)
        {
            string correctYearOpenTag = "<year-" + year + ">";
            bool isCorrectYearOpenTag = correctYearOpenTag.IsCorrectYearOpenTag(year);
            Assert.AreEqual(isCorrectYearOpenTag, true);
        }

        [Test]
        public void IsCorrectYearCloseTag_WithCorrectYearCloseTag_ReturnsTrue([Range(2017, 2027, 1)] int year)
        {
            string correctYearCloseTag = "</year-" + year + ">";
            bool isCorrectYearCloseTag = correctYearCloseTag.IsCorrectYearCloseTag(year);
            Assert.AreEqual(isCorrectYearCloseTag, true);
        }

        [Test]
        public void IsCorrectMonthOpenTag_WithCorrectMonthOpenTag_ReturnsTrue([Range(1, 12, 1)] int month)
        {
            string correctMonthOpenTag = "<month-" + month + ">";
            bool isCorrectMonthOpenTag = correctMonthOpenTag.IsCorrectMonthOpenTag(month);
            Assert.AreEqual(isCorrectMonthOpenTag, true);
        }

        [Test]
        public void IsCorrectMonthCloseTag_WithCorrectMonthCloseTag_ReturnsTrue([Range(1, 12, 1)] int month)
        {
            string correctMonthCloseTag = "</month-" + month + ">";
            bool isCorrectMonthCloseTag = correctMonthCloseTag.IsCorrectMonthCloseTag(month);
            Assert.AreEqual(isCorrectMonthCloseTag, true);
        }

        [Test]
        public void IsCorrectDayOpenTag_WithCorrectDayTag_ReturnsTrue([Range(1, 31, 1)] int day)
        {
            string correctDayTag = "<day-" + day + ">10</day-" + day + ">";
            bool isCorrectDayTag = correctDayTag.IsCorrectDayTag(day);
            Assert.AreEqual(isCorrectDayTag, true);
        }
    }
}