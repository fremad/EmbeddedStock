import { Component, OnInit, Input } from '@angular/core';
import { Workout, Exercise } from '../models/workout';
import { WorkoutService } from '../services/workout.service';

@Component({
    selector: 'exercise',
    templateUrl: './exercise.component.html',
    styleUrls: ['./exercise.component.css']
})

export class ExerciseComponent implements OnInit {
    @Input() workout: Workout;

    constructor(private workoutservice: WorkoutService) {  }


    exercise = new Exercise();

    ngOnInit(): void { }

    onSubmit() {
        this.exercise.workout = this.workout.id;
        this.workoutservice.addExercise(this.exercise).subscribe(() => {
        });
    }
}