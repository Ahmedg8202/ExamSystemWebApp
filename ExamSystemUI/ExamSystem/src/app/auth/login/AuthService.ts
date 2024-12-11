import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { jwtDecode } from "jwt-decode";
import { Observable } from "rxjs";


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5130/Student/Login';

  constructor(private http: HttpClient, private router: Router) { }

  public login(email: string, password: string): Observable<any> {
    const loginPayload = { email, password};
    return this.http.post<any>(this.apiUrl, loginPayload);
  }

  handleLoginSuccess(response: any): void {
    const token = response.token;
    if (token) {
      const decodedToken: any = jwtDecode(token);
      localStorage.setItem('token', token);

      if (decodedToken.role === 'Student') {
        this.router.navigateByUrl('student-Dashboard');
      } else if (decodedToken.rosle === 'Admin') {
        this.router.navigateByUrl('admin-Dashboard');
      } else {
        throw new Error('Invalid role');
      }
    }
  }

  handleLoginError(): void {
    throw new Error('Invalid email or password!');
  }
}
