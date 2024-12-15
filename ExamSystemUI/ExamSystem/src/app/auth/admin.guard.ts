import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './login/AuthService';

@Injectable({
    providedIn: 'root',
  })
  export class AdminGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) {}
  
    canActivate(): boolean {
      const role = this.authService.getRole();
      if (role === 'Admin') {
        return true;
      }
      this.router.navigate(['']); // Redirect if not authorized
      return false;
    }
  }
  