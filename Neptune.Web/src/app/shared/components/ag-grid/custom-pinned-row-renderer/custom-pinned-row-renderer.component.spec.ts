import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CustomPinnedRowRendererComponent } from './custom-pinned-row-renderer.component';

describe('CustomPinnedRowRendererComponent', () => {
  let component: CustomPinnedRowRendererComponent;
  let fixture: ComponentFixture<CustomPinnedRowRendererComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomPinnedRowRendererComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomPinnedRowRendererComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
