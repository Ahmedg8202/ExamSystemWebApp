import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { SubjectService, Subject } from './subject.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.css'],
  imports: [CommonModule, FormsModule],
})
export class SubjectsComponent implements OnInit {
  subjects: Subject[] = [];
  selectedSubject: Subject | null = null;
  filter = {
    page: 1,
    pageSize: 10
  };
  
  constructor(private subjectService: SubjectService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.fetchAllSubjects();
  }
  
  onPrevious() {
    this.filter.page --;
    this.fetchAllSubjects();
  }
  onNext() {
    this.filter.page ++;
    this.fetchAllSubjects();
  }

  fetchAllSubjects(): void {
    this.subjectService.getAllSubjects(this.filter).subscribe(
      (data) => {
        this.subjects = data;
        console.log(this.subjects);
      },
      (error) => {
        console.error('Error fetching subjects:', error);
      }
    );
  }

  fetchSubjectById(id: string): void {
    this.subjectService.getSubjectById(id).subscribe(
      (data) => {
        this.selectedSubject = data;
      },
      (error) => {
        console.error('Error fetching subject:', error);
      }
    );
  }

  closeSubjectDetails(): void {
    this.selectedSubject = null;
  }

  updateSubject(subjectId: string){

  }

  deleteSubject(subjectId: string){

  }
  
  addSubject(){
    this.router.navigateByUrl('/new-subject');
  }
}
