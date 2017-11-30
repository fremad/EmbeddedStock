import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { WorkoutService } from '../services/workout.service'
import { Workout } from '../models/workout'


@Component({
    selector: 'userdata',
    templateUrl: './userdata.component.html',
    providers: [WorkoutService]
})
export class UserDataComponent {


    constructor(private workoutservice: WorkoutService) {}

    workout = new Workout();

    addWorkout() {
        if (this.workout) {
            this.workoutservice.addUserWorkout(this.workout).subscribe((mydata) => {

            });
        }

    }
}

