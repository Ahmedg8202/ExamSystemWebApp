import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  name = 'student';
  isAdmin = false;
  isStudent = false;

  constructor(private router: Router) {}
  
  ngOnInit(): void {
    this.isStudent = localStorage.getItem('userRole') === 'Student';
    this.isAdmin = localStorage.getItem('userRole') === 'Admin';
  }

  login(){
    this.router.navigate(['/login']);
  }

  register(){
    this.router.navigate(['/register']);
  }

  logout(){
    this.router.navigate(['/logout']);
  }
}
