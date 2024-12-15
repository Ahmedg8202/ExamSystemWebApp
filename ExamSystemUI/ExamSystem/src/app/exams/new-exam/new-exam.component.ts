import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-new-exam',
  templateUrl: './new-exam.component.html',
  styleUrls: ['./new-exam.component.css'],
  imports: [CommonModule]
})
export class NewExamComponent implements OnInit {
  questions: any[] = [];
  selectedQuestions: string[] = []; // Array to store selected question IDs
  examName: string = ''; // Input field for exam name
  questionToExam: {
    questionId: string;
    text: string;
  }[] = [];
  subjectId = '54576727-303e-4768-8235-6aeb31ae1fde';
  apiUrl = 'http://localhost:5130/questions/54576727-303e-4768-8235-6aeb31ae1fde'; // Replace with your actual endpoint

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.loadQuestions();
  }

  loadQuestions() {
    const subjectId = "54576727-303e-4768-8235-6aeb31ae1fde";
    this.http.get<any[]>(`${this.apiUrl}`).subscribe({
      next: (data) => {
        this.questions = data;
      },
      error: (error) => {
        console.error('Failed to load questions:', error);
      }
    });
  }

  toggleQuestionSelection(questionId: string) {
    const index = this.selectedQuestions.indexOf(questionId);
    if (index === -1) {
      this.selectedQuestions.push(questionId);
    } else {
      this.selectedQuestions.splice(index, 1);
    }
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

    // Call API to save the exam
    this.http.post('http://localhost:5130/api/Exam/add', newExam).subscribe({
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
