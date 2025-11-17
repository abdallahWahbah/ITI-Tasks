import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  loginForm!: FormGroup;
  submitted = false;
  loading = false;
  errorMessage = '';

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(2)]],
    });
  }
  get f() {
    return this.loginForm?.controls;
  }
  onSubmit() {
    this.submitted = true;
    this.errorMessage = '';

    if (this.loginForm?.invalid) return;
    this.loading = true;

    const { username, password } = this.loginForm?.value;

    this.http.get('https://localhost:7224/api/token', { responseType: 'text' }).subscribe({
      next: (data) => {
        localStorage.setItem("token", data)
      } 
    });
  }
}
