import { Component } from '@angular/core';

@Component({
    selector: 'workout',
    templateUrl: './workout.component.html'
})
export class WorkoutComponent {
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount++;
    }
}
