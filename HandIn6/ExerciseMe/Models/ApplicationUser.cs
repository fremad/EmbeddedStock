using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ExerciseMe.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Workouts = new List<Workout>();
        }

        public string Name { get; set; }
        public List<Workout> Workouts { get; set; }
    }

    public class Workout
    {
        public Workout()
        {
            Exercises = new List<Exercise>();
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public List<Exercise> Exercises { get; set; }
    }

    public class Exercise
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    } 

}
