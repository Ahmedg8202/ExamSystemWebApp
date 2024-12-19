import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExamsService } from '../exams.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-exam-result',
  templateUrl: './exam-result.component.html',
  styleUrls: ['./exam-result.component.css'],
  imports: [CommonModule],
})
export class ExamResultComponent implements OnInit {
  result: any;

  constructor(private route: ActivatedRoute,
    private router: Router,
     private examService: ExamsService) {}

  ngOnInit(): void {
    if (history.state && history.state.result) {
      this.result = history.state.result;
      this.fetchExamResult();
    }
  }

  fetchExamResult(): void {
    console.log(this.result);
  }
}
