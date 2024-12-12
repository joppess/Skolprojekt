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
            Console.WriteLine(" val 6 stigande:");
            Console.WriteLine(" val 7 fallande: ");
            // om boolen ej fångar upp så blir det else sats i val 6-----------------------------------------
            int choice = 0;
            if (!Int32.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("felmedd");
            }

            switch (choice)
            {
                // kom ihåg nummerordning
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    data.GetSortingTemperature(true);
                    break;
                case 7:
                    data.GetSortingTemperature(false);
                    break;


                default:
                    Console.WriteLine("ej giltigt");
                    break;
            }

            // vi anropar funjktionerna
           data.PrintDateTemperature();


            data.PrintAverageTemperature();

            data.GetHighestTemperature();

            data.GetLowestTemperature();

            data.GetMedianTemperature();
          

            //test
            // data.DoSomething("pokenhnhnhthbhn");

        }




    }

}



