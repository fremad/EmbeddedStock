import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service'

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {

    isLoggedIn: boolean = this.authService.isLoggedIn();

    constructor(private authService: AuthService) {  }

    logout(): void {
       this.authService.logout();
    }


}
