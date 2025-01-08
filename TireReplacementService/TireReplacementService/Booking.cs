using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireReplacementService
{
    public class Booking
    {
        public string? KundNamn { get; set; }
        public DateTime BokningsDatum { get; set; } // får ej vara en sträng
        public string? RegistreringsNummer { get; set; }
        public string? DäckBytarTjänst { get; set; }
    }
}
