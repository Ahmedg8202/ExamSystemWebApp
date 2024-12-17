import { Component, NgModule } from '@angular/core';
import { Exam, ExamsService, ExamQuestion, Subject } from './exams.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SubjectsComponent } from "../subjects/subjects.component";

@Component({
  selector: 'app-exams',
  imports: [CommonModule, FormsModule, SubjectsComponent],
  templateUrl: './exams.component.html',
  styleUrl: './exams.component.css'
})
export class ExamsComponent {

  exams: any[] = [];
  selectedExam: any | null = null;
  randomExam: ExamQuestion | null = null;
  subjects: Subject[] = [];
  selectedSubject: string | null = null;
  filter = {
    page: 1,
    pageSize: 10
  };

  isStudent = false;
  isAdmin = false;

  constructor(private examService: ExamsService, private router: Router) {}

  ngOnInit(): void {
    this.isStudent = localStorage.getItem('userRole') === 'Student';
    this.isAdmin = localStorage.getItem('userRole') === 'Admin';
    this.fetchAllExams();
    this.loadSubjects();
  }
  onPrevious() {
    this.filter.page --;
    this.fetchAllExams();
  }
  onNext() {
    this.filter.page ++;
    this.fetchAllExams();
  }
  
  fetchAllExams(): void {
    this.examService.getAllExams().subscribe(
      (data) => {
        this.exams = data;
        console.log(this.exams);
      },
      (error) => {
        console.error('Error fetching subjects:', error);
      }
    );
  }
  fetchExamById(id: string): void {
    this.examService.getExamById(id).subscribe({
      next: (data) => {
        this.selectedExam = data;
        console.log("fetch by id"+ id +" SS "+ data);
      },
      error: (error) => {
        console.error('Error fetching subject:', error);
      }
  });
  }

  submitExam(){
    
  }

  loadSubjects(): void {
    console.log("load subjects");
    this.examService.getAllSubjects().subscribe(
      (data) => {
        this.subjects = data;
        console.log("subjects" + this.subjects);
      },
      (error) => {
        console.error('Error fetching subjects', error);
      }
    );
  }

  takeExam(subjectId: string) {
    this.selectedSubject = subjectId;
    this.router.navigate(['/take-exam', subjectId]);
  }
  closeExamDetails(){
    this.selectedExam = null;
  }

  navigateToAddQuestion(subjectId: string){
    console.log(subjectId);
    this.router.navigate(['/new-question', subjectId]);
  }

  navigateToAddExam(subjectId: string){
    console.log(subjectId);
    this.router.navigate(['new-exam', subjectId]);
  }

  updateExam(examId: string){
    console.log("update " + examId);
  }

  deleteExam(examId: string){
    console.log("delete " + examId);
  }
  
}
