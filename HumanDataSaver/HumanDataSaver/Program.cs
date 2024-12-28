using System.Xml.Linq;

namespace HumanDataSaver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person
            {
                Name = "Benny",
                Gender = Gender.NonBinary,
                HairInformation = new Hair ("Black", 5.0, TypeOfHair.Straight, true),
                BirthDate = new DateTime(2004, 12, 12),
                EyeColor = "Brown"
            };
            Console.WriteLine($" {person} ");
        }
    }
   
}



