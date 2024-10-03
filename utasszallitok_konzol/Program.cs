using System;
using System.IO;

namespace MyConsoleApp
{


    class Program
    {    class Sebessegkategoria
        {
            private int Utazosebesseg;
            public string Kategorianev
            {
                get
                {
                    if (Utazosebesseg < 500) return "Alacsony sebességű";
                    else if (Utazosebesseg < 1000) return "Szubszonikus";
                    else if (Utazosebesseg < 1200) return "Transzszonikus";
                    else return "Szuperszonikus";
                }
            }
            public Sebessegkategoria(int utazosebesseg)
            {
                Utazosebesseg = utazosebesseg;
            }
        }

        struct data {
            public string type;
            public int year, passengers, crew, cruiseSpeed, takeoffWeight;
            public float takeoffDistance;
            public Sebessegkategoria kategoria;
            public data(string _type, int _year, int _passengers, int _crew, int _cruiseSpeed, int _takeoffWeight, float _takeoffDistance) {
                type = _type;
                year = _year;
                passengers = _passengers;
                crew = _crew;
                cruiseSpeed = _cruiseSpeed;
                takeoffWeight = _takeoffWeight;
                takeoffDistance = _takeoffDistance;

                kategoria = new Sebessegkategoria(cruiseSpeed);
            }
        }

        static void Main(string[] args)
        {
            List<data> data = new List<data>();

            List<Sebessegkategoria> sebessegek = new List<Sebessegkategoria> {
                new Sebessegkategoria(499),
                new Sebessegkategoria(999),
                new Sebessegkategoria(1199),
                new Sebessegkategoria(2001)
            };

            var lines = File.ReadAllLines("utasszallitok.txt");
            string type = "";
            int year = 0, passengers = 0, crew = 0, cruiseSpeed = 0, takeoffWeight = 0;
            float takeoffDistance = 0;
            for(var k = 1; k < lines.Length-1; k++) {
                var line = lines[k];
                type = line.Split(';')[0];
                year = int.Parse(line.Split(';')[1]);
                passengers = int.Parse(line.Split(';')[2].Contains('-') ? line.Split(';')[2].Split("-")[1] : line.Split(";")[2]);
                crew = int.Parse(line.Split(';')[3].Contains('-') ? line.Split(';')[3].Split("-")[1] : line.Split(";")[3]);
                cruiseSpeed = int.Parse(line.Split(';')[4]);
                takeoffWeight = int.Parse(line.Split(';')[5]);
                takeoffDistance = float.Parse(line.Split(';')[6]);
                data.Add(new data(type, year, passengers, crew, cruiseSpeed, takeoffWeight, takeoffDistance));
            }

            HashSet<string> types = data.Select(i => i.type).ToHashSet();
            Console.WriteLine($"A következő repülőtípusok vannak({types.Count}):");
            foreach(var planetype in types)
                Console.WriteLine(planetype);

            Console.WriteLine("\n\n");

            List<string> boeingTypes = data.Where(i => i.type.Contains("Boeing")).Select(i => i.type).ToList();
            Console.WriteLine($"A következő Boeing vannak({boeingTypes.Count}):");
            foreach(var planetype in types.Where(i => i.Contains("Boeing")))
                Console.WriteLine($"{planetype}({boeingTypes.Count(i => i == planetype)})");

            Console.WriteLine("\n\n");

            Console.WriteLine("A legtöbb utas szállítására alkalmas gép:");
            data max = data.First(i => i.passengers == data.Max(i => i.passengers));
            Console.WriteLine($"{max.type}({max.passengers})");

            Console.WriteLine("\n\n");

            Console.WriteLine("A sebességkategória amiből nincs:");

            List<Sebessegkategoria> results = new List<Sebessegkategoria>();
            foreach(var plane in data)
                if (!results.Contains(plane.kategoria))
                    results.Add(plane.kategoria);
            for(var k = 0; k < results.Count; k++) {
                try {
                sebessegek.Remove(sebessegek.First(i => i.Kategorianev == results[k].Kategorianev));
                } catch(Exception ex) {
                }
            }
            if (sebessegek.Count == 0)
                Console.WriteLine("Minden sebességkategóriából van repülőgéptípus.");
            else
                for(var k = 0; k < sebessegek.Count; k++)
                    Console.WriteLine(sebessegek[k].Kategorianev);

            Console.WriteLine("\n\n");

            List<string> o = new List<string> {
                "típus;év;utas;személyzet;utazósebesség;felszállótömeg;fesztáv"
            };
            foreach(var line in data)
                o.Add($"{line.type};{year};{line.passengers};{line.crew};{line.cruiseSpeed};{line.takeoffWeight/1000};{line.takeoffDistance*3.2808f}");

            File.WriteAllLines("utasszallitok_new.txt", o);
        }
    }
}