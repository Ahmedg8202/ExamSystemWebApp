import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent {
  @Input() navLinks: { name: string; route: string }[] = [];
  @Input() activeLink: string = '';
  @Output() navigate = new EventEmitter<string>();

  onNavigate(link: { name: string; route: string }) {
    this.navigate.emit(link.route);
    this.activeLink = link.name;
  }
}
