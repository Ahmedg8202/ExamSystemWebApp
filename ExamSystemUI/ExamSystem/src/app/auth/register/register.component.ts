import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [FormsModule],
  standalone: true,
})
export class RegisterComponent {
  name: string = '';
  email: string = '';
  password: string = '';
  confirmPassword: string = '';

  constructor(private router: Router) {}
  register() {
    console.log(this.name, this.email, this.password, this.confirmPassword);
    if (!this.name || !this.email || !this.password || !this.confirmPassword) {
      alert('Please fill in all the fields.');
      return;
    }

    if (this.password !== this.confirmPassword) {
      alert('Passwords do not match!');
      return;
    }

    console.log('Registration successful:', {
      name: this.name,
      email: this.email,
      password: this.password,
    });
    alert('Registration successful!');
    this.router.navigateByUrl('/login');
  }
}
