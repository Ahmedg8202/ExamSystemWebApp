import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Exam {
  examId: string;
  subjectId: string;
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
  subjectId: string;
  dateTime: string;
  score: string;
  status: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class ExamsService {
  private baseUrl = 'http://localhost:5130/api/Exam';
  private questionUrl = 'http://localhost:5130/questions';

  constructor(private http: HttpClient) {}

  getAllExams(): Observable<Exam[]> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
    });
    return this.http.get<Exam[]>(`${this.baseUrl}/Exams`, { headers });
  }

  getAllSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(`http://localhost:5130/api/Subject/AllSubjects`);
  }

  getAllExamsResults(filter: any): Observable<ExamResult[]> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
    });
    const params = new HttpParams()
      .set('page', filter.page)
      .set('pageSize', filter.pageSize);

    return this.http.get<ExamResult[]>(`${this.baseUrl}/History`, { headers, params });
  }

  getStudentHistory(id: string, filter: any): Observable<any[]> {
    const params = new HttpParams()
      .set('page', filter.page)
      .set('pageSize', filter.pageSize);
    return this.http.get<any[]>(`${this.baseUrl}/History/${id}`, {params});
  }
  getRandomExam(subjectId: string): Observable<any> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
    });
    return this.http.get<any>(`${this.baseUrl}/random/${subjectId}`, { headers });
  }
  
  getExamById(id: string): Observable<any> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
    });

    return this.http.get<any>(`${this.baseUrl}/Exam/${id}`, { headers });
  }

  getQuestionsBySubjectId(subjectId: string): Observable<Question[]> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
    });
    return this.http.get<Question[]>(`${this.questionUrl}/${subjectId}`, { headers });
  }
  
  submitExam(examData: SubmitExam): Observable<any> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
    });
    return this.http.post<any>(`${this.baseUrl}/submit`, examData, { headers });
  }

  addExam(newExam: any): Observable<any> {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
    });
    return this.http.post<any>(`${this.baseUrl}/add`, newExam, { headers });
  }

  setExamResult(result: any): void {
    //this.examResult = result;
  }
  getExamResult(): any {
    //return this.examResult;
  }
}
