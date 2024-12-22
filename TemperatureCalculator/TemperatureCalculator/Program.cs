using System.Security.Cryptography.X509Certificates;

namespace TemperaturCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {


            // skapar ett objekt som heter data av typen temperatureData (instansierar ett objekt)
            TemperatureData data = new TemperatureData();
            //  vi kallar på metoden FillTemperatureArray
            data.FillTemperatureArray();

            // vi använder en while-sats så programmet blir mer flytande (loopas)
            bool menuLoop = true;
            while (menuLoop)
            {
                Console.WriteLine("**************************");
                Console.WriteLine(" HUVUDMENY ");
                Console.WriteLine("**************************");
                Console.WriteLine(" val 0 avsluta programmet ");
                Console.WriteLine(" val 1 skriv ut en lista för temperaturen varje dag i Maj");
                Console.WriteLine(" val 2 skriv ut medeltemperaturen");
                Console.WriteLine(" val 3 skriv ut högsta temperaturen i Maj");
                Console.WriteLine(" val 4 skriv ut lägsta temperaturen");
                Console.WriteLine(" val 5 skriv ut median temperaturen");
                Console.WriteLine(" val 6 skriv ut en lista med stigande temperaturer för Maj:");
                Console.WriteLine(" val 7 skriv ut en lista med fallande temperaturer för Maj: ");
                Console.WriteLine(" val 8 skriv ut dagar som är varmare än 20C");
                Console.WriteLine(" val 9 skriv ut valt datum (samt dagen före och efter)");
                Console.WriteLine(" val 10 skriver ut de mest förekommande temperaturen i Maj");
                Console.WriteLine(" ditt val: ");

                int choice = 0;
                if (!Int32.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine(" Fel inmatning. Programmet avslutas ");
                }
                Console.Write("\n");

                switch (choice)
                {
                    // avslutar programmet 
                    case 0:
                        menuLoop = false;
                        break;
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
                    case 10:
                        data.PrintMostCommonTemp();
                        break;
                        // default fångar upp om användaren skriver ett ogiltigt val
                    default:
                        Console.WriteLine("ej giltigt");
                        break;
                }

            }
        }




    }

}



