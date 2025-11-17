import { Component, OnInit } from '@angular/core';
import { StudentService } from '../../services/student.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-student-deleted',
  standalone: true,
  imports: [],
  templateUrl: './student-deleted.html',
  styleUrl: './student-deleted.css',
})
export class StudentDeleted implements OnInit{

  constructor(private _studentService: StudentService, private router: Router, private route: ActivatedRoute){}

  ngOnInit(): void {
    let studentId = +this.route.snapshot.params['id'];
    this._studentService.deleteStudent(studentId).subscribe({
      next: data => this.router.navigate(['/students'])
    });
  }
}