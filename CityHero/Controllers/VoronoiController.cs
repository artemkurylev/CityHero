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

            Voronoi voronoi = new Voronoi(0);
            List<double> x = new List<double>(), y = new List<double>();
            
            foreach (CityHero.Models.Place point in points)
            {
                x.Add(point.CoordX);
                y.Add((double)point.CoordY);
            }

            List<GraphEdge> edges = voronoi.generateVoronoi(
                x.ToArray(), 
                y.ToArray(),
                x.Min() - 1e-5, 
                x.Max() + 1e-5, 
                y.Min() - 1e-5, 
                y.Max() + 1e-5);
            foreach(GraphEdge edge in edges)
            {
                Point newPoint = new Point { CoordX = (float)edge.x1, CoordY = (float)edge.y1 };
                _context.Point.Add(newPoint);
                _context.SaveChanges();
                PlaceArea placeArea = new PlaceArea { PlaceId = points[edge.site1].Id, PointId = newPoint.Id };
                _context.PlaceArea.Add(placeArea);
                _context.SaveChanges();
                newPoint = new Point { CoordX = (float)edge.x2, CoordY = (float)edge.y2 };
                _context.Point.Add(newPoint);
                _context.SaveChanges();
                placeArea = new PlaceArea { PlaceId = points[edge.site2].Id, PointId = newPoint.Id };
                _context.PlaceArea.Add(placeArea);
                _context.SaveChanges();
            }
            return;
        }
    }
}