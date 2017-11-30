import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { User } from '../models/user';
import { FormControl, Validators } from '@angular/forms';

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent {


    constructor(public authService: AuthService, public router: Router) {
        // this.setMessage();
    }

    message: string;
    user = new User();

    //emailFormControl = new FormControl('', [
    //    Validators.required,
    //    Validators.pattern(EMAIL_REGEX)]);

    //passwordFormControl = new FormControl('', [
    //    Validators.required]);

    //userFormControl = new FormControl('', [
    //    Validators.required]);

    onSubmit() {
        this.message = 'Trying to register...';

        this.authService.register(this.user).subscribe(res => {
            if (res) {
                this.router.navigate(['/login']);
            } else {
                console.log('Not able to register');
            }
        });
    }

}