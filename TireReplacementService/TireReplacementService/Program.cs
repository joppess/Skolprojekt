using System.ComponentModel.Design;
using System.Threading.Channels;

namespace TireReplacementService
{
    internal class Program
    {
        // en lista för att lagra bokningar
        private static List<Booking> bookings = new List<Booking>(); // den är privat för aatt Det skyddar variabeln från att exponeras för andra delar av applikationen. - chat
        private static List<DateTime> availableAppointmentTimes = new List<DateTime>();
        static void Main(string[] args)
        {
            GenerateAvailableAppointmentTimes(); // anropar funktionen?

            while (true)
            {
                Console.WriteLine(" Välkommen till administratörspanel för bokningsappen (Däckarns däckbytarfirma) ");
                Console.WriteLine(" Välj följande för att: ");
                Console.WriteLine(" 1. Lägg till bokning ");
                Console.WriteLine(" 2. Ta bort bokning ");
                Console.WriteLine(" 3. Ändra en bokning ");
                Console.WriteLine(" 4. Sök efter lediga tider");
                Console.WriteLine(" 5. Visa dagens bokningar ");
                Console.WriteLine(" 6. Avsluta appen ");

                string? choice = Console.ReadLine(); // null????????????
                Console.WriteLine();

                switch (choice)

                {
                    case "1":
                        AddAppointment();
                        break;
                    case "2":
                        CancelAppointment();
                        break;
                    case "3":
                        RescheduleAppointment();
                        break;
                    case "4":
                        AvailableAppointments();
                        break;
                    case "5":
                        ShowTodayAppointments();
                        break;
                    case "6":
                        Console.WriteLine(" Appen avslutas");
                        return;
                    default:
                        Console.WriteLine(" Fel inmatning. Var vänlig försök igen");
                        break;


                }

            }
        }
        // funktionen består av startdatum (dagens datum) och slutdatum (2veckor fram)
        static void GenerateAvailableAppointmentTimes()
        {
            availableAppointmentTimes.Clear();// Vi säkerställer att listan är tom

            DateTime startDate = DateTime.Today;
            DateTime endDate = startDate.AddDays(14);

            while (startDate < endDate)
            {
                // arbetstimmar per dag
                DateTime start = startDate.AddHours(7);
                DateTime end = startDate.AddHours(17);

                while (start < end)
                {
                    availableAppointmentTimes.Add(start);
                    start = start.AddHours(1); // går vidare till nästa timme
                }
                startDate = startDate.AddDays(1); // här går vi vidare till nästa dag 
            }
        }
        static void AddAppointment()        // lägga till tider mellan 07-16?
        {
            Console.WriteLine(" Ange namn: ");
            string? name = Console.ReadLine();

            Console.WriteLine(" skriv in datum och tid för bokning (yyyy/MM/dd HH:mm)");
            string? newAppointment = Console.ReadLine();

            if (DateTime.TryParseExact(newAppointment, "yyyy/MM/dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {

                // Kontrollera att datumet inte är mer än 14 dagar från idag????????????????????????????????????chat gpt allt
                DateTime twoWeeksFromToday = DateTime.Today.AddDays(14);
                if (dateTime.Day > twoWeeksFromToday.Day || dateTime.Day < DateTime.Today.Day)
                {
                    Console.WriteLine(" Du kan endast boka tider mellan idag och två veckor framåt. Försök igen.");
                    return; // Avsluta metoden om datumet är ogiltigt
                }

                // Kontroll för dubbelbokning???????????????????????????????????????????????????????kolla igenom allt
                if (bookings.Any(b => b.BokningsDatum == dateTime))
                {
                    Console.WriteLine("**************************************************************");
                    Console.WriteLine("                        *OBS*!!!!!                            ");
                    Console.WriteLine("Denna tid är redan bokad. Vänligen välj en annan tid.");
                    Console.WriteLine("**************************************************************");
                    return; // Avbryt metoden om det är en dubbelbokning
                }
                if (!availableAppointmentTimes.Contains(dateTime))
                {
                    Console.WriteLine(" Kan bara lägga till tid mellan kl 07-16 från idag och 2 veckor fram. Försök igen");
                    return;

                }

                Console.WriteLine(" Registreringsnummer för bilen: ");
                string? licensePlate = Console.ReadLine();

                Console.WriteLine(" Välj vilken typ av däcktjänst det gäller: ");
                Console.WriteLine(" 1. Däckbyte: sommardäck till vinterdäck?");
                Console.WriteLine(" 2. Däckbyte: vinterdäck till sommardäck");
                Console.WriteLine(" 3. Däckhotell inkl. Däckbyte");
                Console.WriteLine(" 4. Hjulinställning");
                Console.WriteLine(" 5. Däckbyte till nya däck");
                Console.WriteLine(" 6. Stäng ner appen ");
                string? menuChoice = Console.ReadLine();
                string? däckBytarTjänst = null;

                switch (menuChoice)
                {
                    case "1":
                        däckBytarTjänst = "Däckbyte sommardäck till vinterdäck";
                        break;
                    case "2":
                        däckBytarTjänst = "Däckbyte vinterdäck till sommardäck";
                        break;
                    case "3":
                        däckBytarTjänst = " Däckhotell inkl. Däckbyte";
                        break;
                    case "4":
                        däckBytarTjänst = " Hjulinställning";
                        break;
                    case "5":
                        däckBytarTjänst = " Däckbyte till nya däck";
                        break;
                    case "6":
                        Console.WriteLine(" Stänger ner appen ");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine(" Ogiltigt val, försök igen");
                        return; // avslutar metoden

                }

                bookings.Add(new Booking
                {
                    KundNamn = name,
                    BokningsDatum = dateTime,
                    RegistreringsNummer = licensePlate,
                    DäckBytarTjänst = däckBytarTjänst
                });

                availableAppointmentTimes.Remove(dateTime);
                Console.WriteLine($" Bokning utförd. {name} är tillagd för datumet: {dateTime} med tjänsten {däckBytarTjänst}.");

            }
            else
            {
                Console.WriteLine(" Du har angett datmet i ett ogiltigt format, försök igen");
            }

        }
        static void CancelAppointment()
        {
            Console.WriteLine(" Skriv in den tiden och datumet du vill avboka");
            string? newAppointment = Console.ReadLine();
            if (DateTime.TryParse(newAppointment, out DateTime dateTime))
            {
                var booking = bookings.Find(b => b.BokningsDatum == dateTime); // (Find) söker upp och matchar datumet vi är ute efter 
                if (booking != null)
                {
                    bookings.Remove(booking);
                    availableAppointmentTimes.Add(dateTime);
                    Console.WriteLine($" Avbokning för {booking.KundNamn} utförd för datumet {booking.BokningsDatum} "); // (booking.) för att veta vilken bokning vi syftar på
                }
                else
                {
                    Console.WriteLine(" Kunde ej hitta det angivna datumet, försök igen ");
                }
            }
            else
            {
                Console.WriteLine(" Du har skrivit in datumet fel, försök igen ");
            }

        }
        static void AvailableAppointments()
        {
            Console.WriteLine(" Lediga tider finns: ");
            foreach (var time in availableAppointmentTimes)
            {
                Console.WriteLine(time.ToString("yyyy/MM/dd HH:mm"));
            }
        }
        static void RescheduleAppointment()
        {
            Console.WriteLine(" Skriv in tid och datum för det datumet som ska ändras");
            string? oldAppointment = Console.ReadLine();
            if (DateTime.TryParse(oldAppointment, out DateTime dateTime))
            {
                var booking = bookings.Find(b => b.BokningsDatum == dateTime);
                if (booking != null)
                {
                    Console.WriteLine(" Skriv in tid och datum för din nya bokning");
                    string? newAppointment = Console.ReadLine();
                    if (DateTime.TryParse(newAppointment, out DateTime newDateTime))
                    {
                        if (availableAppointmentTimes.Contains(newDateTime))
                        {
                            booking.BokningsDatum = newDateTime; // vi ändrar bokningens tid
                            availableAppointmentTimes.Remove(newDateTime); // den nya tiden markeras som bokad???
                            availableAppointmentTimes.Add(dateTime);
                            Console.WriteLine($" Du har ombokat tiden till {newDateTime}.");
                        }
                        else
                        {
                            Console.WriteLine(" Ej tillgänlig tid, vänligen välj en annan tid");
                        }
                    }
                    else
                    {
                        Console.WriteLine(" Du har skrivit datumet felaktigt, förösk igen");
                    }
                }
                else
                {
                    Console.WriteLine(" Kunde ej hitta någon bokning för det datumet och tiden");
                }

            }
            else
            {
                Console.WriteLine(" Du har skrivit datumet felaktigt, försök igen");
            }
        }
        static void ShowTodayAppointments() // dagens bokningar
        {
            Console.WriteLine(" Dagens bokningar: ");
            foreach (var booking in bookings)
            {
                if (booking.BokningsDatum.Date == DateTime.Today)
                {
                    Console.WriteLine($" Tid bokad: {booking.BokningsDatum:yyyy-MM-dd HH:mm} av {booking.KundNamn}");
                }
            }
            if (!bookings.Any(b => b.BokningsDatum.Date == DateTime.Today))
            {
                Console.WriteLine(" Det finns än så länge inga bokningar idag ");
            }
        }

    }
}
