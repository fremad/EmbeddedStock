using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseMe.Models.DTOs
{
    public class DtoExercise
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }


        public string Workout { get; set; }
    }
}
