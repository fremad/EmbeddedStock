export class Exercise {
    _id: string;
    exercise: string;
    description: string;
    exset: number;
    reps: number;
}

export class Workout {
    _id: string;
    name: string;
    exercises: Exercise[];
    workoutDate: Date;
    completed: Boolean;
}
