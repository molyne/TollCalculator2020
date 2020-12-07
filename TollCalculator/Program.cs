using System;

namespace TollCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            TollCalculator calculator = new TollCalculator();

            var dates = new DateTime[] { new DateTime(DateTime.Now.Year, 3, 26, 6, 0, 0), new DateTime(DateTime.Now.Year, 3, 26, 8, 0, 0), new DateTime(DateTime.Now.Year, 3, 26, 17, 15, 0), new DateTime(DateTime.Now.Year, 3, 26, 16, 50, 0), new DateTime(DateTime.Now.Year, 3, 26, 18, 20, 0) };

            var result = calculator.GetTollFee(new Car(), dates);

            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
