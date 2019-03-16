using System;
using System.Collections.Generic;

namespace CityHero.Models
{
    public partial class VisitedPlaces
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PlaceId { get; set; }

        public virtual Place Place { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
