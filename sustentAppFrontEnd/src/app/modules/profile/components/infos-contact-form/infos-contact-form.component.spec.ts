import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfosContactFormComponent } from './infos-contact-form.component';

describe('InfosContactFormComponent', () => {
  let component: InfosContactFormComponent;
  let fixture: ComponentFixture<InfosContactFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InfosContactFormComponent]
    });
    fixture = TestBed.createComponent(InfosContactFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
