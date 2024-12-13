import { Component, OnInit } from '@angular/core';
import { ExamsService, QuesiontToSubmit, SubmitExam } from '../exams.service'
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

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
  randomExam!: ExamQuestion;
  randomExamNew = new ExamQuestion();
  examId: string = '';
  studentId: string = 'student-id';
  subjectId: string = 'subject-id';
  selectedAnswers: QuesiontToSubmit[] = [];

  constructor(private examService: ExamsService, private router: Router) {}

  ngOnInit(): void {
    // Get the exam id from route params if needed
    // this.examId = this.route.snapshot.paramMap.get('examId') || '';
    this.fetchRandomExam();
  }

  fetchRandomExam(): void {
    this.examService.getRandomExam().subscribe({
      next: (data) => {
        this.randomExam = data;
        console.log();
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

  submitExam(): void {
    const examData: SubmitExam = {
      subjectId: "54576727-303e-4768-8235-6aeb31ae1fde",
      studentId: "2bc1c983-c12e-45ff-8aff-47ed87f81ab6",
      examId: this.randomExam.examId,
      questions: this.selectedAnswers
    }
    console.log(this.selectedAnswers);
    console.log(examData);
    
    this.examService.submitExam(examData).subscribe(
      (response) => {
        alert('Exam submitted successfully!');
        this.router.navigateByUrl('/exam-result');
      },
      (error) => {
        console.error('Error submitting exam', error);
      }
    );
  }
}
