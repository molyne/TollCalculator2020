using System;
using System.Collections.Generic;
using System.Text;

namespace TollCalculator
{
    public class Motorbike : IVehicle
    {
        public string GetVehicleType()
        {
            return "MotorBike";
        }
    }
}
