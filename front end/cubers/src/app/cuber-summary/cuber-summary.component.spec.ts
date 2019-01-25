import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CuberSummaryComponent } from './cuber-summary.component';

describe('CuberSummaryComponent', () => {
  let component: CuberSummaryComponent;
  let fixture: ComponentFixture<CuberSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CuberSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CuberSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
