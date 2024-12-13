import { Routes } from '@angular/router';

import { LoginComponent } from './auth/login/login.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { StudentDashboardComponent } from './dashboard/student-dashboard/student-dashboard.component';
import { RegisterComponent } from './auth/register/register.component';
import { AdminDashboardComponent } from './dashboard/admin-dashboard/admin-dashboard.component';
import { SubjectComponent } from './subjects/subject/subject.component'
import { ExamsComponent } from './exams/exams.component'
import { QuestionComponent } from './questions/question/question.component';
import { TakeExamComponent } from './exams/take-exam/take-exam.component';
import { ExamResultComponent } from './exams/exam-result/exam-result.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'student-Dashboard', component: StudentDashboardComponent },
    { path: 'admin-Dashboard', component: AdminDashboardComponent },
    { path: 'subject', component:  SubjectComponent},
    { path: 'exams', component:  ExamsComponent},
    { path: 'take-exam', component:  TakeExamComponent},
    { path: 'exam-result', component:  ExamResultComponent},
    { path: 'question', component:  QuestionComponent}

];

