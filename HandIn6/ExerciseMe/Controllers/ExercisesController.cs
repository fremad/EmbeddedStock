using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExerciseMe.DAL;
using ExerciseMe.Models;
using ExerciseMe.Models.DTOs;

namespace ExerciseMe.Controllers
{
    [Produces("application/json")]
    [Route("api/Exercises")]
    public class ExercisesController : Controller
    {
        private readonly AppDbContext _context;

        public ExercisesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Exercises
        [HttpGet]
        public IEnumerable<Exercise> GetExercises()
        {
            return _context.Exercises;
        }

        // GET: api/Exercises/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExercise([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.ID == id);

            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        // PUT: api/Exercises/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise([FromRoute] string id, [FromBody] Exercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exercise.ID)
            {
                return BadRequest();
            }

            _context.Entry(exercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(id))
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

        // POST: api/Exercises
        [HttpPost]
        public async Task<IActionResult> PostExercise([FromBody] DtoExercise exercise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tmp = new Exercise();


            var hep = _context.Workouts.Find(exercise.Workout);

            tmp.Description = exercise.Description;
            tmp.Name = exercise.Name;
            tmp.Reps = exercise.Reps;
            tmp.Sets = exercise.Sets;
            tmp.Workout = hep;

            _context.Exercises.Add(tmp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExercise", new { id = tmp.ID }, exercise);
        }

        // DELETE: api/Exercises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.ID == id);
            if (exercise == null)
            {
                return NotFound();
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return Ok(exercise);
        }

        private bool ExerciseExists(string id)
        {
            return _context.Exercises.Any(e => e.ID == id);
        }
    }
}