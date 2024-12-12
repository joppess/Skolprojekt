using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperaturCalculator
{
    // internal betyder att det gäller för namespacet (egen notering)

    internal class TemperatureData
    {
        // vi skapar en statisk array för antalet dagar i Maj-månad.
        int[] days = new int[31];

        public TemperatureData()
        {
            FillTemperatureArray();
        }

        void FillTemperatureArray()
        {
            // vi skapar variabeln random av datatypen Random som vi ger en ny instans.
            Random random = new Random();
            // vi skapar variabel i och sätter den till 0. Vi loopar (for-loop) för varje dag i Maj.
            // I varje varv i loopen lägger vi till +1. Loopen fortsätts så länge som variablen i är mindre än 31.
            // +1 används för at Maj inte ska börja på dag 0.
            for (int i = 0; i < 31; i++)
            {
                // vi skapar slumpmässiga tal mellan 1 - 23 (min,max+1) för varje dag i arrayen "days"

                days[i] = random.Next(1, 23 + 1);

            }

        }

        // vi skapar variabeln random av datatypen Random som vi ger en ny instans.


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

        //testar 
        public void DoSomething(string poke)
        {
            Console.WriteLine($"{poke}");

        }



    }
}
