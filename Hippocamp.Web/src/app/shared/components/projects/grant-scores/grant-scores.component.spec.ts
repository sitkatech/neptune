import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrantScoresComponent } from './grant-scores.component';

describe('GrantScoresComponent', () => {
  let component: GrantScoresComponent;
  let fixture: ComponentFixture<GrantScoresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrantScoresComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrantScoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
