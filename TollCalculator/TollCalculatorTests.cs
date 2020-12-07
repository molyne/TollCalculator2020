using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using Nager.Date;
using Nager.Date.Model;
using Xunit;

namespace TollCalculator
{
    public class TollCalculatorTests
    {
        [Fact]
        public void GetTollFeeShouldThrowArgumentNullExceptionIfDatesAreNull()
        {
            TollCalculator tollCalculator = new TollCalculator();

            Assert.Throws<ArgumentNullException>(() => tollCalculator.GetTollFee(new Car(), null));
        }

        [Fact]
        public void GetTollFeeShouldThrowArgumentNullExceptionIVehicleIsNull()
        {
            TollCalculator tollCalculator = new TollCalculator();

            DateTime[] dates = new DateTime[] { new DateTime(2020, 08, 08, 12, 0, 0) };

            Assert.Throws<ArgumentNullException>(() => tollCalculator.GetTollFee(null, dates));
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void ShouldGetExpectedTollFee(int expected, params DateTime[] dates)
        {
            TollCalculator calculator = new TollCalculator();

            int result = calculator.GetTollFee(new Car(), dates);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(MaxFeeData))]
        public void ShouldOnlyPayOncePerHourWithHighestToll(int expected, params DateTime[] dates)
        {
            TollCalculator calculator = new TollCalculator();

            int result = calculator.GetTollFee(new Car(), dates);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(TollFreeDays))]
        public void ShouldBeATollFreeDay(params DateTime[] dates)
        {
            TollCalculator calculator = new TollCalculator();
            int expectedResult = 0;

            int result = calculator.GetTollFee(new Car(), dates);

            Assert.Equal(expectedResult, result);

        }

        public static IEnumerable<object[]> TollFreeDays => new List<object[]>
        {
            new object[] { DateHelper.GetNextWeekday(DayOfWeek.Saturday) , DateHelper.GetLastWeekday(DayOfWeek.Saturday) },
            new object[] { new DateTime(DateTime.Now.Year, 12, 24).AddHours(8), new DateTime(DateTime.Now.Year, 01, 01).AddHours(10)},
            new object[] { new DateTime(DateTime.Today.Year, 07, 07).AddHours(9), new DateTime(DateTime.Today.Year, 07, 22).AddHours(14) }
        };

        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { 8, new DateTime(DateTime.Now.Year, 03, 26 , 14, 59, 0), new DateTime(DateTime.Now.Year, 03, 26, 12 , 0, 0)},
            new object[] { 8,  new DateTime(DateTime.Now.Year, 03, 26 , 6, 0, 0), new DateTime(DateTime.Now.Year, 03, 26, 6 , 15, 0)},
            new object[] { 13, new DateTime(DateTime.Now.Year, 03, 26 , 6, 30, 0), new DateTime(DateTime.Now.Year, 03, 26, 6 , 59, 0) },
            new object[] { 18,new DateTime(DateTime.Now.Year, 03, 26 , 7, 0, 0), new DateTime(DateTime.Now.Year, 03, 26, 7 , 59, 0) },
            new object[] { 13, new DateTime(DateTime.Now.Year, 03, 26 , 8, 0, 0), new DateTime(DateTime.Now.Year, 03, 26, 8 , 29, 0) },
            new object[] { 16 , new DateTime(DateTime.Now.Year, 03, 26 , 8, 30, 0), new DateTime(DateTime.Now.Year, 03, 26, 14 , 59, 0), },
            new object[] { 13, new DateTime(DateTime.Now.Year, 03, 26 , 15, 00, 0), new DateTime(DateTime.Now.Year, 03, 26, 15 , 29, 0)},
            new object[] { 13,new DateTime(DateTime.Now.Year, 03, 26 , 15, 28, 0), new DateTime(DateTime.Now.Year, 03, 26, 15 , 29, 0) },
            new object[] { 36,  new DateTime(DateTime.Now.Year, 03, 26 , 15, 30, 0), new DateTime(DateTime.Now.Year, 03, 26, 16 , 59, 0) },
            new object[] { 13 ,new DateTime(DateTime.Now.Year, 03, 26 , 17, 30, 0), new DateTime(DateTime.Now.Year, 03, 26, 17 , 59, 0) },
            new object[] { 8 , new DateTime(DateTime.Now.Year, 03, 26 , 18, 00, 0), new DateTime(DateTime.Now.Year, 03, 26, 18 , 29, 0)},
            new object[] { 0 ,new DateTime(DateTime.Now.Year, 03, 26 , 18, 30, 0), new DateTime(DateTime.Now.Year, 03, 26, 5 , 59, 0)},
            new object[] { 21, new DateTime(DateTime.Now.Year, 03, 26 , 6, 30, 0), new DateTime(DateTime.Now.Year, 03, 26, 18 , 28, 0)},
            new object[] { 47, new DateTime(DateTime.Now.Year, 3, 26, 6, 0, 0), new DateTime(DateTime.Now.Year, 3, 26, 8, 0, 0), new DateTime(DateTime.Now.Year, 3, 26, 17, 15, 0), new DateTime(DateTime.Now.Year, 3, 26, 16, 50, 0), new DateTime(DateTime.Now.Year, 3, 26, 18, 20, 0)}
        };

        public static IEnumerable<object[]> MaxFeeData => new List<object[]>
        {
            new object[]
            {
                13, new DateTime(DateTime.Now.Year, 03, 26 , 6, 30, 0), new DateTime(DateTime.Now.Year, 03, 26, 6 , 0, 0), new DateTime(DateTime.Now.Year, 03, 26, 6 , 1, 0),
                new DateTime(DateTime.Now.Year, 03, 26, 6 , 2, 0), new DateTime(DateTime.Now.Year, 03, 26, 6 , 3, 0)
            }
        };
    }
}
