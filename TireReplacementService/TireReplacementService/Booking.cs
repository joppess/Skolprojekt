using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireReplacementService
{
    // klassen står för en bokning 
    public class Booking
    {
        public string? CustomerName { get; set; } // kundens namn
        public DateTime BookingDate { get; set; } // bokningsdatum
        public string? LicencePlateNumber { get; set; } // registreringsnummer
        public string? TireService { get; set; } // vilken typ av tjänst som kunden valt
        public string? CustomerNote { get; set; } // user story 4, anteckningar till personal
        public PaymentOption? Payment { get; set; } // betalningsalternativ
    }
}