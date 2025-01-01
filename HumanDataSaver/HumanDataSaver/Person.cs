using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanDataSaver
{
    public enum Gender
    {
        Kvinna,
        Man,
        AlphaHane,
        Ickebinär,
        Annat,
        Sigma
    }
    public enum TypeOfHair
    {
        Lockigt,
        Rakt,
        Tjockt,
        Tunt
    }
    public class Person
    { // get: Hämtar (läser) värdet för propertyn.
        // set: Tilldelar (ändrar) värdet för propertyn. -chat
        public string? Name { get; set; } // get:hämtar värdet för egenskapen -chat vi gör värden till null med "?"
        public Gender Gender { get; set; } // set: tilldelar ett nytt värde till egenskapen -chat
        public Hair HairInformation { get; set; } // egenskap för hår - chat
        public DateTime BirthDate { get; set; }
        public string? EyeColor { get; set; }

        public override string ToString() //metod
        {
            string personInfo = string.Empty;
            personInfo = $" Namn: {Name ?? string.Empty},";
            if (!HairInformation.HasHair)
            {
                personInfo += " No hair,";
            }
            else
            {
                personInfo += $" Hårfärg: {HairInformation.Color}, Hårlängd: {HairInformation.Length}cm, Hårtyp: {HairInformation.HairType},";
            }
            personInfo += $"  Ögonfärgen är {EyeColor}, ";
            personInfo += $" Kön: {Gender} ";
            personInfo += $", födelsedag:  {BirthDate.ToString("yyyy/MM/dd")}";
            return personInfo;
        }

    }
    public struct Hair //strukt
    {
        public string Color { get; set; }
        public double Length { get; set; }
        public TypeOfHair HairType { get; set; }
        public bool HasHair { get; set; }

        //konstruktor
        public Hair(string color, double lenght, TypeOfHair type, bool hasHair)
        {
            Color = color;
            Length = lenght;
            HairType = type;
            HasHair = hasHair;
        }
    }
    
}
    

