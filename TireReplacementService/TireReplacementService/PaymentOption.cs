using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireReplacementService
{
    public class PaymentOption
    {
        public string? PaymentMethod {  get; set; }
        public PaymentOption(string paymentMethod)
        {
            PaymentMethod = paymentMethod;
        }
        public override string ToString()
        {
            return $" Betalningalternativ: {PaymentMethod}";
        }
    }

}
