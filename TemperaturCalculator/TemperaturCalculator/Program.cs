using System.Security.Cryptography.X509Certificates;

namespace TemperaturCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // skapar ett objekt som heter data av typen temperatureData
            TemperatureData data = new TemperatureData();

            // vi anropar funjktionerna
            data.PrintDateTemperature();


            data.PrintAverageTemperature();

            data.GetHighestTemperature();

            data.GetLowestTemperature();


            //test
           // data.DoSomething("pokenhnhnhthbhn");
         
        }




    }

}




