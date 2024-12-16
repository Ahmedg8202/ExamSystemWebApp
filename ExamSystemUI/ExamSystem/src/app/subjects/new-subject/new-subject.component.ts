import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SubjectService } from '../subject.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-new-subject',
  templateUrl: './new-subject.component.html',
  styleUrls: ['./new-subject.component.css'],
  imports: [FormsModule, CommonModule],
  
})
export class NewSubjectComponent {
  subject = {
    name: '',
    description: '',
    questionsNumber: 0,
    duration: 0,
    total: 0
  };

  constructor(private subjectService: SubjectService, private router: Router) {}

  onSubmit(subjectForm: any): void {

    if (!subjectForm.valid) {
      return;
    }

    this.subject.name = subjectForm.value.name;
    this.subject.description = subjectForm.value.description;
    this.subject.questionsNumber = subjectForm.value.questionsNumber;
    this.subject.duration = subjectForm.value.duration;
    this.subject.total = subjectForm.value.total;
    console.log(this.subject);
alert(this.subject.name);
    this.subjectService.addSubject(this.subject).subscribe({
      next: (response) => {
        console.log('Subject added successfully!' + response);
        alert('Subject added successfully!');
        this.router.navigateByUrl('/subject');
      },
      error: (error) => {
        console.error('Error adding subject:', error);
        alert('Failed to add subject.');
      }
  });
  }
}
