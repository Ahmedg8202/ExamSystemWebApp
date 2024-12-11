import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  //imports: [HttpClient],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css'],
})
export class AdminDashboardComponent {
  totalStudents: number = 0;
  completedExams: number = 0;
  passedExams: number = 0;
  failedExams: number = 0;
  examDetails: Array<{ name: string; date: string; passed: number; failed: number }> = [];

  private readonly apiUrl = 'http://localhost:5063/api/AdminDashboard'; // Replace with your API endpoint

  constructor(private http: HttpClient) {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    // this.completedExams = 10;
    // this.failedExams = 7;
    // this.passedExams = 3;
    // this.totalStudents = 12;
    this.http.get<any>(this.apiUrl).subscribe(
      (data) => {
        this.totalStudents = data.totalStudents;
        this.completedExams = data.completedExams;
        this.passedExams = data.passedExams;
        this.failedExams = data.failedExams;
      },
      (error) => {
        console.error('Error fetching dashboard data:', error);
      }
    );
  }
}
