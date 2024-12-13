import { Component, OnInit } from '@angular/core';
import { SubjectService, Subject } from './subject.service';

@Component({
  selector: 'app-subjects',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.css'],
})
export class SubjectComponent implements OnInit {
  subjects: Subject[] = [];
  selectedSubject: Subject | null = null;

  constructor(private subjectService: SubjectService) {}

  ngOnInit(): void {
    this.fetchAllSubjects();
  }

  fetchAllSubjects(): void {
    this.subjectService.getAllSubjects().subscribe(
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
  
}
