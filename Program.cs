using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AviationSystem
{
    class Program
    {
        static void Main()
        {
            using (var db = new ApplicationContext())
            {
                
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var country1 = new Country { Name = "USA" };
                var country2 = new Country { Name = "Germany" };
                db.Countries.AddRange(country1, country2);

                var airport1 = new Airport { Name = "JFK Airport", Country = country1 };
                var airport2 = new Airport { Name = "Berlin Tegel", Country = country2 };
                db.Airports.AddRange(airport1, airport2);

                var plane1 = new Plane { Model = "Boeing 747", Airport = airport1 };
                var spec1 = new PlaneSpecification { EngineType = "Turbofan", SeatingCapacity = 416, MaxSpeed = 955, Plane = plane1 };

                var plane2 = new Plane { Model = "Airbus A380", Airport = airport2 };
                var spec2 = new PlaneSpecification { EngineType = "Turbojet", SeatingCapacity = 525, MaxSpeed = 1020, Plane = plane2 };

                db.Planes.AddRange(plane1, plane2);
                db.PlaneSpecifications.AddRange(spec1, spec2);

                db.SaveChanges();
            }

            using (var db = new ApplicationContext())
            {
                var plane = db.Planes
                    .Include(p => p.PlaneSpecification)
                    .Include(p => p.Airport)
                    .ThenInclude(a => a.Country)
                    .FirstOrDefault(p => p.Model == "Boeing 747");

                if (plane != null)
                {
                    Console.WriteLine($"Самолет: {plane.Model}, Характеристики: {plane.PlaneSpecification.EngineType}, Аэропорт: {plane.Airport.Name}, Страна: {plane.Airport.Country.Name}");
                }
                else
                {
                    Console.WriteLine("Самолет не найден.");
                }

                var country = db.Countries
                    .Include(c => c.Airports)
                    .ThenInclude(a => a.Planes)
                    .ThenInclude(p => p.PlaneSpecification)
                    .FirstOrDefault(c => c.Name == "Germany");

                if (country != null)
                {
                    Console.WriteLine($"Страна: {country.Name}");
                    foreach (var airport in country.Airports)
                    {
                        Console.WriteLine($"Аэропорт: {airport.Name}");
                        foreach (var planeInAirport in airport.Planes)
                        {
                            Console.WriteLine($"Самолет: {planeInAirport.Model}, Характеристики: {planeInAirport.PlaneSpecification.EngineType}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Страна не найдена.");
                }
            }
        }
    }
}
