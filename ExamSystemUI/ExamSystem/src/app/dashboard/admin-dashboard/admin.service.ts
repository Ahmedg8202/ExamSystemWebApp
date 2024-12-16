import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Dashboard{
    studentNumber: number;
    examCompleted: number;
    passedExams: number;
    failedExam: number;
}

@Injectable({
    providedIn: 'root',
})
export class AdminService {
    private baseUrl = 'http://localhost:5130/Admin';

    constructor(private http: HttpClient) {}

    dashboard(): Observable<Dashboard> {
        const token = localStorage.getItem('authToken');
        const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        });
        return this.http.get<Dashboard>(`${this.baseUrl}/Dashboard`, { headers });
    }
}