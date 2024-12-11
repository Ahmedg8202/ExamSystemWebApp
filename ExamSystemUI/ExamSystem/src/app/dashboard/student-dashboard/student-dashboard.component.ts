import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-student-dashboard',
  imports: [],
  templateUrl: './student-dashboard.component.html',
  styleUrl: './student-dashboard.component.css'
})
export class StudentDashboardComponent {
  subjects = [
    { id: 1, name: 'Mathematics' },
    { id: 2, name: 'Physics' },
    { id: 3, name: 'Computer Science' }
  ];

  examHistory = [
    { examName: 'Math Final Exam', date: '2024-11-20', score: 85 },
    { examName: 'Physics Mid-Term', date: '2024-10-15', score: 90 },
    { examName: 'Computer Science Quiz', date: '2024-09-10', score: 78 }
  ];

  constructor(private router: Router) {}

  navigateToSubject(subjectId: number) {
    // Navigate to a detailed subject view (if necessary)
    this.router.navigateByUrl(`/subject/${subjectId}`);
  }

  navigateToExamDetails(examName: string) {
    // Navigate to a detailed exam results page
    this.router.navigateByUrl(`/exam-details/${examName}`);
  }
}
