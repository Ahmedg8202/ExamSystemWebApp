import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../auth/login/AuthService';

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

  constructor(private router: Router, private authService: AuthService) {}
  
  ngOnInit(): void {
    this.isStudent = localStorage.getItem('userRole') === 'Student';
    this.isAdmin = localStorage.getItem('userRole') === 'Admin';
    this.name = this.authService.getUserName();
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
