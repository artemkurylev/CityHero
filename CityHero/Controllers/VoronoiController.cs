using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CityHero.Models;
using Voronoi2;
using Microsoft.EntityFrameworkCore;
using Point = CityHero.Models.Point;

namespace CityHero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoronoiController : ControllerBase
    {
        private readonly CityHeroTestContext _context;

        public VoronoiController(CityHeroTestContext context)
        {
            _context = context;
        }


        [HttpGet]
        public void Calc()
        {
            var points = _context.Place.ToList();

            Voronoi voronoi = new Voronoi(0.1);
            List<double> x = new List<double>(), y = new List<double>();
            
            foreach (CityHero.Models.Place point in points)
            {
                x.Add(point.CoordX);
                y.Add((double)point.CoordY);
            }
            List<GraphEdge> edges = voronoi.generateVoronoi(x.ToArray(), y.ToArray(), 43.0, 45.0, 47.0, 49.0);
            foreach(GraphEdge edge in edges)
            {
                Point newPoint = new Point { CoordX = (float)edge.x1, CoordY = (float)edge.y1 };
                _context.Point.Add(newPoint);
                PlaceArea placeArea = new PlaceArea { PlaceId = points[edge.site1].Id, PointId = newPoint.Id };
                _context.PlaceArea.Add(placeArea);
            }
            return;
        }
    }
}