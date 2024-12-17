import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

export interface NewQuestion {
  subjectId: string;
  text: string;
  options: string[];
  correctAnswer: string;
}

@Component({
  selector: 'app-new-question',
  templateUrl: './new-question.component.html',
  styleUrls: ['./new-question.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]
})
export class NewQuestionComponent {
  private apiUrl = 'http://localhost:5130/question';

  newQuestion: NewQuestion = {
    subjectId: '',
    text: '',
    options: [''],
    correctAnswer: ''
  };

  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.newQuestion.subjectId = this.route.snapshot.paramMap.get('subjectId') || '';
  }
  addOption(): void {
    this.newQuestion.options.push('');
  }

  removeOption(index: number): void {
    if (this.newQuestion.options.length > 1) {
      this.newQuestion.options.splice(index, 1);
    } else {
      alert('At least one option is required.');
    }
  }

  addQuestion(): void {
    if (!this.newQuestion.subjectId || !this.newQuestion.text || !this.newQuestion.correctAnswer) {
      alert('Please fill out all required fields.');
      return;
    }

    const token = localStorage.getItem('authToken');
        const headers = new HttpHeaders({
          'Authorization': `Bearer ${token}`,
        });

    this.http.post(this.apiUrl, this.newQuestion, { headers }).subscribe({
      next: (response) => {
        console.log('Question added successfully:', response);
        alert('Question added successfully!');
        this.resetForm();
      },
      error: (error) => {
        console.error('Failed to add question:', error);
        alert('An error occurred while adding the question.');
      }
    });
  }

  private resetForm(): void {
    this.newQuestion = {
      subjectId: '',
      text: '',
      options: [''], // Reset to one empty option
      correctAnswer: ''
    };
  }
}