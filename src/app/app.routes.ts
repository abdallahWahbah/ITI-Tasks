import { Routes } from '@angular/router';
import { Home } from './home/home';
import { Contact } from './contact/contact';
import { About } from './about/about';
import { NotFound } from './not-found/not-found';

export const routes: Routes = [
    {path: "home", component: Home},
    {path: "", redirectTo: 'home', pathMatch: 'full'},
    {path: "contact", component: Contact},
    {path: 'about', component: About},
    {path: 'students', loadChildren: () => import('./student/student.routes').then(s => s.studentRoutes)},
    {path: '**', component: NotFound}
];
