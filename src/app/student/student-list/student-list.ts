import { Component, Input } from '@angular/core';
import { StudentModel } from '../../models/student.model';
import { FirstLetterCheckPipe } from '../new-student-form/first-letter-ckeck-pipe';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [FirstLetterCheckPipe, CommonModule],
  templateUrl: './student-list.html',
  styleUrl: './student-list.css',
})
export class StudentList {

  constructor(private router: Router, private route: ActivatedRoute){
  }

  @Input() filteredStudents!: StudentModel[];

  handleNavigation(id: number, url:string){
    this.router.navigate([url, id], {relativeTo: this.route});    
  }
}
