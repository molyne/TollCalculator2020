using System;
using System.Collections.Generic;
using System.Text;

namespace TollCalculator
{
    public class Car : IVehicle
    {
        public string GetVehicleType()
        {
            return "Car";
        }
    }
}
