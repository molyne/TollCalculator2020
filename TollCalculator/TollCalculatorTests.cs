using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
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

        //skriv ett happycase test typ detta ska ge mig detta resultatet, för tider på dygnet med olika tider
        //skriv ett test för att bara kunna få maxbeloppet

        [Theory]
        [MemberData(nameof(TestData))]
        public void ShouldGetExpectedTollFee(DateTime date1, DateTime date2, int expected)
        {
            TollCalculator calculator = new TollCalculator();
            //lägg på mer testdata för alla tider på dygnet
            DateTime[] dates = new DateTime[] { date1, date2 };
            int result = calculator.GetTollFee(new Car(), dates);

            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(TollFreeDays))]
        public void ShouldBeATollFreeDay(params DateTime[] dates)
        {
            //testa andra datum än saturday
            //testa julafton och alla andra dagar då det inte ska kosta något

            TollCalculator calculator = new TollCalculator();
            int expectedResult = 0;

            int result = calculator.GetTollFee(new Car(), dates);

            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> TollFreeDays => new List<object[]>
        {
            new object[] { DateHelper.GetNextWeekday(DayOfWeek.Saturday) , DateHelper.GetLastWeekday(DayOfWeek.Saturday) },
            new object[] { DateHelper.GetChristmasEve(DateTime.Today), DateHelper.GetNewYearsEve(DateTime.Today), 
                DateHelper.GetThirteenEvening(DateTime.Today), DateHelper.GetThirteenDayChristmas(DateTime.Today)}
        };

        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            new object[] { DateTime.Today.AddHours(14).AddMinutes(59), DateTime.Today.AddHours(12), 8 },
            new object[] { DateTime.Today.AddHours(6), DateTime.Today.AddHours(6).AddMinutes(15), 8},
            new object[] { DateTime.Today.AddHours(6).AddMinutes(30), DateTime.Today.AddHours(6).AddMinutes(59), 13},
            new object[] { DateTime.Today.AddHours(6).AddMinutes(30), DateTime.Today.AddHours(6).AddMinutes(59), 13},
            new object[] { DateTime.Today.AddHours(7), DateTime.Today.AddHours(7).AddMinutes(59), 18},
            new object[] { DateTime.Today.AddHours(8), DateTime.Today.AddHours(8).AddMinutes(29), 13},
            new object[] { DateTime.Today.AddHours(8).AddMinutes(30), DateTime.Today.AddHours(14).AddMinutes(59), 8},
            new object[] { DateTime.Today.AddHours(15).AddMinutes(28), DateTime.Today.AddHours(15).AddMinutes(29), 13},
            new object[] { DateTime.Today.AddHours(15).AddMinutes(30), DateTime.Today.AddHours(16).AddMinutes(59), 18},
            new object[] { DateTime.Today.AddHours(17).AddMinutes(30), DateTime.Today.AddHours(17).AddMinutes(59), 13},
            new object[] { DateTime.Today.AddHours(18), DateTime.Today.AddHours(18).AddMinutes(29), 8},
            new object[] { DateTime.Today.AddHours(18).AddMinutes(30), DateTime.Today.AddHours(5).AddMinutes(59), 0}
        };
    }
}
