import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { StudentService } from '../../services/student.service';
import { StudentModel } from '../../models/student.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-student-details',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './student-details.html',
  styleUrl: './student-details.css',
})
export class StudentDetails implements OnInit, OnDestroy{
  
  student?: StudentModel;
  studentId?: number;
  idSubscription?: Subscription;

  constructor(private _studentService: StudentService, private router: Router, private route: ActivatedRoute){
  }


  ngOnInit(): void {
    this.idSubscription = this.route.params.subscribe(params => {
      let id = +params['id'];
      if(id){
        this.studentId = id;
        this.student = this._studentService.getById(id);
        console.log(this.student);
      } else {
        this.student = undefined;
      }
    });
  }
  ngOnDestroy(): void {
    this.idSubscription?.unsubscribe();
  }
}
