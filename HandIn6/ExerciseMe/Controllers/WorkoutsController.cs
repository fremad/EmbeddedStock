using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExerciseMe.DAL;
using ExerciseMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace ExerciseMe.Controllers
{
    [Produces("application/json")]
    [Route("api/Workouts")]
    //[Authorize]
    public class WorkoutsController : Controller
    {
        private readonly AppDbContext _context;

        public WorkoutsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Workouts
        //[HttpGet]
        //public async Task<IActionResult> GetWorkouts()
        //{
            
        //    var hep = await _context.Workouts
        //        .Include(e => e.Exercises).ToListAsync();
        //    return Json(hep);
        //}
        // GET: api/Workouts
        [HttpGet]
        public async Task<IActionResult> GetMyWorkouts()
        {
            StringValues hep = "";
            HttpContext.Request.Headers.TryGetValue("Authorization", out hep);
            var tmp = hep.ToString().Split(" ").ElementAt(1);
            var jwtHandler = new JwtSecurityTokenHandler();
            var useremail = jwtHandler.ReadJwtToken(tmp).Claims.ElementAt(0).Value; 



            var cool = await _context.Workouts.Where(a => a.OwnerApplicationUser.Email == useremail).Include(e => e.Exercises).ToListAsync();
            return Json(cool);
        }

        // GET: api/Workouts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkout([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workout = await _context.Workouts.SingleOrDefaultAsync(m => m.ID == id);

            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }

        // PUT: api/Workouts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkout([FromRoute] string id, [FromBody] Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workout.ID)
            {
                return BadRequest();
            }

            _context.Entry(workout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
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

        // POST: api/Workouts
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostWorkout([FromBody] Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            StringValues hep = "";
            HttpContext.Request.Headers.TryGetValue("Authorization", out hep);

            var tmp = hep.ToString().Split(" ").ElementAt(1);

            

            var jwtHandler = new JwtSecurityTokenHandler();


            var useremail = jwtHandler.ReadJwtToken(tmp).Claims.ElementAt(0).Value;

            var user =_context.ApplicationUser.Single(a => a.Email == useremail);

            workout.OwnerApplicationUser = user;

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkout", new { id = workout.ID }, workout);
        }

        // DELETE: api/Workouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workout = await _context.Workouts.SingleOrDefaultAsync(m => m.ID == id);
            if (workout == null)
            {
                return NotFound();
            }

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return Ok(workout);
        }

        private bool WorkoutExists(string id)
        {
            return _context.Workouts.Any(e => e.ID == id);
        }
    }
}