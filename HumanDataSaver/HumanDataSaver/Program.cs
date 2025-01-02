using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace HumanDataSaver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // lagrar person-objekt i en lista
            List<Person> people = new List<Person>();

            int hardcodedPersons = 0; // Vi skapar integern för att hålla koll på hårdkodade personer i switch-satsen

            while (true) // bool som gör loopen oändlig tills vi sätter annat krav
            {

                Console.WriteLine("\n*********************");
                Console.WriteLine(" HUVUDMENY ");
                Console.WriteLine("*********************");
                Console.WriteLine(" 1. Lägg till en person ");
                Console.WriteLine(" 2. Lista personer ");
                Console.WriteLine(" 3. Lägg till färdig person ");
                Console.WriteLine(" 4. Avsluta programmet ");
                Console.WriteLine(" Ditt val: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddPerson(people);
                        break;
                    case "2":
                        if (people.Count < 1) // är listan tom gör vi utskrift
                        {
                            Console.WriteLine(" Du har inte lagt till en person. Försök igen");
                        }
                        else
                        { 
                        ListPeople(people);
                        }
                        break;
                    case "3": // för att uppfylla kraven i Sprint 1 för hårdkodade personer.
                        if (hardcodedPersons >= 2) // Är de hårdkodade personerna mer än 2? 
                        {
                            Console.WriteLine("Maximalt antal färdiga personer har redan lagts till. Du kan inte lägga till fler.");
                        }
                        else
                        {
                            // Vi kollar om Johan redan lagts till i listan
                            if (people.Any(p => p.Name == "Johan"))
                            {
                                // Finns Johan så lägger vi till en till person
                                Person newPerson = new Person
                                {
                                    Name = "Benny",
                                    Gender = Gender.Man,
                                    HairInformation = new Hair
                                    {
                                        Color = "Black",
                                        Length = 9.0,
                                        HairType = TypeOfHair.Rakt,
                                        HasHair = true
                                    },
                                    BirthDate = new DateTime(2004, 12, 12),
                                    EyeColor = "Brun"
                                };
                                people.Add(newPerson);
                                Console.WriteLine(" Benny är tillagd. Visa lista för att se detaljer om Benny. ");
                            }
                            else
                            {
                                // Vi lägger till Johan såvida han inte redan finns med 
                                Person hardcodedPerson = new Person
                                {
                                    Name = "Johan",
                                    Gender = Gender.Man,
                                    HairInformation = new Hair
                                    {
                                        Color = "Brun",
                                        Length = 2.0,
                                        HairType = TypeOfHair.Rakt,
                                        HasHair = true
                                    },
                                    BirthDate = new DateTime(1995, 06, 11),
                                    EyeColor = "Grön"
                                };
                                people.Add(hardcodedPerson);
                                Console.WriteLine(" Johan har lagts till. Visa lista för att se detaljer om Johan. ");
                            }
                            hardcodedPersons++; // Vi ökar med +1 för varje person så personerna tillslut når vårt tak på 2 personer
                        }
                        break;
                    case "4":
                        Console.WriteLine(" Programmet avslutas ");
                        return;
                    default:
                        Console.WriteLine(" Ej giltigt val. Försök igen\n");
                        break;
                }
            }
        }
        // Funktionen listar personerna i listan
        static void ListPeople(List<Person> people)
        {
            foreach (var p in people)
            {
                Console.WriteLine($"{p}");
            }

        }
        // funktionen lägger till ny person via input
        static void AddPerson(List<Person> people)
        {
            string? name = string.Empty;
            Gender gender = Gender.Kvinna;
            Hair hair = new Hair();
            DateTime birthday = DateTime.Now;
            string? eyeColor = string.Empty;

            while (true) // namn
            {
                Console.Write(" skriv namn: ");
                name = Console.ReadLine();
                if (name == null || name == string.Empty)
                {
                    Console.WriteLine("Felmedd, skriv in ett namn");
                    continue;
                }

                if (name.Any(char.IsDigit)) //  är det siffra?
                {
                    Console.WriteLine(" Måste vara en bokstav ");
                    continue;
                }
                break;
            }

            while (true) // kön
            {
                int i = 1;

                foreach (var g in Enum.GetValues(typeof(Gender)))
                {
                    Console.WriteLine($"{i}. {g} ");
                    i++;
                }
                Console.Write(" Ange kön: ");
                int choice = 0;
                if (!Int32.TryParse(Console.ReadLine(), out choice)) // om det lyckas konverteras lägg det i choice
                {
                    Console.WriteLine(" Det måste vara ett nummer. Försök igen");
                    continue;
                }
                if (choice >= 1 && choice <= i - 1)
                {
                    gender = (Gender)choice - 1;
                    break;
                }
                else
                {
                    Console.WriteLine($" Måste vara ett val mellan 1 - {i - 1} \n");
                }
            }

            while (true) // har personen hår?
            {
                Console.WriteLine(" Har personen hår? (y/n)");
                string? hasHairInput = Console.ReadLine();
                if (hasHairInput?.ToLower() == "y")
                {
                    hair.HasHair = true;
                    break;
                }
                else if (hasHairInput?.ToLower() == "n")
                {
                    hair.HasHair = false;
                    break;
                }
                else
                {
                    Console.WriteLine(" felmedd. försök igen.");
                }
            }
            if (hair.HasHair)
            {
                while (true) // Hårtyp
                {
                    int i = 1;

                    foreach (var g in Enum.GetValues(typeof(TypeOfHair)))
                    {
                        Console.WriteLine($"{i}. {g} ");
                        i++;
                    }
                    Console.Write(" Hårtyp: ");
                    int choice = 0;
                    if (!Int32.TryParse(Console.ReadLine(), out choice)) // om det lyckas konverteras lägg det i choice
                    {
                        Console.WriteLine(" Det måste vara ett nummer. Försök igen");
                        continue;
                    }
                    if (choice >= 1 && choice <= i - 1)
                    {
                        hair.HairType = (TypeOfHair)choice - 1;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($" Måste vara ett val mellan 1 - {i - 1} \n");
                    }
                }

                while (true) // hårfärg
                {
                    Console.Write(" skriv hårfärg: ");
                    string? hairColor = Console.ReadLine();
                    if (hairColor == null || hairColor == string.Empty)
                    {
                        Console.WriteLine(" felmedd, skriv in ett namn");
                        continue;
                    }

                    if (hairColor.Any(char.IsDigit)) 
                    {
                        Console.WriteLine(" Det måste vara ett nummer. Försök igen");
                        continue;
                    }
                    hair.Color = hairColor;
                    break;
                }

                while (true) // Hårlängd
                {
                    Console.Write(" skriv in Hårlängd: ");
                    double length = 0;
                    if (!double.TryParse(Console.ReadLine(), out length))
                    {
                        Console.WriteLine(" Felmedd gick ej att parsa");
                        continue;
                    }
                  
                    hair.Length = length;
                    break;
                }
            }
            while (true) // Birthday
            {
                Console.Write(" skriv en födelsedag (tex 1997/09/12): ");
                DateTime Birthday = DateTime.Now;
                if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out Birthday))
                {
                    Console.WriteLine(" gick ej att parsa, felmedd");
                    continue;
                }

                birthday = Birthday; // sätt värdet av variabeln birthday till Birthday så det stämmer med rad 111
                break;
            }

            while (true) // Ögonfärg
            {
                Console.Write(" skriv in ögonfärg: ");
                string? EyeColor = Console.ReadLine();
                if (EyeColor == null || EyeColor == string.Empty)
                {
                    Console.WriteLine(" Felmedd, skriv in ett namn");
                    continue;
                }
                if (EyeColor.Any(char.IsDigit)) 
                {
                    Console.WriteLine(" Det måste vara ett nummer. Försök igen");
                    continue;
                }
                eyeColor = EyeColor;
                break;
            }

            // skapa ny person och lägg i listan
            Person person = new Person
            {
                Name = name,
                Gender = gender,
                HairInformation = hair,
                BirthDate = birthday,
                EyeColor = eyeColor
            };
            people.Add(person);
        }
    }
}





