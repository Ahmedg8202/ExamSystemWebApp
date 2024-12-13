import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamsComponent } from './exams.component';

describe('ExamComponent', () => {
  let component: ExamsComponent;
  let fixture: ComponentFixture<ExamsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExamsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExamsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
