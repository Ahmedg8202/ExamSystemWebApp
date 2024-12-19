import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { SubjectService } from '../../subjects/subject.service';
import { ExamsService } from '../exams.service';

@Component({
  selector: 'app-new-exam',
  templateUrl: './new-exam.component.html',
  styleUrls: ['./new-exam.component.css'],
  imports: [CommonModule, FormsModule]
})
export class NewExamComponent implements OnInit {
  questions: any[] = [];
  selectedQuestions: string[] = [];
  examName: string = '';
  questionToExam: {
    questionId: string;
    text: string;
  }[] = [];
  subjectId = '54576727-303e-4768-8235-6aeb31ae1fde';
  apiUrl = 'http://localhost:5130/questions';
  subjectName: string = '';
  constructor(private http: HttpClient, 
    private router: Router, 
    private route: ActivatedRoute,
    private subjectService: SubjectService,
  private examService: ExamsService) {}

  ngOnInit(): void {
    this.subjectId = this.route.snapshot.paramMap.get('subjectId') || '';
    this.subjectService.getSubjectById(this.subjectId).subscribe(subject => this.subjectName = subject.name);
    this.loadQuestions();
  }

  loadQuestions() {
    console.log(this.subjectId);

    this.examService.getQuestionsBySubjectId(this.subjectId).subscribe({
      next: (data) => {
        this.questions = data;
        console.log("sddf" , this.questions);
      },
      error: (error) => {
        console.error('Failed to load questions:', error);
      }
    })
  }

  toggleQuestionSelection(questionId: string) {
    const index = this.selectedQuestions.indexOf(questionId);
    if (index === -1) {
      this.selectedQuestions.push(questionId);
    } else {
      this.selectedQuestions.splice(index, 1);
    }
  }

  isAdded(questionId: string): boolean {
    return this.selectedQuestions.includes(questionId) ? true : false;
  }

  addToExam(questionId: string, text: string){
    if (!this.questionToExam.find(q => q.questionId === questionId)) {
      this.questionToExam.push({questionId, text});
      console.log(this.questionToExam);
    }
  }

  removeFromExam(questionId: any): void {
    this.questionToExam = this.questionToExam.filter(
      q => q.questionId !== questionId
    );
    console.log(this.questionToExam);
  }


  saveExam() {
    if (this.selectedQuestions.length === 0) {
      alert('Please select at least one question.');
      return;
    }

    const newExam = {
      subjectId: this.subjectId,
      questions: this.questionToExam.map(q => q.questionId),
    };

    console.log(newExam);
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
    });
    
    this.examService.addExam(newExam).subscribe({
      next: () => {
        alert('Exam saved successfully!');
        this.router.navigateByUrl('admin-Dashboard');
      },
      error: (error) => {
        console.error('Failed to save exam:', error);
        alert('Failed to save the exam. Please try again.');
      }
    });
  }
}
