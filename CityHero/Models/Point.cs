using System;
using System.Collections.Generic;

namespace CityHero.Models
{
    public partial class Point
    {
        public Point()
        {
            PlaceArea = new HashSet<PlaceArea>();
        }

        public int Id { get; set; }
        public float CoordX { get; set; }
        public float CoordY { get; set; }

        public virtual ICollection<PlaceArea> PlaceArea { get; set; }
    }
}
