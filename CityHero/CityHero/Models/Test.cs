using System;
using System.Collections.Generic;

namespace CityHero.Models
{
    public partial class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
