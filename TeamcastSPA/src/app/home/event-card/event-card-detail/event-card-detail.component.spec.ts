import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventCardDetailComponent } from './event-card-detail.component';

describe('EventCardDetailComponent', () => {
  let component: EventCardDetailComponent;
  let fixture: ComponentFixture<EventCardDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventCardDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventCardDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
