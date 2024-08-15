import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecentsPageComponent } from './recents-page.component';

describe('RecentsPageComponent', () => {
  let component: RecentsPageComponent;
  let fixture: ComponentFixture<RecentsPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecentsPageComponent]
    });
    fixture = TestBed.createComponent(RecentsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
