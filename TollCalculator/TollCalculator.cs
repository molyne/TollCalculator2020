using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nager.Date;

namespace TollCalculator
{
    public class TollCalculator
    {
        /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

        private const int MaxTollFee = 60;
        private const int July = 7;

        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            if (dates == null)
            {
                throw new ArgumentNullException(nameof(dates), "dates are invalid");
            }

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "vehicle is invalid");
            }

            int totalFee = 0;

            var sortedDates = dates.OrderBy(x => x.TimeOfDay).ToList();

            for (int i = 0; i < sortedDates.Count; i++)
            {
                int nextFee = GetTollFeeForSingleEvent(sortedDates[i], vehicle);

                if (i > 0)
                {
                    var tempFee = GetTollFeeForSingleEvent(sortedDates[i - 1], vehicle);

                    var minutes = sortedDates[i].Subtract(sortedDates[i - 1]).TotalMinutes;

                    bool isWithinTimeFrame = minutes <= 60;

                    if (isWithinTimeFrame)
                    {
                        if (totalFee > 0) totalFee -= tempFee;
                        if (nextFee >= tempFee) tempFee = nextFee;
                        totalFee += tempFee;
                    }
                    else
                    {
                        totalFee += nextFee;
                    }

                }
                else
                {
                    totalFee = nextFee;
                }
            }

            if (totalFee > MaxTollFee) totalFee = MaxTollFee;
            return totalFee;
        }

        private bool IsTollFreeVehicle(IVehicle vehicle)
        {
            if (vehicle == null) return false;
            String vehicleType = vehicle.GetVehicleType();

            return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Military.ToString());
        }

        private int GetTollFeeForSingleEvent(DateTime date, IVehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            int month = date.Month;

            if (DateSystem.IsPublicHoliday(date, CountryCode.SE) || DateSystem.IsWeekend(date, CountryCode.SE) ||
                month == July)
                return true;

            return false;
        }

        private enum TollFreeVehicles
        {
            Motorbike = 0,
            Tractor = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5
        }
    }
}
