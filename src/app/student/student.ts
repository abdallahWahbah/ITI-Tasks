import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentModel } from '../models/student.model';
import { StudentFilter } from './student-filter/student-filter';
import { StudentService } from '../services/student.service';
import { StudentList } from "./student-list/student-list";
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-student',
  standalone: true,
  imports: [CommonModule, StudentFilter, StudentList],
  templateUrl: './student.html',
  styleUrl: './student.css'
})
export class Student implements OnInit {

  constructor(private _studentService: StudentService, private router: Router, private route: ActivatedRoute){}

  students: StudentModel[] = [];
  filteredStudents: StudentModel[] = [];
  isUpdatingStudent: boolean = false;
  selectedStudent: StudentModel = {id:0, name: "", age: 0, address:""};
  filterValue?: string;
  
  ngOnInit(): void {
    this.students = this._studentService.students;
    this._studentService.getAll().subscribe({
      next: (data:any) => this.filteredStudents = data
    });
  }
  handleFilterValue(filterString: string){
    this.filteredStudents = 
                  filterString.length === 0 ? 
                  this.students
                  :
                  this.students.filter(std => std.name.toLocaleLowerCase().startsWith(filterString.toLocaleLowerCase()));
  }
  navigateNewStudent(){
    this.router.navigate(['new'], {relativeTo: this.route});
  }
}
