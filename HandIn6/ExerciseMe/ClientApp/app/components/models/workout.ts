export class Exercise {
    id: string;
    name: string;
    description: string;
    sets: number;
    reps: number;
    workout: string;
}

export class Workout {
    id: string;
    name: string;
    exercises: Exercise[];
    workoutDate: Date;
    completed: Boolean;
}
