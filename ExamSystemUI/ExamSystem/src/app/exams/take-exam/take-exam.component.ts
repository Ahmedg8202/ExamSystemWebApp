import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

import { ExamsService, QuesiontToSubmit, SubmitExam, ExamQuestion } from '../exams.service';
import { AuthService } from '../../auth/login/AuthService';

class Answer {
  answerId = '';
  text = '';
  isCorrect = '';
}

class Question {
  questionId = '';
  text = '';
  answers: Answer[] = [];
}

@Component({
  selector: 'app-take-exam',
  templateUrl: './take-exam.component.html',
  styleUrls: ['./take-exam.component.css'],
  imports: [CommonModule],
})

export class TakeExamComponent implements OnInit, OnDestroy {
  randomExam!: ExamQuestion;
  studentId!: string;
  subjectId: string = 'subject-id';
  selectedAnswers: QuesiontToSubmit[] = [];

  examStartTime!: number;
  examDuration!: number;
  endTime!: number;
  remainingTime!: number;
  timerInterval!: any;

  constructor(
    private examService: ExamsService, 
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  get formattedTime(): string {
    const hours = Math.floor(this.remainingTime / (1000 * 60 * 60));
    const minutes = Math.floor((this.remainingTime % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((this.remainingTime % (1000 * 60)) / 1000);
    return `${this.padZero(hours)}:${this.padZero(minutes)}:${this.padZero(seconds)}`;
  }

  ngOnInit(): void {
    this.subjectId = this.route.snapshot.paramMap.get('subjectId') || '';
    console.log('Received subjectId:', this.subjectId);
    this.studentId = this.authService.getId()!;

    this.fetchRandomExam();
  }

  ngOnDestroy(): void {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
  }

  startTimer(): void {
    this.timerInterval = setInterval(() => {
      this.remainingTime = this.endTime - Date.now();

      if (this.remainingTime <= 0) {
        clearInterval(this.timerInterval);
        this.remainingTime = 0;
        this.submitExam();
      }
    }, 1000);
  }

  private padZero(num: number): string {
    return num < 10 ? '0' + num : num.toString();
  }

  fetchRandomExam(): void {
    this.examService.getRandomExam(this.subjectId).subscribe({
      next: (data) => {
        this.randomExam = data;
        this.examDuration = this.randomExam.duration * 1000;
        this.examStartTime = Date.now();
        this.endTime = this.examStartTime + this.examDuration;
        this.startTimer();
      },
      error: (error) => {
        console.error('Error fetching random exam:', error);
        alert('Failed to fetch exam. Please try again later.');
      }
    });
  }

  onAnswerChange(questionId: string, answerId: string): void {
    const existingAnswer = this.selectedAnswers.find(answer => answer.questionId === questionId);
    if (existingAnswer) {
      existingAnswer.answerId = answerId;
    } else {
      this.selectedAnswers.push({ questionId, answerId });
    }
  }

  isReadyToSubmit(): boolean {
    return this.selectedAnswers.length === this.randomExam.questions.length;
  }

  submitExam(): void {
    if (this.selectedAnswers.length === 0 && this.randomExam.questions.length > 0) {
      this.selectedAnswers = this.randomExam.questions.map(question => ({
        questionId: question.questionId,
        answerId: ''
      }));
    }

    const examData: SubmitExam = {
      subjectId: this.subjectId,
      studentId: this.studentId,
      examId: this.randomExam.examId,
      questions: this.selectedAnswers
    };

    this.examService.submitExam(examData).subscribe(
      (response) => {
        console.log('Exam submitted successfully', response);
        alert('Exam submitted successfully!');
        this.router.navigate(['/exam-result'], {
          state: { result: response }
        });
      },
      (error) => {
        console.error('Error submitting exam', error);
        alert('Failed to submit the exam. Please try again.');
      }
    );
  }
}
