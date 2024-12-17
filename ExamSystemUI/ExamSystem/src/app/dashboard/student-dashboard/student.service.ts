import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Student {
  id: string;
  name: string;
  age: number;
  email: string;
  course: string;
}

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  private baseUrl = 'http://localhost:5130/Student/';

  constructor(private http: HttpClient) {}

  // Get all students
  getAllStudents(filter: any): Observable<any[]> {
    const params = new HttpParams()
      .set('page', filter.page)
      .set('pageSize', filter.pageSize);
      console.log("loading " + params.get('page') + " " + params.get('pageSize'));
    return this.http.get<any[]>(`${this.baseUrl}students`, { params });
  }

  // Get a single student by ID
  getStudentById(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  // Add a new student
  addStudent(student: Student): Observable<Student> {
    return this.http.post<Student>(`${this.baseUrl}`, student);
  }

  // Update an existing student
  updateStudent(id: string, student: Student): Observable<Student> {
    return this.http.put<Student>(`${this.baseUrl}/${id}`, student);
  }

  // Delete a student by ID
  deleteStudent(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}