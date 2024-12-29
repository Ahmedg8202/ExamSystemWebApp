import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { SubjectService } from '../subject.service';

@Component({
  selector: 'app-update-subject',
  templateUrl: './update-subject.component.html',
  styleUrls: ['./update-subject.component.css'],
  imports: [FormsModule, CommonModule]
})
export class UpdateSubjectComponent implements OnInit {
  subject: any;
  subjectId!: string;
  constructor(private router: Router, private route: ActivatedRoute, private subjectService: SubjectService) {}


  ngOnInit(): void {
    this.subjectId = this.route.snapshot.paramMap.get('subjectId') || '';
    if(this.subjectId){
      this.subjectService.getSubjectById(this.subjectId).subscribe(
        (data) => {
          this.subject = data;
        },
        (error) =>{
          console.log("Error", error);
        }
      )
    }
  }

  updateSubject() {
    this.subjectService.updateSubject(this.subjectId, this.subject).subscribe({
      next: (response) => {
        alert('Subject updated successfully!');
        this.router.navigate(['/subject']);
      },
      error: (error) => {
        console.error('Error updating subject:', error);
        alert('Failed to update subject. Please try again.');
      },
    });
  }
}
