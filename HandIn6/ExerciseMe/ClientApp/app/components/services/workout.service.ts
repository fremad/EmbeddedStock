import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Workout, Exercise } from '../models/workout';
import 'rxjs/Rx';


@Injectable()
export class WorkoutService {

    constructor(private http: Http) { }

    getWorkouts() {
        return this.http.get('http://localhost:8000/api/Workouts').switchMap(data => {
            console.log(data);
            console.log("WHHATTTT!");

            return data.json();
        });
    }

    addUserWorkout(workout: Workout) {
        // console.log(workout);
        //console.log(environment.apiUrl + 'userworkouts' + workout);
        var token = "Bearer " + JSON.parse(((localStorage.getItem('currentUser'))) as any).token;

        token = token.split('"').join('');
        console.log(token);

        const headers = new Headers({
            'Authorization': token
    });

        console.log(headers);
        return this.http
            .post(
                'http://localhost:8000/api/Workouts', workout, {
                    headers: headers
                }).map(data => {
                console.log(data);
                return data.json();
            });
    }
}