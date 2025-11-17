import { Component, EventEmitter, Input, Output, OnChanges, OnInit } from '@angular/core';
import {FormsModule} from "@angular/forms"
import { StudentModel } from '../../models/student.model';
import { StudentService } from '../../services/student.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-new-student-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './new-student-form.html',
  styleUrl: './new-student-form.css',
})
export class NewStudentForm implements OnInit {

  @Input() isUpdatingStudent?: boolean = false;

  enteredName!: string;
  enteredAge!: string;
  enteredAddress!: string;
  students!: StudentModel[];

  constructor(private _studentService: StudentService, private router: Router, private route: ActivatedRoute){
  }

  ngOnInit() {
    this._studentService.getAll().subscribe({
      next: (data:any) => this.students = data
    });
    let studentId = this.route.snapshot.params['id'];

    if(studentId){ // updating
      this._studentService.getById(studentId).subscribe({
        next: student => {
          this.enteredName = student.name;
          this.enteredAge = student.age.toString();
          this.enteredAddress = student.address;
          this.isUpdatingStudent = true
        }
      })
    }
  }

  handleAddUpdateStudent(){
    if(this.isUpdatingStudent){ // updating student
      this._studentService.updateStudent({
        id: +this.route.snapshot.params['id'],
        name: this.enteredName,
        age: +this.enteredAge,
        address: this.enteredAddress
      }).subscribe({
        next: data => console.log(data)
      })
    }
    else{ // adding new student
      this._studentService.addStudent({
        id: this.students[this.students.length - 1].id + 1,
        name: this.enteredName, 
        age: +this.enteredAge, 
        address: this.enteredAddress,
      }).subscribe({
        next: data => console.log(data)
      })
    }
    this.router.navigate(['/students'])
  }
  resetForm(){
    this.enteredName = '';
    this.enteredAge = '';
    this.enteredAddress = '';
  }
}
