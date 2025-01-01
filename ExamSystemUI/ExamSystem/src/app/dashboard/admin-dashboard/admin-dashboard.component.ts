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
  students: {id: string, email: string, userName: string, active: boolean}[] = [];
  studentsFilter = {
    page: 1,
    pageSize: 10
  };
  resultsFilter = {
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
    this.getAllExamResults();
  }

  getAllExamResults(){
    this.examService.getAllExamsResults(this.resultsFilter).subscribe({
      next: (data) => {
        this.examResults = data;
        console.log(this.examResults);
      },
      error: (error) => {
        
      }
    })
  }
  getAllStudents(){
    console.log(this.studentsFilter.pageSize);
    this.studentService.getAllStudents(this.studentsFilter).subscribe({
      next: (data) => {
        this.students = data;
        console.log("students " , this.students);
      },
      error: (error) => {
        console.error('Error fetching subject:', error);
      }
    })
  }

 studentStatus(studentId: string, isEnabled: boolean): void {
    this.adminService.studentStatus(studentId, isEnabled).subscribe({
      next: (response) => {
          console.log('Student status updated successfully:');
          const student = this.students.find(s => s.id === studentId);
          if (student) {
            student.active = isEnabled;
          }
      },
      error: (error) => {
          console.error('Error updating student status:', error);
      }
  });
  }


  onPreviousSudent() {
    this.studentsFilter.page --;
    this.getAllStudents();
  }
  onNextStudent() {
    this.studentsFilter.page ++;
    this.getAllStudents();
  }

  onPreviousResult() {
    this.resultsFilter.page --;
    this.getAllExamResults();
  }
  onNextResult() {
    this.resultsFilter.page ++;
    this.getAllExamResults();
  }

}
