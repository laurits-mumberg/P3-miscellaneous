using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FakeDataGeneratorP3
{
    class Employee
    {
        public string Username { get; private set; }
        public string Navn { get; private set; }
        public string Afdeling { get; private set; }
        public Computer Computer { get; set; }

        Random rnd = new Random();

        public Employee()
        {
            string[] nameList = File.ReadAllLines("names.txt");

            string fornavn = nameList[rnd.Next(0, nameList.Length)];
            string efternavn = nameList[rnd.Next(0, nameList.Length)];

            Username = $"N1M{fornavn.ToUpper()[0]}{efternavn.ToUpper()[0]}";
            Navn = $"{fornavn} {efternavn}";
            Afdeling = "AFF Aalborg Forsyning";

            Computer = new Computer();
        }

    }
}
