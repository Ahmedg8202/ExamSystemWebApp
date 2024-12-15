import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { ExamResult, ExamsService, Subject } from '../../exams/exams.service';
import { AuthService } from '../../auth/login/AuthService';

@Component({
  selector: 'app-student-dashboard',
  imports: [],
  templateUrl: './student-dashboard.component.html',
  styleUrl: './student-dashboard.component.css'
})
export class StudentDashboardComponent {
  subjects: Subject[] = [];

  examHistory: ExamResult[] = [];

  constructor(private router: Router, 
    private examService: ExamsService, 
    private authService: AuthService) {}

  ngOnInit(): void {
    this.dashboard();
  }
  dashboard() {

    const userId = this.authService.getId();
console.log("UserIDDDDDDDDDDDD " + userId)
    this.examService.getAllSubjects().subscribe((data) => {
      console.log(data);
      this.subjects = data;
    }
    )

    this.examService.getStudentHistory(userId!).subscribe((data) => {      
      console.log(data);
      this.examHistory = data;
    },
    (error) => {
      console.error('Error fetching exams:', error);
    })
  }

  navigateToSubject(subjectId: number) {
    this.router.navigateByUrl(`/subject/${subjectId}`);
  }

  navigateToExamDetails(examName: string) {
    this.router.navigateByUrl(`/exam-details/${examName}`);
  }

  getStudentHistory(studentId: string){
    this.router.navigateByUrl(`/student/${studentId}`)
  }
}
