using System.Security.Cryptography.X509Certificates;

namespace TemperaturCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {


            // skapar ett objekt som heter data av typen temperatureData (instansierar ett objekt)
            TemperatureData data = new TemperatureData();

            Console.WriteLine("Här kommer val");
            Console.WriteLine(" val 1, skriv ut temperaturen varje dag");
            Console.WriteLine(" val 2 skriver ut medeltemperaturen");
            Console.WriteLine(" val 3 skriver ut högsta temperaturen");
            Console.WriteLine(" val 4 skriver ut lägsta temperaturen");
            Console.WriteLine(" val 5 skriver ut median temperaturen");
            Console.WriteLine(" val 6 stigande:");
            Console.WriteLine(" val 7 fallande: ");
            Console.WriteLine(" val 8 dagar med 20+grader C");
            Console.WriteLine(" val 9 skriver ut valt datum (samt dagen före och efter)");
            Console.WriteLine(" val 10 ");
            Console.WriteLine(" ditt val:");

            int choice = 0;
            if (!Int32.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("felmedd");
            }

            switch (choice)
            {
                // vi anropar funktionerna

                case 1:
                    data.PrintDateTemperature();
                    break;
                case 2:
                    data.PrintAverageTemperature();
                    break;
                case 3:
                    data.GetHighestTemperature();
                    break;
                case 4:
                    data.GetLowestTemperature();
                    break;
                case 5:
                    data.GetMedianTemperature();
                    break;
                case 6:
                    data.GetSortingTemperature(true);
                    break;
                case 7:
                    data.GetSortingTemperature(false);
                    break;
                case 8:
                    data.FilterHighTempData();
                    break;
                case 9:
                    data.PrintSpecificDates();
                    break;


                default:
                    Console.WriteLine("ej giltigt");
                    break;
            }


        }




    }

}



