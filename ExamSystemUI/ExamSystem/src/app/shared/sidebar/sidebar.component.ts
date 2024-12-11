import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  imports: [],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})

export class SidebarComponent implements OnInit {
  @Input() section: string = 'dashboard';

  constructor() { }

  ngOnInit(): void {}

  addSubject() {
    console.log("Add Subject");
  }

  updateSubject() {
    console.log("Update Subject");
  }

  deleteSubject() {
    console.log("Delete Subject");
  }
}

