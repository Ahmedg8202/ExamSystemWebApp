import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { ExamResult, ExamsService, Subject } from '../../exams/exams.service';
import { AuthService } from '../../auth/login/AuthService';
import { SubjectService } from '../../subjects/subject.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-student-dashboard',
  imports: [CommonModule],
  templateUrl: './student-dashboard.component.html',
  styleUrl: './student-dashboard.component.css'
})
export class StudentDashboardComponent {
  subjects: Subject[] = [];

  examHistory: ExamResult[] = [];

  constructor(private router: Router, 
    private examService: ExamsService, 
    private authService: AuthService,
    private subjectService: SubjectService) {}

  ngOnInit(): void {
    this.dashboard();
  }
  dashboard() {
    const userId = this.authService.getId();
    this.examService.getAllSubjects().subscribe((data) => {
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

  getSubjectName(subjectId: string) {
    console.log(subjectId);
    this.subjectService.getSubjectById(subjectId).subscribe(subject => {
      return subject.name;
    });
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
