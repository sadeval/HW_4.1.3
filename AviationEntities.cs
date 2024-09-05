using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AviationSystem
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Airport> Airports { get; set; } = new List<Airport>();
    }

    public class Airport
    {
        public int AirportId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Plane> Planes { get; set; } = new List<Plane>();
    }

    public class Plane
    {
        public int PlaneId { get; set; }
        [Required]
        public string Model { get; set; }
        public int AirportId { get; set; }
        public virtual Airport Airport { get; set; }
        public virtual PlaneSpecification PlaneSpecification { get; set; }
    }

    public class PlaneSpecification
    {
        public int PlaneSpecificationId { get; set; }
        public string EngineType { get; set; }
        public int SeatingCapacity { get; set; }
        public double MaxSpeed { get; set; }
        public int PlaneId { get; set; }
        public virtual Plane Plane { get; set; }
    }

}
