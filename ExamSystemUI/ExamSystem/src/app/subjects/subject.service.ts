
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Subject {
  subjectId: string;
  name: string;
  description: string;
  questionsNumber: number;
  duration: number;
  total: number;
}

@Injectable({
  providedIn: 'root',
})
export class SubjectService {
  private baseUrl = 'http://localhost:5130/api/subject';

  constructor(private http: HttpClient) {}

  // Get all subjects
  getAllSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(`${this.baseUrl}`);
  }

  // Get a specific subject by ID
  getSubjectById(id: string): Observable<Subject> {
    return this.http.get<Subject>(`${this.baseUrl}/Subject/${id}`);
  }

  addSubject(subject: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Subject`, subject);
  }
}
