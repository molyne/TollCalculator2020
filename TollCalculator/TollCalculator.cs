using System;
using System.Collections.Generic;
using System.Text;

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

            //gör ett console projekt så jag kan testa där
            //gör tester
            //kolla nullcheckar

            private const int MaxTollFee = 60;
            private const int TimeFrame = 60;
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

                // Är det verkligen säkert att det är start som ligger först i arryen?
                DateTime intervalStart = dates[0];
                int totalFee = 0;
                foreach (DateTime date in dates)
                {
                    int nextFee = GetTollFee(date, vehicle);
                    int tempFee = GetTollFee(intervalStart, vehicle);

                    long diffInMillies = date.Millisecond - intervalStart.Millisecond;
                    long minutes = diffInMillies / 1000 / 60;

                    bool isWithinTimeFrame = minutes <= TimeFrame;

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

                if (totalFee > MaxTollFee) totalFee = MaxTollFee;
                return totalFee;
            }

            private bool IsTollFreeVehicle(IVehicle vehicle)
            {
                if (vehicle == null) return false;
                String vehicleType = vehicle.GetVehicleType();
                //kan man göra detta kortare?
                return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
                       vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
                       vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
                       vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
                       vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
                       vehicleType.Equals(TollFreeVehicles.Military.ToString());
            }

            //har samma namn och är publik, thats wierd?
            //Vad gör d
            private int GetTollFee(DateTime date, IVehicle vehicle)
            {
                if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

                int hour = date.Hour;
                int minute = date.Minute;

                //magic numbers
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
                int year = date.Year;
                int month = date.Month;
                //int day = date.Day;

                //if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

                    //magic numbers
                    //skriv test som testar olika tollfreedates
                    //detta är holidays skriv bättre kod
                    //typ get isweekend
                    //if (month == 1 && day == 1 ||
                    //    month == 3 && (day == 28 || day == 29) ||
                    //    month == 4 && (day == 1 || day == 30) ||
                    //    month == 5 && (day == 1 || day == 8 || day == 9) ||
                    //    month == 6 && (day == 5 || day == 6 || day == 21) ||
                    //    month == 7 ||
                    //    month == 11 && day == 1 ||
                    //    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
                    //{
                    //    return true;
                    //}
                    if (DateHelper.IsHoliday(date) || DateHelper.IsWeekend(date) || month == July)
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
