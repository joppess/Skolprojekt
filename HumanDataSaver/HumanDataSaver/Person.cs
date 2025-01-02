using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanDataSaver
{
    // vi skapar enum för att definiera olika kön
    public enum Gender
    {
        Kvinna,
        Man,
        Ickebinär,
        Annat
    }
    // Enum för olika hårtyper
    public enum TypeOfHair
    {
        Lockigt,
        Rakt,
        Tjockt,
        Tunt
    }
    // vi skapar en klass som representerar en person
    public class Person
    { // get: Hämtar värdet för propertyn.
        // set: ändrar värdet för propertyn. 
        public string? Name { get; set; } 
        public Gender Gender { get; set; } 
        public Hair HairInformation { get; set; } 
        public DateTime BirthDate { get; set; }
        public string? EyeColor { get; set; }

        public override string ToString() // Metod som formaterar information till en sträng
        {

            string personInfo = string.Empty;  
            personInfo = $" Namn: {Name ?? string.Empty},"; // Vi lägger till personens namn
            if (!HairInformation.HasHair) // Har personen hår?
            {
                personInfo += " saknar hår,";
            }
            else // Else-sats om personen har hår
            {
                personInfo += $" Hårfärg: {HairInformation.Color}, Hårlängd: {HairInformation.Length}cm, Hårtyp: {HairInformation.HairType},";
            }
            // Vi lägger till resterande information om personen
            personInfo += $"  ögonfärgen är {EyeColor}, ";
            personInfo += $" kön: {Gender} ";
            personInfo += $", födelsedag:  {BirthDate.ToString("yyyy/MM/dd")}";
            return personInfo;
        }

    }
    public struct Hair // Vi skapar en strukt som representerar hår
    {
        public string Color { get; set; }
        public double Length { get; set; }
        public TypeOfHair HairType { get; set; }
        public bool HasHair { get; set; }

        // Konstruktor som sätter värden för egenskaperna
        public Hair(string color, double lenght, TypeOfHair type, bool hasHair)
        {
            Color = color;
            Length = lenght;
            HairType = type;
            HasHair = hasHair;
        }
    }
    
}
    

