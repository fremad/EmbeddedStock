import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Workout, Exercise } from '../models/workout';
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

    addExercise(exercise: Exercise) {
        console.log('called addExercise');

        var token = "Bearer " + JSON.parse(((localStorage.getItem('currentUser'))) as any).token;

        token = token.split('"').join('');
        console.log(token);

        const headers = new Headers({
            'Authorization': token
        });

        return this.http
            .post('http://localhost:8000/api/Exercises', exercise, {
                headers: headers
            }).map(data => {
                return data.json();
            });
    }
}