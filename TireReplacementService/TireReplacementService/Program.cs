using System.ComponentModel.Design;
using System.Threading.Channels;

namespace TireReplacementService
{
    internal class Program
    {
        // en lista för att lagra bokningar
        private static List<Booking> bookings = new List<Booking>(); // listan är privat för att det skydda variabeln för åtkomst från andra delar av programmet
        private static List<DateTime> availableAppointmentTimes = new List<DateTime>(); // lista som håller de tillgängliga tiderna
        private static List<Booking> backupAppointments = new List<Booking>(); // vi gör en backup av raderade tider som används i User story 3
        static void Main(string[] args)
        {
            GenerateAvailableAppointmentTimes(); // vi genererar tider för 14 dagar fram i tiden
            // huvudmenyn i en evig while-loop
            while (true)
            {
                Console.WriteLine(" Välkommen till administratörspanel för bokningsappen (Däckarns däckbytarfirma) ");
                Console.WriteLine(" Välj följande för att: ");
                Console.WriteLine(" 1. Lägg till bokning ");
                Console.WriteLine(" 2. Ta bort bokning ");
                Console.WriteLine(" 3. Ändra en bokning ");
                Console.WriteLine(" 4. Sök efter lediga tider");
                Console.WriteLine(" 5. Visa dagens bokningar ");
                Console.WriteLine(" 6. Visa alla bokningar "); // user story 1 
                Console.WriteLine(" 7. Rensa bokningar "); //  user story 2
                Console.WriteLine(" 8. Återställ raderade bokningar "); // user story 3
                Console.WriteLine(" 9. Avsluta appen ");
                string? choice = Console.ReadLine();

                // vi lägger till flera metoder i en switch sats 
                switch (choice)

                {
                    case "1":
                        AddAppointment(); // lägg till bokning
                        break;
                    case "2":
                        CancelAppointment(); // ta bort bokning
                        break;
                    case "3":
                        RescheduleAppointment(); // ändra bokning
                        break;
                    case "4":
                        AvailableAppointments(); // sök lediga tider
                        break;
                    case "5":
                        ShowTodayAppointments(); // visa dagens bokningar
                        break;
                    case "6":
                        ShowAllAppointments(); // visa alla boknigar
                        break;
                    case "7":
                        DeleteAllBookings(); // ta bort alla bokningar
                        break;
                    case "8":
                        BackupBookings(); // återställ raderade bokningar
                        break;
                    case "9": // stäng ner appen
                        Console.WriteLine(" Appen avslutas");
                        return;
                    default: // fångar upp felskrift
                        Console.WriteLine(" Fel inmatning. Var vänlig försök igen");
                        break;

                }
            }
        }
        // vi genererar lediga tider med startdatum (dagens datum) och slutdatum (2veckor fram)
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
                    availableAppointmentTimes.Add(start); // varje timme läggs till i listan
                    start = start.AddHours(1); // går vidare till nästa timme
                }
                startDate = startDate.AddDays(1); // här går vi vidare till nästa dag 
            }
        }
        // vi lägger till en bokning
        static void AddAppointment()
        {
            Console.WriteLine(" Ange namn: ");
            string? name = Console.ReadLine();

            if (name != null && name.Any(char.IsDigit)) // vi kontrollerar om name är null och kontrollerar om name innehåller siffror
            {
                Console.WriteLine(" Namnet måste bestå av bokstäver");
                return;
            }
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine(" Namnet får inte vara tomt");
                return;
            }

            Console.WriteLine(" skriv in datum och tid för bokning (yyyy/MM/dd HH:mm)");
            string? newAppointment = Console.ReadLine();

            if (DateTime.TryParseExact(newAppointment, "yyyy/MM/dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {

                // Kontrollera att datumet ligger inom de kommande 14 dagarna
                DateTime futureDate = DateTime.Today.AddDays(14);
                if (dateTime.Day > futureDate.Day || dateTime.Day < DateTime.Today.Day)
                {
                    Console.WriteLine(" Du kan endast boka tider mellan idag och två veckor framåt. Försök igen.");
                    return;
                }

                // Kontroll för dubbelbokning med en alert
                if (bookings.Any(b => b.BookingDate == dateTime))
                {
                    Console.WriteLine("**************************************************************");
                    Console.WriteLine("                        *OBS*!!!!!                            ");
                    Console.WriteLine("Denna tid är redan bokad. Vänligen välj en annan tid.");
                    Console.WriteLine("**************************************************************");
                    return;
                }
                if (!availableAppointmentTimes.Contains(dateTime)) // kontroll om tiden är giltig
                {
                    Console.WriteLine(" Kan bara lägga till tid mellan kl 07-16 (varje hel timme) från idag och 2 veckor fram. Försök igen");
                    return;
                }

                string? licensePlate = null;

                // vi gör så registreringsnummret matchar ett rimligt format
                while (string.IsNullOrWhiteSpace(licensePlate) || !System.Text.RegularExpressions.Regex.IsMatch(licensePlate, "^[A-Z]{2,3}[0-9]{2,3}[A-Z]?$"))
                {
                    Console.WriteLine(" Skriv in registeringsnummret (ABC123, ABC22B, AB676C): ");
                    licensePlate = Console.ReadLine()?.ToUpper();

                    if (string.IsNullOrWhiteSpace(licensePlate) || !System.Text.RegularExpressions.Regex.IsMatch(licensePlate, "^[A-Z]{2,3}[0-9]{2,3}[A-Z]?$"))
                    {
                        Console.WriteLine(" Registreringsnumret har skrivits in fel. Försök igen.");
                    }
                }
                Console.WriteLine();

                string? tireService = null;
                bool validChoice = false;

                while (!validChoice) // en loop för en ny meny och switch-sats
                {
                    Console.WriteLine(" Välj vilken typ av däcktjänst det gäller: ");
                    Console.WriteLine(" 1. Däckbyte sommardäck till vinterdäck");
                    Console.WriteLine(" 2. Däckbyte vinterdäck till sommardäck");
                    Console.WriteLine(" 3. Däckhotell inkl. Däckbyte");
                    Console.WriteLine(" 4. Hjulinställning");
                    Console.WriteLine(" 5. Däckbyte till nya däck");
                    Console.WriteLine(" 6. Stäng ner appen ");

                    string? menuChoice = Console.ReadLine();

                    switch (menuChoice)
                    {
                        case "1":
                            tireService = "Däckbyte sommardäck till vinterdäck";
                            validChoice = true;
                            break;
                        case "2":
                            tireService = "Däckbyte vinterdäck till sommardäck";
                            validChoice = true;
                            break;
                        case "3":
                            tireService = "Däckhotell inkl. Däckbyte";
                            validChoice = true;
                            break;
                        case "4":
                            tireService = "Hjulinställning";
                            validChoice = true;
                            break;
                        case "5":
                            tireService = "Däckbyte till nya däck";
                            validChoice = true;
                            break;
                        case "6":
                            Console.WriteLine(" Stänger ner appen ");
                            Environment.Exit(0); // med Enviroment.Exit avslutas appen
                            break;
                        default:
                            Console.WriteLine(" Ogiltigt val, försök igen."); // vi fångar upp felinmatning
                            break;
                    }
                }

                Console.WriteLine(" Lägg till anteckning om bokning? (tryck enter för att hoppa över)"); // user story 4
                Console.WriteLine(" Anteckningar: ");
                string? notes = Console.ReadLine();

                string paymentMethod = ""; // ger variablen ett tomt värde innan den används

                while (string.IsNullOrWhiteSpace(paymentMethod)) // vi loopar tills ett användaren väljer ett fungerande alternativ i menyn
                {
                    Console.WriteLine(" Vilket betalningsalternativ har kunden valt?");
                    Console.WriteLine(" 1. Bankkort ");
                    Console.WriteLine(" 2. Faktura ");
                    Console.WriteLine(" 3. Swish ");
                    Console.WriteLine(" 4. Kontant ");

                    string? paymentInput = Console.ReadLine();

                    switch (paymentInput)
                    {
                        case "1":
                            paymentMethod = "Bankkort";
                            break;
                        case "2":
                            paymentMethod = "Faktura";
                            break;
                        case "3":
                            paymentMethod = "Swish";
                            break;
                        case "4":
                            paymentMethod = "Kontanter";
                            break;
                        default:
                            Console.WriteLine(" Ogiltigt val. Var vänlig försök igen."); // Felmeddelande för ogiltiga val
                            break;
                    }
                }
                // vi skapar ett nytt objekt av PaymentOption med svaret för betalning
                PaymentOption paymentOption = new PaymentOption(paymentMethod);

                bookings.Add(new Booking // vi lägger till en ny bokning i listan för bokningar
                {
                    CustomerName = name,
                    BookingDate = dateTime,
                    LicencePlateNumber = licensePlate,
                    TireService = tireService,
                    CustomerNote = $"{notes}",
                    Payment = paymentOption
                });

                availableAppointmentTimes.Remove(dateTime);
                Console.WriteLine($" Bokning utförd. {name} är tillagd för datumet: {dateTime}," +
                    $" Registreringsnummer: {licensePlate}," +
                    $" med tjänsten: {tireService}{(string.IsNullOrWhiteSpace(notes) ? "" : $", notering: {notes}")}, kunden betalar med {paymentOption.PaymentMethod}");

            }
            else
            {
                Console.WriteLine(" Du har angett datumet i ett ogiltigt format, försök igen");
            }
            Console.WriteLine();
        }
        // ta bort/avbryt bokning
        static void CancelAppointment()
        {
            Console.WriteLine(" Skriv in den tiden och datumet du vill avboka (yyyy/MM/dd HH:mm)");
            string? newAppointment = Console.ReadLine();

            if (DateTime.TryParse(newAppointment, out DateTime dateTime))
            {
                var booking = bookings.Find(b => b.BookingDate == dateTime); // "Find" söker upp och matchar datumet vi är ute efter 
                if (booking != null)
                {
                    backupAppointments.Add(booking); // vi lägger den raderade tiden i backup listan
                    bookings.Remove(booking); // bokningen tas bort
                    availableAppointmentTimes.Add(dateTime); // tiden återställs som ledig
                    Console.WriteLine($" Avbokning för {booking.CustomerName} utförd för datumet {booking.BookingDate} ");
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
            Console.WriteLine();

        }
        // visa all lediga tider
        static void AvailableAppointments()
        {
            Console.WriteLine(" Lediga tider finns: ");
            //  vi loopar igenom alla lediga tider och skriver ut de med hjälp av ToString + foreach-loop
            foreach (var time in availableAppointmentTimes)
            {
                Console.WriteLine(time.ToString("yyyy/MM/dd HH:mm"));
            }
            Console.WriteLine();
        }
        // omboka tid
        static void RescheduleAppointment()
        {
            Console.WriteLine(" Skriv in tid och datum för bokningen som ska ändras (yyyy/MM/dd HH:mm)");
            string? oldAppointment = Console.ReadLine();
            if (DateTime.TryParseExact(oldAppointment, "yyyy/MM/dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))

            {
                var booking = bookings.Find(b => b.BookingDate == dateTime);
                if (booking != null)
                {
                    Console.WriteLine(" Skriv in tid och datum för din nya bokning (yyyy/MM/dd HH:mm)");
                    string? newAppointment = Console.ReadLine();
                    if (DateTime.TryParseExact(newAppointment, "yyyy/MM/dd HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime newDateTime)) // vi kontrollera om den nya tiden blev inskriven i rätt format ----------------------------------------------------
                    {
                        if (availableAppointmentTimes.Contains(newDateTime)) // vi kontrollerar om nya tiden finns i listan för lediga tider
                        {
                            booking.BookingDate = newDateTime; // vi ändrar bokningens tid till den aktuella tiden
                            availableAppointmentTimes.Remove(newDateTime); // den nya tiden tas bort ur listan för lediga tider
                            availableAppointmentTimes.Add(dateTime); // gamla tiden återställs i listan för lediga tider
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
                    Console.WriteLine(" Kunde ej hitta någon bokning för det datumet och den tiden");
                }

            }
            else
            {
                Console.WriteLine(" Du har skrivit datumet felaktigt, försök igen");
            }
            Console.WriteLine();
        }
        // visa dagens bokningar
        static void ShowTodayAppointments()
        {
            Console.WriteLine(" Dagens bokningar: ");
            foreach (var booking in bookings)
            {
                if (booking.BookingDate.Date == DateTime.Today)
                {
                    Console.WriteLine($" Tid bokad: {booking.BookingDate:yyyy-MM-dd HH:mm} av {booking.CustomerName}," +
                        $" Registreringsnummer: {booking.LicencePlateNumber}," +
                        $"{(string.IsNullOrWhiteSpace(booking.CustomerNote) ? "" : $" notering: {booking.CustomerNote}")}," +
                        $" betalningsmetod: {(booking.Payment != null ? booking.Payment.PaymentMethod : "Ej valt ännu")}");
                    Console.WriteLine();
                }
            }
            if (!bookings.Any(b => b.BookingDate.Date == DateTime.Today))
            {
                Console.WriteLine(" Det finns än så länge inga bokningar idag ");
            }
            Console.WriteLine();
        }
        // visa alla bokningar
        static void ShowAllAppointments()
        {
            if (bookings.Count == 0)
            {
                Console.WriteLine(" Inga bokningar finns");
                Console.WriteLine();
                return;
            }

            Console.WriteLine(" Alla bokningar: ");
            foreach (var booking in bookings)
            {
                Console.Write($"Bokning {booking.BookingDate:yyyy/MM/dd} av {booking.CustomerName}");

                if (!string.IsNullOrWhiteSpace(booking.LicencePlateNumber))
                    Console.Write($" med registreringsnummer {booking.LicencePlateNumber}");

                if (!string.IsNullOrWhiteSpace(booking.TireService))
                    Console.Write($", tjänst: {booking.TireService}");

                if (!string.IsNullOrWhiteSpace(booking.CustomerNote) && booking.CustomerNote != string.Empty)
                    Console.Write($", notering: {booking.CustomerNote}");

                if (booking.Payment != null)
                    Console.Write($", betalningssätt: {booking.Payment.PaymentMethod}");

                Console.WriteLine();
            }
            Console.WriteLine();

        }
        // radera alla bokningar och äterställ lediga tider
        static void DeleteAllBookings()
        {
            if (bookings.Count == 0)
            {
                Console.WriteLine(" Finns inga bokninngar att ta bort. Återvänder till menyn ");
                Console.WriteLine();
                return;
            }
            Console.WriteLine(" Alla bokningar kommer att raderas, vill du fortsätta? (j/n)");
            string? answer = Console.ReadLine();

            if (answer != null && answer.ToLower() == "j")
            {
                backupAppointments.AddRange(bookings); // bokningar säkerhetskopieras
                bookings.Clear(); // vi tömmer alla bokningar
                GenerateAvailableAppointmentTimes(); // vi återställer de tillgängliga tiderna
                Console.WriteLine(" Bokningarna har raderats ");
            }
            else
            {
                Console.WriteLine(" Inga bokningar rensades. Återvänder till menyn");
                return;
            }
            Console.WriteLine();
        }
        // återställ alla raderade bokningar 
        static void BackupBookings()
        {
            if (backupAppointments.Count == 0)
            {
                Console.WriteLine(" Det finns inga bokningar att återställa");
                Console.WriteLine();
                return;
            }
            bookings.AddRange(backupAppointments); // vi återställer alla bokningar till huvudlistan från backup-listan
            backupAppointments.Clear(); // backup-listan töms så vi undviker att det skrivs ut dubbelt
            Console.WriteLine(" Raderade tider har återställts. Se alternativ 6 för bokningar");
            Console.WriteLine();
        }
    }
}