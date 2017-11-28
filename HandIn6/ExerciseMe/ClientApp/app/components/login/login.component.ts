import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { User } from '../models/user';
import { FormControl, Validators } from '@angular/forms';

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

//var user: User;

@Component({
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


    constructor(public authService: AuthService, public router: Router) {
    }

    ngOnInit(): void {
        this.logout();
    }
    message: string
    user = new User;

    emailFormControl = new FormControl('', [
        Validators.required,
        Validators.pattern(EMAIL_REGEX)]);


    login() {
        this.message = 'Trying to log in ...';

        this.authService.login(this.user).subscribe((response) => {

                // Get the redirect URL from our auth service
                // If no redirect has been set, use the default
                const redirect = this.authService.redirectUrl ? this.authService.redirectUrl : '/home';

                // Redirect the user
                this.router.navigate([redirect]);
            },
            (err) => {
                this.message = 'Login attempt failed';
            }
        );
    }



    logout() {
        this.authService.logout();
    }

}
