import { Routes } from '@angular/router';

import { LoginComponent } from './auth/login/login.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { StudentDashboardComponent } from './dashboard/student-dashboard/student-dashboard.component';
import { RegisterComponent } from './auth/register/register.component';
import { AdminDashboardComponent } from './dashboard/admin-dashboard/admin-dashboard.component';
import { SubjectsComponent } from './subjects/subjects.component'
import { ExamsComponent } from './exams/exams.component'
import { QuestionComponent } from './questions/question/question.component';
import { TakeExamComponent } from './exams/take-exam/take-exam.component';
import { ExamResultComponent } from './exams/exam-result/exam-result.component';
import { NewExamComponent } from './exams/new-exam/new-exam.component';
import { NewQuestionComponent } from './questions/new-question/new-question.component';
import { AdminGuard } from './auth/admin.guard';
import { StudentGuard } from './auth/student.guard';
import { LogoutComponent } from './auth/logout/logout.component';
import { NewSubjectComponent } from './subjects/new-subject/new-subject.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'student-Dashboard', component: StudentDashboardComponent, canActivate: [StudentGuard] },
    { path: 'admin-Dashboard', component: AdminDashboardComponent, canActivate: [AdminGuard] },
    { path: 'subject', component:  SubjectsComponent, canActivate: [AdminGuard]},
    { path: 'new-subject', component:  NewSubjectComponent, canActivate: [AdminGuard]},
    { path: 'exams', component:  ExamsComponent},
    { path: 'take-exam/:subjectId', component:  TakeExamComponent, canActivate: [StudentGuard]},
    { path: 'new-exam', component:  NewExamComponent, canActivate: [AdminGuard]},
    { path: 'exam-result', component:  ExamResultComponent},
    { path: 'question', component:  QuestionComponent},
    { path: 'new-question', component:  NewQuestionComponent},
    { path: 'logout', component: LogoutComponent },
];

