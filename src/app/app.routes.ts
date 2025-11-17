import { Routes } from '@angular/router';
import { Home } from './home/home';
import { Contact } from './contact/contact';
import { About } from './about/about';
import { NotFound } from './not-found/not-found';
import { Login } from './login/login';
import { contactAccessGuard } from './guards/contact-access-guard';

export const routes: Routes = [
    {path: "home", component: Home},
    {path: "", redirectTo: 'home', pathMatch: 'full'},
    {path: "contact", component: Contact, canActivate: [contactAccessGuard]},
    {path: 'about', component: About},
    {path: 'login', component: Login},
    {path: 'students', loadChildren: () => import('./student/student.routes').then(s => s.studentRoutes)},
    {path: '**', component: NotFound}
];
