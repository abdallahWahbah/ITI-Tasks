import { Component, signal } from '@angular/core';
import { Home } from "./home/home";
import { RouterModule } from "@angular/router";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [Home, RouterModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('z-lab');
}
