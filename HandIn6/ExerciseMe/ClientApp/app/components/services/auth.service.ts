import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { User } from '../models/user';

import 'rxjs/add/observable/of';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/delay';
import 'rxjs/add/operator/map';

const tokenName = 'currentUser';

@Injectable()
export class AuthService {
    // isLoggedIn = false;
    public token: string;

    constructor(private http: Http) {
        // set token if saved in local storage
        const currentUser = JSON.parse(String(localStorage.getItem('currentUser')));
        this.token = currentUser && currentUser.token;
    }

    // store the URL so we can redirect after logging in
    redirectUrl: string;

    isLoggedIn(): boolean {
        const token = this.getToken();
        if (token) {
            const payload = JSON.parse(window.atob(token.split('.')[1]));
            return payload.exp > Date.now() / 1000;
        } else {
            return false;
        }
    }

    login(user: User): Observable<boolean> {
        return this.http.post('http://localhost:8000/api/Account/Login', { email: user.email, password: user.password })
            .map((response: Response) => {
                console.log(response);
                // login successful if there's a jwt token in the response
              
                const token = response.text();
                console.log(token);
                if (token) {
                    // set token property
                    this.token = token;

                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    // localStorage.setItem('currentUser', JSON.stringify({ token: token }));
                    this.saveToken(token);
                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            });
    }

    logout(): void {
        localStorage.removeItem(tokenName);
    }

    register(user: User): Observable<boolean> {
        // console.log(name, email, password);
        return this.http.post
            ('http://localhost:3000/api/Register', { name: user.name, email: user.email, password: user.password })
            .map((response: Response) => {
                console.log(response);
                // login successful if there's a jwt token in the response
                const token = response.json() && response.json().token;
                if (token) {
                    // set token property
                    this.token = token;

                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    this.saveToken(token);
                    // localStorage.setItem('currentUser', JSON.stringify({ name: user.name, token: token }));
                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            });
    }

    private saveToken(token: string) {
        window.localStorage.setItem(tokenName, JSON.stringify({ token: token }));
    }

    private getToken() {
        if (localStorage.getItem(tokenName)) {
            return localStorage.getItem(tokenName);
        } else {
            return '';
        }
    }
}