import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AuthService } from './AuthService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [FormsModule, CommonModule],
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  userRole: string = '';
  isLoading: boolean = false;
  loginError: string | null = null;
  constructor(@Inject(AuthService) private authService: AuthService) {}

  login(loginForm: any) {
    if (!loginForm.valid) {
      return;
    }
    this.email = loginForm.value.email;
    this.password = loginForm.value.password;
    this.userRole = loginForm.value.userRole;
    this.isLoading = true;

    this.authService.login(this.email, this.password).subscribe(
      (response: any) => { 
        this.isLoading = false;
        try {
          this.authService.handleLoginSuccess(response, this.userRole); 
          console.log(response.token);
        } catch (error:any) {
          this.loginError = error.message as string;
        }
      },
      (error: any) => {
        this.isLoading = false;
        this.loginError = 'Invalid email or password! Please try again.';
      }
    );
  }
}



