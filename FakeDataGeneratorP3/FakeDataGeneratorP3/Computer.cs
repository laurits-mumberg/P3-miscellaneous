using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeDataGeneratorP3
{
    class Computer
    {
        public string PCNavn { get; private set; }
        public string OS { get; private set; }
        public int CPU { get; private set; }
        public int RAM { get; private set; }
        public int HDD { get; private set; }
        public string ServicePack { get; private set; }
        public string Serienummer { get; private set; }
        public DateTime FakturaDato { get; private set; }
        public string Gateway { get; private set; }
        public string Model { get; private set; }
        public string PCType { get; private set; }

        private static List<string> serials = new List<string>();
        private static Random rnd = new Random();

        public Computer()
        {
            PCNavn = getFakePCName(7);
            OS = "Windows 10";
            CPU = 1234;
            RAM = 16000;
            HDD = 250000;
            ServicePack = "10.0.12345.1234";
            Serienummer = GetRandomSerialNumber();
            FakturaDato = RandomDay();
            Gateway = "123.456.789.000";
            Model = "Dell 1234";
            PCType = "LAPTOP";
            
        }

        private string getFakePCName(int numberLen)
        {
            string result = "PC";

            result += rnd.Next(1000000, 9999999);

            return result;
        }

        private string GetRandomSerialNumber()
        {
            string rndString;

            do
            {
                rndString = RandomString(7);
            } while (serials.Contains(rndString));

            serials.Add(rndString);

            return rndString;
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        private static DateTime RandomDay()
        {
            DateTime start = new DateTime(2000, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }
    }
}
