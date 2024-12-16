import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  name = '';
  isAdmin = false;
  isStudent = false;

  constructor(private router: Router) {}
  
  ngOnInit(): void {
    this.isStudent = localStorage.getItem('userRole') === 'Student';
    this.isAdmin = localStorage.getItem('userRole') === 'Admin';
    this.isStudent ? this.name = 'Student' : this.name = '';
    this.isAdmin ? this.name = 'Admin' : this.name = '';
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
