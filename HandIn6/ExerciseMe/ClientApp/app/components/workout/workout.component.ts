import { Component, OnInit } from '@angular/core';
import { WorkoutService } from '../services/workout.service'

@Component({
    selector: 'workout',
    templateUrl: './workout.component.html',
    providers: [WorkoutService]
})
export class WorkoutComponent implements OnInit {

    ngOnInit(): void {
        this.workoutservice.getWorkouts().subscribe(data => {
            console.log(data);
        });
    }

    constructor(private workoutservice: WorkoutService) {  }

    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
    }
}
