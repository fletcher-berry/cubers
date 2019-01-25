import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CuberEditComponent } from './cuber-edit.component';

describe('CuberEditComponent', () => {
  let component: CuberEditComponent;
  let fixture: ComponentFixture<CuberEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CuberEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CuberEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
