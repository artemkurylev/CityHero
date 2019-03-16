using System;
using System.Collections.Generic;

namespace CityHero.Models
{
    public partial class Place
    {
        public Place()
        {
            PlaceArea = new HashSet<PlaceArea>();
            VisitedPlaces = new HashSet<VisitedPlaces>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float CoordX { get; set; }
        public float? CoordY { get; set; }
        public string Description { get; set; }
        public int? IdType { get; set; }

        public virtual ICollection<PlaceArea> PlaceArea { get; set; }
        public virtual ICollection<VisitedPlaces> VisitedPlaces { get; set; }
    }
}
