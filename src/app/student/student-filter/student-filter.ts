import { Component, EventEmitter, Input, Output } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { FloatLabel } from 'primeng/floatlabel';

@Component({
  selector: 'app-student-filter',
  standalone: true,
  imports: [FormsModule, InputTextModule, FloatLabel],
  templateUrl: './student-filter.html',
  styleUrl: './student-filter.css',
})
export class StudentFilter {
  @Input() value?: string;
  @Output() valueChange: EventEmitter<string> = new EventEmitter<string>();
}
