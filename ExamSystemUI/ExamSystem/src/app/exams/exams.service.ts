import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Exam {
  examId: string;
  subjectId: string;
  name: string;
  description: string;
  duration: number;
  questionsNumber: number;
}
  
  export interface Subject {
    subjectId: string;
    name: string;
    description: string;
    questionsNumber: number;
    duration: number;
    total: number;
  }
  
  export class Answer {
    answerId = '';
    text= '';
    isCorrect= '';
  }

  export class Question {
    questionId= '';
    text= '';
    answers: Answer[]=[];
  }
  
  export class ExamQuestion {
    examId = '';
    questions: Question[]=[];
  }

export interface QuesiontToSubmit{
  questionId: string;
  answerId: string;
}
export interface SubmitExam{
  subjectId: string;
  studentId: string;
  examId: string;
  questions: QuesiontToSubmit[];
}

export interface ExamResult{
  studentId: string;
  examId: string;
  score: string;
  status: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class ExamsService {
  private baseUrl = 'http://localhost:5130/api/Exam';

  constructor(private http: HttpClient) {}

  getAllExams(): Observable<Exam[]> {
    return this.http.get<Exam[]>(`${this.baseUrl}/exams`);
  }


  getAllSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(`http://localhost:5130/api/Subject`);
  }

  getAllExamsResults(): Observable<ExamResult[]> {
    return this.http.get<ExamResult[]>(`${this.baseUrl}/History`);
  }

  getRandomExam(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/random`);
  }
  
  getExamById(id: string): Observable<Exam> {
    return this.http.get<Exam>(`${this.baseUrl}/exam/${id}`);
  }

  
  submitExam(examData: SubmitExam): Observable<any> {
    alert("Submitted");
    return this.http.post<any>(`${this.baseUrl}/submit`, examData);
  }

  getExamResult(examId: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${examId}`);
  }
}