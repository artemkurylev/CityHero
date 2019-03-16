using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityHero.Models;

namespace CityHero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly CityHeroTestContext _context;

        public PlacesController(CityHeroTestContext context)
        {
            _context = context;
        }

        // GET: api/Places
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetPlace()
        {
            return await _context.Place
                .Include(p => p.PlaceArea).ThenInclude(pa => pa.Point)
                .ToListAsync();
        }

        // GET: api/Places/5
        [HttpGet("{id}")]
        public ActionResult<Place> GetPlace(int id)
        {
            var place = _context.Place
                .Include( p => p.PlaceArea).ThenInclude( pa => pa.Point)
                .Where( p => p.Id == id)
                .FirstOrDefault();

            if (place == null)
            {
                return NotFound();
            }

            return place;
        }

        // PUT: api/Places/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlace(int id, Place place)
        {
            if (id != place.Id)
            {
                return BadRequest();
            }

            _context.Entry(place).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Places
        [HttpPost]
        public async Task<ActionResult<Place>> PostPlace(Place place)
        {
            _context.Place.Add(place);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlaceExists(place.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlace", new { id = place.Id }, place);
        }

        // DELETE: api/Places/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Place>> DeletePlace(int id)
        {
            var place = await _context.Place.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }

            _context.Place.Remove(place);
            await _context.SaveChangesAsync();

            return place;
        }

        private bool PlaceExists(int id)
        {
            return _context.Place.Any(e => e.Id == id);
        }
    }
}
