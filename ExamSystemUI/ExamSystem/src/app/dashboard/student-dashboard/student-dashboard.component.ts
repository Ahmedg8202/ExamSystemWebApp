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
  userId!: string;
  examHistory: any[] = [];
  subjectFilter = {
    page: 1,
    pageSize: 10
  };
  
  HistoryFilter = {
    page: 1,
    pageSize: 10
  };
  constructor(private router: Router, 
    private examService: ExamsService, 
    private authService: AuthService,
    private subjectService: SubjectService) {}

  ngOnInit(): void {
    this.dashboard();
  }
  dashboard() {
    this.userId = this.authService.getId()!;

    this.getAllSubjects();

    this.studentHistory();
  }

  getAllSubjects() {
    this.subjectService.getAllSubjects(this.subjectFilter).subscribe((data) => {
      this.subjects = data;
    }
    );
  }

  studentHistory() {
    this.examService.getStudentHistory(this.userId!, this.HistoryFilter).subscribe((data) => {
      console.log(data);
      this.examHistory = data;
    },
      (error) => {
        console.error('Error fetching exams:', error);
      });
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
