import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CsvDownloadButtonComponent } from './csv-download-button.component';

describe('CsvDownloadButtonComponent', () => {
  let component: CsvDownloadButtonComponent;
  let fixture: ComponentFixture<CsvDownloadButtonComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CsvDownloadButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CsvDownloadButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
