using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireReplacementService
{
    // klassen representerar betalningsalternativet för bokningen
    public class PaymentOption
    {
        public string? PaymentMethod { get; set; } // betalningsalternativ
        public PaymentOption(string paymentMethod) // nytt objekt skapas av klassen PaymentOption
        {
            PaymentMethod = paymentMethod;
        }
        // skriver över PaymentOption-objektet i ett bättre format
        public override string ToString()
        {
            return $" Betalningalternativ: {PaymentMethod}";
        }
    }
}