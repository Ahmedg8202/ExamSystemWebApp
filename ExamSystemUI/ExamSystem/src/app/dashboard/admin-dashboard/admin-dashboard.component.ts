import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AdminService, Dashboard } from './admin.service';
import { StudentService } from '../student-dashboard/student.service';
import { ExamsService } from '../../exams/exams.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
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
  examResults: any[] = [];
  students: {id: string, email: string, userName: string, active: true}[] = [];
  filter = {
    page: 1,
    pageSize: 10
  };
  constructor(private http: HttpClient, 
    private adminService: AdminService,
    private studentService: StudentService,
    private examService: ExamsService
  ) {
  }

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    console.log('Loading dashboard data...');
    this.adminService.dashboard().subscribe({
      next: (data) =>{
        this.dashboard = data;
        console.log(data);
      },
      error: (error) =>{
        console.error('Error fetching subject:', error);
      }
    })

    this.getAllStudents();

    this.examService.getAllExamsResults().subscribe({
      next: (data) => {
        this.examResults = data;
        console.log(this.examResults);
      },
      error: (error) => {
        
      }
    })
  }

  getAllStudents(){
    console.log(this.filter.pageSize);
    this.studentService.getAllStudents(this.filter).subscribe({
      next: (data) => {
        this.students = data;
        console.log("students " , this.students);
      },
      error: (error) => {
        console.error('Error fetching subject:', error);
      }
    })
  }

  toggleActiveStatus(student: any): void {
    student.active = !student.active; 
    console.log(`Student ${student.userName} is now ${student.active ? 'Active' : 'Inactive'}`);
  }


  onPrevious() {
    this.filter.page --;
    this.getAllStudents();
  }
  onNext() {
    this.filter.page ++;
    this.getAllStudents();
  }

}
