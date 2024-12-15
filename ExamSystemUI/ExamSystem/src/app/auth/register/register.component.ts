import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, Register } from '../login/AuthService';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [FormsModule],
  standalone: true,
})
export class RegisterComponent {
  name!: string;
  email!: string;
  password!: string;
  confirmPassword: string = '';

  constructor(private router: Router, private authService: AuthService) {}
  register() {
    if (!this.name || !this.email || !this.password || !this.confirmPassword) {
      alert('Please fill in all the fields.');
      return;
    }

    if (this.password !== this.confirmPassword) {
      alert('Passwords do not match!');
      return;
    }

    const registerData: Register = {
      userName: this.name,
      email: this.email,
      password: this.password,
    };

    console.log(registerData);
    this.authService.register(registerData).subscribe({
      next: (data) =>{
        console.log(data);
        alert(data);
        this.router.navigateByUrl('/login');
      },
      error: (error) =>{
        console.error('Error fetching subject:', error);
      }
    }
    )
    // console.log(this.name, this.email, this.password, this.confirmPassword);
    

    // console.log('Registration successful:', {
    //   name: this.name,
    //   email: this.email,
    //   password: this.password,
    // });
    // alert('Registration successful!');
    // this.router.navigateByUrl('/login');
  }
}
