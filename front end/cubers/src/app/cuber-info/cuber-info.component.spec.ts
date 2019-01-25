import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CuberInfoComponent } from './cuber-info.component';

describe('CuberInfoComponent', () => {
  let component: CuberInfoComponent;
  let fixture: ComponentFixture<CuberInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CuberInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CuberInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
