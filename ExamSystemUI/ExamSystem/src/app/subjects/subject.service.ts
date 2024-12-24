
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
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
  private baseUrl = 'http://localhost:5130/api/Subject';

  constructor(private http: HttpClient) {}

  getAllSubjects(filter: any): Observable<any> {
    console.log(filter.page, filter.pageSize);
    const params = new HttpParams()
      .set('page', filter.page)
      .set('pageSize', filter.pageSize);
    return this.http.get<Subject[]>(`${this.baseUrl}/AllSubjects`, { params });
  }

  getSubjectById(id: string): Observable<Subject> {
    return this.http.get<Subject>(`${this.baseUrl}/Subject/${id}`);
  }

  addSubject(subject: any): Observable<any> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
    });
    return this.http.post(`${this.baseUrl}/Subject`, subject, { headers });
  }

  updateSubject(subjectId: any, subject: any): Observable<any> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
    });
    return this.http.post(`${this.baseUrl}/Subject/${subjectId}`, subject, { headers });
  }

  deleteSubject(subjectId: any): Observable<any> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
    });
    return this.http.delete(`${this.baseUrl}/Subject/${subjectId}`, { headers });
  }
  
}
