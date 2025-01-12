using System.ComponentModel.Design;
using System.Threading.Channels;

namespace TireReplacementService
{
    internal class Program
    {
        // en lista för att lagra bokningar
        private static List<Booking> bookings = new List<Booking>(); // den är privat för aatt Det skyddar variabeln från att exponeras för andra delar av applikationen. - chat
        private static List<DateTime> availableAppointmentTimes = new List<DateTime>();
        private static List<Booking> backupAppointments = new List<Booking>(); // vi gör en backup av raderade tider som används i User story 3
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
                Console.WriteLine(" 6. Visa alla bokningar "); //User story 1 // Som företagsägare vill jag kunna visa en lista över alla bokningar som ligger inom en viss tidsperiod, för att kunna planera arbetsbelastningen och optimera personalresuerna - chat gpt
                Console.WriteLine(" 7. Rensa bokningar ");// 2 userstories
                Console.WriteLine(" 8. Återställ raderade bokningar "); //3    För att snabbt starta om systemet eller förbereda för en ny bokningssäsong.
                Console.WriteLine(" 9. Avsluta appen ");
                string? choice = Console.ReadLine(); // null????????????
                

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
                        ShowAllAppointments();
                        break;
                    case "7":
                        DeleteAllBookings();
                        break;
                    case "8":
                        BackupBookings();
                        break;
                    case "9":
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

            if (name != null && name.Any(char.IsDigit)) //vi kontrollerar om name är null och kontrollerar om name innehåller siffror
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
                    Console.WriteLine(" Kan bara lägga till tid mellan kl 07-16 (varje hel timme) från idag och 2 veckor fram. Försök igen");
                    return;

                }


                string? licensePlate = null;

                // vi får registreringsnummret att matcha ett rimlig format
                while (string.IsNullOrWhiteSpace(licensePlate) || !System.Text.RegularExpressions.Regex.IsMatch(licensePlate, "^[A-Z]{2,3}[0-9]{2,3}[A-Z]?$"))
                {
                    Console.WriteLine(" Skriv in registeringsnummret (ABC123, ABC22B, AB676C): ");
                    licensePlate = Console.ReadLine()?.ToUpper(); // Gör om till versaler - chat

                    if (string.IsNullOrWhiteSpace(licensePlate) || !System.Text.RegularExpressions.Regex.IsMatch(licensePlate, "^[A-Z]{2,3}[0-9]{2,3}[A-Z]?$"))
                    {
                        Console.WriteLine(" Registreringsnumret har skrivits in fel. Försök igen.");
                    }
                }
                        Console.WriteLine();


                string? däckBytarTjänst = null;
                bool validChoice = false;

                while (!validChoice) // Loopa tills användaren gör ett giltigt val - chat
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
                            däckBytarTjänst = "Däckbyte sommardäck till vinterdäck";
                            validChoice = true; // Giltigt val - chat
                            break;
                        case "2":
                            däckBytarTjänst = "Däckbyte vinterdäck till sommardäck";
                            validChoice = true; 
                            break;
                        case "3":
                            däckBytarTjänst = "Däckhotell inkl. Däckbyte";
                            validChoice = true; 
                            break;
                        case "4":
                            däckBytarTjänst = "Hjulinställning";
                            validChoice = true; 
                            break;
                        case "5":
                            däckBytarTjänst = "Däckbyte till nya däck";
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

                string paymentMethod = ""; // ger variablen ett värde innan den används???

                while (string.IsNullOrWhiteSpace(paymentMethod)) // Loopa tills ett giltigt val görs - chat
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



                PaymentOption paymentOption = new PaymentOption(paymentMethod); // vi skapar ett nytt objekt av PaymentOption???

                bookings.Add(new Booking
                {
                    KundNamn = name,
                    BokningsDatum = dateTime,
                    RegistreringsNummer = licensePlate,
                    DäckBytarTjänst = däckBytarTjänst,
                    Anteckning =$"{notes}",
                    Payment = paymentOption
                });

                availableAppointmentTimes.Remove(dateTime);
                Console.WriteLine($" Bokning utförd. {name} är tillagd för datumet: {dateTime}," +
                    $" Registreringsnummer: {licensePlate}," +
                    $" med tjänsten: {däckBytarTjänst}{(string.IsNullOrWhiteSpace(notes) ? "" : $", notering: {notes}")}, kunden betalar med {paymentOption.PaymentMethod}");

            }
            else
            {
                Console.WriteLine(" Du har angett datmet i ett ogiltigt format, försök igen");
            }
            Console.WriteLine();
        }
        static void CancelAppointment()
        {
            Console.WriteLine(" Skriv in den tiden och datumet du vill avboka (yyyy/MM/dd HH:mm)");
            string? newAppointment = Console.ReadLine();

            if (DateTime.TryParse(newAppointment, out DateTime dateTime))
            {
                var booking = bookings.Find(b => b.BokningsDatum == dateTime); // (Find) söker upp och matchar datumet vi är ute efter 
                if (booking != null)
                {
                    backupAppointments.Add(booking); // vi lägger den raderade tiden i backup listan???????????
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
            Console.WriteLine();

        }
        static void AvailableAppointments()
        {
            Console.WriteLine(" Lediga tider finns: ");
            foreach (var time in availableAppointmentTimes)
            {
                Console.WriteLine(time.ToString("yyyy/MM/dd HH:mm"));
            }
            Console.WriteLine();
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
                    Console.WriteLine(" Kunde ej hitta någon bokning för det datumet och den tiden");
                }

            }
            else
            {
                Console.WriteLine(" Du har skrivit datumet felaktigt, försök igen");
            }
            Console.WriteLine();
        }
        static void ShowTodayAppointments() // dagens bokningar
        {
            Console.WriteLine(" Dagens bokningar: ");
            foreach (var booking in bookings)
            {
                if (booking.BokningsDatum.Date == DateTime.Today)
                {
                    Console.WriteLine($" Tid bokad: {booking.BokningsDatum:yyyy-MM-dd HH:mm} av {booking.KundNamn}," +
                        $" Registreringsnummer: {booking.RegistreringsNummer}," +
                        $"{(string.IsNullOrWhiteSpace(booking.Anteckning) ? "" : $" notering: {booking.Anteckning}")}," +
                        $" betalningsmetod: {(booking.Payment != null ? booking.Payment.PaymentMethod : "Ej valt ännu")}");
                    Console.WriteLine();
                }
            }
            if (!bookings.Any(b => b.BokningsDatum.Date == DateTime.Today))
            {
                Console.WriteLine(" Det finns än så länge inga bokningar idag ");
            }
                    Console.WriteLine();
        }
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
                Console.Write($"Bokning {booking.BokningsDatum:yyyy/MM/dd} av {booking.KundNamn}");

                if (!string.IsNullOrWhiteSpace(booking.RegistreringsNummer))
                    Console.Write($" med registreringsnummer {booking.RegistreringsNummer}");

                if (!string.IsNullOrWhiteSpace(booking.DäckBytarTjänst))
                    Console.Write($", tjänst: {booking.DäckBytarTjänst}");

                if (!string.IsNullOrWhiteSpace(booking.Anteckning) && booking.Anteckning != string.Empty)
                    Console.Write($", notering: {booking.Anteckning}");

                if (booking.Payment != null)
                    Console.Write($", betalningssätt: {booking.Payment.PaymentMethod}");

                Console.WriteLine();
            }
                Console.WriteLine();

        }
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
        static void BackupBookings()
        {
            if (backupAppointments.Count == 0)
            {
                Console.WriteLine(" Det finns inga bokningar att återställa");
                Console.WriteLine();
                return;
            }
            bookings.AddRange(backupAppointments); // vi återställer alla bokningar till huvudlistan
            backupAppointments.Clear(); // listan töms så vi undviker så det skrivs ut dubbelt
            Console.WriteLine(" Raderade tider har återställts. Se alternativ 6 för bokningar");
            Console.WriteLine();
        }
    }
}
