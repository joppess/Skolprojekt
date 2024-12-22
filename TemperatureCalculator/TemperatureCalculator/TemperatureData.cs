using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace TemperaturCalculator
{
    internal class TemperatureData
    {
        // vi skapar en statisk array för antalet dagar i Maj-månad.
        int[] days = new int[31];
        
        public TemperatureData() // konstruktor
        {

        }

        // vi skapar en funktion som går att anropa i Program klassen
         public void FillTemperatureArray()
        {
            // vi skapar variabeln random av datatypen Random som vi ger en ny instans.
            Random random = new Random();
            // vi skapar variabel "i" och sätter den till 0. Vi loopar (for-loop) för varje dag i Maj.
            // i varje varv i loopen lägger vi till +1. Loopen fortsätts så länge som variablen i är mindre än 31.
            // +1 används för at Maj inte ska börja på dag 0.
            for (int i = 0; i < 31; i++)
            {
                // vi använder metoden "next" vilket är en del av klassen Random för att skapa slumpmässiga tal mellan 1 - 23 (min,max+1) för varje dag i arrayen.
                
                days[i] = random.Next(1, 23 + 1);

            }

        }
            // vi fortsätter mer fler metoder som kan anropas i Program.cs
        public void PrintDateTemperature()
        {
            // vi skapar ett nytt datum för varje varv i loopen och skriver ut med hjälp av tab (\t) för att snygga till utskriften.
            for (int i = 0; i < 31; i++)
            {
               
                DateTime dateTime = new DateTime(2024, 5, i + 1);
                Console.WriteLine(dateTime.ToString("yyyy/MM/dd") + "\t" + (days[i]) + "C");
            }
            Console.Write("\n");
        }
        // vi skapar funktionen PrintAverageTemperature
        // den är public så att vi kan anropa den från olika delar i programmet
        public void PrintAverageTemperature()
        {
            int averageTemperature = 0;

            for (int i = 0; i < 31; i++)
            {
                averageTemperature += days[i]; // här läggs dagens temperatur till i averageTemperature
            }

            // medeltemperaturen beräknas
            averageTemperature /= 31;
            Console.WriteLine($" Medeltemperaturen är {averageTemperature}C \n");

        }
        // en till funktion skapas för att beräkna den högsta temperaturen i Maj
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

            Console.WriteLine($" {dateTime.ToString("yyyy/MM/dd")} är den varmaste dagen med: {hottestTemperature}C");
            Console.WriteLine(" se till att uttnyttja dagen väl :) \n ");
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
            Console.WriteLine($" {dateTime.ToString("yyyy/MM/dd")} är kallaste dagen med {coldestTemperature}C");
            Console.WriteLine(" Informera byborna att klä sig efter kylan :)\n");
        }

        public void GetMedianTemperature()
        {
            var temporaryArray = new int[31];
            days.CopyTo(temporaryArray, 0);

            // vi sorterar dagarna i maj
            Array.Sort(temporaryArray);

            int medianTemperature = 0;

            for (int i = 0; i < 31; i++)
            {
                medianTemperature += temporaryArray[i];
                // vi använder modulo (%) som räknar ut resten av ett tal när det delas med ett annat tal.
                // vi får då svaret udda dagar.
                // med "2 != 0" kontrollerar vi så 2 inte är lika med 0. Vilket är sant.
                if (31 % 2 != 0)
                {
                    medianTemperature = temporaryArray[31 / 2]; //  efter beräkningen  så bir elementet i arrayen placerats i mitten medianvärdet

                    
                }

            }
            Console.WriteLine($"Median temperaturen i Maj är {medianTemperature}C \n");
        }
         // parametern "bool acsending" avgör om temperaturerna i arrayen ska sorteras stigande eller fallande beroende på false/true             
        public void GetSortingTemperature(bool acsending) 
        {
            var temporaryArray = new int[31];
            days.CopyTo( temporaryArray, 0 );

            if (acsending)
            {
                Array.Sort(temporaryArray);
                Console.WriteLine("Temperaturerna sorterade i stigande ordning");

            }
            else
            {
                Array.Sort(temporaryArray);
                Array.Reverse(temporaryArray);
                Console.WriteLine("Temperaturerna sorterade i fallande ordning");
            }
            for (int i = 0; i < 31; i++)
            {
                // vi skriver ut dagarna som hämtas från arayyen i stigande eller fallande ordning
                Console.WriteLine($" {temporaryArray[i]}C ");
            }
            Console.WriteLine("\n");
        }

        public void FilterHighTempData()
        {   // vi sätter värdet på filterHighTemp till 20. Är värdet (villkor) 20grader eller över så skriver vi ut datum och grader
            int filterHighTemp = 20;
            DateTime dateTime = DateTime.Now;
            Console.WriteLine(" Dagar med temperatur över 20C ");

            for (int i = 0; i < 31; i++)
            {
                if (filterHighTemp <= days[i])
                {
                    dateTime = new DateTime(2024, 5, i + 1);
                    Console.WriteLine($" {dateTime.ToString("yyyy/MM/dd")} \t {days[i]}C");

                }
            }
            Console.WriteLine("\n");
        }

        public void PrintSpecificDates()
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine(" välj en dag du vill veta temperaturen på i Maj ");
            int choice = 0;

            // om användarens svarar knasigt
            if (!Int32.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Ogiltigt val. Du tas tillbaka till menyn");
                return;
            }
            // innehåller användarens svar dagarna 1-31 så gör vi utskrift.
            else if (choice >= 1 && choice <= 31)
            {
                if (choice > 1) // är dagarna över 1 skriv ut "choice - 2 då det är dagen innan"
                {
                    dateTime = new DateTime(2024, 5, choice - 1);
                    Console.WriteLine($" Dagen innan {dateTime.ToString("yyyy/MM/dd")} erbjuder {days[choice - 2]}C");
                }
                    // skriver ut den valda dagen
                dateTime = new DateTime(2024, 5, choice);
                Console.WriteLine($" Den önskade dagen {dateTime.ToString("yyyy/MM/dd")} erbjuder {days[choice - 1]}C");

                if (choice < 31) // är dagarna under 31 skriv ut choice då det är dagen efter
                {
                    dateTime = new DateTime(2024, 5, choice + 1);
                    Console.WriteLine($" Dagen efter {dateTime.ToString("yyyy/MM/dd")} erbjuder {days[choice]}C");

                }
            }
            Console.WriteLine("\n");
        }

        public void PrintMostCommonTemp()
        {

            DateTime dateTime = DateTime.Now;

            // vi skapar en Dictionary
            Dictionary<int, int> mostCommonTemp = new Dictionary<int, int>();

            Console.WriteLine("Den vanligast förekommande temperaturen i Maj är:");
            for (int i = 0; i < 31; i++)
            {
                int currentDayTemp = days[i]; // hämtar temperaturen för dagen den aktuella dagen

                if (mostCommonTemp.ContainsKey(currentDayTemp)) // om temperaturen finns i Dictionaryn så ökar vi antalet med 1
                {
                    mostCommonTemp[currentDayTemp]++;
                }
                else
                {
                    mostCommonTemp[currentDayTemp] = 1; // annars lägger vi till temperaturen med värdet 1
                }
            }
            int commonTemp = 0; // vi sparar den vanligast förekommande temperaturen i den här variabeln
            int highestTempCount = 0; // vi sparar hur många gånger den uppenbarar sig i denna variabel

            // iteration genom varje nyckel och värde i Dictionaryn
            foreach (var temp in mostCommonTemp)
            {
                // om värdet av en temperatur (temp.Value) är större än det största värdet som registrerats
                if (temp.Value > highestTempCount)
                {
                    commonTemp = temp.Key; // vi gör commonTemp till temp.Key
                    highestTempCount = temp.Value; // antalet förekomster för temperaturen sätts till highestTempCount
                }

            }
            Console.WriteLine($"{commonTemp}C \n");

        }



    }
}