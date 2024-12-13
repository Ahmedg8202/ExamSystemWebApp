import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExamsService } from '../exams.service';

@Component({
  selector: 'app-exam-result',
  templateUrl: './exam-result.component.html',
  styleUrls: ['./exam-result.component.css'],
})
export class ExamResultComponent implements OnInit {
  result: any;

  constructor(private route: ActivatedRoute, private examService: ExamsService) {}

  ngOnInit(): void {
    const examId = this.route.snapshot.paramMap.get('examId');
    if (examId) {
      this.fetchExamResult(examId);
    }
  }

  fetchExamResult(examId: string): void {
    this.examService.getExamResult(examId).subscribe(
      (data) => {
        this.result = data;
      },
      (error) => {
        console.error('Error fetching result', error);
      }
    );
  }
}
