import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import 'rxjs/Rx';


@Injectable()
export class WorkoutService {

    constructor(private http: Http) { }

    getWorkouts() {
        return this.http.get('http://localhost:8000/api/Workouts').map(data => {
            console.log(data);

            return data.json();
        });
    }
}