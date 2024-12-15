import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
        return this.http.get<Dashboard>(`${this.baseUrl}/Dashboard`);
    }
}