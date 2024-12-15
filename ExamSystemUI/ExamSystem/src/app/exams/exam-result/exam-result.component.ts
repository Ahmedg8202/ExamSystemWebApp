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
    console.log("historyyyy", JSON.stringify(history.state)); // Log the state object

  // Access the state directly from history
    if (history.state && history.state.result) {
      this.result = history.state.result; // Access 'result' from state
      console.log("test");
      console.log(this.result); // Log the actual result object
      this.fetchExamResult(); // Fetch the exam result using the data
    }
    // const examId = this.route.snapshot.paramMap.get('examId');
    // if (examId) {
    //   this.fetchExamResult(examId);
    // }
  }

  fetchExamResult(): void {
    alert("examresult");
    console.log(this.result);
  }
}
