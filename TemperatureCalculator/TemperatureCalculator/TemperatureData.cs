using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace TemperaturCalculator
{
    // internal betyder att det gäller för namespacet (egen notering)

    internal class TemperatureData
    {
        // vi skapar en statisk array för antalet dagar i Maj-månad.
        int[] days = new int[31];
        // instans skapas???????????
        // vi kallar på metoden FillTemperatureArray????????
        public TemperatureData()
        {
            FillTemperatureArray();
        }

        void FillTemperatureArray()
        {
            // vi skapar variabeln random av datatypen Random som vi ger en ny instans.
            Random random = new Random();
            // vi skapar variabel "i" och sätter den till 0. Vi loopar (for-loop) för varje dag i Maj.
            // I varje varv i loopen lägger vi till +1. Loopen fortsätts så länge som variablen i är mindre än 31.
            // +1 används för at Maj inte ska börja på dag 0.
            for (int i = 0; i < 31; i++)
            {
                // vi skapar slumpmässiga tal mellan 1 - 23 (min,max+1) för varje dag i arrayen "days"

                days[i] = random.Next(1, 23 + 1);

            }

        }
        public void PrintDateTemperature()
        {
            // likadant här men vi skriver ut det med hjälp av tab (\t) för att snygga till utskriften.
            // Vi skapar ett nytt datum för varje varv i loopen. 
            for (int i = 0; i < 31; i++)
            {
                Console.Write(days[i] + "\t");
                DateTime dateTime = new DateTime(2024, 5, i + 1);
                Console.Write(dateTime.ToString("yyyy/MM/dd") + "\n");
            }

        }
        // vi skapar funktionen PrintAverageTemperature
        // den är public så att vi kan anropa den från olika delar i programmet
        public void PrintAverageTemperature()
        {
            int averageTemperature = 0;

            for (int i = 0; i < 31; i++)
            {
                // averageTemperature = averageTemperature = days[i]
                averageTemperature += days[i];


            }

            // avergageTemperature / averageTemperature = days.Length;
            averageTemperature /= days.Length;
            Console.WriteLine($" The average temperature is {averageTemperature}");

        }
        // en till funktion skapas för att beräkna den högsta temperaturen?????????????????????????????
        public void GetHighestTemperature()
        {
            int hottestTemperature = 0;

            DateTime dateTime = DateTime.Now;
            for (int i = 0; i < 31; i++)
            {
                if (hottestTemperature < days[i])
                {

                    hottestTemperature = days[i];
                    dateTime = new DateTime(2024, 5, i + 1);
                }
            }

            Console.WriteLine($" Datumet {dateTime.ToString("yyyy/MM/dd")} var den varmaste dagen med högsta temperaturen: {hottestTemperature}C ");
        }

        public void GetLowestTemperature()
        {
            int coldestTemperature = 25;
            DateTime dateTime = DateTime.Now;
            for (int i = 0; i < 31; i++)
            {
                if (coldestTemperature > days[i])
                {
                    coldestTemperature = days[i];
                    dateTime = new DateTime(2024, 5, i + 1);
                }

            }
            Console.WriteLine($" Datumet {dateTime.ToString("yyyy/MM/dd")} var den kallaste dagen med lägsta tmperaturen {coldestTemperature}");
        }

        public void GetMedianTemperature()
        {
            // vi sorterar dagarna i maj för att kunna beräkna medianen???????????
            Array.Sort(days);

            int medianTemperature = 0;

            for (int i = 0; i < 31; i++)
            {
                medianTemperature += days[i];
                // vi hämtar antalet element i arrayen med "days.Length"
                // vi använder ( modulo %) som räknar ut resten av ett tal när det delas med ett annat tal (days.Lenght % 2)
                // vi får då svaret udda dagar----------------------------------------------
                // med "2 != 0" kontrollerar vi så 2 inte är lika med 0. Vilket är sant.
                if (days.Length % 2 != 0)
                {
                    medianTemperature = days[days.Length / 2];

                    // lägga till else sats här?? med medelvärdet istället???
                }

            }
            Console.WriteLine($"Median temperaturen i Maj är {medianTemperature} ");
        }
        // förklaring                 
        public void GetSortingTemperature(bool acsending) //hur gör vi den false?????
        {
            // vi hämtar datumet ???????????????
            DateTime dateTime = DateTime.Now;

            if (acsending)
            {
                Array.Sort(days);
                Console.WriteLine("Temperaturerna sorterade i stigande ordning");

            }
            else
            {
                Array.Sort(days);
                Array.Reverse(days);
                Console.WriteLine("Temperaturerna i fallande ordning");
            }
            for (int i = 0; i < 31; i++)
            {
                // vi tilldelar ett nytt datum för variablen dateTime
                dateTime = new DateTime(2024, 5, i + 1);
                // vi skriver ut dagarna som hämtas från arayyen +1 samt skriver det konvereratde datumet till en sträng enligt yyyy/MM/dd????????????????????????????????skriv om
                Console.WriteLine($" {days[i]} C {dateTime.ToString("yyyy/MM/dd")} ");
                // {days[i]}°C");????????????????????????????????????????????????????????
            }
        }

        public void FilterHighTempData()
        {   // vi sätter värdet på filterHighTemp till 20. Är värdet (villkor) 20grader eller över så skriver vi ut datum och grader
            int filterHighTemp = 20;
            DateTime dateTime = DateTime.Now;
            Console.WriteLine(" Dagar med temperatur över 20 ");

            for (int i = 0; i < 31; i++)
            {
                if (filterHighTemp <= days[i])
                {
                    dateTime = new DateTime(2024, 5, i + 1);
                    Console.WriteLine($" {days[i]}C {dateTime.ToString("yyyy/MM/dd")}");

                }
            }
        }

        public void PrintSpecificDates()
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine(" välj en dag du vill veta temperaturen på i Maj ");
            int choice = 0;

            if (!Int32.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("felmedd");
                return;
            }
            else if (choice >= 1 && choice <= 31)
            {
                dateTime = new DateTime(2024, 5, choice - 1);
                Console.WriteLine($"dagen innan är {days[choice]}C {dateTime.ToString("yyyy/MM/dd")}");

                dateTime = new DateTime(2024, 5, choice);
                Console.Write($" Tempen är {days[choice - 1]}C ");
                Console.WriteLine($" datumet gäller {dateTime.ToString("yyyy/MM/dd")}");

                dateTime = new DateTime(2024, 5, choice + 1);
                Console.WriteLine($"dagen efter är {days[choice]}C {dateTime.ToString("yyyy/MM/dd")}");
            }
            
        }

        public void PrintMostCommonTemp()
        {

            DateTime dateTime = DateTime.Now;
            List<int> mostCommonTemp = new List<int>(); // vi skapar en lista

            Console.WriteLine("Den vanligast förekommande temperaturen är:");
            for (int i = 0; i < 31; i++)
            {
                // här hämtar vi temperaturen för den dagen det gäller
                int currentTemp = days[i];
                dateTime = new DateTime(2024, 5, i + 1);

                // har vi redan räknat tempen??
                if (!mostCommonTemp.Contains(currentTemp))
                {
                    // om vi redan räknat temperaturen så lägger vi in den i listan
                    mostCommonTemp.Add(currentTemp);
                }
            }

        }



    }
}