using NUnit.Framework;

namespace CustomerMaintainer.Tests
{
    [TestFixture]
    public class IndentationTests
    {
        [Test]
        public void YearIndentation_HasThreeSpaces()
        {
            string yearIndentation = "   ";
            Assert.AreEqual(yearIndentation, Indentation.Year);
        }

        [Test]
        public void MonthIndentation_HasSixSpaces()
        {
            string monthIndentation = "      ";
            Assert.AreEqual(monthIndentation, Indentation.Month);
        }

        [Test]
        public void DayIndentation_HasNineSpaces()
        {
            string dayIndentation = "         ";
            Assert.AreEqual(dayIndentation, Indentation.Day);
        }
    }
}
