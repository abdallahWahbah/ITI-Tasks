import { Routes } from "@angular/router";
import { Student } from "./student";
import { NewStudentForm } from "./new-student-form/new-student-form";
import { StudentList } from "./student-list/student-list";
import { StudentDetails } from "./student-details/student-details";
import { StudentDeleted } from "./student-deleted/student-deleted";

export const studentRoutes: Routes = [
    {path: '', component: Student},
    {path: 'list', component: StudentList},
    {path: 'new', component: NewStudentForm},
    {path: 'edit/:id', component: NewStudentForm},
    {path: 'deleted/:id', component: StudentDeleted},
    {path: 'details/:id', component: StudentDetails},
]