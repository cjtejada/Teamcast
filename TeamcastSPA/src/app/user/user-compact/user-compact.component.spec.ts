import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserCompactComponent } from './user-compact.component';

describe('UserCompactComponent', () => {
  let component: UserCompactComponent;
  let fixture: ComponentFixture<UserCompactComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserCompactComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserCompactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
