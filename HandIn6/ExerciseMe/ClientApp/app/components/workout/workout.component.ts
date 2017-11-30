import { Component, OnInit } from '@angular/core';
import { WorkoutService } from '../services/workout.service'
import { Workout } from '../models/workout'

@Component({
    selector: 'workout',
    templateUrl: './workout.component.html',
    providers: [WorkoutService]
})
export class WorkoutComponent implements OnInit {

    workouts: Workout[];
    selectedWorkout: Workout;


    ngOnInit(): void {
        this.workoutservice.getWorkouts().subscribe(data => {
            console.log(data);
            this.workouts = data;
        });
    }

    constructor(private workoutservice: WorkoutService) {  }

    onSelect(workout: Workout): void {
        this.selectedWorkout = workout;
    }


}
