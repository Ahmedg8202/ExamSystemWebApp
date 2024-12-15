import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { jwtDecode } from "jwt-decode";
import { Observable } from "rxjs";

export interface Register{
  userName: string;
  email: string;
  password: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5130/Student';
  private tokenKey = 'authToken';
  private userId = 'userId';
  private userRole = 'userRole';

  constructor(private http: HttpClient, private router: Router) { }

  public login(email: string, password: string): Observable<any> {
    const loginPayload = { email, password};
    return this.http.post<any>(`${this.apiUrl}/login`, loginPayload);
  }

  register(register: Register): Observable<any>{
    return this.http.post<any>(`${this.apiUrl}/Register`, register);
  }

  handleLoginSuccess(response: any, userRole: string): void {
    
    if (response.token) {

      this.setToken(response.token);
      console.log("success LogIn");

      if (this.getRole() === 'Student') {
      console.log("success LogIn student"); 
        this.router.navigateByUrl('student-Dashboard');
      console.log("success LogIn");
      } else if (this.getRole() === 'Admin') {
      console.log("success LogIn admin");
      this.router.navigateByUrl('admin-Dashboard');
      } else {
        throw new Error('Invalid role');
      }

    }
  }

  handleLoginError(): void {
    throw new Error('Invalid email or password!');
  }


  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
    localStorage.setItem(this.userRole, this.getRole()!);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getDecodedToken(): any {
    const token = this.getToken();
    return token ? jwtDecode(token) : null;
  }

  getRole(): string | null {
    const decoded = this.getDecodedToken();
    return decoded ? decoded.role : null;
  }

  getId(): string | null {
    const decoded = this.getDecodedToken();
    return decoded ? decoded.Id : null;
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}
