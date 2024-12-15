import { Component, NgModule } from '@angular/core';
import { Exam, ExamsService, ExamQuestion, Subject } from './exams.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-exams',
  imports: [CommonModule, FormsModule],
  templateUrl: './exams.component.html',
  styleUrl: './exams.component.css'
})
export class ExamsComponent {

  exams: Exam[] = [];
  selectedExam: ExamQuestion | null = null;
  randomExam: ExamQuestion | null = null;
  subjects: Subject[] = [];
  selectedSubject: string | null = null;

  isStudent = false;
  isAdmin = false;

  constructor(private examService: ExamsService, private router: Router) {}

  ngOnInit(): void {
    this.isStudent = localStorage.getItem('userRole') === 'Student';
    this.isAdmin = localStorage.getItem('userRole') === 'Admin';
    this.fetchAllExams();
    this.loadSubjects();
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
        console.log("fetch by id"+ id +" SS "+ data?.questions[0]);//object
      },
      error: (error) => {
        console.error('Error fetching subject:', error);
      }
  });
  }

  submitExam(){
    
  }

  loadSubjects(): void {
    this.examService.getAllSubjects().subscribe(
      (data) => {
        this.subjects = data;
        console.log("subjects"+this.subjects);
      },
      (error) => {
        console.error('Error fetching subjects', error);
      }
    );
  }

  takeExam(subjectId: string) {
    this.selectedSubject = subjectId;
    alert(this.selectedSubject);
    this.router.navigate(['/take-exam', subjectId]);
  }
  closeExamDetails(){

  }

  navigateToAddQuestion(){
    this.router.navigateByUrl('new-question');
  }

  navigateToAddExam(){
    this.router.navigateByUrl('new-exam');
  }

  updateExam(examId: string){
    console.log("update " + examId);
  }

  deleteExam(examId: string){
    console.log("delete " + examId);
  }
  
}
