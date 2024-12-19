import { Component, OnInit, Input} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

import { ExamsService, QuesiontToSubmit, SubmitExam } from '../exams.service'
import { AuthService } from '../../auth/login/AuthService';

 class Answer {
  answerId = '';
  text= '';
  isCorrect= '';
}

 class Question {
  questionId= '';
  text= '';
  answers: Answer[]=[];
}

 class ExamQuestion {
  examId = '';
  questions: Question[]=[];
}

@Component({
  selector: 'app-take-exam',
  templateUrl: './take-exam.component.html',
  styleUrls: ['./take-exam.component.css'],
  imports: [CommonModule],
})

export class TakeExamComponent implements OnInit {
  //@Input({required: true}) subjectId!: string;
  randomExam!: ExamQuestion;
  examId: string = '';
  studentId!: string;
  subjectId: string = 'subject-id';
  selectedAnswers: QuesiontToSubmit[] = [];

  constructor(private examService: ExamsService, 
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.subjectId = this.route.snapshot.paramMap.get('subjectId') || '';
    console.log('Received subjectId:', this.subjectId);
    this.studentId = this.authService.getId()!;
    this.fetchRandomExam();
  }

  fetchRandomExam(): void {
    this.examService.getRandomExam(this.subjectId).subscribe({
      next: (data) => {
        this.randomExam = data;
        console.log(this.randomExam);
      },
      error: (error) => {
        console.error('Error fetching random exam:', error);
      } 
    });
  }

  onAnswerChange(questionId: string, answerId: string): void {
    const existingAnswer = this.selectedAnswers.find(answer => answer.questionId === questionId);
    if (existingAnswer) {
      existingAnswer.answerId = answerId;
      console.log(answerId);
    } else {
      this.selectedAnswers.push({ questionId, answerId });
    }
  }

  isReadyToSubmit(): boolean{
    return this.selectedAnswers.length !== this.randomExam.questions.length;
  }
  submitExam(): void {
    
    const examData: SubmitExam = {
      subjectId: this.subjectId,
      studentId: this.studentId,
      examId: this.randomExam.examId,
      questions: this.selectedAnswers
    }
    
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
      }
    );
  }
}
