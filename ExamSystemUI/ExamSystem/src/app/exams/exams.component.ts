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
  selectedExam: Exam | null = null;
  randomExam: ExamQuestion | null = null;
  subjects: Subject[] = [];
  selectedSubject: string | null = null;

  constructor(private examService: ExamsService, private router: Router) {}

  ngOnInit(): void {
    this.fetchAllExams();
    this.fetchRandomExam();
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

 fetchRandomExam(): void {
    // this.examService.getRandomExam().subscribe(
    //   (data) => {
    //     this.randomExam = data;
    //     console.log('Random Exammmmmm:', data);
    //   },
    //   (error) => {
    //     console.error('Error fetching random exam:', error);
    //   }
    // );
  }

  fetchExamById(id: string): void {
    this.examService.getExamById(id).subscribe(
      (data) => {
        this.selectedExam = data;
      },
      (error) => {
        console.error('Error fetching subject:', error);
      }
    );
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
    this.router.navigateByUrl('take-exam');
  }
  closeExamDetails(){

  }
}
