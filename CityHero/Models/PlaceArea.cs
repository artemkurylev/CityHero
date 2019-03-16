using System;
using System.Collections.Generic;

namespace CityHero.Models
{
    public partial class PlaceArea
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public int PointId { get; set; }

        public virtual Place Place { get; set; }
        public virtual Point Point { get; set; }
    }
}
