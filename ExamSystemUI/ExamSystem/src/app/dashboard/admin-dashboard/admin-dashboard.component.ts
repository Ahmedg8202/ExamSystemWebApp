import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AdminService, Dashboard } from './admin.service';

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
  dashboard: Dashboard| null = null;
  constructor(private http: HttpClient, private adminService: AdminService) {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.adminService.dashboard().subscribe({
      next: (data) =>{
        this.dashboard = data;
        console.log(data);
      },
      error: (error) =>{
        console.error('Error fetching subject:', error);
      }
    })
    // this.http.get<any>(this.apiUrl).subscribe(
    //   (data) => {
    //     this.totalStudents = data.totalStudents;
    //     this.completedExams = data.completedExams;
    //     this.passedExams = data.passedExams;
    //     this.failedExams = data.failedExams;
    //   },
    //   (error) => {
    //     console.error('Error fetching dashboard data:', error);
    //   }
    // );
  }
}
