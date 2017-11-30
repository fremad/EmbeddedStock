import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { UserDataComponent } from './components/userdata/userdata.component';
import { WorkoutComponent } from './components/workout/workout.component';
import { AuthGuard } from './components/services/auth-guard.service';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent} from './components/register/register.component';
import { AuthService } from './components/services/auth.service';



const approutes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'workout', component: WorkoutComponent },
    { path: 'userdata', component: UserDataComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: '**', redirectTo: 'home' }
]


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        WorkoutComponent,
        UserDataComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot(approutes)
    ],
    providers: [AuthGuard, AuthService]
})
export class AppModuleShared {
}
